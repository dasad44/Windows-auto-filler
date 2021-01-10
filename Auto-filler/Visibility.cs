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
            Form.SaverDirectory.IsHitTestVisible = true;
            Form.SaverDirectory.Opacity = 1;
            Form.SaverDirectoryButton.IsHitTestVisible = true;
            Form.SaverDirectoryButton.Opacity = 1;
            Form.SaverListView.IsHitTestVisible = true;
            Form.SaverListView.Opacity = 1;
            Form.ImportantScreenList.IsHitTestVisible = true;
            Form.ImportantScreenList.Opacity = 1;
            Form.AllScreenList.IsHitTestVisible = true;
            Form.AllScreenList.Opacity = 1;
            Form.CancelDeleteList.IsHitTestVisible = true;
            Form.CancelDeleteList.Opacity = 1;
            Form.TempScreenList.IsHitTestVisible = true;
            Form.TempScreenList.Opacity = 1;
            Form.DayLimit.IsHitTestVisible = true;
            Form.DayLimit.Opacity = 1;
            Form.DayLimitclick.IsHitTestVisible = true;
            Form.DayLimitclick.Opacity = 1;
            Form.DayLimitText.IsHitTestVisible = true;
            Form.DayLimitText.Opacity = 1;
            Form.AutoDelete.IsHitTestVisible = true;
            Form.AutoDelete.Opacity = 1;
        }
        public void ScreenShotInVis()
        {
            Form.SaverDirectory.IsHitTestVisible = false;
            Form.SaverDirectory.Opacity = 0.5;
            Form.SaverDirectoryButton.IsHitTestVisible = false;
            Form.SaverDirectoryButton.Opacity = 0.5;
            Form.SaverListView.IsHitTestVisible = false;
            Form.SaverListView.Opacity = 0.5;
            Form.ImportantScreenList.IsHitTestVisible = false;
            Form.ImportantScreenList.Opacity = 0.5;
            Form.AllScreenList.IsHitTestVisible = false;
            Form.AllScreenList.Opacity = 0.5;
            Form.CancelDeleteList.IsHitTestVisible = false;
            Form.CancelDeleteList.Opacity = 0.5;
            Form.TempScreenList.IsHitTestVisible = false;
            Form.TempScreenList.Opacity = 0.5;
            Form.DayLimit.IsHitTestVisible = false;
            Form.DayLimit.Opacity = 0.5;
            Form.DayLimitclick.IsHitTestVisible = false;
            Form.DayLimitclick.Opacity = 0.5;
            Form.DayLimitText.IsHitTestVisible = false;
            Form.DayLimitText.Opacity = 0.5;
            Form.AutoDelete.IsHitTestVisible = false;
            Form.AutoDelete.Opacity = 0.5;
        }
    }
}
