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
using System.Windows.Shapes;

namespace Auto_filler
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        RegistryOnOff regis = new RegistryOnOff();
        Notification notification = new Notification();
        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.CheckOnOff)
            {
                CheckinOnOff.Content = "CheckOff";
            }
            else
            {
                CheckinOnOff.Content = "CheckOn";
            }
        }
        private void AutoTurnon_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Halo");
            regis.AutoStartOn();
            MessageBox.Show("Udało się!");
        }
        private void Information(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Udało się!");
        }

        private void TurningOff_Click(object sender, RoutedEventArgs e)
        {
            regis.AutoStartOff();
            MessageBox.Show("Udało się!");
        }
        private void HideButton_Click(object sender, RoutedEventArgs e)
        {
            notification.CustomNotifyAlert("Application has been hidden!", "Use Alt + Ctrl + G to show application again");
            this.Hide();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void redGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        private void CheckOff(object sender, EventArgs e)
        {
            if (Auto_filler.MainWindow.mainwindow.ScreenshotCheck.IsChecked == true)
            {
                Auto_filler.MainWindow.mainwindow.ScreenshotCheck.IsChecked = false;
            }
            if (Auto_filler.MainWindow.mainwindow.LinkCheck.IsChecked == true)
            {
                Auto_filler.MainWindow.mainwindow.LinkCheck.IsChecked = false;
            }
            if (Auto_filler.MainWindow.mainwindow.ClipboardCheck.IsChecked == true)
            {
                Auto_filler.MainWindow.mainwindow.ClipboardCheck.IsChecked = false;
            }
            if (Auto_filler.MainWindow.mainwindow.SnippCheck.IsChecked == true)
            {
                Auto_filler.MainWindow.mainwindow.SnippCheck.IsChecked = false;
            }
        }
        private void CheckOn(object sender, EventArgs e)
        {


            if (Properties.Settings.Default.CheckOnOff)
            {
                Properties.Settings.Default.CheckOnOff = false;
                Properties.Settings.Default.Save();
                CheckinOnOff.Content = "CheckOn";


                Auto_filler.MainWindow.mainwindow.ScreenshotCheck.IsChecked = false;
                Auto_filler.MainWindow.mainwindow.LinkCheck.IsChecked = false;
                Auto_filler.MainWindow.mainwindow.ClipboardCheck.IsChecked = false;
                Auto_filler.MainWindow.mainwindow.SnippCheck.IsChecked = false;

            }
            else
            {
                Properties.Settings.Default.CheckOnOff = true;
                Properties.Settings.Default.Save();
                CheckinOnOff.Content = "CheckOff";



                Auto_filler.MainWindow.mainwindow.ScreenshotCheck.IsChecked = true;
                Auto_filler.MainWindow.mainwindow.LinkCheck.IsChecked = true;
                Auto_filler.MainWindow.mainwindow.ClipboardCheck.IsChecked = true;
                Auto_filler.MainWindow.mainwindow.SnippCheck.IsChecked = true;

            }


        }


    }
}
