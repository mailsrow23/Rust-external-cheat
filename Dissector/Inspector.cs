using Dissector.Helpers;
using System;
using System.Collections.Generic;
using System.IO;

namespace Dissector
{
    public class Inspector
    {
        private  ulong _cr3;
        private  IntPtr _memHelper;

        /// <summary>
        /// m_vDumpedRegion
        /// 
        ///     The memory dumped from the external process.
        /// </summary>
        private byte[] _dumpedMemoryRegion;

        /// <summary>
        /// m_vAddress
        /// 
        ///     The starting address we want to begin reading at.
        /// </summary>
        private IntPtr _regionStartAddress;

        /// <summary>
        /// m_vSize
        /// 
        ///     The number of bytes we wish to read from the process.
        /// </summary>
        private int _regionSize;


        #region "sigScan Class Construction"
        /// <summary>
        /// SigScan
        /// 
        ///     Main class constructor that uses no params. 
        ///     Simply initializes the class properties and 
        ///     expects the user to set them later.
        /// </summary>
        public Inspector()
        {
            this._regionStartAddress = IntPtr.Zero;
            this._regionSize = 0;
            this._dumpedMemoryRegion = null;
        }

        /// <summary>
        /// SigScan
        /// 
        ///     Overloaded class constructor that sets the class
        ///     properties during construction.
        /// </summary>
        /// <param name="proc">The process to dump the memory from.</param>
        /// <param name="addr">The started address to begin the dump.</param>
        /// <param name="size">The size of the dump.</param>
        public Inspector(IntPtr addr, int size, IntPtr memHelper, ulong cr3)
        {
            this._regionStartAddress = addr;
            this._regionSize = size;

            _memHelper = memHelper;
            _cr3 = cr3;
        }
        #endregion

        #region "sigScan Class Private Methods"
        /// <summary>
        /// DumpMemory
        /// 
        ///     Internal memory dump function that uses the set class
        ///     properties to dump a memory region.
        /// </summary>
        /// <returns>Boolean based on RPM results and valid properties.</returns>
        private bool DumpMemory()
        {
            try
            {
                if (this._regionStartAddress == IntPtr.Zero)
                    return null;
                if (this._regionSize == 0)
                    return false;

                // Create the region space to dump into.
                this._dumpedMemoryRegion = new byte[this._regionSize];

                bool bReturn = true;

                // Dump the memory only to help us go through the region, reading each address individually.
                bReturn = PInvoke.ReadMemVirtual(_memHelper, _cr3, (ulong)this._regionStartAddress, _dumpedMemoryRegion, this._regionSize);

                Dump();

                // Validation checks.
                if (bytes.Length % 4 != 0)
                throw new ArgumentException();

            float[] floats = new float[bytes.Length / 4];

            for (int i = 0; i < floats.Length; i++)
                floats[i] = BitConverter.ToSingle(bytes, i * 4);

            return continue;
            }
        }

        /// <summary>
        /// Loop through the dumped region
        /// Write to a text file the 8 bytes at each address in the dumped region
        /// Write to a text file the 8 bytes re-read at each address with only an 8 byte buffer (I suspect this is the correct way to pattern scan with phys mem)
        /// </summary>
        private void Dump()
        {
            StreamWriter stw = new StreamWriter("dump" + _regionStartAddress.ToString("X") + ".txt");

            string currentMemoryRangeAddress = "";

            /* Current address within the memory range
             * We'll use this to re-read this single address instead of relying on what is in the dumped region */
            IntPtr currentAddress;

            /* Loops through the dumped region of memory to output the bytes at each 8 byte offset */
            for (int i = 0; i < _dumpedMemoryRegion.Length; i = i + 8)
            {
                var currentMemoryRangeBytes = ConvertBytesToStringHex(i, _dumpedMemoryRegion);

                /* i is the offset so we add that to our address to get the address within the memory range for these 8 bytes */
                if (i == 0)
                {
                    currentMemoryRangeAddress = _regionStartAddress.ToString("X");
                    currentAddress = _regionStartAddress;
                }
                else
                {
                    currentMemoryRangeAddress = (_regionStartAddress + i).ToString("X");
                    currentAddress = _regionStartAddress + i;
                }

                /* Re-read this address instead of relying on the dumped region bytes - read 8 bytes (64bit) just at this address */
                byte[] newBuffer = new byte[8];
                PInvoke.ReadMemVirtual(_memHelper, _cr3, (ulong)currentAddress, newBuffer, 8);
                var currentMemoryAddressBytes = ConvertBytesToStringHex(0, newBuffer);


                stw.WriteLine("0x{0} : {1}   -:-   {2}", currentMemoryRangeAddress, currentMemoryRangeBytes, currentMemoryAddressBytes);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private string ConvertBytesToStringHex(int i, byte[] buffer)
        {
            /* Gets 8 bytes worth of data */
            var onebyte = buffer[i];
            var twobyte = buffer[i + 2];
            var threebyte = buffer[i + 2];
            var fourbyte = buffer[i + 3];
            var fivebyte = buffer[i + 4];
            var sixbyte = buffer[i + 5];
            var sevenbyte = buffer[i + 6];
            var eightbyte = buffer[i + 7];

            /* Converts the bytes into hex for dumping into file */
            var memoryRangeBytes = onebyte.ToString("X") + " " +
                                   twobyte.ToString("X") + " " +
                                   threebyte.ToString("X") + " " +
                                   fourbyte.ToString("X") + " " +
                                   fivebyte.ToString("X") + " " +
                                   sixbyte.ToString("X") + " " +
                                   sevenbyte.ToString("X") + " " +
                                   eightbyte.ToString("X");

            return Dumper_memoryRangeBytes;
        }

        /// <summary>
        /// MaskCheck
        /// 
        ///     Compares the current pattern byte to the current memory dump
        ///     byte to check for a match. Uses wildcards to skip bytes that
        ///     are deemed unneeded in the compares.
        /// </summary>
        /// <param name="nOffset">Offset in the dump to start at.</param>
        /// <param name="btPattern">Pattern to scan for.</param>
        /// <param name="strMask">Mask to compare against.</param>
        /// <returns>Boolean depending on if the pattern was found.</returns>
        private bool MaskCheck(int nOffset, byte[] btPattern, string strMask, out List<byte> maskBytes)
        {
            try
            {
                /* The address in which we are checking the mask against
                 * We need to read this into a buffer the size of our btPattern */
                var currentAddress = (_regionStartAddress + nOffset);

                byte[] newBuffer = new byte[btPattern.Length];
                PInvoke.ReadMemVirtual(_memHelper, _cr3, (ulong)currentAddress, newBuffer, btPattern.Length);
                var currentMemoryAddressBytes = ConvertBytesToStringHex(0, newBuffer);

                int maskUpCounter = 0;

                maskBytes = new List<byte>();

                // Loop the pattern and compare to the mask and dump.
                for (int x = 0; x < btPattern.Length; x++)
                {
                    // If the mask char is a wildcard, just continue.
                    if (strMask[x] == '?')
                    {
                        maskBytes.Add( newBuffer[x]);
                        maskUpCounter++;
                        continue;
                    }                        

                    // If the mask char is not a wildcard, ensure a match is made in the pattern.
                    /* Replace this with local address dump (buffer needs to be same size as btPattern */
                    if ((strMask[x] == 'x') && (btPattern[x] != newBuffer[0 + x]))
                        return false;
                    else
                        continue;
                }

                // The loop was successful so we found the pattern.
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }            
        }
        #endregion

        #region "sigScan Class Public Methods"
        /// <summary>
        /// FindPattern
        /// 
        ///     Attempts to locate the given pattern inside the dumped memory region
        ///     compared against the given mask. If the pattern is found, the offset
        ///     is added to the located address and returned to the user.
        /// </summary>
        /// <param name="btPattern">Byte pattern to look for in the dumped region.</param>
        /// <param name="strMask">The mask string to compare against.</param>
        /// <param name="nOffset">The offset added to the result address.</param>
        /// <returns>IntPtr - zero if not found, address if found.</returns>
        public IntPtr FindPattern(byte[] btPattern, string strMask, int nOffset)
        {
            try
            {
                if (!this.DumpMemory())
                    return IntPtr.Zero;

                // Ensure the mask and pattern lengths match.
                if (strMask.Length != btPattern.Length)
                    return IntPtr.Zero;

                // Loop the region every 8 bytes until we find the pattern we need and look for the pattern.
                for (int x = 0; x < this._dumpedMemoryRegion.Length; x++)
                {
                    List<byte> maskBuffer = new List<byte>();

                    if (this.MaskCheck(x, btPattern, strMask, out maskBuffer))
                    {
                        // The pattern was found, return it.
                        var ptr = new IntPtr((long)this._regionStartAddress + (x + nOffset));

                        return ptr;
                    }
                }

                // Pattern was not found.
                return IntPtr.Zero;
            }
            catch (Exception ex)
            {
                return IntPtr.Zero;
            }
        }

public class SigScan
{
    private byte[] _dumpedMemoryRegion;
    private int _regionSize;

    public SigScan()
    {
        this._dumpedMemoryRegion = null;
        this._regionSize = 0;
    }

    public void ResetRegion()
    {
        this._dumpedMemoryRegion = null;
        this._regionSize = 0;
    }

    public IntPtr Address { get; set; }

    public int Size 
    { 
        get { return this._regionSize; }
        set { this._regionSize = value; }
    }
}
