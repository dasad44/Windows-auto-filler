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
using System.Windows.Shapes;

namespace Auto_filler
{
    /// <summary>
    /// Logika interakcji dla klasy MarkedScreenWindow.xaml
    /// </summary>
    public partial class MarkedScreenWindow : Window
    {
        public MarkedScreenWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            this.Width = SnippingToolWindow.endx - SnippingToolWindow.startx;
            this.Height = SnippingToolWindow.endy - SnippingToolWindow.starty;
            Console.WriteLine("X:" + this.Width + " Y: " + this.Height);
        }
    }
}
