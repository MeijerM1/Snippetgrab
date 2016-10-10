using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snippetgrab
{
    public partial class Form1 : Form
    {
        public User currentUser { get; set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tstbLoggedInUser.Text = currentUser.Name;
            UpdateAccountControls();
        }

        private void accountInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(0);
            UpdateAccountControls();
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
