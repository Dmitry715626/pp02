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

namespace ПП02
{
    /// <summary>
    /// Логика взаимодействия для SpisokPostacshik.xaml
    /// </summary>
    public partial class SpisokPostacshik : Window
    {
        //Подвключение
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-7Q0SKM1\\SQLEXPRESS;Initial Catalog=ПП02;Integrated Security=True");
        public SpisokPostacshik()
        {
            InitializeComponent();

            //Запрос данных из базы
            con.Open();
            SqlCommand sqlCommand = new SqlCommand("SELECT Name, Surname, JobTitle.Name, phone FROM Jobs " +
                "JOIN JobTitle " +
                "ON Jobs.idJobTitle = JobTitle.id", con);
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            con.Close();

            //Заполнение данных
            ListProd.ItemsSource = dataTable.DefaultView;
        }
    }
}
