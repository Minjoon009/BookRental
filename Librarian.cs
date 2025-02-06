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
    public partial class frmLibrarian : Form
    {
        public frmLibrarian()
        {
            InitializeComponent();
        }

        private void Librarian_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            lb.viewBooks(dgvBooks,"All");
            lblBookTotal.Text = lb.booksCount();
            dgvBooks.ReadOnly = true;
        }

        Librarian lb = new Librarian();
        Book b = new Book();
        private void btnBorrow_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(b.Title))
            {
                tabLibrarian.SelectedIndex = 1;
                txtBorrowDate.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                txtTitle.Text = b.Title;
            }
            else
            {
                MessageBox.Show("Please Select The Book", "Select Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            tabLibrarian.SelectedIndex = 2;
        }

        private void btnOverDate_Click(object sender, EventArgs e)
        {
            tabLibrarian.SelectedIndex = 3;
        }

        private void btnAddBook_Click(object sender, EventArgs e)
        {
            tabLibrarian.SelectedIndex = 5;
        }

        private void btnDeleteBook_Click(object sender, EventArgs e)
        {

        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            tabLibrarian.SelectedIndex = 5;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "" && txtPhone.Text != "")
            {
                lblName.Text = txtName.Text;
                lblPhoneNumber.Text = txtPhone.Text;
                lb.Name = txtName.Text;
                lb.Phone = txtPhone.Text;
                lb.updateInfo(Program.currentEmail, lb.Name, lb.Phone);
                MessageBox.Show("Information Updated Successfully", "Update Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Clear();
                txtPhone.Clear();
            }
            else
            {
                MessageBox.Show("Please Enter Full Information", "Empty Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            tabLibrarian.SelectedIndex = 0;
            lb.viewBooks(dgvBooks, "All");
            lblBookTotal.Text = lb.booksCount();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            b.Isbn = dgvBooks.Rows[e.RowIndex].Cells[5].Value.ToString();
            b.Title = dgvBooks.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void cboDays_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtReturnDate.Text = DateTime.Now.AddDays(Convert.ToInt32(cboDays.SelectedItem)).ToString("yyyy-MM-dd hh:mm:ss");
        }

        private void btnBooks_Click(object sender, EventArgs e)
        {
            tabLibrarian.SelectedIndex = 4;
        }

        private void lblSearch_Click(object sender, EventArgs e)
        {
            lblSearch.Hide();
            textSearch.Focus();
        }

        private void textSearch_Enter(object sender, EventArgs e)
        {
            lblSearch.Hide();
        }

        private void textSearch_Leave(object sender, EventArgs e)
        {
            if (textSearch.Text == "")
            {
                lblSearch.Show();
            }
        }

        private void cboSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvBooks.Sort(dgvBooks.Columns[cboSort.SelectedIndex + 1], ListSortDirection.Ascending);
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string reg = "^[a-zA-Z0-9]+@dragon.edu.mm$";
            if (!Regex.IsMatch(email, reg))
            {
                lblError.Text = "Invalid Email";
                return;
            }
            lblError.Text = "";
        }

        private void btnBorrowSubmit_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text != "")
            {
                if (lb.isSchoolAcc(txtEmail.Text))
                {
                    lb.borrowBook(txtEmail.Text, b.Title, b.Isbn, txtBorrowDate.Text, txtReturnDate.Text);
                    MessageBox.Show("Borrow Successfully", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lb.updateQty(b.Isbn, "borrow");
                    tabLibrarian.SelectedIndex = 0;
                    b.Title = "";
                }
                else
                {
                    MessageBox.Show("You are not our Student", "Invalid Informaton", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
            else
            {
                MessageBox.Show("Enter All Information", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void LogOut_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmLogin l = new frmLogin();
            l.ShowDialog();
        }

        private void btnDeleteBook_Click_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(b.Title))
            {
                DialogResult dr = MessageBox.Show("Are you sure you want to Delete?", "Deletion", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    lb.deleteBook(b.Isbn);
                }
            }
            else
            {
                MessageBox.Show("Please Select The Book", "Select Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void textSearch_TextChanged(object sender, EventArgs e)
        {
            lb.viewBooks(dgvBooks, textSearch.Text);
        }
    }
}
