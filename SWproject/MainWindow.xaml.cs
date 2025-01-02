using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Security.Cryptography;
using System.Text;

namespace SWproject
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Event Handler for Login Button Click
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string Username = UsernameTextBox.Text.Trim();
            string Password = PasswordBox.Password.Trim();

            if (AuthenticateUser(Username, Password))
            {
                // Redirect to next page: dashboard
                if (Username == "admin" && Password == "1234")
                {
                    var dashboard = new dashboard();
                    MainFrame.Navigate(dashboard);
                    Window pageWindow = new Window
                    {
                        Title = "Page Window",
                        WindowState = WindowState.Maximized
                    };
                    Frame frame = new Frame();
                    frame.Navigate(new Uri("\\dashboard.xaml", UriKind.Relative));
                    pageWindow.Content = frame;
                    pageWindow.Show();
                    this.Close();
                }


            }
            else
            {
                MessageBox.Show("Invalid Username or Password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);

                
            }
        }
        public void ClearLoginFields()
        {
            UsernameTextBox.Text = string.Empty;
            PasswordBox.Password = string.Empty;
        }

        // Authenticate User with Database
        private bool AuthenticateUser(string Username, string Password)
        {
            string connectionString = "Server=localhost;Database=softdb;Uid=root;Pwd=1234;";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Query to fetch the hashed password
                    string query = "SELECT Password FROM Users WHERE Username = @Username";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", Username);
                        var result = command.ExecuteScalar();

                        if (result != null)
                        {
                            string storedHash = result.ToString();
                            string inputHash = ComputeSha256Hash(Password);
                            return storedHash == inputHash;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return false;
        }

        // Helper Method to Hash the Password
        private string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }

        }
       
    }
}


 



