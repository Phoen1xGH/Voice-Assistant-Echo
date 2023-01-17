using System.Net;
using System.Net.Mail;
using System.Windows;
using System.Windows.Input;

namespace VAEcho
{
    /// <summary>
    /// Логика взаимодействия для UserSupport.xaml
    /// </summary>
    public partial class UserSupport : Window
    {
        #region WindowInit

        public UserSupport()
        {
            InitializeComponent();
            AnimationClass.OpacityAnimation(this);//appearance animation
        }

        #endregion

        #region Sending an error message

        /// <summary>
        /// Method for sending a message to technical support
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendMessage(object sender, RoutedEventArgs e)
        {
            //sender's email
            MailAddress from = new MailAddress("echo.client@mail.ru");
            //recipient's email
            MailAddress to = new MailAddress("echo.supp0rt@mail.ru");
            //message object
            MailMessage message = new MailMessage(from, to);
            //message theme
            message.Subject = string.Empty;
            //message content
            message.Body = $"<h2>{messTextBox.Text}</h2>";
            //message represents the html code
            message.IsBodyHtml = true;
            //the address of the smtp server and the port from which user will send the message
            SmtpClient smtp = new SmtpClient("smtp.mail.ru", 25);
            //login and password
            smtp.Credentials = new NetworkCredential("echo.client@mail.ru", "QVP2eiKMzjebqStMq5nq");
            smtp.EnableSsl = true;
            //sending
            smtp.Send(message);
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
