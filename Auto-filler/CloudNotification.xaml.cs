﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
    /// Interaction logic for CloudNotification.xaml
    /// </summary>
    public partial class CloudNotification : Window
    {
        public const Int32 HTTRANSPARENT = 0x0084;
        public CloudNotification()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            Point point = Position.GetMousePosition();
            this.Top = point.Y;
            this.Left = point.X;
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            Point point = Position.GetMousePosition();
            this.Top = point.Y;
            this.Left = point.X;
        }
    }
}
