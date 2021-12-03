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

namespace Practika_01_12_2021.AddControls
{
    public partial class AddClient : Form
    {
        public AddClient()
        {
            InitializeComponent();
            label6.BackColor = Color.White;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<MaskedTextBox> list = new List<MaskedTextBox>();
            list.Add(maskedTextBox1);
            list.Add(maskedTextBox2);
            list.Add(maskedTextBox3);
            list.Add(maskedTextBox4);
            list.Add(maskedTextBox5);
            if (DB.ChecBox(list, label6))
                return;
            else
            {
                //Добавление записи в бд
                DB dB = new DB();

                MySqlCommand command =
                    new MySqlCommand("INSERT INTO `клиент` " +
                    "(`id_k`, `Компания`, `Телефон`, `Mail`, `Адрес`, `pass`)" +
                    " VALUES (NULL, @p0, @p1, @p2, @p3, @p4)",
                    dB.getConnection());
                command.Parameters.Add("@p0", MySqlDbType.VarChar).Value = maskedTextBox1.Text;
                command.Parameters.Add("@p1", MySqlDbType.VarChar).Value = maskedTextBox2.Text; 
                command.Parameters.Add("@p2", MySqlDbType.VarChar).Value = maskedTextBox3.Text; 
                command.Parameters.Add("@p3", MySqlDbType.VarChar).Value = maskedTextBox4.Text;
                command.Parameters.Add("@p4", MySqlDbType.VarChar).Value = maskedTextBox5.Text;




                dB.openConnection();

                if (command.ExecuteNonQuery() == 1)
                    MessageBox.Show("Аккаунт был создан!");
                else
                    MessageBox.Show("Аккаунт не был создан!");

                dB.closeConnection();

                this.Hide();
            }
            
        }
    }
}
