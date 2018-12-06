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

namespace CineScreenSaver
{
    /// <summary>
    /// Interaction logic for Blackout.xaml
    /// </summary>
    public partial class Blackout : Window
    {
        public Blackout()
        {
            InitializeComponent();
            this.Loaded += OnMainWindowLoaded;
        }

        void OnMainWindowLoaded(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
        }

        private void OnMousePressed(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void OnKeyboardPressed(object sender, KeyEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
