using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.Win32;
using System.IO;

namespace Auto_filler
{
    public partial class MainWindow : Window
    {
        private KeyboardListener _listener;
        bool ScreenShot = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void HideButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        void _listener_OnKeyPressed(object sender, KeyPressedArgs e)
        {
           // if (Keyboard.IsKeyDown(Key.G)
           // && Keyboard.IsKeyDown(Key.LeftCtrl)
           // && Keyboard.IsKeyDown(Key.LeftAlt)
          //  || Keyboard.IsKeyDown(Key.G)
           // && Keyboard.IsKeyDown(Key.RightCtrl)
          //  && Keyboard.IsKeyDown(Key.RightAlt))
           //     this.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _listener = new KeyboardListener(this);
            _listener.OnKeyPressed += _listener_OnKeyPressed;
            _listener.HookKeyboard();
        }

        private void ScreenshotCheck_Checked(object sender, RoutedEventArgs e)
        {
                ScreenShot = true;
        }
        private void ScreenshotCheck_Unchecked(object sender, RoutedEventArgs e)
        {
                ScreenShot = false;
        }
        public void CaptureMyScreen()

        {
            if (ScreenShot)
            {
                try
                {
                    int screenWidth = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
                    int screenHeight = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
                    Bitmap captureBitmap = new Bitmap(screenWidth, screenHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                    System.Drawing.Rectangle captureRectangle = System.Windows.Forms.Screen.AllScreens[0].Bounds;
                    Graphics captureGraphics = Graphics.FromImage(captureBitmap);
                    captureGraphics.CopyFromScreen(captureRectangle.Left, captureRectangle.Top, 0, 0, captureRectangle.Size);
                    //captureBitmap.Save(@"I:\Capture.jpg", ImageFormat.Jpeg); Nie zapisuje na C:\ (Coś z uprawnieniami)
                    MessageBox.Show("Screen Captured");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void ValueSaver_Click(object sender, RoutedEventArgs e)
        {
            string link = ValueHolder.Text;
            _listener.CatchLink(link);
        }

        private void ValueHolder_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        RegistryKey reg;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            reg.SetValue("Auto filler", System.IO.Path.GetFullPath("Auto-filler.exe"));
            MessageBox.Show("Udało się!");
        }
    }
}