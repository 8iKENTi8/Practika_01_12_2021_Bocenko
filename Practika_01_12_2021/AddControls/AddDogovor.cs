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
    public partial class AddDogovor : Form
    {
        MySqlCommand command;
        MySqlDataAdapter adapter;
        DataTable tab;
        public AddDogovor()
        {
            InitializeComponent();
            label6.BackColor = Color.White;

            DB dB = new DB();

            tab = new DataTable();

            adapter = new MySqlDataAdapter();

            command =
               new MySqlCommand("SELECT * " +
               "FROM `клиент`", dB.getConnection());

            adapter.SelectCommand = command;

            adapter.Fill(tab);



            for (int i = 0; i < tab.Rows.Count; i++)
            {
                guna2ComboBox1.Items.Add(tab.Rows[i][1].ToString());
            }

            tab = new DataTable();

            command =
               new MySqlCommand("SELECT * " +
               "FROM `сотрудник`", dB.getConnection());

            adapter.SelectCommand = command;

            adapter.Fill(tab);



            for (int i = 0; i < tab.Rows.Count; i++)
            {
                guna2ComboBox2.Items.Add(tab.Rows[i][1].ToString());
            }

            tab = new DataTable();

            command =
               new MySqlCommand("SELECT * " +
               "FROM `услуга`", dB.getConnection());

            adapter.SelectCommand = command;

            adapter.Fill(tab);



            for (int i = 0; i < tab.Rows.Count; i++)
            {
                guna2ComboBox3.Items.Add(tab.Rows[i][1].ToString());
            }

            guna2ComboBox1.SelectedIndex = 0;
            guna2ComboBox2.SelectedIndex = 0;
            guna2ComboBox3.SelectedIndex = 0;
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

                dB.openConnection();




                command =
                    new MySqlCommand("CALL `Add_dogovor`(@p0, @p1, @p2, @p3, @p4);",
                    dB.getConnection());
                command.Parameters.Add("@p0", MySqlDbType.VarChar).Value = guna2ComboBox1.SelectedItem.ToString();
                command.Parameters.Add("@p1", MySqlDbType.VarChar).Value = guna2ComboBox2.SelectedItem.ToString();
                command.Parameters.Add("@p2", MySqlDbType.VarChar).Value = guna2ComboBox3.SelectedItem.ToString();
                command.Parameters.Add("@p3", MySqlDbType.Date).Value = DateTime.Parse(maskedTextBox1.Text);
                command.Parameters.Add("@p4", MySqlDbType.Date).Value = DateTime.Parse(maskedTextBox2.Text);
                




                if (command.ExecuteNonQuery() == 1)
                    MessageBox.Show("Договор был создан!");
                else
                    MessageBox.Show("Услуга не была создана!");

                dB.closeConnection();

                this.Hide();
            }
        }
    }
}
