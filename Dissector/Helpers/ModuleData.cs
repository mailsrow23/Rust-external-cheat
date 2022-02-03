using System;

namespace Dissector.Helpers
{
    public class ModuleData
    {
        /// <summary>
        /// These offsets can be found in Windbg if you !dt out the struct types to see the offset of each member
        /// </summary>
        #region Offsets

        private int _baseAddressOffset = 0x020;
        private int _intialNamePtrOffset = 0x048;
        private int _actualNamePtrOffset = 0x008;

        #endregion

        private IntPtr _currentModule;

        public IntPtr BaseAddress
        {
            get
            {
                return KeeperOf.Memory.Read<IntPtr>(_currentModule + _baseAddressOffset); 
            }
        }

        public string Name
        {
            get
            {
                /* Create the name of the module and check to see if it's the one we need */
                var intialNamePtr = _currentModule + _intialNamePtrOffset;
                IntPtr actualNamePtr = KeeperOf.Memory.Read<IntPtr>(intialNamePtr + _actualNamePtrOffset);
                var actualName = KeeperOf.Memory.ReadUnicodeString(actualNamePtr);

                return actualName;
            }
        }

        public ModuleData(IntPtr currentModule)
        {
            _currentModule = currentModule;
        }
    }
}
