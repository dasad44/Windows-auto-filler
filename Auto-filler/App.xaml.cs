using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;


namespace Auto_filler
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        System.Windows.Forms.NotifyIcon nIcon = new System.Windows.Forms.NotifyIcon();
        public App()
        {
            nIcon.Icon = new Icon(@"C:\Users\Michal\Desktop\favicon.ico");
            nIcon.Visible = true;
            nIcon.ShowBalloonTip(5000, "Windows Auto Filler", "I'm still here!", System.Windows.Forms.ToolTipIcon.Info);
            nIcon.Click += nIcon_Click;
        }

        void nIcon_Click(object sender, EventArgs e)
        {
            MainWindow.Visibility = Visibility.Visible;
            MainWindow.WindowState = WindowState.Normal;
        }
    }

}

