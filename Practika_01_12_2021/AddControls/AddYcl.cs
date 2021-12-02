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
    public partial class AddYcl : Form
    {
        public AddYcl()
        {
            InitializeComponent();
            label6.BackColor = Color.White;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<MaskedTextBox> list = new List<MaskedTextBox>();
            list.Add(maskedTextBox1);
            list.Add(maskedTextBox2);
       
            if (DB.ChecBox(list, label6))
                return;
            else
            {
                //Добавление записи в бд
                DB dB = new DB();

                MySqlCommand command =
                    new MySqlCommand("INSERT INTO `услуга` " +
                    "(`id_y`, `Название`, `Стоимость`) " +
                    "VALUES (NULL, @p0, @p1)",
                    dB.getConnection());
                command.Parameters.Add("@p0", MySqlDbType.VarChar).Value = maskedTextBox1.Text;
                command.Parameters.Add("@p1", MySqlDbType.VarChar).Value = maskedTextBox2.Text;
            

                dB.openConnection();

                if (command.ExecuteNonQuery() == 1)
                    MessageBox.Show("Услуга была создана!");
                else
                    MessageBox.Show("Услуга не была создана!");

                dB.closeConnection();

                this.Hide();
            }
            }
    }
}
