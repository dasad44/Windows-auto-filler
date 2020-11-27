using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Animation;

namespace Auto_filler
{
    class Notification
    {
        ImageOperation imageoperation = new ImageOperation();
        SnippingToolAlert sta = new SnippingToolAlert();
        NotificationAlert na = new NotificationAlert();
        public void Show(string title, string body, int timeout)
        {
            NotifyIcon notifyicon = new NotifyIcon();
            notifyicon.Visible = true;
            notifyicon.Icon = SystemIcons.Application;
            notifyicon.ShowBalloonTip(timeout, title, body, ToolTipIcon.Info);
            notifyicon.Dispose();
        }

        public async void CustomNotifyImageAlert(string title, Bitmap bitmap)
        {
            sta.notifytext.Text = title;
            sta.screenshotimage.Source = imageoperation.ImageSourceFromBitmap(bitmap);  // converting bitmap to Media.Source
            sta.Show();
            await Task.Delay(3500);
            sta.Close();
        }

        public async void CustomNotifyAlert(string title, string body)
        {
            na.notifytitle.Text = title;
            na.notifibody.Text = body;
            na.Show();
            await Task.Delay(4000);
            na.Close();

        }
    }
}
