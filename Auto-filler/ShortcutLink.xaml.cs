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
    /// Interaction logic for ShortcutLink.xaml
    /// </summary>
    public partial class ShortcutLink : Window
    {
        string t;
        public ShortcutLink()
        {
            InitializeComponent();
           if (MainWindow.linkhot == "1")
            t = Properties.Settings.Default.Linkkey1;
           else if (MainWindow.linkhot == "2")
                t = Properties.Settings.Default.Linkkey2;
            else if (MainWindow.linkhot == "3")
                t = Properties.Settings.Default.Linkkey3;
            else if (MainWindow.linkhot == "4")
                t = Properties.Settings.Default.Linkkey4;
            else if (MainWindow.linkhot == "5")
                t = Properties.Settings.Default.Linkkey5;
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
            Shortcut.Focus();
            Shortcut.Text = "";
            t = "";
        }
        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (t.Length != 0)
            {
                if (MainWindow.linkhot == "1")
                    Properties.Settings.Default.Linkkey1 = t;
                else if (MainWindow.linkhot == "2")
                    Properties.Settings.Default.Linkkey2 = t;
                else if (MainWindow.linkhot == "3")
                    Properties.Settings.Default.Linkkey3 = t;
                else if (MainWindow.linkhot == "4")
                    Properties.Settings.Default.Linkkey4 = t;
                else if (MainWindow.linkhot == "5")
                    Properties.Settings.Default.Linkkey5 = t;

                Properties.Settings.Default.Save();
                this.Hide();
            }
            else
            {
                if (MainWindow.linkhot == "1")
                    t = Properties.Settings.Default.Linkkey1;
                else if (MainWindow.linkhot == "2")
                    t = Properties.Settings.Default.Linkkey2;
                else if (MainWindow.linkhot == "3")
                    t = Properties.Settings.Default.Linkkey3;
                else if (MainWindow.linkhot == "4")
                    t = Properties.Settings.Default.Linkkey4;
                else if (MainWindow.linkhot == "5")
                    t = Properties.Settings.Default.Linkkey5;
                MessageBox.Show("Enter hotkey");
            }

        }


        private void Window_Activated(object sender, EventArgs e)
        {
            Shortcut.Focus();
            if (MainWindow.linkhot == "1")
                Shortcut.Text = Properties.Settings.Default.Linkkey1;
            else if (MainWindow.linkhot == "2")
                Shortcut.Text = Properties.Settings.Default.Linkkey2;
            else if (MainWindow.linkhot == "3")
                Shortcut.Text = Properties.Settings.Default.Linkkey3;
            else if (MainWindow.linkhot == "4")
                Shortcut.Text = Properties.Settings.Default.Linkkey4;
            else if (MainWindow.linkhot == "5")
                Shortcut.Text = Properties.Settings.Default.Linkkey5;

        }
    }
}
