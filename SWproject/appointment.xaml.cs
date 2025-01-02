using System;
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;

namespace SWproject
{
    public partial class appointment : Page
    {
        // Replace with your MySQL connection details
        private string connectionString = "Server=localhost;Database=softdb;Uid=root;Pwd=1234;";

        public appointment()
        {
            InitializeComponent();
        }

        private void AddAppointmentButton_Click(object sender, RoutedEventArgs e)
        {
            // Get input from the UI
            string patientName = PatientNameTextBox.Text.Trim();
            DateTime? selectedDate = AppointmentDatePicker.SelectedDate;
            ComboBoxItem selectedTimeItem = AppointmentTimeComboBox.SelectedItem as ComboBoxItem;
            string selectedTime = selectedTimeItem != null ? selectedTimeItem.Content.ToString() : null;

            // Validate inputs
            if (string.IsNullOrWhiteSpace(patientName) || selectedDate == null || selectedTime == "Select Time" || string.IsNullOrWhiteSpace(selectedTime))
            {
                MessageBox.Show("Please fill in all fields.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Query to insert the appointment into the database
                    string query = "INSERT INTO Appointments (PatientId, PatientName, AppointmentDate, AppointmentTime, Reason, Status) " +
                                   "VALUES (@PatientId, @PatientName, @AppointmentDate, @AppointmentTime, @Reason, @Status)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Assuming you don't have a PatientId yet, set it to NULL for now
                        command.Parameters.AddWithValue("@PatientId", DBNull.Value); // Replace with the actual PatientId if available
                        command.Parameters.AddWithValue("@PatientName", patientName);
                        command.Parameters.AddWithValue("@AppointmentDate", selectedDate.Value.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@AppointmentTime", selectedTime);
                        command.Parameters.AddWithValue("@Reason", ""); // Add a reason if you have it in the UI
                        command.Parameters.AddWithValue("@Status", "Scheduled");

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show($"Appointment added for {patientName} on {selectedDate.Value.ToShortDateString()} at {selectedTime}.",
                                            "Appointment Added", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to add appointment.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Navigation Button Click Handlers (Add appropriate navigation URIs)
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("\\dashboard.xaml", UriKind.Relative));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Page2.xaml", UriKind.Relative));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("\\patient.xaml", UriKind.Relative));
        }
    }
}
