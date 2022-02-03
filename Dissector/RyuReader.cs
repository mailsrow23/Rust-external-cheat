using Binarysharp.MemoryManagement.Internals;
using Dissector.Helpers;
using System;
using System.Threading;

namespace Dissector
{
    public class RyuReader
    {
        private int _processId;
        private ulong _cr3;
        private IntPtr _memHelper;
        private IntPtr _startAddress;
        private IntPtr _endAddress;
        private Action<string> _logActivity;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="memHelper"></param>
        /// <param name="cr3"></param>
        /// <param name="baseAddress"></param>
        public RyuReader(int processId, IntPtr memHelper, ulong cr3, Action<string> logActivity)
        {
            _processId = processId;
            _memHelper = memHelper;
            _cr3 = cr3;

            /* Start and Stop addresses */
            _startAddress = (IntPtr)0x00000000;
            _endAddress = (IntPtr)0x7FFFFFFFFFFFFF;

            _logActivity = logActivity;
        }

        /// <summary>
        /// Attempts to find a byte signature given a mask and offset
        /// </summary>
        /// <param name="sig"></param>
        /// <param name="mask"></param>
        /// <param name="offset"></param>
        /// <returns>Address + offset in which the byte signature was found</returns>
        public IntPtr FindSignature(byte[] sig, string mask, int offset)
        {
            try
            {
                // saving the values as long ints so I won't have to do a lot of casts later
                ulong startAddress_l = (ulong)_startAddress;
                ulong endAddress_l = (ulong)_endAddress;

                // opening the process with desired access level
                IntPtr processHandle = PInvoke.OpenProcess(PInvoke.PROCESS_QUERY_INFORMATION | PInvoke.PROCESS_WM_READ, false, _processId);

                Inspector scanner = new Inspector(_startAddress, 0x1000, _memHelper, _cr3);

                // this will store any information we get from VirtualQueryEx()
                PInvoke.MEMORY_BASIC_INFORMATION64 mem_basic_info = new PInvoke.MEMORY_BASIC_INFORMATION64();

                while (startAddress_l < endAddress_l)
                {
                    // 48 = sizeof(MEMORY_BASIC_INFORMATION)
                    PInvoke.VirtualQueryEx(processHandle, _startAddress, out mem_basic_info, (uint)MarshalType<PInvoke.MEMORY_BASIC_INFORMATION64>.Size);

                    /* Set the size here so the dumped region is scanned properly */
                    scanner.Size = (int)mem_basic_info.RegionSize;

                    // if this memory chunk is accessible and meets the size of the memory chunk that the basenetworkable object is in
                    if (mem_basic_info.Protect == PInvoke.PAGE_EXECUTE_READWRITE)
                    {
                        _logActivity("Scanning Memory Region: " + mem_basic_info.RegionSize.ToString());
                        IntPtr patternAddress = scanner.FindPattern(sig, mask, offset);

                        if ((long)patternAddress > 0)
                        {
                            var opCodeValue = KeeperOf.Memory.Read<ulong>(patternAddress);
                            var commentValue = KeeperOf.Memory.Read<ulong>(new IntPtr((long)opCodeValue));
                            PInvoke.CloseHandle(processHandle);
                            return new IntPtr((long)commentValue);
                        }
                    }

                    // move to the next memory chunk
                    startAddress_l += mem_basic_info.RegionSize;
                    _startAddress = new IntPtr((long)startAddress_l);
                    scanner.Address = _startAddress;

                    Thread.Sleep(1);
                }
                PInvoke.CloseHandle(processHandle);
                return IntPtr.Zero;
            }
            catch (Exception ex)
            {
                _logActivity(ex.Message);
                throw;
            }
        }
    }
}