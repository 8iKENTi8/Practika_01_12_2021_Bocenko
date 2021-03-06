using MySql.Data.MySqlClient;
using Practika_01_12_2021.AddControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practika_01_12_2021.UserControls
{
    public partial class Dogovora : UserControl
    {
        public Dogovora()
        {
            InitializeComponent();
        }

        DataTable tab;
        string[] idArr;

        private void ReloadDB()
        {

            DB dB = new DB();

            tab = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command =
                new MySqlCommand("SELECT *, 'Update','Delete'" +
                "FROM `договор`", dB.getConnection());

            adapter.SelectCommand = command;

            adapter.Fill(tab);

            table.DataSource = tab;
            Pain_Column();



        }

        public void Pain_Column()
        {
            idArr = new string[table.Rows.Count];

            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                idArr[i] = table.Rows[i].Cells[0].Value.ToString();

                table[7, i] = linkCell;
                table[7, i].Style.BackColor = Color.FromArgb(46, 169, 79);
            }

            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                table[8, i] = linkCell;
                table[8, i].Style.BackColor = Color.Tomato;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReloadDB();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new AddDogovor().ShowDialog();
        }

        private void table_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 7)
                {
                    string task = table.Rows[e.RowIndex].Cells[7].Value.ToString();
                    if (task == "Update")
                    {

                        if (Check(e))
                            return;

                        //Вопрос вы точно хотите обновить строку , да или нет?
                        if (MessageBox.Show("Обновить эту строку",
                            "Обновление", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int rowIndex = e.RowIndex;

                            DB db = new DB();
                            MySqlCommand command = new MySqlCommand("UPDATE `договор` SET `id_d` = @ul,`id_k` = @ul1, `id_c` = @ul2, `id_y` = @ul3, " +
                                "`Дата_Закл` = @ul4, `Дата_Окон` = @ul5, " +
                                "`Итог_дней` = @ul6 WHERE `договор`.`id_d` = @id", db.getConnection());

                            command.Parameters.Add("@id", MySqlDbType.Int32).Value = idArr[e.RowIndex];
                            command.Parameters.Add("@ul", MySqlDbType.Int32).Value = table[0, rowIndex].Value.ToString();
                            command.Parameters.Add("@ul1", MySqlDbType.VarChar).Value = table[1, rowIndex].Value.ToString();
                            command.Parameters.Add("@ul2", MySqlDbType.VarChar).Value = table[2, rowIndex].Value.ToString();
                            command.Parameters.Add("@ul3", MySqlDbType.VarChar).Value = table[3, rowIndex].Value.ToString();
                            command.Parameters.Add("@ul4", MySqlDbType.Date).Value = DateTime.Parse(table[4, rowIndex].Value.ToString());
                            command.Parameters.Add("@ul5", MySqlDbType.Date).Value = DateTime.Parse(table[5, rowIndex].Value.ToString());
                            command.Parameters.Add("@ul6", MySqlDbType.VarChar).Value = table[6, rowIndex].Value.ToString();


                            db.openConnection();
                            if (command.ExecuteNonQuery() == 1) { MessageBox.Show("Запись была обнавлена"); ReloadDB(); }

                            db.closeConnection();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                if (e.ColumnIndex == 8)
                {
                    string task = table.Rows[e.RowIndex].Cells[8].Value.ToString();
                    if (task == "Delete")
                    {
                        if (MessageBox.Show("Удалить эту строку",
                            "Удаление", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int rowIndex = e.RowIndex;

                            DB db = new DB();
                            MySqlCommand command = new MySqlCommand("DELETE FROM `договор`" +
                                " WHERE `договор`.`id_d` = @ul ", db.getConnection());
                            command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = table[0, rowIndex].Value.ToString();

                            table.Rows.RemoveAt(rowIndex);

                            db.openConnection();
                            if (command.ExecuteNonQuery() == 1) { MessageBox.Show("Запись  была удалена"); }

                            db.closeConnection();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

      

        private bool Check(DataGridViewCellEventArgs e)
        {

            for (int j = 0; j < table.ColumnCount - 2; j++)
            {
                if (table.Rows[e.RowIndex].Cells[j].Value.ToString() == "")
                {

                    table[j, e.RowIndex].Style.BackColor = Color.Tomato;
                    MessageBox.Show("Не введена запись: " + e.RowIndex + " " + j);
                    return true;
                }
                else
                    table[j, e.RowIndex].Style.BackColor = Color.White;

            }
            return false;
        }

        private void Dogovora_Load(object sender, EventArgs e)
        {
            ReloadDB();
        }
    }
}
