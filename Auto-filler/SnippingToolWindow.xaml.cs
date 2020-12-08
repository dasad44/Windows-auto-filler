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
using static Auto_filler.MouseHook;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Threading;
namespace Auto_filler
{
    /// <summary>
    /// Interaction logic for SnippingToolWindow.xaml
    /// </summary>
    public partial class SnippingToolWindow : Window
    {
        public static int startx = 0;
        public static int starty = 0;
        public static int endx = 0;
        public static int endy = 0;
        public static bool MarkStart = false;
        public SnippingToolWindow()
        {
            InitializeComponent();
        }
       
        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Hide();
            MarkedScreen.Hide();
        }

        MarkedScreenWindow MarkedScreen = new MarkedScreenWindow();
        private ScreenFreeze _screenFreeze;
        ImageOperation imageoperation = new ImageOperation();
        public void Marking()
        {
            POINT startV;
            POINT endV;
            startV.x = startx;
            startV.y = starty;
            endV.x = endx;
            endV.y = endy;
            _screenFreeze = new ScreenFreeze();


            if (startx > endx)
            {
                int t = startx;
                startx = endx;
                endx = t;
            }
            if (starty > endy)
            {
                int t = starty;
                starty = endy;
                endy = t;
            }
            MarkedScreen.Width = endx - startx;
            MarkedScreen.Height = endy - starty;
            if (startx != endx && starty != endy)
            {
                Bitmap Mainbitmap = _screenFreeze.GetScreen(startV, endV);
                System.Drawing.Rectangle r = new System.Drawing.Rectangle(0, 0, Mainbitmap.Width, Mainbitmap.Height);
                
                
                int alpha = 128;
                using (Graphics g = Graphics.FromImage(Mainbitmap))
                {
                    using (Brush cloud_brush = new SolidBrush(Color.FromArgb(alpha, Color.Black)))
                    {
                        g.FillRectangle(cloud_brush, r);
                    }
                }
                MarkedScreen.wholescreenimage.Source = imageoperation.ImageSourceFromBitmap(Mainbitmap);
            }
            Console.WriteLine("X:" + MarkedScreen.Width + " Y: " +  MarkedScreen.Height);   
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {

            if (MarkStart == true)
            {
                MarkedScreen.Left = startx;
                MarkedScreen.Top = starty;
                MarkedScreen.Show();
                Marking();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
