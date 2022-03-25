using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WpfApp1.UserControls;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isButtonToggle;
        private bool isChildWindowCancel = true;
        private double childLeft, childTop;

        private const double ChildPadding = 2;

        private Window childWindow = null;

        private Rect mainWindowSize = new Rect();
        private Rect childWindowSize = new Rect();
        private Point mainWindowBoundSize = new Point();
        private Point childWindowBoundSize = new Point();

        private double mainLeft, mainTop, mainMinWidth;

        private bool isMainWindowMouseDown;

        private const int WM_NCLBUTTONDOWN = 0x00a1;
        private const int WM_NCLBUTTONUP = 0x00A2;

        public MainWindow()
        {
            InitializeComponent();

            HwndSource source = HwndSource.FromHwnd(new WindowInteropHelper(this).EnsureHandle());
            source.AddHook(new HwndSourceHook(WndProc));
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {

                case WM_NCLBUTTONDOWN:
                    {
                        isMainWindowMouseDown = true;
                        break;
                    }
                case WM_NCLBUTTONUP:
                    {
                        isMainWindowMouseDown = false;
                        break;
                    }
            }

            if (msg == 0x0112 && ((int)wParam & 0xFFF0) == 0xF020)
                isMainWindowMouseDown = false;

            return IntPtr.Zero;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            isButtonToggle = !isButtonToggle;
            if (isButtonToggle)
            {
                SplitMain();
                CreateChild();
            }
            else
                RestoreMain();
        }

        private void CreateChild()
        {
            childWindow = new Window();

            childWindow.Opacity = 0;

            childWindow.Width = Width;
            childWindow.Height = Height;
            childWindow.MinWidth = MinWidth;
            childWindow.MinHeight = MinHeight;
            childWindow.Left = childLeft - ChildPadding;
            childWindow.Top = Top;

            childWindow.Closing += ChildWindow_Closing;
            childWindow.SourceInitialized += ChildWindow_SourceInitialized;
            childWindow.StateChanged += ChildWindow_StateChanged;
            childWindow.ResizeMode = ResizeMode.NoResize;

            childWindow.Content = new SecondWindowUserControl();

            childWindow.Opacity = 1;
            childWindow.Show();
        }

        private void RestoreMain()
        {
            Opacity = 0;
            isChildWindowCancel = false;
            MaxWidth = double.PositiveInfinity;
            Width += childWindow.Width;
            MaxWidth = Width;
            MinWidth = mainMinWidth;

            Left = childLeft + ChildPadding;

            childWindow.Close();
            isChildWindowCancel = true;
            childWindow = null;
            Opacity = 1;
        }

        private void SplitMain()
        {
            Opacity = 0;
            mainMinWidth = MinWidth;
            MinWidth = 0;
            MinWidth = Width / 2.0;
            Width = MinWidth;

            childLeft = Left;
            childTop = Top;
            Left += Width;
            mainLeft = Left;
            Opacity = 1;
        }

        private void ChildWindow_StateChanged(object sender, EventArgs e)
        {
            if ((sender as Window).WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Normal;
                RestoreWindowsState();
            }
        }

        private void ChildWindow_SourceInitialized(object sender, EventArgs e)
        {
            (sender as Window).HideMinimizeAndMaximizeButtons();
        }

        private void ChildWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = isChildWindowCancel;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isChildWindowCancel = false;
            if (childWindow != null)
                childWindow.Close();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            mainLeft = Left;
            mainTop = Top;
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Minimized)
            {
                SaveWindowsState();
                if (childWindow != null)
                {
                    childWindow.Left = childLeft;
                    childWindow.Top = childTop;

                    childWindow.WindowState = WindowState.Minimized;
                }
            }
            else if (this.WindowState == WindowState.Normal)
            {
                if (childWindow != null)
                    childWindow.WindowState = WindowState.Normal;
                RestoreWindowsState();
            }
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            if (isMainWindowMouseDown && childWindow != null
                )
            {
                childWindow.Left += Left - mainLeft;
                childWindow.Top += Top - mainTop;
                mainLeft = Left;
                mainTop = Top;
                childLeft = childWindow.Left;
            }
        }

        private void SaveWindowsState()
        {
            mainWindowSize.X = mainLeft;
            mainWindowSize.Y = mainTop;
            mainWindowSize.Width = Width;
            mainWindowSize.Height = Height;
            mainWindowBoundSize.X = MinWidth;
            mainWindowBoundSize.Y = MaxWidth;

            if (childWindow != null)
            {
                childWindowSize.X = childWindow.Left;
                childWindowSize.Y = childWindow.Top;
                childWindowSize.Width = childWindow.Width;
                childWindowSize.Height = childWindow.Height;
                childWindowBoundSize.X = childWindow.MinWidth;
                childWindowBoundSize.Y = childWindow.MaxWidth;
            }
        }

        private void RestoreWindowsState()
        {
            mainLeft = mainWindowSize.X;
            mainTop = mainWindowSize.Y;
            Width = mainWindowSize.Width;
            Height = mainWindowSize.Height;
            MinWidth = mainWindowBoundSize.X;
            MaxWidth = mainWindowBoundSize.Y;

            if (childWindow != null)
            {
                childWindow.Left = childWindowSize.X;
                childWindow.Top = childWindowSize.Y;
                childWindow.Width = childWindowSize.Width;
                childWindow.Height = childWindowSize.Height;
                childWindow.MinWidth = childWindowBoundSize.X;
                childWindow.MaxWidth = childWindowBoundSize.Y;
            }
        }
    }
}
