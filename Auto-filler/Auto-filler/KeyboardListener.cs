﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Input;
using System.Windows;

namespace Auto_filler
{
    class KeyboardListener
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_SYSKEYDOWN = 0x0104;
        private const int WM_KEYUP = 0x0101;

        static bool ctrlPressed = false;
        static bool altPressed = false;

        Auto_filler.MainWindow mw = new MainWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        public delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        public event EventHandler<KeyPressedArgs> OnKeyPressed;

        private LowLevelKeyboardProc _proc;
        private IntPtr _hookID = IntPtr.Zero;

        public KeyboardListener()
        {
            _proc = HookCallback;
        }

        public void HookKeyboard()
        {
            _hookID = SetHook(_proc);
        }

        public void UnHookKeyboard()
        {
            UnhookWindowsHookEx(_hookID);
        }

        private IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }
        //main function
        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYUP || wParam == (IntPtr)WM_SYSKEYDOWN)
            {
                CtrlCHandler(lParam);
                AppShow(lParam, wParam);
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        private void CtrlCHandler(IntPtr lParam)
        {
            int vkCode = Marshal.ReadInt32(lParam);
            if (vkCode == 162 || vkCode == 163)
            {
                ctrlPressed = true;
            }
            else if (vkCode == 67 && ctrlPressed == true)
            {
                Clipboard.SetDataObject("dsadaaaaaaas");
                ctrlPressed = false;
            }
            else
            {
                ctrlPressed = false;
                altPressed = false;
            }
            //every key
            //OnKeyPressed(this, new KeyPressedArgs(KeyInterop.KeyFromVirtualKey(vkCode)));
        }

        public void AppShow(IntPtr lParam, IntPtr wParam)
        {
            int vkCode = Marshal.ReadInt32(lParam);
            if (vkCode == 162 || vkCode == 163) //162 is Left Ctrl, 163 is Right Ctrl
            {
                ctrlPressed = true;
            }
            else if (vkCode == 164 || vkCode == 165 && ctrlPressed == true) //162 is Left Alt, 163 is Right Alt
            {
                ctrlPressed = false;
                altPressed = true;
            }
            else if (vkCode == 71 && altPressed == true) //is G key
            {
                ctrlPressed = false;
                altPressed = false;
            }
            else
            {
                ctrlPressed = false;
                altPressed = false;
            }
        }
    }

    public class KeyPressedArgs : EventArgs
    {
        public Key KeyPressed { get; private set; }

        public KeyPressedArgs(Key key)
        {
            KeyPressed = key;
        }
    }
}

