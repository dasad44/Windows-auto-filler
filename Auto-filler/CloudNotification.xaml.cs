using System;
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
        public static CloudNotification cloudnotification;
        public CloudNotification()
        {
            InitializeComponent();
            cloudnotification = this;
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            Point point = Position.GetMousePosition();
            this.Top = point.Y - 205;
            this.Left = point.X + 5;
        }
        public void SetPosition()
        {
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            Point point = Position.GetMousePosition();
            this.Top = point.Y - 205;
            this.Left = point.X + 5;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            showClipboards();
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {

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
    }
}
