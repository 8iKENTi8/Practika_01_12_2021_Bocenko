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
    public partial class AddEmploy : Form
    {
        MySqlCommand command;
        MySqlDataAdapter adapter;
        DataTable tab;
        public AddEmploy()
        {
            InitializeComponent();
            label6.BackColor = Color.White;

            DB dB = new DB();

             tab = new DataTable();

             adapter = new MySqlDataAdapter();

             command =
                new MySqlCommand("SELECT * " +
                "FROM `должность`", dB.getConnection());

            adapter.SelectCommand = command;

            adapter.Fill(tab);

            

            for (int i = 0; i < tab.Rows.Count; i++)
            {
                guna2ComboBox1.Items.Add(tab.Rows[i][1].ToString());
            }

            guna2ComboBox1.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<MaskedTextBox> list = new List<MaskedTextBox>();
            list.Add(maskedTextBox1);
            list.Add(maskedTextBox2);
            list.Add(maskedTextBox3);
            list.Add(maskedTextBox4);

            if (DB.ChecBox(list, label6))
                return;
            else
            {
                //Добавление записи в бд
                DB dB = new DB();

                dB.openConnection();

                command =
                    new MySqlCommand("SELECT `id_do` FROM `должность` WHERE `Название` = @ul",
                    dB.getConnection());
                command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = 
                    guna2ComboBox1.SelectedItem.ToString();

                adapter = new MySqlDataAdapter();

                adapter.SelectCommand = command;

                adapter.Fill(tab);

                

                command =
                    new MySqlCommand("CALL `add_empl`(@id, @p0, @p1, @p2, @p3);",
                    dB.getConnection());
                command.Parameters.Add("@p0", MySqlDbType.VarChar).Value = maskedTextBox1.Text;
                command.Parameters.Add("@p1", MySqlDbType.VarChar).Value = maskedTextBox2.Text;
                command.Parameters.Add("@p2", MySqlDbType.VarChar).Value = maskedTextBox3.Text;
                command.Parameters.Add("@p3", MySqlDbType.VarChar).Value = maskedTextBox4.Text;
                command.Parameters.Add("@id", MySqlDbType.VarChar).Value = 
                    guna2ComboBox1.SelectedItem.ToString();




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
