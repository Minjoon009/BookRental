using BookRental.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookRental
{
    public partial class frmSA : Form
    {
        public frmSA()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        StudentAffair sa = new StudentAffair();
        private void SA_Load(object sender, EventArgs e)
        {
            
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            tabStudentAffair.SelectedIndex = 0;
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            tabStudentAffair.SelectedIndex = 1;
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            tabStudentAffair.SelectedIndex = 2;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {

        }

        private void btnSubmit_Click_1(object sender, EventArgs e)
        {
            if (txtName.Text != "" || txtPhone.Text != "")
            {
                lblName.Text = txtName.Text;
                lblPhoneNumber.Text = txtPhone.Text;
                sa.Name = txtName.Text;
                sa.Phone = txtPhone.Text;
                sa.updateInfo(Program.currentEmail, sa.Name, sa.Phone);
                MessageBox.Show("Information Updated Successfully", "Update Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Clear();
                txtPhone.Clear();
            }
            else
            {
                MessageBox.Show("Please Enter Full Information", "Empty Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmLogin l = new frmLogin();
            l.ShowDialog();
        }
    }
}
