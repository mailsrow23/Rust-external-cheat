using System;
using Dissector.Memory;

namespace Dissector.Helpers
{
    public static class KeeperOf
    {
        private static Memory _memory;
        private static IntPtr _baseNetworkableAddress;

        public static Memory Memory
        {
            get
            {
                if(_memory == null)
                    throw new Exception("Memory object is not initialized.");
                return _memory;
            }
            set => _memory = value;
        }

        public static IntPtr BaseNetworkableAddress
        {
            get
            {
                if(_baseNetworkableAddress == IntPtr.Zero)
                    throw new Exception("BaseNetworkableAddress is not initialized.");
                return _baseNetworkableAddress;
            }
            set => _baseNetworkableAddress = value;
        }
    }
}
