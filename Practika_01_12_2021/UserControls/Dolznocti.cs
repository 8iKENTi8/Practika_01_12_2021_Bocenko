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
    public partial class Dolznocti : UserControl
    {
        public Dolznocti()
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
                "FROM `должность`", dB.getConnection());

            adapter.SelectCommand = command;

            adapter.Fill(tab);

            table.DataSource = tab;
            Pain_Column();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            new AddDol().ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReloadDB();
        }

        public void Pain_Column()
        {
            idArr = new string[table.Rows.Count];

            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                idArr[i] = table.Rows[i].Cells[0].Value.ToString();

                table[3, i] = linkCell;
                table[3, i].Style.BackColor = Color.FromArgb(46, 169, 79);
            }

            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                table[4, i] = linkCell;
                table[4, i].Style.BackColor = Color.Tomato;
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                DataView data = tab.DefaultView;
                data.RowFilter = string.Format("`Название` like '%{0}%'", txtSearch.Text);
                table.DataSource = data.ToTable();

                Pain_Column();
            }

            if (txtSearch.Text == "")
            {
                Pain_Column();
            }
        }

        private bool Check(DataGridViewCellEventArgs e)
        {

            for (int j = 0; j < table.ColumnCount - 2; j++)
            {
                if (table.Rows[e.RowIndex].Cells[j].Value.ToString() == "")// проверяем 3-й столбец на пустые ячейки
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

        private void table_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 3)
                {
                    string task = table.Rows[e.RowIndex].Cells[3].Value.ToString();
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
                            MySqlCommand command = new MySqlCommand("UPDATE `должность` SET `id_do` = @ul," +
                                " `Название` = @ul1, " +
                                "`Зарплата` = @ul2 WHERE `должность`.`id_do` = @id", db.getConnection());

                            command.Parameters.Add("@id", MySqlDbType.Int32).Value = idArr[e.RowIndex];
                            command.Parameters.Add("@ul", MySqlDbType.Int32).Value = table[0, rowIndex].Value.ToString();
                            command.Parameters.Add("@ul1", MySqlDbType.VarChar).Value = table[1, rowIndex].Value.ToString();
                            command.Parameters.Add("@ul2", MySqlDbType.VarChar).Value = table[2, rowIndex].Value.ToString();


                            db.openConnection();
                            if (command.ExecuteNonQuery() == 1) { MessageBox.Show("Запись была обнавлена"); }

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
                if (e.ColumnIndex == 4)
                {
                    string task = table.Rows[e.RowIndex].Cells[4].Value.ToString();
                    if (task == "Delete")
                    {
                        if (MessageBox.Show("Удалить эту строку",
                            "Удаление", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int rowIndex = e.RowIndex;

                            DB db = new DB();
                            MySqlCommand command = new MySqlCommand("DELETE FROM `должность`" +
                                " WHERE `должность`.`id_do` = @ul ", db.getConnection());
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

        private void Dolznocti_Load(object sender, EventArgs e)
        {
            ReloadDB();
        }
    }
}
