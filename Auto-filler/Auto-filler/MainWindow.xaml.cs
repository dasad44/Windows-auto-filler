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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Auto_filler
{
    public partial class MainWindow : Window
    {

        private KeyboardListener _listener;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void HideButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        void _listener_OnKeyPressed(object sender, KeyPressedArgs e)
        {
           // if (Keyboard.IsKeyDown(Key.G)
           // && Keyboard.IsKeyDown(Key.LeftCtrl)
           // && Keyboard.IsKeyDown(Key.LeftAlt)
          //  || Keyboard.IsKeyDown(Key.G)
           // && Keyboard.IsKeyDown(Key.RightCtrl)
          //  && Keyboard.IsKeyDown(Key.RightAlt))
           //     this.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _listener = new KeyboardListener();
            _listener.OnKeyPressed += _listener_OnKeyPressed;
            _listener.HookKeyboard();
        }
    }
}
