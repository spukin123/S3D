using S3D.Desktop.UserControls;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.UserControls
{
    /// <summary>
    /// Interaction logic for SecondWindowUserControl.xaml
    /// </summary>
    public partial class SecondWindowUserControl : UserControl
    {
        public SecondWindowUserControl()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window fullscreenWindow = new Window();
            FullscreenControl fs = new FullscreenControl();
            fs.Tag = fullscreenWindow;
            fullscreenWindow.Content = fs;
            fullscreenWindow.WindowState = WindowState.Maximized;
            fullscreenWindow.Show();
        }
    }
}
