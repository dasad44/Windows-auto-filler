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
using System.IO;

namespace Auto_filler
{
    class ScreenshotAutoDelete
    {
        string[] filePaths;
        public void AutoDelete()
        {

            filePaths = Directory.GetFiles(@Properties.Settings.Default.ScreenPath, "AutoFiller*.jpg");
            foreach (string element in filePaths)
            {
                string name = System.IO.Path.GetFileName(element);
                string Sdate = name.Substring(11, 10);
                DateTime date = Convert.ToDateTime(Sdate);
                int days = (DateTime.Now - date).Days;
                if (days >= 3)
                    File.Delete(element);
            }
        }
        

    }
}
