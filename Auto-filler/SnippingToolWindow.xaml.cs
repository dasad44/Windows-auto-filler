using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static Auto_filler.MouseHook;

namespace Auto_filler
{
    /// <summary>
    /// Interaction logic for SnippingToolWindow.xaml
    /// </summary>
    public partial class SnippingToolWindow : Window
    {
        public static int startx = 2;
        public static int starty = 2;
        public static int endx = 1;
        public static int endy = 1;
        public static bool MarkStart = false;
        string path = Properties.Settings.Default.ScreenPath + "\\Test" + ".jpg";
        Bitmap bit;
        ImageOperation imageoperation = new ImageOperation();
        public SnippingToolWindow()
        {
            InitializeComponent();
            POINT startV;
            POINT endV;
            startV.x = 0;
            startV.y = 0;
            endV.x = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            endV.y = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            string path = Properties.Settings.Default.ScreenPath + "\\Test" + ".jpg";
            int screenWidth = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            int x = screenWidth - startV.x - (screenWidth - endV.x);
            int y = screenHeight - startV.y - (screenHeight - endV.y);
            Bitmap captureBitmap = new Bitmap(x, y, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            System.Drawing.Rectangle captureRectangle = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
            Graphics captureGraphics = Graphics.FromImage(captureBitmap);
            captureGraphics.CopyFromScreen(startV.x, startV.y, 0, 0, captureRectangle.Size);
            captureBitmap.Save(@path, ImageFormat.Jpeg);

            bit = (Bitmap)Bitmap.FromFile(@path);
        }
       
        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            this.Hide();
        }


        public void Marking(Bitmap largeBmp)
        {

            var bitmap = new Bitmap(3000, 3000, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            var graphics = Graphics.FromImage(bitmap);
            graphics.DrawRectangle(new System.Drawing.Pen(System.Drawing.Color.White), 10, 10, endx-startx, endy-starty);
            Graphics g = Graphics.FromImage(largeBmp);
            g.CompositingMode = CompositingMode.SourceOver;
            g.DrawImage(bitmap, new System.Drawing.Point(startx, starty));
            wholescreenimage.Source = imageoperation.ImageSourceFromBitmap(largeBmp);  // converting bitmap to Media.Source
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {    
            if (MarkStart == true)
            {
                
                bit = (Bitmap)Bitmap.FromFile(@path);
                //Marking(bit);
            }
        }
    }
}
