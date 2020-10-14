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
using System.Diagnostics;

namespace Auto_filler
{
    class ScreenshotSave
    {
        private GetCurrentTime _getCurrentTime;

        public void CaptureMyScreen(System.Windows.Point mouseStart,
        System.Windows.Point mouseEnd)
        {
            
            if (MainWindow.ScreenShot)
            {
                try
                {
                    _getCurrentTime = new GetCurrentTime();
                    String Date = _getCurrentTime.GetTime();

                    string path = Properties.Settings.Default.ScreenPath + "\\AutoFiller-" + Date + ".jpg";
                    double Xd = MainWindow.posStart.X;
                    double Yd = MainWindow.posStart.Y;
                    int X = (int)Xd;
                    int Y = (int)Yd;
                    int screenWidth = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - 500 - 200;
                    int screenHeight = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - 300 - 200;
                    Bitmap captureBitmap = new Bitmap(screenWidth, screenHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                    System.Drawing.Rectangle captureRectangle = System.Windows.Forms.Screen.AllScreens[0].Bounds;
                    Graphics captureGraphics = Graphics.FromImage(captureBitmap);
                    captureGraphics.CopyFromScreen(0,0, -500, -300, captureRectangle.Size);
                    captureBitmap.Save(@path, ImageFormat.Jpeg);
                    MessageBox.Show("Screen Captured " + X + " " + Date + "ssss" + mouseStart + " ss " + mouseEnd);
                    Process photoViewer = new Process();
                    photoViewer.StartInfo.FileName = @path;
                    photoViewer.StartInfo.Arguments = @"\Windows Photo Viewer\PhotoViewer.dll";
                    photoViewer.Start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
       


    }
}
