using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Input;
using System.Windows;
using System.Diagnostics;
using System.Threading;

namespace Auto_filler
{
    class KeyboardListener
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_SYSKEYDOWN = 0x0104;
        private const int WM_KEYUP = 0x0101;
        bool cond = false;
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

        public MainWindow Mainwindow { get; }

        public KeyboardListener(MainWindow mainwindow)
        {
            _proc = HookCallback;
            Mainwindow = mainwindow;
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
                // int vkCode = Marshal.ReadInt32(lParam);
                MultiClipboard();
                AppShow();
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        private void MultiClipboard()
        {
            if (Keyboard.IsKeyDown(Key.C)
          && Keyboard.IsKeyDown(Key.LeftCtrl) && cond == false
          || Keyboard.IsKeyDown(Key.RightCtrl)
          && Keyboard.IsKeyDown(Key.C) && cond == false)
            {
                Clipboard.SetDataObject("aaaaaaaaa");
                //MessageBox.Show("D1111");
                cond = true;
            }
            else if (Keyboard.IsKeyUp(Key.C)
          && Keyboard.IsKeyUp(Key.LeftCtrl)
          || Keyboard.IsKeyUp(Key.RightCtrl)
          && Keyboard.IsKeyUp(Key.C))
            {
                cond = false;
            }
            else if (Keyboard.IsKeyDown(Key.C)
          && Keyboard.IsKeyDown(Key.LeftCtrl) && cond == true
          || Keyboard.IsKeyDown(Key.RightCtrl)
          && Keyboard.IsKeyDown(Key.C) && cond == true)
            {
                cond = false;
                Clipboard.SetDataObject("bbbbbbbbb");
                //MessageBox.Show("D1111");
            }
            
        }

        public void AppShow()
        {
            if (Keyboard.IsKeyDown(Key.G)
            && Keyboard.IsKeyDown(Key.LeftCtrl)
            && Keyboard.IsKeyDown(Key.LeftAlt)
             || Keyboard.IsKeyDown(Key.G)
            && Keyboard.IsKeyDown(Key.RightCtrl)
             && Keyboard.IsKeyDown(Key.RightAlt))
                Mainwindow.Show();
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

