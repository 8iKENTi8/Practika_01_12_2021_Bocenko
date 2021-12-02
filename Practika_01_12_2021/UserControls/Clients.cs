﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Practika_01_12_2021.UserControls
{
    public partial class Clients : UserControl
    {
        public Clients()
        {
            InitializeComponent();

            Pain_Column();
        }

        DataTable tab;

        private void ReloadDB()
        {

            DB dB = new DB();

            tab = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command =
                new MySqlCommand("SELECT *, 'Update','Delete'" +
                "FROM `клиент`", dB.getConnection());

            adapter.SelectCommand = command;

            adapter.Fill(tab);

            table.DataSource = tab;
            Pain_Column();

        }

        private void Clients_Load(object sender, EventArgs e)
        {
            ReloadDB();

        }
        public  void Pain_Column()
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                table[5, i] = linkCell;
                table[5, i].Style.BackColor = Color.FromArgb(46, 169, 79);
            }

            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                table[6, i] = linkCell;
                table[6, i].Style.BackColor = Color.Tomato;
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                DataView data = tab.DefaultView;
                data.RowFilter = string.Format("`Компания` like '%{0}%'", txtSearch.Text);
                table.DataSource = data.ToTable();

                Pain_Column();
            }

            if (txtSearch.Text == "")
            {
                Pain_Column();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private bool Check(DataGridViewCellEventArgs e)
        {
            
                for (int j = 0; j < table.ColumnCount-2; j++)
               {
                    if (table.Rows[e.RowIndex].Cells[j].Value.ToString() == "")// проверяем 3-й столбец на пустые ячейки
                    {

                    table[j, e.RowIndex].Style.BackColor = Color.Tomato;
                    MessageBox.Show("Не введена запись: "+ e.RowIndex + " "+j);
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
                if (e.ColumnIndex == 5)
                {
                    string task = table.Rows[e.RowIndex].Cells[5].Value.ToString();
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
                            MySqlCommand command = new MySqlCommand("UPDATE `клиент` SET `id_k` = @ul, `Компания` = @ul1," +
                                " `Телефон` = @ul2, `Mail` = @ul3, `Адрес` = @ul4 " +
                                "WHERE `клиент`.`id_k` = @ul", db.getConnection());

                            command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = table[0, rowIndex].Value.ToString();
                            command.Parameters.Add("@ul1", MySqlDbType.VarChar).Value = table[1, rowIndex].Value.ToString();
                            command.Parameters.Add("@ul2", MySqlDbType.VarChar).Value = table[2, rowIndex].Value.ToString();
                            command.Parameters.Add("@ul3", MySqlDbType.VarChar).Value = table[3, rowIndex].Value.ToString();
                            command.Parameters.Add("@ul4", MySqlDbType.VarChar).Value = table[4, rowIndex].Value.ToString();

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
                if (e.ColumnIndex == 6)
                {
                    string task = table.Rows[e.RowIndex].Cells[6].Value.ToString();
                    if (task == "Delete")
                    {
                        if (MessageBox.Show("Удалить эту строку",
                            "Удаление", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int rowIndex = e.RowIndex;

                            DB db = new DB();
                            MySqlCommand command = new MySqlCommand("DELETE FROM `клиент`" +
                                " WHERE `клиент`.`id_k` = @ul ", db.getConnection());
                            command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = table[0, rowIndex].Value.ToString();

                            table.Rows.RemoveAt(rowIndex);

                            db.openConnection();
                            if (command.ExecuteNonQuery() == 1) { MessageBox.Show("Запист  была удалена"); }

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

        private void button2_Click(object sender, EventArgs e)
        {
            ReloadDB();
        }
    }
}
