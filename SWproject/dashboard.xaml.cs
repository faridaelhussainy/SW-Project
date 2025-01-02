using System;
using MySql.Data.MySqlClient;
using System.Windows;
using System.Windows.Controls;

namespace SWproject
{
    public partial class dashboard : Page
    {
        // Connection string to MySQL
        private readonly string connectionString = "Server=localhost;Database=softdb;Uid=root;Pwd=1234;";

        public dashboard()
        {
            InitializeComponent();
            LoadDashboardData();

        }

        // Method to load data into the dashboard
        private void LoadDashboardData()
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Query for total patient count
                    string patientQuery = "SELECT COUNT(*) FROM Patients;";
                    MySqlCommand patientCmd = new MySqlCommand(patientQuery, connection);
                    var totalPatients = Convert.ToInt32(patientCmd.ExecuteScalar());
                    TotalPatientsTextBlock.Text = $"Total Patients: {totalPatients}";

                    // Query for new and old patients
                    string patientSummaryQuery = @"
                        SELECT 
                            SUM(CASE WHEN DateOfRegistration >= CURDATE() - INTERVAL 30 DAY THEN 1 ELSE 0 END) AS NewPatients,
                            SUM(CASE WHEN DateOfRegistration < CURDATE() - INTERVAL 30 DAY THEN 1 ELSE 0 END) AS OldPatients
                        FROM Patients;";
                    MySqlCommand patientSummaryCmd = new MySqlCommand(patientSummaryQuery, connection);
                    using (var reader = patientSummaryCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            NewPatientsTextBlock.Text = $"New: {reader["NewPatients"]}";
                            OldPatientsTextBlock.Text = $"Old: {reader["OldPatients"]}";
                        }
                    }

                    // Query for appointment summary
                    string appointmentQuery = @"
                        SELECT 
                            COUNT(*) AS TotalAppointments,
                            SUM(CASE WHEN Status = 'Cancelled' THEN 1 ELSE 0 END) AS CancelledAppointments,
                            SUM(CASE WHEN Status = 'Scheduled' THEN 1 ELSE 0 END) AS ActiveAppointments
                        FROM Appointments;";
                    MySqlCommand appointmentCmd = new MySqlCommand(appointmentQuery, connection);
                    using (var reader = appointmentCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            VisitsTextBlock.Text = $"Appointments: {reader["TotalAppointments"]} | Cancelled: {reader["CancelledAppointments"]} | Active: {reader["ActiveAppointments"]}";
                        }
                    }

                    // Query for today's scheduled requests
                    string requestsQuery = @"
    SELECT Patients.Name, Appointments.Reason, TIME(Appointments.AppointmentTime) AS AppointmentTime
    FROM Appointments
    JOIN Patients ON Appointments.PatientId = Patients.Id
    WHERE Appointments.AppointmentDate = CURDATE()
    AND Status = 'Scheduled';";
                    MySqlCommand requestsCmd = new MySqlCommand(requestsQuery, connection);
                    using (var reader = requestsCmd.ExecuteReader())
                    {
                        RequestListView.Items.Clear();
                        while (reader.Read())
                        {
                            var request = $"{reader["Name"]} - {reader["Reason"]} - {reader["AppointmentTime"]}";
                            RequestListView.Items.Add(request);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading dashboard data: {ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Event handler for navigating to the Appointment page
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("\\appointment.xaml", UriKind.Relative));
        }


        // Event handler for navigating to the Patients page
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("\\patient.xaml", UriKind.Relative));
        }
    }
}
