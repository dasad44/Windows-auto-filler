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
using static Auto_filler.MouseHook;
using System.Windows.Interop;
using System.Runtime.InteropServices;

namespace Auto_filler
{
    class ScreenFreeze
    {

        
        public Bitmap GetScreen(POINT startV,
        POINT endV)
        {
            int screenWidth = Convert.ToInt32(System.Windows.SystemParameters.VirtualScreenWidth);
            int screenHeight = Convert.ToInt32(System.Windows.SystemParameters.VirtualScreenHeight);
            int sourceX = Convert.ToInt32(System.Windows.SystemParameters.VirtualScreenLeft);
            int sourceY = Convert.ToInt32(System.Windows.SystemParameters.VirtualScreenTop);
            Bitmap captureBitmap = new Bitmap(screenWidth, screenHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            System.Drawing.Rectangle captureRectangle = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
            System.Drawing.Rectangle rectangle1 = new System.Drawing.Rectangle(sourceX, sourceY, screenWidth, screenHeight);
            captureRectangle.Size = rectangle1.Size;
            Graphics captureGraphics = Graphics.FromImage(captureBitmap);
            captureGraphics.CopyFromScreen(sourceX, sourceY, 0, 0, captureRectangle.Size);
            return captureBitmap;
        }
    }
}

