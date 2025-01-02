using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace SWproject
{
    /// <summary>
    /// Interaction logic for prescription.xaml
    /// </summary>
    public partial class prescription : Page
    {
        // Connection string to your database
        private readonly string connectionString = "Server=localhost;Database=softdb;Uid=root;Pwd=1234;";

        public prescription()
        {
            InitializeComponent();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            // Gather input values
            string firstName = FirstNameTextBox.Text;
            string lastName = LastNameTextBox.Text;
            string diagnosis = DiagnosisTextBox.Text;
            string medicationName = MedicationTextBox.Text;
            DateTime? startDate = StartDatePicker.SelectedDate;
            DateTime? endDate = EndDatePicker.SelectedDate;
            string dosage = DosageTextBox.Text;
            string dosePeriod = DosePeriodComboBox.Text;

            // Validate the input
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(diagnosis) || string.IsNullOrWhiteSpace(medicationName) ||
                !startDate.HasValue || string.IsNullOrWhiteSpace(dosage) || dosePeriod == "Select")
            {
                MessageBox.Show("Please fill all required fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Save to database
            bool isSaved = SavePrescriptionToDatabase(firstName, lastName, diagnosis, medicationName, startDate.Value, endDate, dosage, dosePeriod);

            if (isSaved)
            {
                MessageBox.Show("Prescription submitted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Clear fields
                FirstNameTextBox.Clear();
                LastNameTextBox.Clear();
                DiagnosisTextBox.Clear();
                MedicationTextBox.Clear();
                StartDatePicker.SelectedDate = null;
                EndDatePicker.SelectedDate = null;
                DosageTextBox.Clear();
                DosePeriodComboBox.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Failed to save the prescription. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool SavePrescriptionToDatabase(string firstName, string lastName, string diagnosis, string medicationName, DateTime startDate, DateTime? endDate, string dosage, string dosePeriod)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                        INSERT INTO Prescriptions 
                        (FirstName, LastName, Diagnosis, MedicationName, StartDate, EndDate, Dosage, DosePeriod) 
                        VALUES 
                        (@FirstName, @LastName, @Diagnosis, @MedicationName, @StartDate, @EndDate, @Dosage, @DosePeriod)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", firstName);
                        command.Parameters.AddWithValue("@LastName", lastName);
                        command.Parameters.AddWithValue("@Diagnosis", diagnosis);
                        command.Parameters.AddWithValue("@MedicationName", medicationName);
                        command.Parameters.AddWithValue("@StartDate", startDate);
                        command.Parameters.AddWithValue("@EndDate", (object)endDate ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Dosage", dosage);
                        command.Parameters.AddWithValue("@DosePeriod", dosePeriod);

                        command.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("\\dashboard.xaml", UriKind.Relative));
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("\\appointment.xaml", UriKind.Relative));
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Page2.xaml", UriKind.Relative));
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("\\patient.xaml", UriKind.Relative));
        }
    }
}
