﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Input;
using System.Windows;
using Microsoft.Win32;
using System.IO;
using System.Drawing;
using System.Windows.Media.Imaging;
using static Auto_filler.MouseHook;

namespace Auto_filler
{
    class KeyboardListener
    {
        private const int WH_KEYBOARD_LL = 13;
        //private const int WM_SYSKEYDOWN = 0x0104;
        private const int WM_SYSKEYUP = 0x0105;
        private const int WM_KEYUP = 0x0101;

        private bool ctrl1clicked = false, ctrl2clicked = false;
        string text_1 = "", text_2 = "", text_3 = "", tmp1 = "";
        ClipboardHandler clipboardhandler = new ClipboardHandler();
        IDataObject tmp_clipboard;
        DataObject clipboard_1 = new DataObject("");
        DataObject clipboard_2 = new DataObject("");
        DataObject clipboard_3 = new DataObject("");
        DataObject actualclipboard = new DataObject();

        string _link;
        string _link2;
        string _link3;
        string _link4;
        string _link5;
        CatchLink cl = new CatchLink();
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
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYUP || wParam == (IntPtr)WM_SYSKEYUP)
            {
                SaveMultiClipboard();
                AppShow();
                ScreenShot();
                LinkButton();
                saveImg();
                MouseHookOn();
                MouseHookOff();
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        private void saveImg()
        {
            if (Properties.Settings.Default.ClipboardCheck == false)
                return;
            if (Keyboard.IsKeyDown(Key.PrintScreen) && Keyboard.IsKeyDown(Key.LeftAlt) || Keyboard.IsKeyDown(Key.PrintScreen) && Keyboard.IsKeyDown(Key.RightAlt) || Keyboard.IsKeyDown(Key.PrintScreen))
            {
                tmp_clipboard = Clipboard.GetDataObject();
                clipboard_3 = clipboard_2;
                clipboard_2 = clipboard_1;
                clipboard_1 = clipboardhandler.ReadClipboard(tmp_clipboard);
            }
        }

        private void SaveMultiClipboard()
        {
            if (Properties.Settings.Default.ClipboardCheck == false)
                return;
            if (Keyboard.IsKeyDown(Key.C)           //ctrl+C
          && Keyboard.IsKeyDown(Key.LeftCtrl) && ctrl1clicked == false && ctrl2clicked == false
          || Keyboard.IsKeyDown(Key.RightCtrl)
          && Keyboard.IsKeyDown(Key.C) && ctrl1clicked == false && ctrl2clicked == false)
            {
                tmp_clipboard = Clipboard.GetDataObject();
                actualclipboard = clipboardhandler.ReadClipboard(tmp_clipboard);
                //checking if clipboard has new value(is marked)
                tmp1 = Clipboard.GetText();

                if (actualclipboard.Equals(clipboard_1) || actualclipboard.Equals(clipboard_2) || actualclipboard.Equals(clipboard_3) || tmp1 == "" || tmp1 == text_2 || tmp1 == text_3 || tmp1 == text_1)
                {
                    clipboardhandler.UpdateClipboard(clipboard_1);
                }
                else
                {
                    clipboard_3 = clipboard_2;
                    clipboard_2 = clipboard_1;
                    clipboard_1 = clipboardhandler.ReadClipboard(tmp_clipboard);
                    text_3 = text_2;
                    text_2 = text_1;
                    text_1 = clipboardhandler.SaveText();
                }

                ctrl1clicked = true;
            }
            else if (Keyboard.IsKeyUp(Key.C)
          && Keyboard.IsKeyUp(Key.LeftCtrl)
          || Keyboard.IsKeyUp(Key.RightCtrl)
          && Keyboard.IsKeyUp(Key.C)
          || Keyboard.IsKeyUp(Key.LeftCtrl)
          || Keyboard.IsKeyUp(Key.C))
            {
                ctrl1clicked = false;
                ctrl2clicked = false;
            }
            else if (Keyboard.IsKeyDown(Key.C)           //ctrl+CC
          && Keyboard.IsKeyDown(Key.LeftCtrl) && ctrl1clicked == true && ctrl2clicked == false
          || Keyboard.IsKeyDown(Key.RightCtrl)
          && Keyboard.IsKeyDown(Key.C) && ctrl1clicked == true && ctrl2clicked == false)
            {
                ctrl2clicked = true;
                clipboardhandler.UpdateClipboard(clipboard_2);
            }
            else if (Keyboard.IsKeyUp(Key.C)
          && Keyboard.IsKeyUp(Key.LeftCtrl)
          || Keyboard.IsKeyUp(Key.RightCtrl)
          && Keyboard.IsKeyUp(Key.C) 
          || Keyboard.IsKeyUp(Key.LeftCtrl)
          || Keyboard.IsKeyUp(Key.C))
            {
                ctrl1clicked = false;
                ctrl2clicked = false;
            }
            else if (Keyboard.IsKeyDown(Key.C)                  //ctrl+CCC
          && Keyboard.IsKeyDown(Key.LeftCtrl) && ctrl2clicked == true && ctrl2clicked == true
          || Keyboard.IsKeyDown(Key.RightCtrl)
          && Keyboard.IsKeyDown(Key.C) && ctrl2clicked == true && ctrl2clicked == true)
            {
                //text_1 = clipboardhandler.SaveText();
                clipboardhandler.UpdateClipboard(clipboard_3);
                ctrl2clicked = false;
                ctrl1clicked = false;
            }
        }

        public void AppShow()
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.LeftAlt) && Keyboard.IsKeyDown(Key.G))
            {
                Mainwindow.Show();
            }
        }

        private ScreenshotSave _screenSave;
        public void ScreenShot()
        {
            if (Keyboard.IsKeyDown(Key.PrintScreen) && Properties.Settings.Default.ScreenCheck == true)
            {
                POINT startV;
                POINT endV;
                startV.x = 0;
                startV.y = 0;
                endV.x = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
                endV.y = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
                _screenSave = new ScreenshotSave();
                _screenSave.CaptureMyScreen(startV, endV);
            }
        }
        public void MouseHookOn()
        {

            if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.Q) && Properties.Settings.Default.SnippCheck == true)
            {
                MouseHook.Start();
            }
        }
        public void MouseHookOff()
        {

            if (Keyboard.IsKeyDown(Key.Escape))
            {
                MouseHook.stop();
            }
        }

        public void CatchLink(string link)
        {
            _link = link;
        }
        public void CatchLink2(string link2)
        {
            _link2 = link2;
        }
        public void CatchLink3(string link3)
        {
            _link3 = link3;
        }
        public void CatchLink4(string link4)
        {
            _link4 = link4;
        }
        public void CatchLink5(string link5)
        {
            _link5 = link5;
        }
        public void LinkButton()
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.D1) && Properties.Settings.Default.LinkCheck == true)
            {
                cl.LinkOpen(_link);
            }
            else if (Keyboard.IsKeyDown(Key.RightCtrl) && Keyboard.IsKeyDown(Key.D1) && Properties.Settings.Default.LinkCheck == true)
            {
                cl.LinkOpen(_link);
            }
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.D2) && Properties.Settings.Default.LinkCheck == true)
            {
                cl.LinkOpen2(_link2);
            }
            else if (Keyboard.IsKeyDown(Key.RightCtrl) && Keyboard.IsKeyDown(Key.D2) && Properties.Settings.Default.LinkCheck == true)
            {
                cl.LinkOpen2(_link2);
            }
            else if (Keyboard.IsKeyDown(Key.RightCtrl) && Keyboard.IsKeyDown(Key.D3) && Properties.Settings.Default.LinkCheck == true)
            {
                cl.LinkOpen3(_link3);
            }
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.D3) && Properties.Settings.Default.LinkCheck == true)
            {
                cl.LinkOpen3(_link3);
            }
            else if (Keyboard.IsKeyDown(Key.RightCtrl) && Keyboard.IsKeyDown(Key.D4) && Properties.Settings.Default.LinkCheck == true)
            {
                cl.LinkOpen4(_link4);
            }
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.D4) && Properties.Settings.Default.LinkCheck == true)
            {
                cl.LinkOpen4(_link4);
            }
            else if (Keyboard.IsKeyDown(Key.RightCtrl) && Keyboard.IsKeyDown(Key.D5) && Properties.Settings.Default.LinkCheck == true)
            {
                cl.LinkOpen5(_link5);
            }
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.D5) && Properties.Settings.Default.LinkCheck == true)
            {
                cl.LinkOpen5(_link5);
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