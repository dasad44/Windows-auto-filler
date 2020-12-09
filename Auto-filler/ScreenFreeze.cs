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
            int screenWidth = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
                int screenHeight = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
                int x = screenWidth - startV.x - (screenWidth - endV.x);
                int y = screenHeight - startV.y - (screenHeight - endV.y);
                Bitmap captureBitmap = new Bitmap(x, y, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                System.Drawing.Rectangle captureRectangle = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
                Graphics captureGraphics = Graphics.FromImage(captureBitmap);
                captureGraphics.CopyFromScreen(startV.x, startV.y, 0, 0, captureRectangle.Size);
            return captureBitmap;
        }
    }
}

