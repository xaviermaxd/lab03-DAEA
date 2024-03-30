using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Lógica de interacción para Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
        }


        string connectionString = "Data Source=LAB1504-19\\SQLEXPRESS;Initial Catalog=DelegadoDB;User Id=userXavier; Password=123456";

        private void dataTable_Click(object sender, RoutedEventArgs e)
        {
            List<Student> students = new List<Student>();
            try
            {
                //Cadena de conexión
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                //Comandos de TRANSACT SQL
                SqlCommand command = new SqlCommand("SELECT * FROM Students", connection);

                //CONECTADA
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int StudentId = reader.GetInt32("StudentId");
                    string FirstName = reader.GetString("FirstName");
                    string SecondName = reader.GetString("SecondName");

                    students.Add(new Student { StudentID = StudentId, FirstName = FirstName, SecondName = SecondName });

                }

                connection.Close();
                dataGrid.ItemsSource = students;

                //dgvDemo.DataSource = clientes;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                //throw;
            }
        }



        private void dataReader_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Cadena de conexión
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                //Comandos de TRANSACT SQL
                SqlCommand command = new SqlCommand("SELECT * FROM Students", connection);

                //Intermediario
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                connection.Close();

                dataGrid.ItemsSource = dataTable.DefaultView;



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            List<Student> students = new List<Student>();
            try
            {
                //Cadena de conexión
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                //Comandos de TRANSACT SQL
                string filter = filterText.Text;
                SqlCommand command = new SqlCommand($"SELECT * FROM Students WHERE FirstName LIKE '%{filter}%'", connection);

                //CONECTADA
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int StudentId = reader.GetInt32("StudentId");
                    string FirstName = reader.GetString("FirstName");
                    string SecondName = reader.GetString("SecondName");

                    students.Add(new Student { StudentID = StudentId, FirstName = FirstName, SecondName = SecondName });
                }

                connection.Close();
                dataGrid.ItemsSource = students;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
