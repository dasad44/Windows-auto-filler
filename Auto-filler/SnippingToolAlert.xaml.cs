using System;
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
            //this.Top = 785;
            //this.Left = Screen.PrimaryScreen.Bounds.Width - this.Width - 20;
        }

    }
}
