using System;
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

namespace Auto_filler
{
    class KeyboardListener
    {
        private const int WH_KEYBOARD_LL = 13;
        //private const int WM_KEYDOWN = 0x0100;
        private const int WM_SYSKEYDOWN = 0x0104;
        private const int WM_KEYUP = 0x0101;
        //Clipboard formats
        private static readonly string[] clipboardMetaFormats = { "application/x-moz-nativeimage", "FileContents", "EnhancedMetafile", "System.Drawing.Imaging.Metafile", "MetaFilePict", "Object Descriptor", "ObjectLink", "Link Source Descriptor", "Link Source", "Embed Source", "Hyperlink" };
        private bool ctrl1clicked = false, ctrl2clicked = false;
        string text_1 = "", text_2 = "", text_3 = "", tmp1 = "";
        ClipboardHandler clipboardhandler = new ClipboardHandler();
        BitmapSource bitmap;
        BitmapSource tmpbitmap, tmpbitmap2, tmpbitmap3;
        IDataObject d, c, clipboard_1, clipboard_2, clipboard_3, tmp_clipboard;
        DataObject doo = new DataObject();
        DataObject coo = new DataObject();
        bool isImage = false, isImage2 = false;

        string _link;
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
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYUP || wParam == (IntPtr)WM_SYSKEYDOWN)
            {
                SaveMultiClipboard();
                AppShow();
                ScreenShot();
                LinkButton();
                saveImg();
                test();
                test2();
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        private void saveImg()
        {
            if (Keyboard.IsKeyDown(Key.PrintScreen))
            {
                bitmap = Clipboard.GetImage();
                //  d = Clipboard.GetDataObject();

                //tmpbitmap2 = (BitmapSource)d.GetData(DataFormats.Bitmap);
            }
        }
        //format IdataObject to DataObject
        private DataObject ReadClipboard(IDataObject d)
        {
            DataObject result = new DataObject();
            d = Clipboard.GetDataObject();
            string[] formats = d.GetFormats()?.Except(clipboardMetaFormats).ToArray() ?? Array.Empty<string>();
            foreach (string format in formats)
            {
                try
                {
                    object data = d.GetData(format);
                    if (data != null) result.SetData(format, data);
                }
                catch (ExternalException ex)
                {
                    Debug.WriteLine($"Error {ex.ErrorCode}: {ex.Message}");
                }
            }
            return result;
        }

        private void UpdateClipboard(DataObject data)
        {
            if (data == null) return;
            try
            {
                Clipboard.SetDataObject(data);
            }
            catch (ExternalException ex)
            {
                Debug.WriteLine($"Error {ex.ErrorCode}: {ex.Message}");
            }
        }


        public void test()
        {
            if (Keyboard.IsKeyDown(Key.D))
            {
                doo = ReadClipboard(d);
                coo = doo;
                string dd = doo.GetData(DataFormats.Text).ToString();      //content
                MessageBox.Show(dd);
            }
        }

        private void test2()
        {
            if (Keyboard.IsKeyDown(Key.S))
            {
                string dd = coo.GetData(DataFormats.Text).ToString();      //content

                MessageBox.Show(dd);
            }
        }

        private void SaveMultiClipboard()
        {
            if (Keyboard.IsKeyDown(Key.C)           //ctrl+C
          && Keyboard.IsKeyDown(Key.LeftCtrl) && ctrl1clicked == false && ctrl2clicked == false
          || Keyboard.IsKeyDown(Key.RightCtrl)
          && Keyboard.IsKeyDown(Key.C) && ctrl1clicked == false && ctrl2clicked == false)
            {
                tmp_clipboard = Clipboard.GetDataObject();

                if (tmp_clipboard == null || tmp_clipboard == clipboard_1 || tmp_clipboard == clipboard_2 || tmp_clipboard == clipboard_3)
                {
                    Clipboard.SetDataObject(text_1);
                }
                else
                {
                    clipboard_3 = clipboard_2;
                    clipboard_2 = clipboard_1;
                    clipboard_1 = Clipboard.GetDataObject();
                    text_1 = clipboard_1.GetData(DataFormats.Text).ToString();
                    
                }


                //bitmap = null;aaa

                ctrl1clicked = true;
            }
            else if (Keyboard.IsKeyUp(Key.C)
          && Keyboard.IsKeyUp(Key.LeftCtrl)
          || Keyboard.IsKeyUp(Key.RightCtrl)
          && Keyboard.IsKeyUp(Key.C))
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
                text_2 = clipboard_2.GetData(DataFormats.Text).ToString();
                Clipboard.SetDataObject(text_2);

            }
            else if (Keyboard.IsKeyUp(Key.C)
          && Keyboard.IsKeyUp(Key.LeftCtrl)
          || Keyboard.IsKeyUp(Key.RightCtrl)
          && Keyboard.IsKeyUp(Key.C))
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
                text_3 = clipboard_3.GetData(DataFormats.Text).ToString();
                Clipboard.SetDataObject(text_3);
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

            if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.LeftAlt) && Keyboard.IsKeyDown(Key.S))
            {
                _screenSave = new ScreenshotSave();
                _screenSave.CaptureMyScreen();
            }
        }
        public void CatchLink(string link)
        {
            _link = link;
        }
        public void LinkButton()
        {
            if (Keyboard.IsKeyDown(Key.NumPad1))
            {
                cl.LinkOpen(_link);
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