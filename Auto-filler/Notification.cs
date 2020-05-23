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
        string title = "test test";
        string body = "one two three";
        int timeout = 1000;
        public void Show()
        {
            NotifyIcon notifyicon = new NotifyIcon();
            notifyicon.Visible = true;
            notifyicon.Icon = SystemIcons.Application;
            notifyicon.ShowBalloonTip(timeout, title, body, ToolTipIcon.Info);
            notifyicon.Dispose();
        }
    }
}
