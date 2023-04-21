using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ПП02
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Подключение
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-7Q0SKM1\\SQLEXPRESS;Initial Catalog=ПП02;Integrated Security=True");
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Проверка полей
            if(Validation())
            {
                GetJob();
            }
        }

        //Валидация полей
        private bool Validation()
        {
            if(Login.Text.Length < 0 || Pass.Text.Length < 0)
            {
                MessageBox.Show("Заполните все поля!", "Info");
                return false;
            }
            else
            {
                return true;
            }
        }
        private void GetJob()
        {
            //Запрос в базу для получения сотрудника по логину и паролю
            con.Open();
            SqlCommand sqlCommand = new SqlCommand($"SELECT * FROM Jobs WHERE Login = '{Login}' AND Pass = '{Pass}'", con);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            if(sqlDataReader.Read()) //Если такой сотрудник найден - заполняем класс Job данными
            {
                Job.Name = sqlDataReader.GetString(1);
                Job.Surname = sqlDataReader.GetString(2);
                Job.id = sqlDataReader.GetInt32(0);
            }
            else //Если сотрудник не найден то выводим сообщение об ошибке
            {
                MessageBox.Show("Такого пользователя не существует!", "Info");
            }
            con.Close();
        }
    }
}
