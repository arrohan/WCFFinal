using ChatInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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

namespace ChatClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static IChatService server;
        private static DuplexChannelFactory<IChatService> ChannelFactory;

        public MainWindow()
        {
            InitializeComponent();
            ChannelFactory = new DuplexChannelFactory<IChatService>(new ClientCallBack(), "ChattingServiceEndPoint");
            server = ChannelFactory.CreateChannel();


        }

        public void TakeMessage(string Message, string userName)
        {
            TextDisplayTextBox.Text += userName + ": " + Message + "\n";
            TextDisplayTextBox.ScrollToEnd();
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            if(MessageTextBox.Text.Length == 0)
            {
                return;
            }
            server.sendMessage(MessageTextBox.Text, UserNameTextBox.Text);
            TextDisplayTextBox.Text += "You : " + MessageTextBox.Text + "\n";
            TextDisplayTextBox.ScrollToEnd();
            MessageTextBox.Clear(); 
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int returnedValue = server.Login(UserNameTextBox.Text);
            if(returnedValue == 1 )
            {
                MessageBox.Show("You are already loggedin!!");
            }
            else
            { 
                MessageBox.Show("You logged in!!");
                UserNameTextBox.IsEnabled = false;
                Login.IsEnabled = false;
                WelcomeLabel.Content = "Welcome " + UserNameTextBox.Text;
                this.Title = UserNameTextBox.Text;
            }
        }
    }
}
