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

namespace Auto_filler
{
    public partial class MainWindow : Window
    {
        private KeyboardListener _listener;
        private ScreenshotAutoDelete _autoDelete;
        public static bool ScreenShot;
        public static bool leftButton;
        public static String path;
        public static System.Windows.Point posStart;
        public static System.Windows.Point posEnd;
        RegistryOnOff regis = new RegistryOnOff();


        public MainWindow()
        {
            InitializeComponent();
        }



        //private void Event(object sender, EventArgs e) => mh.mouse();

        private void HideButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
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
        Visibility visibility = null;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _listener = new KeyboardListener(this);
            _listener.OnKeyPressed += _listener_OnKeyPressed;
            _listener.HookKeyboard();
            visibility = new Visibility(); //Now we can safely create an instantiation of our UI class.
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

            _autoDelete = new ScreenshotAutoDelete();
            _autoDelete.AutoDelete();
            AllList();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
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
                File.Move(sourceFile, destFile);
            }
        }
        private void ListRefresh_Click(object sender, RoutedEventArgs e)
        {
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
        }
        private void ScreenshotCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.ScreenCheck = false;
            Properties.Settings.Default.Save();
            visibility.ScreenShotInVis();
        }
        private void SnippCheck_Checked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.SnippCheck = true;
            Properties.Settings.Default.Save();
        }
        private void SnippCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.SnippCheck = false;
            Properties.Settings.Default.Save();
        }
        private void LinkCheck_Checked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.LinkCheck = true;
            Properties.Settings.Default.Save();
        }
        private void LinkCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.LinkCheck = false;
            Properties.Settings.Default.Save();
        }
        private void ClipboardCheck_Checked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.ClipboardCheck = true;
            Properties.Settings.Default.Save();
        }
        private void ClipboardCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.ClipboardCheck = false;
            Properties.Settings.Default.Save();
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
        

        private void ValueSaver_Click(object sender, RoutedEventArgs e)
        {
            string link = ValueHolder.Text;
            _listener.CatchLink(link);
        }

        private void ValueHolder_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void AutoTurnon_Click(object sender, RoutedEventArgs e)
        {
            regis.AutoStartOn();
            MessageBox.Show("Udało się!");
        }

        private void TurningOff_Click(object sender, RoutedEventArgs e)
        {
            regis.AutoStartOff();
            MessageBox.Show("Udało się!");
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MouseHook.Start();
            //MouseHook.MouseAction += new EventHandler(Event);
        }
       

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }



        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Minimized) this.Hide();
            base.OnStateChanged(e);
        }

        
    }
}