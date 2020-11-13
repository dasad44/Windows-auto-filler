using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace Auto_filler
{
    class Notification
    {

        public void Show(string title, string body, int timeout)
        {
            NotifyIcon notifyicon = new NotifyIcon();
            notifyicon.Visible = true;
            notifyicon.Icon = SystemIcons.Application;
            notifyicon.ShowBalloonTip(timeout, title, body, ToolTipIcon.Info);
            notifyicon.Dispose();
        }

        public void ShowWithImage(string title, string imageLink)
        {
            string toastXmlString =
            $@"<toast><visual>
            <binding template='ToastGeneric'>
            <text>{title}</text>
            <image src='{imageLink}'/>
            </binding>
            </visual></toast>";

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(toastXmlString);

            var toastNotification = new ToastNotification(xmlDoc);

            var toastNotifier = ToastNotificationManager.CreateToastNotifier();
            toastNotifier.Show(toastNotification);
        }

    }
}
