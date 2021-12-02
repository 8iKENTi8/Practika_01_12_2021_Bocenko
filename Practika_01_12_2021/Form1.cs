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
        }

        //Сотрудники
        private void guna2Button4_Click(object sender, EventArgs e)
        {
            employee uc = new employee();
            addUserControl(uc);
        }

        //Должности
        private void guna2Button5_Click(object sender, EventArgs e)
        {
            Dolznocti uc = new Dolznocti();
            addUserControl(uc);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);

        }
    }
}
