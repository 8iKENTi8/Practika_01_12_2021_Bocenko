using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Practika_01_12_2021
{
    internal class DB
    {
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
    }
}
