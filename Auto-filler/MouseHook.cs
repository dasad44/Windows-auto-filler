﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using Point = System.Drawing.Point;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace Auto_filler
{
    class MouseHook
    {
        public static event EventHandler MouseAction = delegate { };

        public static void Start() => _hookID = SetHook(_proc);
        public static void stop() => UnhookWindowsHookEx(_hookID);

        private static LowLevelMouseProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;

        private static IntPtr SetHook(LowLevelMouseProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_MOUSE_LL, proc,
                  GetModuleHandle(curModule.ModuleName), 0);
            }
        }
        static POINT StartPoint;
        static POINT EndPoint;
        private static SnippingTool _snippingTool;
        static POINT CursorPosition;
        private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);
        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            //CloudNotification 
            if (nCode >= 0 && MouseMessages.WM_MOUSEMOVE == (MouseMessages)wParam)
            {
                MSLLHOOKSTRUCT hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
                MouseAction(null, new EventArgs());
                if (KeyboardListener.SnippCondition == true)
                {
                    Auto_filler.CloudNotification.cloudnotification.SetPosition();                                       
                }
                CursorPosition.x = Cursor.Position.X;
                CursorPosition.y = Cursor.Position.Y;
                //Console.WriteLine("Xx:" + Cursor.Position.X + " Yy: " + Cursor.Position.Y);
                }
            if(KeyboardListener.SnippCondition == false)
            {
                if (nCode >= 0 && MouseMessages.WM_LBUTTONDOWN == (MouseMessages)wParam)
                {
                    MSLLHOOKSTRUCT hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
                    MouseAction(null, new EventArgs());
                    SnippingToolWindow.MarkStart = true;
                    StartPoint.x = Cursor.Position.X;
                    StartPoint.y = Cursor.Position.Y;
                    Console.WriteLine("X:" + Cursor.Position.X + " Y: " + Cursor.Position.Y);
                }
                if (nCode >= 0 && MouseMessages.WM_LBUTTONUP == (MouseMessages)wParam)
                {
                    MSLLHOOKSTRUCT hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
                    MouseAction(null, new EventArgs());

                    EndPoint.x = Cursor.Position.X;
                    EndPoint.y = Cursor.Position.Y;
                    Console.WriteLine("X:" + Cursor.Position.X + " Y: " + Cursor.Position.Y);
                }
            }           
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }
        public static POINT getStartValue()
        {
            return StartPoint;
        }

        public static POINT getEndValue()
        {
            return EndPoint;
        }
        public static POINT getMousePosition()
        {
            return CursorPosition;
        }

        private const int WH_MOUSE_LL = 14;

        private enum MouseMessages
        {
            WM_LBUTTONDOWN = 0x0201,
            WM_LBUTTONUP = 0x0202,
            WM_MOUSEMOVE = 0x0200,
            WM_MOUSEWHEEL = 0x020A,
            WM_RBUTTONDOWN = 0x0204,
            WM_RBUTTONUP = 0x0205,
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MSLLHOOKSTRUCT
        {
            public POINT pt;
            public uint mouseData, flags, time;
            public IntPtr dwExtraInfo;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
          LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
          IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

    }
}
