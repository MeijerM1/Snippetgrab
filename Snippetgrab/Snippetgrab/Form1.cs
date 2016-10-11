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
            int selectedItem = lbAdminUsers.SelectedIndex;

            if (lbAdminUsers.SelectedIndex > -1)
            {
                User user = userRepo.GetById(selectedItem + 1);

                if (user.IsAdmin)
                    cbAdminMakeAdmin.Checked = true;
                else
                    cbAdminMakeAdmin.Checked = false;
            }
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

        private void btDeleteUser_Click(object sender, EventArgs e)
        {
            RemoveUser();
        }

        private void RemoveUser()
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this user, This action cannot be undone", "Confirm delete", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (lbAdminUsers.SelectedIndex > -1)
                {
                    userRepo.Remove(lbAdminUsers.SelectedIndex + 1);
                    UpdateAdminControls();
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
        }
    }
}
