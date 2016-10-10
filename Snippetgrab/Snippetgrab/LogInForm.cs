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
    public partial class LogInForm : Form
    {
        private UserRepository userRepo;

        public LogInForm()
        {
            InitializeComponent();
            userRepo = new UserRepository(new UserSqlContext());
        }

        private void btLogIn_Click(object sender, EventArgs e)
        {
            if (tbEmail.Text == "")
            {
                tbEmail.BackColor = Color.DarkRed;
                MessageBox.Show("Some of the fields are blank");
                return;
            }
            else if (tbPassword.Text == "")
            {
                tbPassword.BackColor = Color.DarkRed;
                MessageBox.Show("Some of the fields are blank");
                return;
            }

            if (userRepo.CheckPasssword(tbEmail.Text, tbPassword.Text))
            {
                Form1 mainForm = new Form1();
                mainForm.currentUser = userRepo.GetByEmail(tbEmail.Text);
                mainForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Incorrect email or password");
            }
        }

        private void btRegister_Click(object sender, EventArgs e)
        {
            RegisterForm regForm = new RegisterForm();
            regForm.ShowDialog();
        }
    }
}
