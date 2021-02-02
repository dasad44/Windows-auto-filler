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
        String Feature;
        public ShortcutChange(string feature)
        {
            InitializeComponent();
            Feature = feature;
            if (Feature == "Snipp")
                t = Properties.Settings.Default.SnippKey;
            else if(Feature == "AppShow")
                t = Properties.Settings.Default.AppShowKey;
        }
        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (t != "")
                t = t.Insert(t.Length, "+");
            if (e.SystemKey.ToString() != "None")
                t = t + e.SystemKey.ToString();
            else
                t = t + e.Key.ToString();
            Shortcut.Text = t;
        }


        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            Shortcut.Focus();
            Shortcut.Text = "";
            t = "";
        }
        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (Feature == "Snipp")
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
            if (Feature == "AppShow")
            {
                if (t.Length != 0)
                {
                    Properties.Settings.Default.AppShowKey = t;
                    Properties.Settings.Default.Save();
                    this.Hide();
                }
                else
                {
                    t = Properties.Settings.Default.AppShowKey;
                    MessageBox.Show("Enter hotkey");
                }
            }
        }


        private void Window_Activated(object sender, EventArgs e)
        {
            Shortcut.Focus();
            if(Feature == "Snipp")
            Shortcut.Text = Properties.Settings.Default.SnippKey;
            else if (Feature == "AppShow")
            Shortcut.Text = Properties.Settings.Default.AppShowKey;
        }
    }
}
