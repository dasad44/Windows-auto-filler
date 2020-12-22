using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;
using System.IO;

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
            string ff = System.Reflection.Assembly.GetExecutingAssembly().Location;
            ff = ff.Remove(ff.Length - 25);
            ff = ff + "bin/Debug/images\\favicon.ico";


            nIcon.Icon = new Icon(@ff);
            nIcon.Visible = true;
           
            nIcon.Click += NIcon_Click;
        }
        void NIcon_Click(object sender, EventArgs e)
        {
            MainWindow.Visibility = System.Windows.Visibility.Visible;
            MainWindow.WindowState = WindowState.Normal;
        }
    }
}

