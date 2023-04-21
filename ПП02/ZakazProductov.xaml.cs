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

namespace ПП02
{
    /// <summary>
    /// Логика взаимодействия для ZakazProductov.xaml
    /// </summary>
    public partial class ZakazProductov : Window
    {
        //Подключение
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-7Q0SKM1\\SQLEXPRESS;Initial Catalog=ПП02;Integrated Security=True");
        public ZakazProductov()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Проверка полей на заполнение
            if(Name.Text.Length > 0 && Post.Text.Length > 0 && Price.Text.Length > 0 && Count.Text.Length > 0)
            {
                OrderProduct();
            }
            else
            {
                MessageBox.Show("Заполниет все поля!", "Info");
            }
        }
        private void OrderProduct()
        {
            con.Open();
            SqlCommand command = new SqlCommand($"INSERT INTO materials ([Наименование товара], [Поставщик], [Цена], [Количество]) " +
                $"VALUES ('{Name.Text}', '{Post.Text}','{Price.Text}','{Count.Text}')", con);
            command.ExecuteNonQuery();
            con.Close();
        }
    }
}
