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
            SnippingToolAlert sta = new SnippingToolAlert();
            sta.Topmost = true;
            sta.ShowInTaskbar = false; // Nie pojawia się na pasku zadań
            sta.notifytext.Text = title;
            sta.screenshotimage.Source = imageoperation.ImageSourceFromBitmap(bitmap);  // converting bitmap to Media.Source
            sta.Show();
            await Task.Delay(3500);
            sta.Close();
        }

        public async void CustomNotifyAlert(string title, string body)
        {
            NotificationAlert na = new NotificationAlert();
            na.Topmost = true;
            na.ShowInTaskbar = false; // Nie pojawia się na pasku zadań
            na.notifytitle.Text = title;
            na.notifibody.Text = body;
            na.Show();
            await Task.Delay(4000);
            na.Close();
        }

        public async void CustomClipboardAlert()
        {
            MultiClipboardNotification mcn = new MultiClipboardNotification();
            mcn.Topmost = true;
            mcn.ShowInTaskbar = false; // Nie pojawia się na pasku zadań
            await Task.Delay(50);  
            mcn.Show();
            await Task.Delay(2200);
            mcn.Close();
        }

        public async void CloudNotificationClipboardAlert()
        {
            CloudNotification cn = new CloudNotification();
            cn.Topmost = true;
            cn.ShowInTaskbar = false;
            cn.Show();
            await Task.Delay(2200);
            cn.Close();
        }
    }
}
