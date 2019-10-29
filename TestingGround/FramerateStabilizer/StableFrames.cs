using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;

namespace FramerateStabilizer
{
    public static class StableFrames
    {
        /// <summary>
        /// Used in a loop to variably increase processing time in order to normalize it
        /// </summary>
        /// <param name="millisecondsPerFrame">Loop's maximum time per iteration</param>
        /// <param name="timer">A timer that is already started</param>
        public static void Stabilize(long millisecondsPerFrame, Stopwatch timer)
        {
            long frameCount = timer.ElapsedMilliseconds / millisecondsPerFrame;
            double sleepTime = (millisecondsPerFrame - (timer.ElapsedMilliseconds - (millisecondsPerFrame * frameCount)));
            Thread.Sleep((int)sleepTime);
        }
    }

    /// <summary>
    /// Twice as fast as Console.Clear()!!! (takes about 40ms to execute) 
    /// (found on Microsoft's website)
    /// </summary>
    public class ClearConsole
    {
        private const int STD_OUTPUT_HANDLE = -11;
        private const byte EMPTY = 32;

        [StructLayout(LayoutKind.Sequential)]
        struct COORD
        {
            public short x;
            public short y;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct SMALL_RECT
        {
            public short Left;
            public short Top;
            public short Right;
            public short Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct CONSOLE_SCREEN_BUFFER_INFO
        {
            public COORD dwSize;
            public COORD dwCursorPosition;
            public int wAttributes;
            public SMALL_RECT srWindow;
            public COORD dwMaximumWindowSize;
        }

        [DllImport("kernel32.dll", EntryPoint = "GetStdHandle", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern int GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll", EntryPoint = "FillConsoleOutputCharacter", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern int FillConsoleOutputCharacter(int hConsoleOutput, byte cCharacter, int nLength, COORD dwWriteCoord, ref int lpNumberOfCharsWritten);

        [DllImport("kernel32.dll", EntryPoint = "GetConsoleScreenBufferInfo", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern int GetConsoleScreenBufferInfo(int hConsoleOutput, ref CONSOLE_SCREEN_BUFFER_INFO lpConsoleScreenBufferInfo);

        [DllImport("kernel32.dll", EntryPoint = "SetConsoleCursorPosition", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern int SetConsoleCursorPosition(int hConsoleOutput, COORD dwCursorPosition);

        private int hConsoleHandle;

        public ClearConsole()
        {
            // 
            // TODO: Add constructor logic here.
            // 
            hConsoleHandle = GetStdHandle(STD_OUTPUT_HANDLE);
        }

        public void Clear()
        {
            int hWrittenChars = 0;
            CONSOLE_SCREEN_BUFFER_INFO strConsoleInfo = new CONSOLE_SCREEN_BUFFER_INFO();
            COORD Home;
            Home.x = Home.y = 0;
            GetConsoleScreenBufferInfo(hConsoleHandle, ref strConsoleInfo);
            FillConsoleOutputCharacter(hConsoleHandle, EMPTY, strConsoleInfo.dwSize.x * strConsoleInfo.dwSize.y, Home, ref hWrittenChars);
            SetConsoleCursorPosition(hConsoleHandle, Home);
        }
    }
}


