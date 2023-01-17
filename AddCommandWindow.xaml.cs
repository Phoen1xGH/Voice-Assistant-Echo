using System.IO;
using System.Windows;
using System.Windows.Input;

namespace VAEcho
{
    /// <summary>
    /// Логика взаимодействия для AddCommandWindow.xaml
    /// </summary>
    public partial class AddCommandWindow : Window
    {
        #region WindowInit

        public static string FilePath = "";

        public AddCommandWindow()
        {
            InitializeComponent();
            AnimationClass.OpacityAnimation(this);//appearance animation
        }
        #endregion

        #region Functional
        /// <summary>
        /// Method of getting the file path
        /// </summary>
        private void AddFilePath()
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                FileName = "",
                DefaultExt = ""
            };

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                FilePath = dialog.FileName;
            }
        }

        private void AddFileClick(object sender, RoutedEventArgs e)
        {
            AddFilePath();
        }

        private void AddCommandClick(object sender, RoutedEventArgs e)
        {
            Commands com = new Commands();
            string newCommand = $"\n{NameComBox.Text.ToUpper()},{FilePath}";
            File.AppendAllText("commands.txt", newCommand);
        }
        #endregion

        #region ToolBar

        public void Exit_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        public void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        public void Toolbar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }
        #endregion
    }
}
