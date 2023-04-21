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
        //Подвключение
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-7Q0SKM1\\SQLEXPRESS;Initial Catalog=ПП02;Integrated Security=True");
        DataTable dataTable = new DataTable();
        public SpisokProduct()
        {
            InitializeComponent();

            //Запрос данных из базы
            con.Open();
            SqlCommand sqlCommand = new SqlCommand("SELECT MaterialName = materials.[Наименование материала], " +
                "MaterialType = materials.[Тип материала], " +
                $"MaterialPicture = Concat('{System.IO.Directory.GetCurrentDirectory()}',materials.Изображение), " +
                "MaterialCount = materials.[Количество на складе], " +
                "MaterialMinCount = materials.[Минимальное количество], " +
                "MaterialEdIzm = materials.[Единица измерения], " +
                "Postavchik = postavchik.[Возможный поставщик], " +
                "MaterialPrice = Price " +
                "FROM materials " +
                "JOIN postavchik " +
                "ON postavchik.[Наименование материала] = materials.[Наименование материала]", con);
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            
            adapter.Fill(dataTable);
            con.Close();

            //Заполнение данных
            ListProd.ItemsSource = dataTable.DefaultView;
            Sort.Items.Add("Сортировка");
            Filtr.Items.Add("Фильтр");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new ZakazProductov().ShowDialog();
        }

        private void ListProd_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Выбранный товар
            int id = ListProd.SelectedIndex;

            //Создаем экземпляр класса товара и передаем туда данные выбранного товара
            Product product = new Product();
            product.MaterialName = dataTable.Rows[id]["MaterialName"].ToString();
            product.MaterialCount = dataTable.Rows[id]["MaterialCount"].ToString();
            product.Postavchik = dataTable.Rows[id]["Postavchik"].ToString();
            product.Price = dataTable.Rows[id]["MaterialPrice"].ToString();

            //Открываем новую форму редактирования и передаем туда экземпляр класса товара
            new RedactProduct(product).ShowDialog();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
