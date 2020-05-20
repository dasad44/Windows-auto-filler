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
        public static bool ScreenShot;
        public static String path;
        public static System.Windows.Point pos;
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
        }
        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            pos = e.GetPosition(this);
        }
        private void ScreenshotCheck_Checked(object sender, RoutedEventArgs e)
        {
            ScreenShot = true;
            SaverDirectory.Visibility = Visibility.Visible;
            SaverDirectoryButton.Visibility = Visibility.Visible;
        }
        private void ScreenshotCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            ScreenShot = false;
            SaverDirectory.Visibility = Visibility.Hidden;
            SaverDirectoryButton.Visibility = Visibility.Hidden;
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

        RegistryKey reg;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            reg.SetValue("Auto filler", System.IO.Path.GetFullPath("Auto-filler.exe"));
            MessageBox.Show("Udało się!");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            reg.DeleteValue("Auto filler");
            MessageBox.Show("Udało się!");
        }
    }
}