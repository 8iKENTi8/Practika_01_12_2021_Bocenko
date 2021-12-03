using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Practika_01_12_2021
{
    internal class DB
    {
        public static int user_role,user_id;
        public static string user_name;
        //Строка подключения к бд

        MySqlConnection connection = new MySqlConnection("server=localhost;" +
            "port=3307;username=root;password=root;database=ekz");

        //Открывает соединение
        public void openConnection()
        {
            //Если соединение закрыто, то открываем
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
        }

        //Закрывает соединение
        public void closeConnection()
        {
            //Если соединение открыто, то закрывавем
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }

        //Получаем соединение
        public MySqlConnection getConnection()
        {
            return connection;
        }

        public static bool ChecBox(List<MaskedTextBox> tx,Label lb)
        {

            foreach (var i in tx)
            {
                if (i.Text == "")
                {
                    lb.BackColor = ColorTranslator.FromHtml("#f75e25");
                    lb.Text = "Заполните все данные";
                    i.BackColor = ColorTranslator.FromHtml("#f75e25");
                    return true;
                }
                else
                {
                    lb.BackColor = Color.White;
                    i.BackColor = Color.White;
                }
            }

            return false;
        }
    }
}
