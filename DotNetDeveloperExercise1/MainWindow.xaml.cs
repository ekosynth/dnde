using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
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

namespace DotNetDeveloperExercise1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SQLiteConnection connection;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            const string filePath = "C:\\Users\\rjd99\\Documents\\Visual Studio 2019\\Projects\\DotNetDeveloperExercise1\\DotNetDeveloperExercise1\\database.sqlite";
            connection = new SQLiteConnection($"Data Source={filePath};Version=3;");
            connection.Open();
        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh(true);
        }

        private void CreateTableButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string sql = "CREATE TABLE IF NOT EXISTS cars (id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, make VARCHAR(40) NOT NULL, model VARCHAR(40) NOT NULL, colour VARCHAR(40) NOT NULL, engine_size REAL NOT NULL, UNIQUE(make, model, colour, engine_size))";
                SQLiteCommand command = new SQLiteCommand(sql, connection);
                command.ExecuteNonQuery();

                MessageBox.Show("Created database table successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Failed to create database table.\n\nException message:\n{exception.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void InsertCarsButton_Click(object sender, RoutedEventArgs e)
        {
            // Create a list of cars
            IList<Car> cars = new List<Car> {
                new Car { Make = "Nissan", Model = "Micra", Colour = "Black", EngineSize = 1.0f },
                new Car { Make = "Audi", Model = "TT", Colour = "White", EngineSize = 2.0f },
                new Car { Make = "Ford", Model = "Focus", Colour = "Green", EngineSize = 1.3f },
                new Car { Make = "Toyata", Model = "Prius", Colour = "White", EngineSize = 1.5f },
                new Car { Make = "Kia", Model = "Picanto", Colour = "Blue", EngineSize = 0.9f }
            };

            try
            {
                foreach (Car car in cars)
                {
                    // No need to use a parameterised SQL query since the data inserted into the query is not user-inputted
                    string sql = $"INSERT OR IGNORE INTO cars VALUES (NULL, '{car.Make}', '{car.Model}', '{car.Colour}', {car.EngineSize})";
                    SQLiteCommand command = new SQLiteCommand(sql, connection);
                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Inserted dummy car data into database successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Failed to insert dummy car data into database.\n\nException message:\n{exception.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void Refresh(bool suppressMessages = false)
        {
            try
            {
                string sql = "SELECT * FROM cars";
                SQLiteCommand command = new SQLiteCommand(sql, connection);
                using (SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(command.CommandText, connection))
                {
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    dataGrid.ItemsSource = dataTable.AsDataView();
                }

                if (!suppressMessages)
                {
                    MessageBox.Show("Refreshed successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception exception)
            {
                if (!suppressMessages)
                {
                    MessageBox.Show($"Failed to refresh database.\n\nException message:\n{exception.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            connection.Close();
        }
    }
}
