using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
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
    /// Логика взаимодействия для RedactProduct.xaml
    /// </summary>
    public partial class RedactProduct : Window
    {
        //Подключение
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-7Q0SKM1\\SQLEXPRESS;Initial Catalog=ПП02;Integrated Security=True");

        //Экзмепляр материала
        Product product;
        public RedactProduct(Product product)
        {
            InitializeComponent();
            //Получаем передаваемый экземпляр класса товара
            this.product = product;

            //Присваиваем данные из класса в поля
            Name.Text = product.MaterialName;
            Post.Text = product.Postavchik;
            Count.Text = product.MaterialCount;
            Price.Text = product.Price;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Проверка полей на заполнение
            if (Name.Text.Length != 0 && Post.Text.Length != 0 && Price.Text.Length != 0 && Count.Text.Length != 0)
            {
                UpdateProduct();
            }
            else
            {
                MessageBox.Show("Поля не должны быть пустыми!", "Info");
            }
        }
        //Обновление базы
        private void UpdateProduct()
        {
            con.Open();
            SqlCommand command = new SqlCommand($"UPDATE  materials " +
                $"SET [Наименование товара] = '{Name.Text}', [Поставщик] = '{Post.Text}', [Цена] = '{Price.Text}', [Количество] = '{Count.Text}') ", con;
            command.ExecuteNonQuery();
            con.Close();
        }
    }
}
