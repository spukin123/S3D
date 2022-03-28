using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace S3D.Desktop.UserControls
{
    /// <summary>
    /// Interaction logic for FullscreenControl.xaml
    /// </summary>
    public partial class FullscreenControl : UserControl
    {
        public FullscreenControl()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            S3DViewportInstance.Apply();
        }

        private void Close()
        {
            (Tag as Window).Close();
        }
    }
}
