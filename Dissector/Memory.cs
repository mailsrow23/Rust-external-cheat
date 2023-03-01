using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Diagnostics;

namespace Covet.Memory
{
    public static class MemoryManager
    {
        public const string WindowName = "Rust";

        private static readonly MemoryModule memoryModule = new MemoryModule("RustClient");

        [StructLayout(LayoutKind.Sequential)]
        private struct MemoryStruct
        {
            public int StrLength { get; set; }
            public string Str { get; set; }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct WindowRect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        public static Process GetProcess()
        {
            try
            {
                var process = Process.GetProcessesByName(WindowName).FirstOrDefault();
                if (process == null)
                {
                    throw new ApplicationException($"Process with name {WindowName} not found.");
                }

                return process;
            }
            catch (Exception ex)
            {
                // log the exception and return null or throw a custom exception
                return null;
            }
        }

        // other methods and properties go here
    }
}


private static SafeHandle GetProcessHandle()
        {
            if (processHandle == null || processHandle.IsClosed || processHandle.IsInvalid)
            {
                Process process = GetProcess();

                if (process != null)
                {
                    processHandle = NativeMethods.OpenProcess(ProcessAccessFlags.All, false, process.Id);
                }
            }

            return processHandle;
        }
    }
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

private static byte[] ReadMemory(IntPtr address, int numOfBytes, out int bytesRead)
{
    if (!ReadProcessMemory(ProcessHandle, address, out byte[] buffer, numOfBytes, out IntPtr pBytesRead))
    {
        throw new Win32Exception(Marshal.GetLastWin32Error());
    }

    bytesRead = pBytesRead.ToInt32();

    return buffer;
}

