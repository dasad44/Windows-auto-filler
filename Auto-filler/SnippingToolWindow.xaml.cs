using System;
using System.Collections.Generic;
using System.Drawing;
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
using static Auto_filler.MouseHook;

namespace Auto_filler
{
    /// <summary>
    /// Interaction logic for SnippingToolWindow.xaml
    /// </summary>
    public partial class SnippingToolWindow : Window
    {
        
        public SnippingToolWindow()
        {
            InitializeComponent();
        }
       
        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Hide();
        }
        
    }
}
