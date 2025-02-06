using BookRental.Class;
using Org.BouncyCastle.Crypto.Macs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Xml.Linq;
using System.Text.RegularExpressions;
namespace BookRental
{
    public partial class frmAdmin : Form
    {
        public frmAdmin()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void btnAccounts_Click(object sender, EventArgs e)
        {
            tabAdmin.SelectedIndex = 0;
            adm.showAccounts(dgvAccounts, "All");
            adm.userCount("Student", lblStudentTotal);
            adm.userCount("SA", lblStudentAffairTotal);
            adm.userCount("Librarian", lblLibrarianTotal);
            adm.userCount("All", lblTotal);
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            tabAdmin.SelectedIndex = 1;
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            tabAdmin.SelectedIndex = 3;
        }

        Admin adm = new Admin();
        string _email, _name, _gmail, _phone;
        private void frmAdmin_Load(object sender, EventArgs e)
        {
            adm.showAccounts(dgvAccounts, "All");
            dgvLibAccounts.ReadOnly = true;
            dgvAccounts.ClearSelection();
            //dgvLibAccounts.ClearSelection();
            adm.userCount("Student", lblStudentTotal);
            adm.userCount("SA", lblStudentAffairTotal);
            adm.userCount("Librarian", lblLibrarianTotal);
            adm.userCount("All", lblTotal);
        }

        private void dgvAccounts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            _name = dgvLibAccounts.Rows[e.RowIndex].Cells[0].Value.ToString();
            _email = dgvLibAccounts.Rows[e.RowIndex].Cells[1].Value.ToString();
            _gmail = dgvLibAccounts.Rows[e.RowIndex].Cells[2].Value.ToString();
            _phone = dgvLibAccounts.Rows[e.RowIndex].Cells[3].Value.ToString();
            //MessageBox.Show($"{_name}, {_email}, {_gmail}, {_phone}");
        }
        Color buttonColor = Color.FromArgb(55, 200, 10);

        private void btnAccounts_MouseEnter(object sender, EventArgs e)
        {
            btnDashboard.BackColor = Color.FromArgb(55, 150, 10);
        }

        private void btnCreate_MouseEnter(object sender, EventArgs e)
        {
            btnCreate.BackColor = Color.FromArgb(55, 150, 10);
        }

        private void btnUpdate_MouseEnter(object sender, EventArgs e)
        {
            btnLibAccount.BackColor = Color.FromArgb(55, 150, 10);

        }

        private void btnProfile_MouseEnter(object sender, EventArgs e)
        {
            btnProfile.BackColor = Color.FromArgb(55, 150, 10);

        }

        private void btnAccounts_MouseLeave(object sender, EventArgs e)
        {
            btnDashboard.BackColor = buttonColor;
        }

        private void btnCreate_MouseLeave(object sender, EventArgs e)
        {
            btnCreate.BackColor = buttonColor;
        }

        private void btnUpdate_MouseLeave(object sender, EventArgs e)
        {
            btnLibAccount.BackColor = buttonColor;

        }

        private void btnProfile_MouseLeave(object sender, EventArgs e)
        {
            btnProfile.BackColor = buttonColor;
        }



        private void pStudent_Click(object sender, EventArgs e)
        {
            adm.showAccounts(dgvAccounts, "Student");
            //tabAdmin.SelectedIndex = 0;
        }
        Color pStudentColor = Color.FromArgb(50, 50, 50);
        Color pSAColor = Color.FromArgb(180, 30, 30);
        Color pLibrarianColor = Color.FromArgb(180, 130, 0);
        Color pTotalColor = Color.FromArgb(204, 85, 0);

        private void pStudent_MouseEnter(object sender, EventArgs e)
        {
            pStudent.BackColor = Color.FromArgb(80, 80, 80);
        }

        private void pStudent_MouseLeave(object sender, EventArgs e)
        {
            pStudent.BackColor = pStudentColor;
        }

        private void pSA_MouseClick(object sender, MouseEventArgs e)
        {
            adm.showAccounts(dgvAccounts, "SA");
            //tabAdmin.SelectedIndex = 0;
        }

        private void pSA_MouseEnter(object sender, EventArgs e)
        {
            pSA.BackColor = Color.FromArgb(210, 30, 30);
        }

        private void pSA_MouseLeave(object sender, EventArgs e)
        {
            pSA.BackColor = pSAColor;
        }

        private void pLibraian_MouseClick(object sender, MouseEventArgs e)
        {

            adm.showAccounts(dgvAccounts, "Librarian");
            //tabAdmin.SelectedIndex = 0;
        }

        private void pLibraian_MouseEnter(object sender, EventArgs e)
        {
            pLibraian.BackColor = Color.FromArgb(200, 150, 0);
        }

        private void pLibraian_MouseLeave(object sender, EventArgs e)
        {
            pLibraian.BackColor = pLibrarianColor;
        }

        private void btnDel_Click_1(object sender, EventArgs e)
        {
            if (_email != "" && !String.IsNullOrEmpty(_email))
            {
                DialogResult dr = MessageBox.Show("Are you sure you want to delete?", "Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    adm.deleteAccount(_email);
                }
                adm.showAccounts(dgvAccounts, "All");
            }
            else
            {
                MessageBox.Show("Please Select The Account", "Select Account", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void panel2_MouseClick(object sender, MouseEventArgs e)
        {
            adm.showAccounts(dgvAccounts, "All");
        }

        private void panel2_MouseEnter(object sender, EventArgs e)
        {
            pTotal.BackColor = Color.FromArgb(234, 100, 0);

        }

        private void panel2_MouseLeave(object sender, EventArgs e)
        {
            pTotal.BackColor = pTotalColor;

        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmLogin l = new frmLogin();
            l.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _name = txtName.Text;
            _email = txtEmail.Text;
            _gmail = txtGmail.Text;
            _phone = txtPhone.Text;
            string pwd = txtPassword.Text;
            if (!String.IsNullOrEmpty(_name) || !String.IsNullOrEmpty(_phone) || !String.IsNullOrEmpty(_gmail) || !String.IsNullOrEmpty(_email))
            {
                adm.updateLibAccount(_name, _email, _gmail, _phone, pwd);
                
            }
            else
            {
                MessageBox.Show($"Please Enter all information");
            }
        }

        private void btnCreateAccount_Click(object sender, EventArgs e)
        {
            adm.Name = txtUserName.Text;
            adm.Email = txtUserEmail.Text;
            if (cboType.SelectedIndex == 0)
            {
                adm.Type = "Student"; //for Student
            }
            else if (cboType.SelectedIndex == 1)
            {
                adm.Type = "SA"; //for SA
            }
            else if (cboType.SelectedIndex == 2)
            {
                adm.Type = "Librarian"; //for Librarian
            }
            else
            {
                MessageBox.Show("You must select one of user type");
                return;
            }
            string emailPattern = "^[a-zA-Z0-9]+@dragon.edu.mm$";
            string errorMessage = "";
            if (adm.Name == "" || string.IsNullOrEmpty(adm.Email) || string.IsNullOrEmpty(adm.Type))
            {
                errorMessage = "All information must be entered to create account";
            }
            else
            {
                if (!Regex.IsMatch(adm.Email, emailPattern))
                {
                    errorMessage += "Email is not valid\n";
                }
                else
                {
                    adm.createAccount(adm.Name,adm.Email,adm.Type);
                }
            }
            if (!string.IsNullOrEmpty(errorMessage))
            {
                MessageBox.Show(errorMessage, "Invalid Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cboType.SelectedIndex == 0)
            {
                pboProfile.Image = Properties.Resources.StudentProf;
            }
            else if(cboType.SelectedIndex == 1)
            {
                pboProfile.Image = Properties.Resources.StudentAffairProf;
            }
            else
            {
                pboProfile.Image = Properties.Resources.LibrarianProf;
            }
        }

        private void cboSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSort.SelectedIndex == 0)
            {
                dgvAccounts.Sort(dgvAccounts.Columns[0], ListSortDirection.Ascending);
            }
            else if (cboSort.SelectedIndex == 1)
            {
                dgvAccounts.Sort(dgvAccounts.Columns[1], ListSortDirection.Ascending);
            }
            else if(cboSort.SelectedIndex == 2){
                dgvAccounts.Sort(dgvAccounts.Columns[3], ListSortDirection.Ascending);

            }
        }

        private void lblSort_Click(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                dgvLibAccounts.Sort(dgvLibAccounts.Columns[0], ListSortDirection.Ascending);
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                dgvLibAccounts.Sort(dgvLibAccounts.Columns[1], ListSortDirection.Ascending);
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                dgvLibAccounts.Sort(dgvLibAccounts.Columns[3], ListSortDirection.Ascending);

            }
        }

        private void btnLibAccount_Click(object sender, EventArgs e)
        {
            tabAdmin.SelectedIndex = 4;
            adm.showlibAccounts(dgvLibAccounts, "All");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (_email != "" && !string.IsNullOrEmpty(_email))
            {
                DialogResult dr = MessageBox.Show("Are you sure you want to delete?", "Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    adm.deleteLibAccount(_email);
                }
            }
            else
            {
                MessageBox.Show("Please Select The Account", "Select Account", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvAccounts_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            _email = dgvAccounts.Rows[e.RowIndex].Cells[1].Value.ToString();
            //MessageBox.Show(_email);
        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            if(_email!="" && !string.IsNullOrEmpty(_email))
            {
                tabAdmin.SelectedIndex = 2;
                txtEmail.Text = _email;
                txtGmail.Text = _gmail;
                txtName.Text = _name;
                txtPhone.Text = _phone;
            }
            else
            {
                MessageBox.Show("Please Select The Account", "Select Account", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
