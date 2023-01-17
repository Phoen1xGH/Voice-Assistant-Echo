using System;
using System.Windows;
using System.Windows.Input;

namespace VAEcho
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region WindowInit

        public MainWindow()
        {
            InitializeComponent();
            AnimationClass.OpacityAnimation(this);//appearance animation
            var primaryMonitorArea = SystemParameters.WorkArea;
            Left = primaryMonitorArea.Right - Width - 30;
            Top = primaryMonitorArea.Bottom - Height - 30;//setting the window position
            EchoCall.EchoRecognize();//recognition appeal to Echo
        }

        #endregion

        #region Switch to other windows

        private void SupportClick(object sender, RoutedEventArgs e)
        {
            Hide();
            UserSupport userSupport = new UserSupport();
            userSupport.Show();
            Close();
        }

        private void SettingsClick(object sender, RoutedEventArgs e)
        {
            Hide();
            AddCommandWindow addWindow = new AddCommandWindow();
            addWindow.Show();
        }

        #endregion

        #region ToolBar
        public void ExitClick(object sender, RoutedEventArgs e)
        {
            AnimationClass.OpacityAnimation(this);
            Environment.Exit(0);
        }

        public void MinimizeClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        public void ToolbarMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }
        #endregion
    }
}
