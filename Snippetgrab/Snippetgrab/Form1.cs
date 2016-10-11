using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Snippetgrab.data;
using Snippetgrab.Logic;

namespace Snippetgrab
{
    public partial class Form1 : Form
    {
        public User currentUser { get; set; }
        UserRepository userRepo = new UserRepository(new UserSqlContext());

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tstbLoggedInUser.Text = currentUser.Name;
            if (currentUser.IsAdmin)
            {
                tstbLoggedInUser.Text = tstbLoggedInUser.Text + "(ADMIN)";
                tsbAdminPannel.Enabled = true;
            }
            else
            {
                tsbAdminPannel.Enabled = false;
            }

            UpdateAccountControls();
        }

        private void accountInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(0);
            UpdateAccountControls();
        }



        private void allToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(1);
        }

        private void allToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(2);
        }

        private void tsbAdminPannel_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(3);
            UpdateAdminControls();
        }

        private void lbAdminUsers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void UpdateAdminControls()
        {
            List<User> users = userRepo.GetAll();

            lbAdminUsers.Items.Clear();
            foreach (var i in users)
            {
                lbAdminUsers.Items.Add(i.Name);
            }
        }

        private void UpdateAccountControls()
        {
            lbAccountInfoName.Text = currentUser.Name;
            lbAccountInfoJoinDate.Text = Convert.ToString(currentUser.JoinDate);
            lbAccountInfoReputation.Text = Convert.ToString(currentUser.Reputation);
            lbAccountInfoEmail.Text = currentUser.Email;
        }


    }
}
