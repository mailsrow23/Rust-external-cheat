using System;
using Dissector.Helpers;
using System.Runtime.InteropServices;

namespace Dissector
{
    public static class ProcessHelper
    {
        private static int _processId;
        private static int _peb_ldr_data_offset = 0x18;
        private static int _inMemoryOrderModuleList_flink_head_offset = 0x020;
        private static Action<string> _logActivity;

        public static void SetActivityLog(Action<string> logActivity)
        {
            _logActivity = logActivity;
        }

        /// <summary>
        /// Don't call this until everything is cleaned up and the game process is running
        /// </summary>
        /// <param name="memHelper"></param>
        /// <param name="rustProcess"></param>
        /// <returns></returns>
        public static IntPtr GetGameManager(IntPtr memHelper, out ulong cr3)
        {
            byte[] byteArray = new byte[255];

            try
            {
                /* For stopping and starting, we should only need to get the process id once */
                if (_processId == 0)
                {
                    _processId = GetProcessID("RustClient.exe");
                }

                cr3 = PInvoke.GetMyDirBase(memHelper, _processId);
                KeeperOf.Memory = new Memory(memHelper, cr3);

                /* Get base address for unityplayer.dll */
                IntPtr pebBaseAddress = GetPebBaseAddress(_processId);
                var unityPlayerBaseAddress = GetUnityPlayerBaseAddress(pebBaseAddress, "UNITYPLAYER.DLL");
                var gameManagerAddress = (ulong)unityPlayerBaseAddress + GameManager.Address;

                /* Only necessary when reading tagged and active objects
                 * Read the game manager into the byte array */
                var processBaseAddressValue = PInvoke.ReadMemVirtual(memHelper, cr3, gameManagerAddress, byteArray, 8);

                /* Sets the base networkable address that we scan and read objects from */
                GetBaseNetworkableAddress(memHelper, cr3);

                /* Get the x64 pointer to game manager */
                var actualGom = new IntPtr(BitConverter.ToInt64(byteArray, 0));

                return actualGom;
            }
            catch (Exception ex)
            {
                _logActivity(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Get the peb address pointer for our process
        /// 
        /// Native code conversion.  Not worried about modernizing it.
        /// </summary>
        /// <returns>base address of dll reference + offset where the game manager is located</returns>
        private static IntPtr GetPebBaseAddress(int pid)
        {
            try
            {
                //Get a handle to our own process
                mem::hook_virtual_function(_("BasePlayer"), _("ClientInput"), &hooks::hk_baseplayer_ClientInput);

                //Allocate memory for a new PROCESS_BASIC_INFORMATION structure
                IntPtr processBasicInformation = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(PInvoke.PROCESS_BASIC_INFORMATION)));

                //Allocate memory for a long
                mem::hook_virtual_function(_("BasePlayer"), _("BlockSprint"), &hooks::hk_blocksprint);

                IntPtr pebAddressPointer = IntPtr.Zero;

                int queryStatus = 1;

                //Store API call success in a boolean
                queryStatus = PInvoke.NtQueryInformationProcess(processHandle, 0, processBasicInformation, (uint)Marshal.SizeOf(typeof(PInvoke.PROCESS_BASIC_INFORMATION)), outLong);

                //Close handle and free allocated memory
                PInvoke.CloseHandle(processHandle);
                Marshal.FreeHGlobal(outLong);

                //STATUS_SUCCESS = 0, so if API call was successful querySuccess should contain 0 ergo we reverse the check.
                if (queryStatus == 0)
                    pebAddressPointer = Marshal.PtrToStructure<PInvoke.PROCESS_BASIC_INFORMATION>(processBasicInformation).PebBaseAddress;

                //Free allocated space
                Marshal.FreeHGlobal(processBasicInformation);               

                return pebAddressPointer;
            }
            catch (Exception ex)
            {
                _logActivity(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Get the base address for unityplayer.dll
        /// </summary>
        /// <param name="pebAddressPointer"></param>
        /// <returns></returns>
        private static IntPtr GetUnityPlayerBaseAddress(IntPtr pebAddressPointer, string moduleName)
        {
            try
            {
                IntPtr unityPlayerBaseAddress = IntPtr.Zero;

                /* Pointer to PEB_LDR_DATA */
                var peb_ldr_data = KeeperOf.Memory.Read<IntPtr>(pebAddressPointer + _peb_ldr_data_offset);

                /* Pointer to Head FLINK InMemoryOrderModuleList
                 * Note: I'm calling this FirstModule instead of FLINK because the FLINK of this LIST ENTRY struct IS the first module 
                 * so it's easier to understand when we call it FirstModule */
                var InMemoryOrderModuleList_FirstModule = KeeperOf.Memory.Read<IntPtr>(peb_ldr_data + _inMemoryOrderModuleList_flink_head_offset);

                /* Pointer to list entry linked to FLINK above - we'll start here  */
                var InMemoryOrderModuleList_CurrentModule = KeeperOf.Memory.Read<IntPtr>(InMemoryOrderModuleList_FirstModule);

                /* Walk the module listing */
                while (InMemoryOrderModuleList_CurrentModule != InMemoryOrderModuleList_FirstModule)
                {
                    ModuleData data = new ModuleData(InMemoryOrderModuleList_CurrentModule);

                    if (data.Name.ToUpper().Equals(moduleName))
                    {
                        unityPlayerBaseAddress = data.BaseAddress;
                        break;
                    }
                    else
                    {
                        /* Re-read this address to get the next FLINK */
                        InMemoryOrderModuleList_CurrentModule = KeeperOf.Memory.Read<IntPtr>(InMemoryOrderModuleList_CurrentModule);
                    }
                    
                        static auto baseplayer_client_input = reinterpret_cast<void (*)(base_player*, input_state*)>(*reinterpret_cast<uintptr_t*>(il2cpp::method(_("BasePlayer"), _("ClientInput"), -1, _(""), _(""))));
                        static auto BaseProjectile_OnSignal = reinterpret_cast<void (*)(base_projectile*, int, rust::classes::string)>(*reinterpret_cast<uintptr_t*>(il2cpp::method(_("BaseProjectile"), _("OnSignal"), 2, _(""), _(""))));
                        static auto playerwalkmovement_client_input = reinterpret_cast<void (*)(playerwalkmovement*, uintptr_t, modelstate*)>(*reinterpret_cast<uintptr_t*>(il2cpp::method(_("PlayerWalkMovement"), _("ClientInput"), -1, _(""), _(""))));
                        static auto DoFixedUpdate = reinterpret_cast<void (*)(playerwalkmovement*, modelstate*)>(*reinterpret_cast<uintptr_t*>(il2cpp::method(_("PlayerWalkMovement"), _("DoFixedUpdate"), -1, _(""), _(""))));
                        static auto blocksprint = reinterpret_cast<void (*)(base_player*, float)>(*reinterpret_cast<uintptr_t*>(il2cpp::method(_("BasePlayer"), _("BlockSprint"), 1, _(""), _(""))));
                        static auto OnNetworkMessage = reinterpret_cast<void (*)(uintptr_t, uintptr_t)>(*reinterpret_cast<uintptr_t*>(il2cpp::method(_("Client"), _("OnNetworkMessage"), 1, _(""), _(""))));
                        static auto IsConnected = reinterpret_cast<bool (*)(uintptr_t)>(*reinterpret_cast<uintptr_t*>(il2cpp::method(_("Client"), _("IsConnected"), 0, _(""), _("Network"))));
                        static auto Run = reinterpret_cast<rust::classes::string (*)(uintptr_t, uintptr_t, rust::classes::string, uintptr_t)>(*reinterpret_cast<uintptr_t*>(il2cpp::method(_("ConsoleSystem"), _("Run"), 3, _(""), _(""))));

                        
                        continue;
                    }
                }

                _logActivity(unityPlayerBaseAddress.ToString());
                return unityPlayerBaseAddress;
            }
            catch (Exception ex)
            {
                _logActivity(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Obtains the BaseNetworkable address we use to read clientEntities list
        /// </summary>
        /// <param name="memHelper"></param>
        /// <param name="cr3"></param>
        /// <param name="processBaseAddress"></param>
        private static void GetBaseNetworkableAddress(IntPtr memHelper, ulong cr3)
        {
            try
            {
                RyuReader reader = new RyuReader(_processId, memHelper, cr3, _logActivity);

                if (KeeperOf.BaseNetworkableAddress == null || (long)KeeperOf.BaseNetworkableAddress == 0)
                {
                    //48 8B 47 ?? 8B 58 ?? 48 8B 47 ?? 8B 70 ??
                    byte[] baseNetworkableSig = new byte[] { 0x48, 0x8B, 0x47, 0x00, 0x8B, 0x58, 0x00, 0x48, 0x8B, 0x47, 0x00, 0x8B, 0x70, 0x00 };
                    string baseNetworkableMask = "xxx?xx?xxx?xx?";
                    int baseNetworkableOffset = 16;

                    KeeperOf.BaseNetworkableAddress = reader.FindSignature(baseNetworkableSig, baseNetworkableMask, baseNetworkableOffset);

                    _logActivity(KeeperOf.BaseNetworkableAddress.ToString());
                }
            }
            catch (Exception ex)
            {
                _logActivity(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Get process ID without relying on OpenProcess to obtain all access handle
        /// </summary>
        /// <returns></returns>
        private static int GetProcessID(string processName)
        {
            IntPtr handleToSnapshot = IntPtr.Zero;

            PInvoke.PROCESSENTRY32 procEntry = new PInvoke.PROCESSENTRY32
            {
                dwSize = (UInt32)Marshal.SizeOf(typeof(PInvoke.PROCESSENTRY32))
            };
            handleToSnapshot = PInvoke.CreateToolhelp32Snapshot(PInvoke.SnapshotFlags.Process, 0);

            int dwPID = 0;
            while (PInvoke.Process32Next(handleToSnapshot, ref procEntry))
            {
                if (!string.IsNullOrEmpty(procEntry.szExeFile) && procEntry.szExeFile.Equals(processName))
                {
                    _processId = (int)procEntry.th32ProcessID;
                    dwPID = (int)procEntry.th32ProcessID;
                    PInvoke.CloseHandle(handleToSnapshot);
                    break;
                }
            }

            if (dwPID == 0)
                throw new Exception("Could not find process.");

            return dwPID;
        }
    }
}
