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

namespace Auto_filler
{
    //Narazie nie używana
    class ScreenshotSave
    {
        bool ScreenShot = false;
        //string path = @Environment.GetFolderPath(Environment.SpecialFolder.Desktop); 
        public void CaptureMyScreen()

        {
            if (!ScreenShot)
            {
                try
                {
                    Bitmap captureBitmap = new Bitmap(1024, 768, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                    System.Drawing.Rectangle captureRectangle = System.Windows.Forms.Screen.AllScreens[0].Bounds;
                    Graphics captureGraphics = Graphics.FromImage(captureBitmap);
                    captureGraphics.CopyFromScreen(captureRectangle.Left, captureRectangle.Top, 0, 0, captureRectangle.Size);
                    //captureBitmap.Save(@"I:\Capture.jpg", ImageFormat.Jpeg);    Nie może zapisać na C:\ (Brak uprawnień??)
                    MessageBox.Show("Screen Captured");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


        private void ScreenSaveOnn()
        {
            if (!ScreenShot)
            {
                MessageBox.Show("Screenshot ON");
                ScreenShot = true;
            }
            else if(ScreenShot)
            {
                MessageBox.Show("Screenshot OFF");
                ScreenShot = false;
            }
        }
        public void ScreenSaveOn()
        {
            ScreenSaveOnn();
        }
    }
}
