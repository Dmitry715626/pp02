using System;
using System.Collections.Generic;
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
using System.Data.SqlClient;
using System.Data;

namespace ПП02
{
    /// <summary>
    /// Логика взаимодействия для SpisokProduct.xaml
    /// </summary>
    public partial class SpisokProduct : Window
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-7Q0SKM1\\SQLEXPRESS;Initial Catalog=ПП02;Integrated Security=True");
        public SpisokProduct()
        {
            InitializeComponent();
            List<Product> products = new List<Product>();
            con.Open();
            SqlCommand sqlCommand = new SqlCommand("SELECT MaterialName = materials.[Наименование материала], " +
                "MaterialType = materials.[Тип материала], " +
                $"MaterialPicture = Concat('{System.IO.Directory.GetCurrentDirectory()}',materials.Изображение), " +
                "MaterialCount = materials.[Количество на складе], " +
                "MaterialMinCount = materials.[Минимальное количество], " +
                "MaterialEdIzm = materials.[Единица измерения], " +
                "Postavchik = postavchik.[Возможный поставщик] " +
                "FROM materials " +
                "JOIN postavchik " +
                "ON postavchik.[Наименование материала] = materials.[Наименование материала]", con);
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            MessageBox.Show(dataTable.Rows[0][2].ToString());
            con.Close();
            ListProd.ItemsSource = dataTable.DefaultView;
            Sort.Items.Add("Сортировка");
            Filtr.Items.Add("Фильтр");
        }
    }
}
