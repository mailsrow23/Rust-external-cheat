using Binarysharp.MemoryManagement.Internals;
using Dissector.Helpers;
using System;
using System.Text;

namespace Dissector
{
    public class Memory
    {
        private IntPtr _memHelper;

        private ulong _cr3;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="memHelper"></param>
        /// <param name="cr3"></param>
        public Memory(IntPtr memHelper, ulong cr3)
        {
            _memHelper = memHelper;
            _cr3 = cr3;
        }

        /// <summary>
        /// I suppose we might need to update this but I don't really know.  I added it as a test for some behavior I thought was caused by a modified cr3 value.
        /// </summary>
        /// <param name="cr3"></param>
        public void SetCr3(ulong cr3)
        {
            _cr3 = cr3;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        /// <param name="numOfBytes"></param>
        /// <param name="bytesRead"></param>
        /// <returns></returns>
        private unsafe byte[] ReadMemory(IntPtr address, int numOfBytes, out long bytesRead)
        {
            byte[] buffer = new byte[numOfBytes];

            IntPtr pBytesRead = IntPtr.Zero;

            PInvoke.ReadMemVirtual(_memHelper, _cr3, (ulong)address, buffer, numOfBytes);

            bytesRead = pBytesRead.ToInt64();

            return buffer;
        }

        /// <summary>
        /// Allows us to pass in a "path" of pointers with the final one being the offset for the value we want to marshal into our managed object <T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseAddressPtr"></param>
        /// <param name="offsets"></param>
        /// <returns></returns>
        public unsafe T ReadMultiLevelManaged<T>(IntPtr baseAddressPtr, params int[] offsets)
        {
            long bytesRead;

            IntPtr managedPtr = IntPtr.Zero;

            var intialPtr = baseAddressPtr;
            long lastAddress = 0;

            T returnValue = default(T);

            foreach (var offset in offsets)
            {
                /* This is the last offset being read so we should go ahead and marshal the value to T and return it */
                if (offset == offsets[offsets.Length - 1])
                {
                    var newValue = ReadMemory(IntPtr.Add(intialPtr, offset), 8, out bytesRead);

                    var newPtr = new IntPtr(lastAddress);

                    managedPtr = IntPtr.Add(newPtr, offset);

                    returnValue = Read<T>(IntPtr.Add(newPtr, offset));

                    break;
                }
                else
                {
                    var newValue = ReadMemory(IntPtr.Add(intialPtr, offset), 8, out bytesRead);

                    lastAddress = BitConverter.ToInt64(newValue, 0);

                    intialPtr = new IntPtr(lastAddress);
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Allows us to pass in a "path" of pointers with the final one being the offset for the value we want to read as an int32
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseAddressPtr"></param>
        /// <param name="offsets"></param>
        /// <returns></returns>
        public unsafe int ReadMultiLevelInt32(IntPtr baseAddressPtr,  params int[] offsets)
        {
            long bytesRead;

            IntPtr ptr = IntPtr.Zero;

            var initialPtr = baseAddressPtr;
            long lastAddress = 0;

            int returnValue = 0;

            foreach (var offset in offsets)
            {
                /* This is the last offset being read so we should go ahead and marshal the value to int and return it */
                if (offset == offsets[offsets.Length - 1])
                {
                    var nextLevelPtr = lastAddress == 0 ? baseAddressPtr : initialPtr;

                    ptr = IntPtr.Add(nextLevelPtr, offset);

                    returnValue = Read<int>(IntPtr.Add(nextLevelPtr, offset));
                }
                else
                {
                    var newValue = ReadMemory(IntPtr.Add(initialPtr, offset), 8, out bytesRead);

                    lastAddress = BitConverter.ToInt64(newValue, 0);

                    /* Update the initial pointer so we can read the next offset assuming it's not the last one */
                    initialPtr = new IntPtr(lastAddress);
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Allows us to pass in a "path" of pointers with the final one being the offset for the value we want to read as an int32
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseAddressPtr"></param>
        /// <param name="offsets"></param>
        /// <returns></returns>
        public unsafe float ReadMultiLevelFloat(IntPtr baseAddressPtr, params int[] offsets)
        {
            long bytesRead;

            IntPtr ptr = IntPtr.Zero;

            var initialPtr = baseAddressPtr;
            long lastAddress = 0;

            float returnValue = 0;

            foreach (var offset in offsets)
            {
                /* This is the last offset being read so we should go ahead and marshal the value to int and return it */
                if (offset == offsets[offsets.Length - 1])
                {
                    var nextLevelPtr = lastAddress == 0 ? baseAddressPtr : initialPtr;

                    ptr = IntPtr.Add(nextLevelPtr, offset);

                    returnValue = Read<float>(IntPtr.Add(nextLevelPtr, offset));
                }
                else
                {
                    var newValue = ReadMemory(IntPtr.Add(initialPtr, offset), 8, out bytesRead);

                    lastAddress = BitConverter.ToInt64(newValue, 0);

                    /* Update the initial pointer so we can read the next offset assuming it's not the last one */
                    initialPtr = new IntPtr(lastAddress);
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Allows us to pass in a "path" of pointers with the final one being the offset for the value we want to read as an int32
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseAddressPtr"></param>
        /// <param name="offsets"></param>
        /// <returns></returns>
        public unsafe IntPtr ReadMultiLevelPointer(IntPtr baseAddressPtr, params int[] offsets)
        {
            long bytesRead;

            IntPtr ptr = IntPtr.Zero;

            var initialPtr = baseAddressPtr;
            long lastAddress = 0;

            IntPtr returnValue = IntPtr.Zero;

            foreach (var offset in offsets)
            {
                var newValue = ReadMemory(IntPtr.Add(initialPtr, offset), 8, out bytesRead);

                lastAddress = BitConverter.ToInt64(newValue, 0);

                /* Update the initial pointer so we can read the next offset assuming it's not the last one */
                initialPtr = new IntPtr(lastAddress);               
            }

            returnValue = initialPtr;
            return returnValue;
        }

        /// <summary>
        /// Allows us to pass in a "path" of pointers with the final one being the offset for the value we want to convert into our string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseAddress"></param>
        /// <param name="offsets"></param>
        /// <returns></returns>
        public unsafe string ReadMultiLevelUTF8String(IntPtr baseAddress, params int[] offsets)
        {
            long bytesRead;

            var intialPtr = baseAddress;
            long lastAddress = 0;
            var returnValue = "";

            foreach (var offset in offsets)
            {
                /* This is the last offset being read so we should go ahead and marshal the value to T and return it */
                if (offset == offsets[offsets.Length - 1])
                {
                    var newValue = ReadMemory(IntPtr.Add(intialPtr, offset), 256, out bytesRead);

                    lastAddress = BitConverter.ToInt64(newValue, 0);

                    var newPtr = new IntPtr(lastAddress);

                    returnValue = ReadUTF8String(newPtr);
                }
                else
                {
                    var newValue = ReadMemory(IntPtr.Add(intialPtr, offset), 256, out bytesRead);

                    lastAddress = BitConverter.ToInt64(newValue, 0);

                    intialPtr = new IntPtr(lastAddress);
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Allows us to pass in a "path" of pointers with the final one being the offset for the value we want to convert into our string
        /// This currently only to process player names so it has the added 0x14 on the "newPtr" for the time being until I can figure out a better way.
        /// If the 0x14 is passed into the offsets parameter, it won't read correctly when 0x14 is the last offset in the list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseAddress"></param>
        /// <param name="offsets"></param>
        /// <returns></returns>
        public unsafe string ReadMultiLevelUnicodeString(IntPtr baseAddress, params int[] offsets)
        {
            long bytesRead;

            var initialPtr = baseAddress;
            long lastAddress = 0;
            var returnValue = "";

            foreach (var offset in offsets)
            {
                var newValue = ReadMemory(initialPtr, 8, out bytesRead);

                /* This is the last offset being read so we should go ahead and marshal the value to T and return it */
                if (offset == offsets[offsets.Length - 1])
                {
                    lastAddress = BitConverter.ToInt64(newValue, 0);

                    var newPtr = new IntPtr(lastAddress) + 0x14;

                    returnValue = ReadUnicodeString(newPtr);
                }
                else
                {
                    lastAddress = BitConverter.ToInt64(newValue, 0);

                    initialPtr = new IntPtr(lastAddress) + offset;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Allows us to pass in a "path" of pointers with the final one being the offset for the value we want to read as an int16
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseAddressPtr"></param>
        /// <param name="offsets"></param>
        /// <returns></returns>
        public unsafe Int16 ReadMultiLevelInt16(IntPtr baseAddressPtr, params int[] offsets)
        {
            long bytesRead;

            var intialPtr = baseAddressPtr;
            long lastAddress = 0;

            Int16 returnValue = 0;

            foreach (var offset in offsets)
            {
                /* This is the last offset being read so we should go ahead and convert the value at the last address */
                if (offset == offsets[offsets.Length - 1])
                {
                    var nextLevelPtr = lastAddress == 0 ? baseAddressPtr : intialPtr;

                    returnValue = Read<Int16>(IntPtr.Add(nextLevelPtr, offset));
                }
                else
                {
                    var newValue = ReadMemory(IntPtr.Add(intialPtr, offset), 8, out bytesRead);

                    lastAddress = BitConverter.ToInt64(newValue, 0);

                    intialPtr = new IntPtr(lastAddress);
                }
            }

            return returnValue;
        }



        /// <summary>
        /// Reads an address and converts its value to a string
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public unsafe string ReadUTF8String(IntPtr address)
        {
            long bytesRead;

            var actualObjectNameValue = ReadMemory(address, 256, out bytesRead);

            var objectNameCharArray = Encoding.UTF8.GetChars(actualObjectNameValue);

            return new string(objectNameCharArray).Split('\0')[0];
        }

        /// <summary>
        /// Reads an address and converts its value to a string
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public unsafe string ReadUnicodeString(IntPtr address)
        {
            long bytesRead;

            var actualObjectNameValue = ReadMemory(address, 256, out bytesRead);

            var objectNameCharArray = Encoding.Unicode.GetChars(actualObjectNameValue);

            return new string(objectNameCharArray).Split('\0')[0];
        }

        /// <summary>
        /// Reads an address to return a long value
        /// </summary>
        /// <param name="baseAddressPtr"></param>
        /// <returns></returns>
        public unsafe long ReadLong(IntPtr baseAddressPtr)
        {
            var gameObjectValue = ReadMemory(baseAddressPtr, 8, out long bytesRead);
            return BitConverter.ToInt64(gameObjectValue, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseAddressPtr"></param>
        /// <param name="offsets"></param>
        /// <returns></returns>
        public unsafe long ReadMultiLevelLong(IntPtr baseAddressPtr, int[] offsets)
        {
            long bytesRead;

            var intialPtr = baseAddressPtr;
            long lastAddress = 0;

            long returnValue = 0;

            foreach (var offset in offsets)
            {
                /* This is the last offset being read so we should go ahead and convert the value at the last address */
                if (offset == offsets[offsets.Length - 1])
                {
                    var nextLevelPtr = lastAddress == 0 ? baseAddressPtr : intialPtr;

                    returnValue = ReadLong(IntPtr.Add(nextLevelPtr, offset));
                }
                else
                {
                    var newValue = ReadMemory(IntPtr.Add(intialPtr, offset), 8, out bytesRead);

                    lastAddress = BitConverter.ToInt64(newValue, 0);

                    intialPtr = new IntPtr(lastAddress);
                }
            }

            return returnValue;
        }


        /// <summary>
        /// Reads the value of a specified type in the remote process.
        /// Cannot read (convert byte array to integral data types, i.e. int, uint, string, etc.)
        /// Can only convert byte arrays to a managed object
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="address">The address where the value is read.</param>
        /// <param name="isRelative">[Optional] State if the address is relative to the main module.</param>
        /// <returns>A value.</returns>
        public unsafe T Read<T>(IntPtr address)
        {
            return MarshalType<T>.ByteArrayToObject(ReadBytes(address, MarshalType<T>.Size));
        }

        /// <summary>
        /// Reads an array of bytes at an address
        /// </summary>
        /// <param name="address">The address where the array is read.</param>
        /// <param name="size">The number of cells.</param>
        /// <param name="isRelative">[Optional] State if the address is relative to the main module.</param>
        /// <returns>The array of bytes.</returns>
        public unsafe byte[] ReadBytes(IntPtr address, int size)
        {
            byte[] buffer = new byte[size];

            PInvoke.ReadMemVirtual(_memHelper, _cr3, (ulong)address, buffer, size);

            return buffer;
        }
    }
}