using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practika_01_12_2021
{
    public partial class Sign_in : Form
    {
        public Sign_in()
        {
            InitializeComponent();
            guna2TextBox2.UseSystemPasswordChar = true;

        }

        DataTable table;
        MySqlCommand command;
        MySqlDataAdapter adapter;

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string login = guna2TextBox1.Text.Trim(),
                   pass = guna2TextBox2.Text.Trim();

            if (login.Length < 4)
            {
                MessageBox.Show("Логин введен неверно!");
                return;
            }

            if (pass.Length < 4)
            {
                MessageBox.Show("Пароль введен неверно!");
                return;
            }

            if (isUserExists(login, pass))
            {
                this.Hide();
                Form1 form = new Form1();
                form.Show();
                return;
            }
            else
                MessageBox.Show("Пользователя не существует");
        }

        // Проверка есть ли пользователь в бд
        public Boolean isUserExists(string log, string pass)
        {
            DB dB = new DB();

            table = new DataTable();

             adapter = new MySqlDataAdapter();

             command =
                new MySqlCommand("SELECT * FROM `сотрудник` WHERE `Имя` = @ul AND" +
                "`pass`= @up", dB.getConnection());

            command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = log;
            command.Parameters.Add("@up", MySqlDbType.VarChar).Value = pass;

            adapter.SelectCommand = command;

            adapter.Fill(table);



            if (table.Rows.Count > 0)
            {
                DB.user_role = Int32.Parse( table.Rows[0][5].ToString());
                DB.user_name = table.Rows[0][2].ToString();

                return true;
            }

            table = new DataTable();

            adapter = new MySqlDataAdapter();

            command =
               new MySqlCommand("SELECT * FROM `клиент` WHERE `Компания` = @ul AND" +
               "`pass`= @up", dB.getConnection());

            command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = log;
            command.Parameters.Add("@up", MySqlDbType.VarChar).Value = pass;

            adapter.SelectCommand = command;

            adapter.Fill(table);



            if (table.Rows.Count > 0)
            {
                DB.user_role = -1;
                DB.user_name = table.Rows[0][1].ToString();
                DB.user_id = Int32.Parse( table.Rows[0][0].ToString());

                return true;
            }

            return false;

        }
    }
}
