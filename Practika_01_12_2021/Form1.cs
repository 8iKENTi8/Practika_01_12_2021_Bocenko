using Practika_01_12_2021.UserControls;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Yclyga uc = new Yclyga();
            addUserControl(uc);
            label1.Text = DB.user_name;

            //В зависимости от роли производим следующие действия
            if(DB.user_role==-1)
                    {
                        guna2Button3.Visible = false;
                        guna2Button4.Visible = false;
                        guna2Button5.Visible = false;
                        guna2Button2.Text = "Мои договора";

            }
            


        }

      
        private void addUserControl(UserControl us)
        {
            us.Dock = DockStyle.Fill;
            panelContainer.Controls.Clear();
            panelContainer.Controls.Add(us);
            us.BringToFront();
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Yclyga uc = new Yclyga();
            addUserControl(uc);
            if(DB.user_role!=-1)
                uc.Pain_Column();

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Clients uc = new Clients();
            addUserControl(uc);
            uc.Pain_Column();
        }

        //Договора
        private void guna2Button3_Click(object sender, EventArgs e)
        {
            Dogovora uc = new Dogovora();
            addUserControl(uc);
            if (DB.user_role != -1)
                uc.Pain_Column();
           
        }

        //Сотрудники
        private void guna2Button4_Click(object sender, EventArgs e)
        {
            employee uc = new employee();
            addUserControl(uc);
            uc.Pain_Column();
        }

        //Должности
        private void guna2Button5_Click(object sender, EventArgs e)
        {
            Dolznocti uc = new Dolznocti();
            addUserControl(uc);
            uc.Pain_Column();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);

        }
    }
}
