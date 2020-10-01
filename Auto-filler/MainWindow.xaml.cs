﻿using System;
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

namespace Auto_filler
{
    public partial class MainWindow : Window
    {
        private KeyboardListener _listener;
        private ScreenshotAutoDelete _autoDelete;
        public static bool ScreenShot;
        public static bool leftButton;
        public static String path;
        public static System.Windows.Point pos;
        RegistryOnOff regis = new RegistryOnOff();
        public MainWindow()
        {
            InitializeComponent();
        }

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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _listener = new KeyboardListener(this);
            _listener.OnKeyPressed += _listener_OnKeyPressed;
            _listener.HookKeyboard();
            _autoDelete = new ScreenshotAutoDelete();
            _autoDelete.AutoDelete();
            SaverDirectory.Text = Properties.Settings.Default.ScreenPath;
            AllList();
        }
        
        private void Timer_(object sender, MouseEventArgs e)
        {
            pos = e.GetPosition(this);

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                leftButton = true;
                MessageBox.Show("dasda");
            }
            else if (e.LeftButton == MouseButtonState.Released)
            {
                leftButton = false;
                MessageBox.Show("hoy");
            }
        }
        public void AllList()
        {
            SaverListView.Items.Clear();
            DirectoryInfo dinfo = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
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
            SaverListView.Items.Clear();
            DirectoryInfo dinfo = new DirectoryInfo(@Properties.Settings.Default.ScreenPath);
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
            ScreenShot = true;
            SaverDirectory.Visibility = Visibility.Visible;
            SaverDirectoryButton.Visibility = Visibility.Visible;
            SaverListView.Visibility = Visibility.Visible;
            ListRefresh.Visibility = Visibility.Visible;
        }
        private void ScreenshotCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            ScreenShot = false;
            SaverDirectory.Visibility = Visibility.Hidden;
            SaverDirectoryButton.Visibility = Visibility.Hidden;
            SaverListView.Visibility = Visibility.Hidden;
            ListRefresh.Visibility = Visibility.Hidden;
        }
        private void SaverDirectoryButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            fbd.RootFolder = Environment.SpecialFolder.Desktop;
            fbd.Description = "Wybierz folder";
            fbd.ShowNewFolderButton = false;
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SaverDirectory.Text = fbd.SelectedPath;
                path = SaverDirectory.Text;
                Properties.Settings.Default.ScreenPath = path;
                Properties.Settings.Default.Save();
            }
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
    }
}