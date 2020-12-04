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
    class ScreenshotSave
    {
        IDataObject tmp_clipboard;
        private GetCurrentTime _getCurrentTime;
        Notification notification = new Notification();
        ClipboardHandler clipboardhandler = new ClipboardHandler();
        public void CaptureMyScreen(POINT startV,
        POINT endV)
        {
            try
            {
                _getCurrentTime = new GetCurrentTime();
                String Date = _getCurrentTime.GetTime();

                if (startV.x > endV.x)
                {
                    int t = startV.x;
                    startV.x = endV.x;
                    endV.x = t;
                }
                if (startV.y > endV.y)
                {
                    int t = startV.y;
                    startV.y = endV.y;
                    endV.y = t;
                }
                if(startV.x == endV.x || startV.y == endV.y)
                {
                    MessageBox.Show("Zaznacz fragment ekranu");
                    return;
                }
                //MessageBox.Show(startV.x + "  " + startV.y + "  " + endV.x + "  " + endV.y + "  ");
                string path = Properties.Settings.Default.ScreenPath + "\\AutoFiller-" + Date + ".jpg";
                int screenWidth = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
                int screenHeight = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
                int x = screenWidth - startV.x - (screenWidth - endV.x);
                int y = screenHeight - startV.y - (screenHeight - endV.y);
                //MessageBox.Show(startV.x + "  " + startV.y + "  " + endV.x + "  " + endV.y + "  "+x + "  " + y);
                Bitmap captureBitmap = new Bitmap(x, y, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                System.Drawing.Rectangle captureRectangle = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
                Graphics captureGraphics = Graphics.FromImage(captureBitmap);
                captureGraphics.CopyFromScreen(startV.x, startV.y, 0, 0, captureRectangle.Size);
                captureBitmap.Save(@path, ImageFormat.Jpeg);
                //multiclipboard queue actions
                Clipboard.SetDataObject(captureBitmap);
                tmp_clipboard = Clipboard.GetDataObject();
                ClipboardValueContainer.clipboard_3 = ClipboardValueContainer.clipboard_2;
                ClipboardValueContainer.clipboard_2 = ClipboardValueContainer.clipboard_1;
                ClipboardValueContainer.clipboard_1 = clipboardhandler.ReadClipboard(tmp_clipboard);

                //MessageBox.Show("Screen Captured  " + startV.x + "  "+ startV.y + "  "+ endV.x + "  "+ endV.y + "  ");
                Process photoViewer = new Process();
                notification.CustomNotifyImageAlert("Screenshot has been captured", captureBitmap);
                photoViewer.StartInfo.FileName = @path;
                photoViewer.StartInfo.Arguments = @"\Windows Photo Viewer\PhotoViewer.dll";
                //photoViewer.Start();
                KeyboardListener.SnippCondition = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
