using System;
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;

namespace SWproject
{
    public partial class patient : Page
    {
        // Replace with your actual connection string
        private readonly string connectionString = "Server=localhost;Database=softdb;Uid=root;Pwd=1234;";

        public patient()
        {
            InitializeComponent();
        }

        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            // Collect information from input fields
            string name = NameTextBox.Text.Trim();
            string ageText = AgeTextBox.Text.Trim();
            string date = DateTextBox.Text.Trim();
            string gender = GenderTextBox.Text.Trim();
            string bloodType = (BloodTypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            string allergies = AllergiesTextBox.Text.Trim();
            string diseases = DiseasesTextBox.Text.Trim();
            string heightText = HeightTextBox.Text.Trim();
            string patientID = PatientIDTextBox.Text.Trim();
            string lastVisit = LastVisitTextBox.Text.Trim();

            // Input validation
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(ageText) || string.IsNullOrWhiteSpace(date))
            {
                MessageBox.Show("Name, Age, and Date are required fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(ageText, out int age))
            {
                MessageBox.Show("Invalid Age. Please enter a valid number.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!double.TryParse(heightText, out double height))
            {
                height = 0; // Default value for optional height
            }

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                        INSERT INTO Patients 
                        (Name, Age, DateOfRegistration, Gender, BloodType, Allergies, Diseases, Height, PatientID, LastVisit)
                        VALUES (@Name, @Age, @Date, @Gender, @BloodType, @Allergies, @Diseases, @Height, @PatientID, @LastVisit)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@Age", age);
                        command.Parameters.AddWithValue("@Date", date);
                        command.Parameters.AddWithValue("@Gender", gender);
                        command.Parameters.AddWithValue("@BloodType", bloodType ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Allergies", allergies ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Diseases", diseases ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Height", height);
                        command.Parameters.AddWithValue("@PatientID", patientID ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@LastVisit", lastVisit ?? (object)DBNull.Value);

                        // Execute the query
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Patient information saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                            ClearForm();
                        }
                        else
                        {
                            MessageBox.Show("Failed to save patient information.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Clears the form after successful submission
        private void ClearForm()
        {
            NameTextBox.Clear();
            AgeTextBox.Clear();
            DateTextBox.Clear();
            GenderTextBox.Clear();
            BloodTypeComboBox.SelectedIndex = -1;
            AllergiesTextBox.Clear();
            DiseasesTextBox.Clear();
            HeightTextBox.Clear();
            PatientIDTextBox.Clear();
            LastVisitTextBox.Clear();
        }

        // Navigation event handlers (if needed)
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("\\prescription.xaml", UriKind.Relative));
        }
        private void DashboardButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("\\dashboard.xaml", UriKind.Relative));
        }

        private void AppointmentButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("\\appointment.xaml", UriKind.Relative));
        }

       
    }
}
