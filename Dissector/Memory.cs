using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Diagnostics;

namespace covet.cc.Memory
{
    class Memory
    {
        private static readonly string WindowName = "Rust";
        private static Process process;
        private static SafeHandle processHandle;
        private static IntPtr unityPlayer;

        private static readonly MemoryModule Mem = new MemoryModule("RustClient");

        [StructLayout(LayoutKind.Sequential)]
        private struct Memory
        {
            public string Pad { get; } = new string('\0', 0x10);
            public int StrLength { get; set; }
            public string Str { get; set; } = new string('\0', 256);
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);

        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(System.Windows.Forms.Keys vKey);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool ReadProcessMemory(IntPtr hProcess, long lpBaseAddress, [In, Out] byte[] lpBuffer, ulong dwSize, out IntPtr lpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool WriteProcessMemory(IntPtr hProcess, long lpBaseAddress, byte[] buffer, ulong size, out IntPtr lpNumberOfBytesWritten);

        public static T ReadMemory<T>(long address) where T : struct
        {
            int structSize = Marshal.SizeOf<T>();
            byte[] buffer = new byte[structSize];
            ReadProcessMemory
                
            return ByteArrayToStructure<T>(buffer);
        }

        private static unsafe byte[] ReadMemory(IntPtr address, int numOfBytes, out long bytesRead)
        {
            byte[] buffer = new byte[numOfBytes];

            IntPtr pBytesRead = IntPtr.Zero;

            ReadProcessMemory((int)ProcessHandle, (int)address, buffer, buffer.Length, ref numOfBytes);

            bytesRead = pBytesRead.ToInt64();

            return buffer;
        }
