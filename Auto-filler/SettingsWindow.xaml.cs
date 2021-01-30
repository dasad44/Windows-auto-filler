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

        public void CloseButton_Click(object sender, MouseButtonEventArgs e)
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
            MessageBox.Show("Aplikacja została stworzona przez Dawida Sokoła, Michała Stasiewicza oraz Michała Wojdałę. Jesteśmy studentami czwartego roku Technologii Informatycznych na " +
                "Nadnoteckim Instytucie UAM w Pile. Zadaniem aplikacji jest ułatwienie i usprawnienie działania różnych funkcjonalności systemów Windows. Planujemy regularne ulepszanie " +
                "i udoskonalanie aplikacji. Dziękujemy za każde pobranie i zachęcamy do poinformowania o niej znajomych. Jesteśmy otwarci na wszelakie sugestie i pomysły nowych udoskonaleń, " +
                "w celu skontaktowania się z nami prosimy wysłać maila pod adres ti.pila.amu@gmail.com.");
            
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
        private void redGrid_MouseDown2(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
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

        private void Darkclick(object sender, RoutedEventArgs e)
        {
            MainWindow.darkmod = true;
            Auto_filler.MainWindow.mainwindow.element1.Background = Brushes.Gray;
            Auto_filler.MainWindow.mainwindow.element2.Background = Brushes.Gray;
            settbackground.Background = Brushes.Gray;
            Auto_filler.MainWindow.mainwindow.SaverListView.Background = Brushes.Gray;
            


            Image darkpropertyimg = new Image();
            darkpropertyimg.Source = new BitmapImage(new Uri(@"bin\Debug\images\darksetting.PNG", UriKind.RelativeOrAbsolute));
            Auto_filler.MainWindow.mainwindow.settingimagemain.Source = darkpropertyimg.Source;

            Image darkminimize = new Image();
            darkminimize.Source = new BitmapImage(new Uri(@"bin\Debug\images\darkminimize.PNG", UriKind.RelativeOrAbsolute));
            Auto_filler.MainWindow.mainwindow.minimizedark.Source = darkminimize.Source;

            Image turnoffdark = new Image();
            turnoffdark.Source = new BitmapImage(new Uri(@"bin\Debug\images\darkturnoff.PNG", UriKind.RelativeOrAbsolute));
            Auto_filler.MainWindow.mainwindow.darkturnoff.Source = turnoffdark.Source;


            Image logoimagedark = new Image();
            logoimagedark.Source = new BitmapImage(new Uri(@"bin\Debug\images\DarkLogo.PNG", UriKind.RelativeOrAbsolute));
            Auto_filler.MainWindow.mainwindow.Logoimage.Source = logoimagedark.Source;

            AutoTurnon.Style = Resources["colorsfore"] as Style;
            TurningOff.Style = Resources["colorsfore"] as Style;
            About.Style = Resources["colorsfore"] as Style;
            CheckinOnOff.Style = Resources["colorsfore"] as Style;
            ChangeHotKey.Style = Resources["colorsfore"] as Style;

            Auto_filler.MainWindow.mainwindow.DayLimitclick.Style = Resources["colorsfore"] as Style;
            Auto_filler.MainWindow.mainwindow.SaverDirectoryButton.Style = Resources["colorsfore"] as Style;
            Auto_filler.MainWindow.mainwindow.CancelDeleteList.Style = Resources["colorsfore"] as Style;
            Auto_filler.MainWindow.mainwindow.AllScreenList.Style = Resources["colorsfore"] as Style;
            Auto_filler.MainWindow.mainwindow.ImportantScreenList.Style = Resources["colorsfore"] as Style;
            Auto_filler.MainWindow.mainwindow.TempScreenList.Style = Resources["colorsfore"] as Style;
            Auto_filler.MainWindow.mainwindow.ChangeHotKey.Style = Resources["colorsfore"] as Style;
            Auto_filler.MainWindow.mainwindow.Remover.Style = Resources["colorsfore"] as Style;
            Auto_filler.MainWindow.mainwindow.Adder.Style = Resources["FeatureOnButtonDark"] as Style;
            Auto_filler.MainWindow.mainwindow.ValueSaver.Style = Resources["colorsfore"] as Style;
            Auto_filler.MainWindow.mainwindow.ValueSaver2.Style = Resources["colorsfore"] as Style;
            Auto_filler.MainWindow.mainwindow.ValueSaver3.Style = Resources["colorsfore"] as Style;
            Auto_filler.MainWindow.mainwindow.ValueSaver4.Style = Resources["colorsfore"] as Style;
            Auto_filler.MainWindow.mainwindow.ValueSaver5.Style = Resources["colorsfore"] as Style;
            Auto_filler.MainWindow.mainwindow.hotkeychange.Style = Resources["colorsfore"] as Style;
            Auto_filler.MainWindow.mainwindow.hotkeychange2.Style = Resources["colorsfore"] as Style;
            Auto_filler.MainWindow.mainwindow.hotkeychange3.Style = Resources["colorsfore"] as Style;
            Auto_filler.MainWindow.mainwindow.hotkeychange4.Style = Resources["colorsfore"] as Style;
            Auto_filler.MainWindow.mainwindow.hotkeychange5.Style = Resources["colorsfore"] as Style;
            Auto_filler.MainWindow.mainwindow.buttons1.Style = Resources["colorsfore"] as Style;
            Auto_filler.MainWindow.mainwindow.buttons2.Style = Resources["colorsfore"] as Style;




        }

        private void Normalclick(object sender, RoutedEventArgs e)
        {
            MainWindow.darkmod = false;


            Auto_filler.MainWindow.mainwindow.element1.Background = Brushes.White;
            Auto_filler.MainWindow.mainwindow.element2.Background = Brushes.White;
            settbackground.Background = Brushes.White;
            Auto_filler.MainWindow.mainwindow.SaverListView.Background = Brushes.White;

            Image lightpropertyimg = new Image();
            lightpropertyimg.Source = new BitmapImage(new Uri(@"bin\Debug\images\settings.jpg", UriKind.RelativeOrAbsolute));
            Auto_filler.MainWindow.mainwindow.settingimagemain.Source = lightpropertyimg.Source;

            Image lightminimize = new Image();
            lightminimize.Source = new BitmapImage(new Uri(@"bin\Debug\images\minimalize.png", UriKind.RelativeOrAbsolute));
            Auto_filler.MainWindow.mainwindow.minimizedark.Source = lightminimize.Source;

            Image turnofflight = new Image();
            turnofflight.Source = new BitmapImage(new Uri(@"bin\Debug\images\turnoff.png", UriKind.RelativeOrAbsolute));
            Auto_filler.MainWindow.mainwindow.darkturnoff.Source = turnofflight.Source;

            Image logoimagelight = new Image();
            logoimagelight.Source = new BitmapImage(new Uri(@"bin\Debug\images\Logo.PNG", UriKind.RelativeOrAbsolute));
            Auto_filler.MainWindow.mainwindow.Logoimage.Source = logoimagelight.Source;


            AutoTurnon.Style = Resources["DefaultButton"] as Style;
            TurningOff.Style = Resources["DefaultButton"] as Style;
            About.Style = Resources["DefaultButton"] as Style;
            CheckinOnOff.Style = Resources["DefaultButton"] as Style;
            ChangeHotKey.Style = Resources["DefaultButton"] as Style;

            Auto_filler.MainWindow.mainwindow.DayLimitclick.Style = Resources["DefaultButton"] as Style;
            Auto_filler.MainWindow.mainwindow.SaverDirectoryButton.Style = Resources["DefaultButton"] as Style;
            Auto_filler.MainWindow.mainwindow.CancelDeleteList.Style = Resources["DefaultButton"] as Style;
            Auto_filler.MainWindow.mainwindow.AllScreenList.Style = Resources["DefaultButton"] as Style;
            Auto_filler.MainWindow.mainwindow.ImportantScreenList.Style = Resources["DefaultButton"] as Style;
            Auto_filler.MainWindow.mainwindow.TempScreenList.Style = Resources["DefaultButton"] as Style;
            Auto_filler.MainWindow.mainwindow.ChangeHotKey.Style = Resources["DefaultButton"] as Style;
            Auto_filler.MainWindow.mainwindow.Remover.Style = Resources["DefaultButton"] as Style;
            Auto_filler.MainWindow.mainwindow.Adder.Style = Resources["FeatureOnButton"] as Style;
            Auto_filler.MainWindow.mainwindow.ValueSaver.Style = Resources["DefaultButton"] as Style;
            Auto_filler.MainWindow.mainwindow.ValueSaver2.Style = Resources["DefaultButton"] as Style;
            Auto_filler.MainWindow.mainwindow.ValueSaver3.Style = Resources["DefaultButton"] as Style;
            Auto_filler.MainWindow.mainwindow.ValueSaver4.Style = Resources["DefaultButton"] as Style;
            Auto_filler.MainWindow.mainwindow.ValueSaver5.Style = Resources["DefaultButton"] as Style;
            Auto_filler.MainWindow.mainwindow.hotkeychange.Style = Resources["DefaultButton"] as Style;
            Auto_filler.MainWindow.mainwindow.hotkeychange2.Style = Resources["DefaultButton"] as Style;
            Auto_filler.MainWindow.mainwindow.hotkeychange3.Style = Resources["DefaultButton"] as Style;
            Auto_filler.MainWindow.mainwindow.hotkeychange4.Style = Resources["DefaultButton"] as Style;
            Auto_filler.MainWindow.mainwindow.hotkeychange5.Style = Resources["DefaultButton"] as Style;
            Auto_filler.MainWindow.mainwindow.buttons1.Style = Resources["DefaultButton"] as Style;
            Auto_filler.MainWindow.mainwindow.buttons2.Style = Resources["DefaultButton"] as Style;

        }

        private void ChangeHotKey_Click(object sender, RoutedEventArgs e)
        {
            ShortcutChange shortcutChange = new ShortcutChange();
            shortcutChange.Show();
        }

    }
}
