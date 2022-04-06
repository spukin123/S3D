using S3D.Render;
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
    /// Interaction logic for S3DViewport.xaml
    /// </summary>
    public partial class S3DViewport : UserControl
    {
        private readonly Scene scene;

        private bool isMLBDown;
        private Point cursorPos;

        public S3DViewport()
        {
            InitializeComponent();

            scene = new Scene(S3DViewportInstance);
        }

        public void Apply()
        {
            scene.RenderApply();
        }

        private void S3DViewportInstance_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            cursorPos = e.GetPosition(this);
            isMLBDown = true;
        }

        private void S3DViewportInstance_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isMLBDown = false;
        }

        private void S3DViewportInstance_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMLBDown)
            {
                scene.RotateCamera(e.GetPosition(this).X - cursorPos.X, 
                    e.GetPosition(this).Y - cursorPos.Y);
                cursorPos = e.GetPosition(this);
            }
        }
    }
}
