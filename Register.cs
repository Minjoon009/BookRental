using BookRental.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookRental
{
    public partial class frmRegister : Form
    {
        public frmRegister()
        {
            InitializeComponent();
        }

        private void cbxShow_CheckedChanged(object sender, EventArgs e)
        {
            ControlFunction.togglePasswordBox(txtPassword);
        }

        private void llblRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLogin login = new frmLogin();
            this.Hide();
            login.ShowDialog();
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string reg = "^[a-zA-Z0-9]+@dragon.edu.mm$";
            if (!Regex.IsMatch(email, reg))
            {
                lblInvalidEmail.Text = "Invalid Email";
                return;
            }
            lblInvalidEmail.Text = "";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string email = txtGmail.Text;
            string reg = "^[a-zA-Z0-9]+@gmail.com$";
            if (!Regex.IsMatch(email, reg))
            {
                lblInvalidGmail.Text = "Invalid Email";
                return;
            }
            lblInvalidGmail.Text = "";
        }

        private void cbxShowConfirmPassword_CheckedChanged(object sender, EventArgs e)
        {
            ControlFunction.togglePasswordBox(txtConfirmPassword);
        }

        User u = new User();
        private void btnRegister_Click_1(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;
            string email = txtEmail.Text;
            string gmail = txtGmail.Text;
            string phone = txtPhone.Text;
            string userType = "";
            if (cboType.SelectedIndex == 0)
            {
                userType = "Student"; //for Student
            }
            else if (cboType.SelectedIndex == 1)
            {
                userType = "SA"; //for SA
            }
            else if (cboType.SelectedIndex == 2)
            {
                userType = "Librarian"; //for Librarian
            }
            else
            {
                MessageBox.Show("You must select one of user type");
                return;
            }
            //define regx
            string usernamePattern = "^[a-zA-Z0-9]*$";
            string passwordPattern = "^(?=.*[a-z])(?=.*[A-Z]).{8,16}$";

            string errorMessage = "";

            if (name == "" || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(gmail) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword) || string.IsNullOrEmpty(userType.ToString()))
            {
                errorMessage = "All information must be entered to create account";
            }
            else
            {
                if (!Regex.IsMatch(name, usernamePattern))
                {
                    errorMessage += "Username is not valid\n";
                }
                if (!Regex.IsMatch(password, passwordPattern))
                {
                    errorMessage += "Password must be enter at leat one upper and lower case letter\n";
                }
                if (password != confirmPassword)
                {
                    errorMessage += "Password does not match\n";
                }

            }
            if (!string.IsNullOrEmpty(errorMessage))
            {
                MessageBox.Show(errorMessage, "Invalid Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {

                if (u.isSchoolAcc(email)) //check school member or not
                {
                    if (u.isAlready(email) == false) //check account duplicate
                    {

                        if (u.checkType(email, userType)) //check valid acc type
                        {
                            u.Register(name, email, gmail, phone, password, userType);
                            switch (userType)
                            {
                                case "Student": MessageBox.Show("Student Account Create Sucessfully"); break;
                                case "SA": MessageBox.Show("Student Affair Account Create Sucessfully"); break;
                                case "Librarian": MessageBox.Show("Librarian Account Create Sucessfully"); break;
                            }
                            ControlFunction.ClearInput(txtName, txtEmail, txtGmail, txtPhone, txtPassword, txtConfirmPassword, cboType);
                            lblInvalidEmail.Text = "";
                            lblInvalidGmail.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("You're choosing incorrect account type.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Account Already Exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("You are not our Dragon School's member", "Invalid Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) || txtPhone.Text.Length >= 11)
            {
                e.Handled = true;

            }
            if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
                return;
            }

        }
    }
}
