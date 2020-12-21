using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Auto_filler
{
    class Visibility
    {
        MainWindow Form = Application.Current.Windows[0] as MainWindow;


        public void ScreenShotVis()
        {
            Form.SaverDirectory.Visibility = System.Windows.Visibility.Visible;
            Form.SaverDirectoryButton.Visibility = System.Windows.Visibility.Visible;
            Form.SaverListView.Visibility = System.Windows.Visibility.Visible;
            Form.ListRefresh.Visibility = System.Windows.Visibility.Visible;
            Form.ImportantScreenList.Visibility = System.Windows.Visibility.Visible;
            Form.AllScreenList.Visibility = System.Windows.Visibility.Visible;
            Form.CancelDeleteList.Visibility = System.Windows.Visibility.Visible;
            Form.TempScreenList.Visibility = System.Windows.Visibility.Visible;
        }
        public void ScreenShotInVis()
        {
            Form.SaverDirectory.Visibility = System.Windows.Visibility.Hidden;
            Form.SaverDirectoryButton.Visibility = System.Windows.Visibility.Hidden;
            Form.SaverListView.Visibility = System.Windows.Visibility.Hidden;
            Form.ListRefresh.Visibility = System.Windows.Visibility.Hidden;
            Form.ImportantScreenList.Visibility = System.Windows.Visibility.Hidden;
            Form.AllScreenList.Visibility = System.Windows.Visibility.Hidden;
            Form.CancelDeleteList.Visibility = System.Windows.Visibility.Hidden;
            Form.TempScreenList.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}
