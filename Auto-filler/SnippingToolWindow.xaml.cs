using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Input;
using System.Windows;
using Microsoft.Win32;
using System.IO;
using System.Drawing;
using System.Windows.Media.Imaging;
using static Auto_filler.MouseHook;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Threading;
namespace Auto_filler
{
    /// <summary>
    /// Interaction logic for SnippingToolWindow.xaml
    /// </summary>
    public partial class SnippingToolWindow : Window
    {

        POINT startV;
        POINT endV;
        public static bool MarkStart = false;
        bool StartPosition = true;
        bool v = false;
        private static SnippingTool _snippingTool;
        int sourceX;
        int sourceY;

        public SnippingToolWindow()
        {
            InitializeComponent();
        }
       
        private async void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            StartPosition = true;
            MarkStart = false;
            Hide_window();
            await Task.Delay(50);//
            endV = MouseHook.getEndValue();
            startV = MouseHook.getStartValue();
            this.Hide();
            _snippingTool = new SnippingTool();
            _snippingTool.snippingTool(startV, endV);
        }
        private void Hide_window()
        {
            this.MarkingSquare.Visibility = System.Windows.Visibility.Hidden;
        }
        public void Marking()
        {
            endV = MouseHook.getMousePosition();
            startV = MouseHook.getStartValue();
            sourceX = Convert.ToInt32(System.Windows.SystemParameters.VirtualScreenLeft);
            sourceY = Convert.ToInt32(System.Windows.SystemParameters.VirtualScreenTop);

            if (startV.x > endV.x && startV.y < endV.y)
            {

                this.MarkingSquare.Margin = new Thickness(-sourceX + endV.x, -sourceY + startV.y, 0, 0);
                this.MarkingSquare.Width = startV.x - endV.x;
                this.MarkingSquare.Height = endV.y - startV.y;
                return;
            }
            else if(startV.x < endV.x && startV.y > endV.y)
            {
                this.MarkingSquare.Width = endV.x - startV.x;
                this.MarkingSquare.Height = startV.y - endV.y;
                this.MarkingSquare.Margin = new Thickness(-sourceX + startV.x, -sourceY + endV.y, 0, 0);
                return;
            }
            else if(startV.x > endV.x && startV.y > endV.y)
            {
                this.MarkingSquare.Margin = new Thickness(-sourceX + endV.x, -sourceY + endV.y, 0, 0);
                this.MarkingSquare.Width = startV.x - endV.x;
                this.MarkingSquare.Height = startV.y - endV.y;
                return;
            }   
            else if(startV.x == endV.x || startV.y == endV.y)
            {
                this.MarkingSquare.Width = 0;
                return;
            }
            else
            {
                this.MarkingSquare.Width = endV.x - startV.x;
                this.MarkingSquare.Height = endV.y - startV.y;
                return;
            }    
             
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {

            if (MarkStart == true)
            {
                if(StartPosition == true)
                {
                    sourceX = Convert.ToInt32(System.Windows.SystemParameters.VirtualScreenLeft);
                    sourceY = Convert.ToInt32(System.Windows.SystemParameters.VirtualScreenTop);
                    this.MarkingSquare.Visibility = System.Windows.Visibility.Visible;
                    startV = MouseHook.getStartValue();
                    this.MarkingSquare.Margin = new Thickness(-sourceX + startV.x, -sourceY + startV.y, 0, 0);
                }
                StartPosition = false;
                Marking();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
