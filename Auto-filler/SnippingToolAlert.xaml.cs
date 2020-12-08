﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Auto_filler
{
    /// <summary>
    /// Interaction logic for SnippingToolAlert.xaml
    /// </summary>
    public partial class SnippingToolAlert : Window
    {
        public SnippingToolAlert()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //setting Alert position
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            this.Top = desktopWorkingArea.Bottom - this.Height - 15;
            //setting start animation points    1940    1605 / 2240    1905
            int virtualScreenWidth = Convert.ToInt32(System.Windows.SystemParameters.VirtualScreenLeft);
            this.showinganimation.From = this.Left = (System.Windows.SystemParameters.VirtualScreenWidth - NumberOperation.ToPositive(virtualScreenWidth)) + 20;
            this.showinganimation.To = this.Left = System.Windows.SystemParameters.VirtualScreenWidth - NumberOperation.ToPositive(virtualScreenWidth) - 315;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
