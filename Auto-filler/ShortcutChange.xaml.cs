using System;
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
    /// Logika interakcji dla klasy Window1.xaml
    /// </summary>
    public partial class ShortcutChange : Window
    {
        string t;
        public ShortcutChange()
        {
            InitializeComponent();
            t = Properties.Settings.Default.SnippKey;
        }
        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (t != "")
                t = t.Insert(t.Length, "+");
            t = t + e.Key.ToString();
            Shortcut.Text = t;          
        }


        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            Shortcut.Text = "";
            t = "";
        }
        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (t.Length != 0)
            {
                Properties.Settings.Default.SnippKey = t;
                Properties.Settings.Default.Save();
                this.Hide();
            }
            else
            {
                t = Properties.Settings.Default.SnippKey;
                MessageBox.Show("Enter hotkey");
            }

        }


        private void Window_Activated(object sender, EventArgs e)
        {
            Shortcut.Text = Properties.Settings.Default.SnippKey;
        }
    }
}
