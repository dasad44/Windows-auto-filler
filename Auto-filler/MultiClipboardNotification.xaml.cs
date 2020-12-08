using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for MultiClipboardNotification.xaml
    /// </summary>
    public partial class MultiClipboardNotification : Window
    {
        public MultiClipboardNotification()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //setting Alert position
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            this.Top = desktopWorkingArea.Bottom - this.Height - 15;
            //setting start animation points
            int virtualScreenWidth = Convert.ToInt32(System.Windows.SystemParameters.VirtualScreenLeft);
            this.showinganimation.From = this.Left = (System.Windows.SystemParameters.VirtualScreenWidth - NumberOperation.ToPositive(virtualScreenWidth)) + 20;
            this.showinganimation.To = this.Left = System.Windows.SystemParameters.VirtualScreenWidth - NumberOperation.ToPositive(virtualScreenWidth) - 315;
            showClipboards();
        }

        private void showClipboards()
        {
            try
            {
                if (ClipboardValueContainer.clipboard_1.ContainsText())
                {
                    this.clipboardtext1.Text = ClipboardValueContainer.clipboard_1.GetText();
                }
                else
                {
                    this.clipboardimage1.Source = ClipboardValueContainer.clipboard_1.GetImage();
                }

                if (ClipboardValueContainer.clipboard_2.ContainsText())
                {
                    this.clipboardtext2.Text = ClipboardValueContainer.clipboard_2.GetText();
                }
                else
                {
                    this.clipboardimage2.Source = ClipboardValueContainer.clipboard_2.GetImage();
                }

                if (ClipboardValueContainer.clipboard_3.ContainsText())
                {
                    this.clipboardtext3.Text = ClipboardValueContainer.clipboard_3.GetText();
                }
                else
                {
                    this.clipboardimage3.Source = ClipboardValueContainer.clipboard_3.GetImage();
                }
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e.Message + " /error occurs when clipboard 2 and 3 don't have value yet");
            }
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
