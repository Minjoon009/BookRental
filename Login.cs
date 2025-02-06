using BookRental.Class;
using MySql.Data.MySqlClient;
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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void cbxShow_CheckedChanged(object sender, EventArgs e)
        {
            ControlFunction.togglePasswordBox(txtPassword);
        }

        private void llblRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmRegister register = new frmRegister();
            this.Hide();
            register.ShowDialog();

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

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
        User u = new User();
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text != "" && txtPassword.Text != "")
            {
                u.Email = txtEmail.Text;
                u.Password = txtPassword.Text;

                // Check admin credentials first
                if (u.Email == "aung09@dragon.edu.mm" && u.Password == "Admin135790")
                {
                    this.Hide();
                    frmAdmin adm = new frmAdmin();
                    adm.Show();
                    return; // Exit after showing admin form
                }

                // Proceed with normal login
                MySqlDataReader dr = u.Login(u.Email, u.Password);
                if (dr.Read())
                {
                    u.Type = dr["type"].ToString();
                    u.Name = dr["name"].ToString();
                    u.Gmail = dr["gmail"].ToString();
                    u.Phone = dr["phone"].ToString();
                    u.Points = Convert.ToInt32(dr["points"]);

                    Program.currentName = u.Name;
                    Program.currentEmail = u.Email;
                    Program.currentPhone = u.Phone;
                    Program.currentGmail = u.Gmail;
                    Program.currentPoints = u.Points;

                    // Handle user types
                    if (u.Type == "Student")
                    {
                        if (u.Points != 0)
                        {
                            this.Hide();
                            frmStudent student = new frmStudent();
                            student.Show();
                        }
                        else
                        {
                            MessageBox.Show("Your Account is Permanently Banned", "Banned", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else if (u.Type == "SA")
                    {
                        this.Hide();
                        frmSA sa = new frmSA();
                        sa.Show();
                    }
                    else if (u.Type == "Librarian")
                    {
                        this.Hide();
                        frmLibrarian lb = new frmLibrarian();
                        lb.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Incorrect Email or Password");
                }
            }
            else
            {
                MessageBox.Show("Please Enter Informations", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
