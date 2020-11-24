using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Auto_filler
{
    class Notification
    {
        ImageOperation imageoperation = new ImageOperation();

        public void Show(string title, string body, int timeout)
        {
            NotifyIcon notifyicon = new NotifyIcon();
            notifyicon.Visible = true;
            notifyicon.Icon = SystemIcons.Application;
            notifyicon.ShowBalloonTip(timeout, title, body, ToolTipIcon.Info);
            notifyicon.Dispose();
        }

        public void CustomNotifyImageAlert(Bitmap bitmap)
        {
            SnippingToolAlert sta = new SnippingToolAlert();
            sta.screenshotimage.Source = imageoperation.ImageSourceFromBitmap(bitmap);  // converting bitmap to Media.Source
            sta.Show();
        }




    }
}
