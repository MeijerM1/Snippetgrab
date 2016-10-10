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
    public partial class RegisterForm : Form
    {
        private UserRepository userRepo;

        public RegisterForm()
        {
            InitializeComponent();
            userRepo = new UserRepository(new UserSqlContext());
        }

        private void btCreate_Click(object sender, EventArgs e)
        {
            if (tbPassword.Text != tbPassworCheck.Text)
            {
                MessageBox.Show("Passwords do not match");
                return;
            }            

            User user = new User(tbName.Text, DateTime.Today, 0, tbEmail.Text, false, tbPassword.Text);

            if (userRepo.Insert(user))
            {
                MessageBox.Show("Account succesfully created");
                this.Hide();
                return;
            }
            else
            {
                MessageBox.Show("Something went wrong");
                return;
            }
        }
    }
}
