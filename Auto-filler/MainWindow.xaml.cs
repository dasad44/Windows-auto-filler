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
using Microsoft.Win32;
using System.IO;
using System.Runtime.InteropServices;
using static Auto_filler.MouseHook;
using System.Text.RegularExpressions;
using Brushes = System.Windows.Media.Brushes;
using System.Diagnostics;
using System.Threading;
using Image = System.Windows.Controls.Image;

namespace Auto_filler
{

    public partial class MainWindow : Window 
    {
        SettingsWindow secondform;
        public static MainWindow mainwindow;
        private KeyboardListener _listener;
        private ScreenshotAutoDelete _autoDelete;
        Notification notification = new Notification();
        public static bool ScreenShot;
        public static bool leftButton;
        public static String path;
        public static System.Windows.Point posStart;
        public static System.Windows.Point posEnd;
        RegistryOnOff regis = new RegistryOnOff();
        public static bool linkeditor = true;
        public static bool linkeditor2 = true;
        public static bool linkeditor3 = true;
        public static bool linkeditor4 = true;
        public static bool linkeditor5 = true;
        public static string linkhot;
        public static string link;
        public static string link2;
        public static string link3;
        public static string link4;
        public static string link5;





        public MainWindow()
        {
            
            InitializeComponent();
            mainwindow = this;


            
        }
       
        //private void Event(object sender, EventArgs e) => mh.mouse();

        private void HideButton_Click(object sender, RoutedEventArgs e)
        {
            //notification.CustomNotifyAlert("Application has been hidden!", "Use Alt + Ctrl + G to show application again");
            //this.Hide();
        }

        void _listener_OnKeyPressed(object sender, KeyPressedArgs e)
        {
            // if (Keyboard.IsKeyDown(Key.G)
            // && Keyboard.IsKeyDown(Key.LeftCtrl)
            // && Keyboard.IsKeyDown(Key.LeftAlt)
            //  || Keyboard.IsKeyDown(Key.G)
            // && Keyboard.IsKeyDown(Key.RightCtrl)
            //  && Keyboard.IsKeyDown(Key.RightAlt))
            //     this.Show();
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void SetPropertyButtonVal()
        {
            if (Properties.Settings.Default.ClipboardNotification)
            {
                notificationbutton.Content = "Notification Off";
                notificationbutton.Style = Resources["FeatureOnButton"] as Style;
            }
            else
            {
                notificationbutton.Content = "Notification On";
                notificationbutton.Style = Resources["DefaultButton"] as Style;
                
            }

            if (Properties.Settings.Default.ClipboardCloudNotification)
            {
                cloudbutton.Content = "Cloud Notification Off";
                cloudbutton.Style = Resources["FeatureOnButton"] as Style;
            }
            else
            {
                cloudbutton.Content = "Cloud Notification On";
                cloudbutton.Style = Resources["DefaultButton"] as Style;
            }

            if (Properties.Settings.Default.ScreenShowCheck)
            {
                ScreenshotShow.Content = "Photoviewer Off";
                ScreenshotShow.Style = Resources["FeatureOnButton"] as Style;
            }
            else
            {
                ScreenshotShow.Content = "Photoviewer On";
                ScreenshotShow.Style = Resources["DefaultButton"] as Style;
            }
            if (Properties.Settings.Default.SnippSaveCheck)
            {
                SnippSaveCheck.Content = "Save Snipp Off";
                SnippSaveCheck.Style = Resources["FeatureOnButton"] as Style;

            }
            else
            {
                SnippSaveCheck.Content = "Save Snipp On";
                SnippSaveCheck.Style = Resources["DefaultButton"] as Style;
            }
        }

        Visibility visibility = null;


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            notification.CustomNotifyAlert("Windows Auto-filler hidden!", "Click Alt + Ctrl + G to show application again");
            this.Hide();
            //creating file in appdata/roaming for any cache files like tmp images or text
            string appdatapath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Auto_filler");
            if (!Directory.Exists(appdatapath))
            {
                DirectoryInfo di = Directory.CreateDirectory(appdatapath);
            }
            SetPropertyButtonVal();
            _listener = new KeyboardListener(this);
            _listener.OnKeyPressed += _listener_OnKeyPressed;
            _listener.HookKeyboard();
            visibility = new Visibility(); //Now we can safely create an instantiation of our UI class.
            DayLimit.Text = Properties.Settings.Default.DayLimit.ToString();
            if (@Properties.Settings.Default.ScreenPath == "")
            {
                SaverDirectory.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                Properties.Settings.Default.ScreenPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                Properties.Settings.Default.Save();
            }

            else
                SaverDirectory.Text = Properties.Settings.Default.ScreenPath;

            if (Properties.Settings.Default.ScreenCheck == true)
            {
                ScreenshotCheck.IsChecked = true;
            }
            else
                ScreenshotCheck.IsChecked = false;
            if (Properties.Settings.Default.LinkCheck == true)
            {
                LinkCheck.IsChecked = true;
            }
            else
                LinkCheck.IsChecked = false;
            if (Properties.Settings.Default.ClipboardCheck == true)
            {
                ClipboardCheck.IsChecked = true;
            }
            else
                ClipboardCheck.IsChecked = false;

            if (Properties.Settings.Default.SnippCheck == true)
            {
                SnippCheck.IsChecked = true;
            }
            else
                SnippCheck.IsChecked = false;
            if (Properties.Settings.Default.AutoDeleteCheck == true)
            {
                AutoDelete.IsChecked = true;
            }
            else
                AutoDelete.IsChecked = false;

            _autoDelete = new ScreenshotAutoDelete();
            _autoDelete.AutoDelete();
            AllList();

             ValueHolder.Text = Properties.Settings.Default.FirstLink;
            ValueHolder2.Text = Properties.Settings.Default.SecondLink;
            ValueHolder3.Text = Properties.Settings.Default.ThirdLink;
            ValueHolder4.Text = Properties.Settings.Default.FourthLink;
            ValueHolder5.Text = Properties.Settings.Default.FifthLink;


        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void redGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }
        private void Timer_(object sender, MouseEventArgs e)
        {
            posStart = e.GetPosition(this);

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                leftButton = true;
            }
            else if (e.LeftButton == MouseButtonState.Released)
            {
                leftButton = false;
            }
        }
        public void AllList()
        {
            DirectoryInfo dinfo;
            SaverListView.Items.Clear();
            dinfo = new DirectoryInfo(@Properties.Settings.Default.ScreenPath);
            FileInfo[] Files = dinfo.GetFiles("AutoFiller*.jpg");
            foreach (FileInfo file in Files)
            {
                SaverListView.Items.Add(file.Name);
            }
            string path = Properties.Settings.Default.ScreenPath + "\\AutoFiller-Important";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            dinfo = new DirectoryInfo(@path);
            Files = dinfo.GetFiles("AutoFiller*.jpg");
            foreach (FileInfo file in Files)
            {
                SaverListView.Items.Add(file.Name);
            }
        }
        public void ImportantList()
        {
            SaverListView.Items.Clear();
            string path = Properties.Settings.Default.ScreenPath + "\\AutoFiller-Important";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            DirectoryInfo dinfo = new DirectoryInfo(path);
            FileInfo[] Files = dinfo.GetFiles("AutoFiller*.jpg");
            foreach (FileInfo file in Files)
            {
                SaverListView.Items.Add(file.Name);
            }
        }
        public void TempList()
        {
            DirectoryInfo dinfo;
            SaverListView.Items.Clear();
            dinfo = new DirectoryInfo(@Properties.Settings.Default.ScreenPath);
            FileInfo[] Files = dinfo.GetFiles("AutoFiller*.jpg");
            foreach (FileInfo file in Files)
            {
                SaverListView.Items.Add(file.Name);
            }
        }
        private void CancelDeleteList_Click(object sender, RoutedEventArgs e)
        {
            DirectoryInfo dinfo = new DirectoryInfo(@Properties.Settings.Default.ScreenPath);
            FileInfo[] Files = dinfo.GetFiles("AutoFiller*.jpg");
            string path = Properties.Settings.Default.ScreenPath + "\\AutoFiller-Important";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            for (int i = 0; i < SaverListView.SelectedItems.Count; i++)
            {
                var list = SaverListView.SelectedItems;
                string sourceFile = System.IO.Path.Combine(@Properties.Settings.Default.ScreenPath, list[i].ToString());
                string destFile = System.IO.Path.Combine(path, list[i].ToString());
                try
                {
                    File.Move(sourceFile, destFile);
                }
                catch
                {
                }
            }
            AllList();
        }

        private void AllScreenList_Click(object sender, RoutedEventArgs e)
        {
            AllList();
        }
        private void ImportantScreenList_Click(object sender, RoutedEventArgs e)
        {
            ImportantList();
        }
        private void TempScreenList_Click(object sender, RoutedEventArgs e)
        {
            TempList();
        }
        private void ScreenshotCheck_Checked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.ScreenCheck = true;
            Properties.Settings.Default.Save();
            visibility.ScreenShotVis();
            var bc = new BrushConverter();
            ScreenShotItem.Foreground = (System.Windows.Media.Brush)bc.ConvertFrom("#b20837");
        }
        private void ScreenshotCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.ScreenCheck = false;
            Properties.Settings.Default.Save();
            visibility.ScreenShotInVis();
            var bc = new BrushConverter();
            ScreenShotItem.Foreground = (System.Windows.Media.Brush)bc.ConvertFrom("#e7e8ea");
        }
        private void ScreenshotShow_Click(object sender, RoutedEventArgs e)
        {
            if(Properties.Settings.Default.ScreenShowCheck)
            {
                Properties.Settings.Default.ScreenShowCheck = false;
                Properties.Settings.Default.Save();
                ScreenshotShow.Content = "Photoviewer On";
                ScreenshotShow.Style = Resources["DefaultButton"] as Style;
            }
            else
            {
                Properties.Settings.Default.ScreenShowCheck = true;
                Properties.Settings.Default.Save();
                ScreenshotShow.Content = "Photoviewer Off";
                ScreenshotShow.Style = Resources["FeatureOnButton"] as Style;
            }

        }
        private void SnippSaveCheck_Click(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.SnippSaveCheck)
            {
                Properties.Settings.Default.SnippSaveCheck = false;
                Properties.Settings.Default.Save();
                SnippSaveCheck.Content = "Save Snipp On";
                SnippSaveCheck.Style = Resources["DefaultButton"] as Style;
            }
            else
            {
                Properties.Settings.Default.SnippSaveCheck = true;
                Properties.Settings.Default.Save();
                SnippSaveCheck.Content = "Save Snipp Off";
                SnippSaveCheck.Style = Resources["FeatureOnButton"] as Style;
            }
        }
        private void ScreenshotShow_Checked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.ScreenShowCheck = true;
            Properties.Settings.Default.Save();
            //visibility.ScreenShotInVis();
        }
        private void ScreenshotShow_Unchecked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.ScreenShowCheck = false;
            Properties.Settings.Default.Save();
            //visibility.ScreenShotInVis();
        }
        private void SnippCheck_Checked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.SnippCheck = true;
            Properties.Settings.Default.Save();
            var bc = new BrushConverter();
            SnippingItem.Foreground = (System.Windows.Media.Brush)bc.ConvertFrom("#b20837");
        }
        private void SnippCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.SnippCheck = false;
            Properties.Settings.Default.Save();
            var bc = new BrushConverter();
            SnippingItem.Foreground = (System.Windows.Media.Brush)bc.ConvertFrom("#e7e8ea");
        }
        private void LinkCheck_Checked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.LinkCheck = true;
            Properties.Settings.Default.Save();
            var bc = new BrushConverter();
            LinkItem.Foreground = (System.Windows.Media.Brush)bc.ConvertFrom("#b20837");
        }
        private void LinkCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.LinkCheck = false;
            Properties.Settings.Default.Save();
            var bc = new BrushConverter();
            LinkItem.Foreground = (System.Windows.Media.Brush)bc.ConvertFrom("#e7e8ea");
        }
        private void ClipboardCheck_Checked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.ClipboardCheck = true;
            Properties.Settings.Default.Save();
            var bc = new BrushConverter();
            ClipboardItem.Foreground = (System.Windows.Media.Brush)bc.ConvertFrom("#b20837");
        }
        private void ClipboardCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.ClipboardCheck = false;
            Properties.Settings.Default.Save();
            var bc = new BrushConverter();
            ClipboardItem.Foreground = (System.Windows.Media.Brush)bc.ConvertFrom("#e7e8ea");
        }
        private void SaverDirectoryButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            fbd.Description = "Wybierz folder";
            fbd.ShowNewFolderButton = false;
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SaverDirectory.Text = fbd.SelectedPath;
                path = SaverDirectory.Text;
                Properties.Settings.Default.ScreenPath = path;
                Properties.Settings.Default.Save();
            }
            AllList();
        }


        public void ValueSaver_Click(object sender, RoutedEventArgs e)
        {
            if (ValueSaver.IsVisible)
            {
                link = ValueHolder.Text;
                _listener.CatchLink(link);
                Properties.Settings.Default.FirstLink = link;
                Properties.Settings.Default.Save();
                linkeditor5 = true;
            }
            

        }
        public void ValueHolder_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        public void ValueSaver_Click2(object sender, RoutedEventArgs e)
        {
            if (ValueSaver2.IsVisible)
            {
                 link2 = ValueHolder2.Text;
                _listener.CatchLink2(link2);
                Properties.Settings.Default.SecondLink = link2;
                Properties.Settings.Default.Save();

                linkeditor4 = true;
            }
                
        }
        private void ValueHolder_TextChanged2(object sender, TextChangedEventArgs e)
        {

        }
        public void ValueSaver_Click3(object sender, RoutedEventArgs e)
        {
            if (ValueSaver3.IsVisible)
            {
                 link3 = ValueHolder3.Text;
                _listener.CatchLink3(link3);
                Properties.Settings.Default.ThirdLink = link3;
                Properties.Settings.Default.Save();

                linkeditor3 = true;
            }
        }
        private void ValueHolder_TextChanged3(object sender, TextChangedEventArgs e)
        {

        }
        public void ValueSaver_Click4(object sender, RoutedEventArgs e)
        {
            if (ValueSaver4.IsVisible)
            {
                 link4 = ValueHolder4.Text;
                _listener.CatchLink4(link4);
                Properties.Settings.Default.FourthLink = link4;
                Properties.Settings.Default.Save();

                linkeditor2 = true;
            }
        }
        private void ValueHolder_TextChanged4(object sender, TextChangedEventArgs e)
        {

        }
        private void donothing(object sender, RoutedEventArgs e)
        {

        }

        public void ValueSaver_Click5(object sender, RoutedEventArgs e)
        {    
            if (ValueSaver5.IsVisible)
            {
                 link5 = ValueHolder5.Text;
                _listener.CatchLink5(link5);
                Properties.Settings.Default.FifthLink = link5;
                Properties.Settings.Default.Save();
                linkeditor = true;
            }        
        }

        private void ValueHolder_TextChanged5(object sender, TextChangedEventArgs e)
        {

        }
        private void RemoveField(object sender, RoutedEventArgs e)
        {
            
            if (ValueSaver5.IsVisible)
            {
                ValueSaver5.Visibility = System.Windows.Visibility.Hidden;
                linkeditor = false;
                ValueHolder5.Visibility = System.Windows.Visibility.Hidden;
       
                hotkeychange5.Visibility = System.Windows.Visibility.Hidden;
            }
            else if (ValueSaver5.IsVisible == false & ValueSaver4.IsVisible)
            {
                ValueSaver4.Visibility = System.Windows.Visibility.Hidden;
                linkeditor2 = false;
                ValueHolder4.Visibility = System.Windows.Visibility.Hidden;
           
                hotkeychange4.Visibility = System.Windows.Visibility.Hidden;
            }
            else if (ValueSaver5.IsVisible == false & ValueSaver4.IsVisible == false & ValueSaver3.IsVisible)
            {
                ValueSaver3.Visibility = System.Windows.Visibility.Hidden;
                linkeditor3 = false;
                ValueHolder3.Visibility = System.Windows.Visibility.Hidden;
               
                hotkeychange3.Visibility = System.Windows.Visibility.Hidden;
            }

            else if (ValueSaver5.IsVisible == false & ValueSaver4.IsVisible == false & ValueSaver3.IsVisible == false & ValueSaver2.IsVisible)
            {
                ValueSaver2.Visibility = System.Windows.Visibility.Hidden;
                linkeditor4 = false;
                ValueHolder2.Visibility = System.Windows.Visibility.Hidden;
             
                hotkeychange2.Visibility = System.Windows.Visibility.Hidden;
            }

            else if (ValueSaver5.IsVisible == false & ValueSaver4.IsVisible == false & ValueSaver3.IsVisible == false & ValueSaver2.IsVisible == false && ValueSaver.IsVisible)
            {
                ValueSaver.Visibility = System.Windows.Visibility.Hidden;
                linkeditor5 = false;
                ValueHolder.Visibility = System.Windows.Visibility.Hidden;
                ValueHolder.Text = string.Empty;
                hotkeychange.Visibility = System.Windows.Visibility.Hidden;

            }


        }
        private void AddField(object sender, RoutedEventArgs e)
        {
           
            if(ValueSaver5.IsVisible == false & ValueSaver4.IsVisible == false & ValueSaver3.IsVisible == false & ValueSaver2.IsVisible == false && ValueSaver.IsVisible)
            {
                ValueSaver2.Visibility = System.Windows.Visibility.Visible;
                linkeditor4 = true;                
                ValueHolder2.Visibility = System.Windows.Visibility.Visible;
                hotkeychange2.Visibility = System.Windows.Visibility.Visible;
            }
            else if (ValueSaver5.IsVisible == false & ValueSaver4.IsVisible == false & ValueSaver3.IsVisible == false & ValueSaver2.IsVisible && ValueSaver.IsVisible)
            {
                ValueSaver3.Visibility = System.Windows.Visibility.Visible;
                linkeditor3 = true;
                ValueHolder3.Visibility = System.Windows.Visibility.Visible;
                hotkeychange3.Visibility = System.Windows.Visibility.Visible;
            }
            else if (ValueSaver5.IsVisible == false & ValueSaver4.IsVisible == false & ValueSaver3.IsVisible & ValueSaver2.IsVisible && ValueSaver.IsVisible)
            {
                ValueSaver4.Visibility = System.Windows.Visibility.Visible;
                linkeditor2 = true;
                ValueHolder4.Visibility = System.Windows.Visibility.Visible;
                hotkeychange4.Visibility = System.Windows.Visibility.Visible;
            }
            else if (ValueSaver5.IsVisible == false & ValueSaver4.IsVisible & ValueSaver3.IsVisible & ValueSaver2.IsVisible && ValueSaver.IsVisible)
            {
                ValueSaver5.Visibility = System.Windows.Visibility.Visible;
                linkeditor = true;
                ValueHolder5.Visibility = System.Windows.Visibility.Visible;
                hotkeychange5.Visibility = System.Windows.Visibility.Visible;
            }
            else if (ValueSaver5.IsVisible == false & ValueSaver4.IsVisible == false & ValueSaver3.IsVisible == false & ValueSaver2.IsVisible == false && ValueSaver.IsVisible == false)
            {
                ValueSaver.Visibility = System.Windows.Visibility.Visible;
                linkeditor5 = true;
                ValueHolder.Visibility = System.Windows.Visibility.Visible;
                hotkeychange.Visibility = System.Windows.Visibility.Visible;
            }
        }   
        private void Information(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Udało się!");
        }


        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            
           
            _listener.SnippWindowClose();

            
                if (App.nIcon != null)
                {
                    App.nIcon.Visible = false;
                App.nIcon.Icon = null;
                App.nIcon.Dispose();
                App.nIcon = null;
                }


            
                
                App.Current.Shutdown();
            
                
           


            

         }
        

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
            notification.CustomNotifyAlert("Windows Auto-filler hidden!", "Click Alt + Ctrl + G to show application again");
        }

        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Minimized) this.Hide();
            base.OnStateChanged(e);
        }

        public void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            SettingsWindow settings = new SettingsWindow();
            settings.Show();
        }

        private void DayLimitclick_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.DayLimit = int.Parse(DayLimit.Text);
            Properties.Settings.Default.Save();
        }

        private void notificationbutton_Click(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.ClipboardNotification)
            {
                Properties.Settings.Default.ClipboardNotification = false;
                Properties.Settings.Default.Save();
                notificationbutton.Content = "Notification On";
                notificationbutton.Style = Resources["DefaultButton"] as Style;
            }

            else
            {
                Properties.Settings.Default.ClipboardNotification = true;
                Properties.Settings.Default.Save();
                notificationbutton.Content = "Notification Off";
                notificationbutton.Style = Resources["FeatureOnButton"] as Style;
            }
        }

        private void AutoDelete_Unchecked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.AutoDeleteCheck = false;
            Properties.Settings.Default.Save();
        }

        private void AutoDelete_Checked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.AutoDeleteCheck = true;
            Properties.Settings.Default.Save();
        }

        private void cloudbutton_Click(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.ClipboardCloudNotification)
            {
                Properties.Settings.Default.ClipboardCloudNotification = false;
                Properties.Settings.Default.Save();
                cloudbutton.Content = "Cloud Notification On";
                cloudbutton.Style = Resources["DefaultButton"] as Style;
            }
            else
            {
                Properties.Settings.Default.ClipboardCloudNotification = true;
                Properties.Settings.Default.Save();
                cloudbutton.Content = "Cloud Notification Off";
                cloudbutton.Style = Resources["FeatureOnButton"] as Style;
            }
        }
        private void Clearclick(object sender, EventArgs e)
        {
            info1.Text = "";
            info2.Text = "";
            info3.Text = "";
            info4.Text = "";
            info5.Text = "";
            info6.Text = "";
            info7.Text = "";
            info8.Text = "";
            info1.IsEnabled = false;
            info2.IsEnabled = false;
            info3.IsEnabled = false;
            info4.IsEnabled = false;
            info5.IsEnabled = false;
            info6.IsEnabled = false;
            info7.IsEnabled = false;
            info8.IsEnabled = false;
        }
        private void SystemInformation(object sender, EventArgs e)
        {
            info1.IsEnabled = false;
            info2.IsEnabled = false;
            info3.IsEnabled = false;
            info4.IsEnabled = false;
            info5.IsEnabled = false;
            info6.IsEnabled = false;
            info7.IsEnabled = false;
            info8.IsEnabled = false;
            string pcinfo = Environment.MachineName; //Nazwa naszego PC
            info1.Text = pcinfo;

            string userinfo = Environment.UserName; //Nazwa naszego aktualnego użytkownika
            info2.Text = Convert.ToString(userinfo);

            int proccount = Environment.ProcessorCount; //Liczy ilość wątków procesora
            info6.Text = Convert.ToString(proccount);

            bool systembit = Environment.Is64BitOperatingSystem; //Czy system jest 64 bitowy
            if (systembit == true)
            {
                info7.Text = "64bit";
            }
            else
            {
                info7.Text = "32bit";
            }

            string procbit = System.Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE", EnvironmentVariableTarget.Machine);
            if (procbit.IndexOf("64") > 0)
            {
                info8.Text = "64bit";
            }
            else
            {
                info8.Text = "32bit";
            }

            string osver = Environment.OSVersion.Version.ToString(); //Wersja systemu
            info3.Text = osver;

            string vers = (Environment.OSVersion.ToString()); //Identyfikator platformy i numer wersji
            info4.Text = vers;

            string platf = Environment.OSVersion.Platform.ToString(); //Platofrma systemu
            info5.Text = platf;

        }

        private void Popups_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Image_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            string snippingtoolInstruction = "This feature allows us to quickly capture any part of a screen we need to send, edit or simply save just as any other screenshot. By pressing Ctrl + q our app enters snipping mode when we use mouse to cut part that we want or press Escape to quit.";
            MessageBox.Show(snippingtoolInstruction, "SnippingTool Instruction");
        }

        private void Image_MouseLeftButtonDown_3(object sender, MouseButtonEventArgs e)
        {
            string screenshotInstruction = "This feature helps in creation and management of a local screenshot database. Pressing printscreen automatically saves a picture on our PC in a selected directory. All screenshots are divided into 2 groups - Temporary and Important. Temporary files are being deleted after amount of days user set(3 by default). By selecting screenshot on a list and pressing cancel delete button we can change its group to Important making it an exception from auto delete feature.";
            MessageBox.Show(screenshotInstruction, "ScreenShot Instruction");
        }

        private void Image_MouseLeftButtonDown_4(object sender, MouseButtonEventArgs e)
        {
            string multiclipboardInstruction = "We have additional 2 clipboards(Ctrl + C/Ctrl + V) to use. Ctrl + C is adding element to clipboard queue. After clicking that first clipboard comes to second place and second clipboard comes to third. To get access to second or third clipboard click " + "'Ctrl + CC'" + " for second and " + "'Ctrl + CCC'" + " for third clipboard. Ctrl + V will show current element.";
            MessageBox.Show(multiclipboardInstruction, "MultiClipboard Instruction");
        }

        private void Image_MouseLeftButtonDown_5(object sender, MouseButtonEventArgs e)
        {
            string pagehotkeyInstruction = "We have fields with text boxes to input links to internet pages for example www.youtube.com . After click on save, value is assigned to our hotkey. We have hotkeys from ctrl + 1 to 5. It goes from the top to buttom. We have buttons delete and add. After pressing them we can delete or add field for internet link.";
            MessageBox.Show(pagehotkeyInstruction, "PageHotKey Instruction");
        }

        private void ChangeHotKey_Click1(object sender, RoutedEventArgs e)
        {
            linkhot = "1";
            ShortcutLink shortcutChange = new ShortcutLink();
            shortcutChange.Show();
        }
        private void ChangeHotKey_Click2(object sender, RoutedEventArgs e)
        {
            linkhot = "2";
            ShortcutLink shortcutChange = new ShortcutLink();
            shortcutChange.Show();
        }
        private void ChangeHotKey_Click3(object sender, RoutedEventArgs e)
        {
            linkhot = "3";
            ShortcutLink shortcutChange = new ShortcutLink();
            shortcutChange.Show();
        }
        private void ChangeHotKey_Click4(object sender, RoutedEventArgs e)
        {
            linkhot = "4";
            ShortcutLink shortcutChange = new ShortcutLink();
            shortcutChange.Show();
        }
        private void ChangeHotKey_Click5(object sender, RoutedEventArgs e)
        {
            linkhot = "5";
            ShortcutLink shortcutChange = new ShortcutLink();
            shortcutChange.Show();
        }
        private void ChangeHotKey_Click(object sender, RoutedEventArgs e)
        {
            ShortcutChange shortcutChange = new ShortcutChange();
            shortcutChange.Show();
        }
    }
}