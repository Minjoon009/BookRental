using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BookRental.Class;

namespace BookRental
{
    public partial class frmStudentAffair : Form
    {
        public frmStudentAffair()
        {
            InitializeComponent();
            //this.WindowState = FormWindowState.Maximized;
        }

        private void frmStudentAffair_Load(object sender, EventArgs e)
        {
            
        }

        
        private void btnProfile_Click(object sender, EventArgs e)
        {
           tabStudentAffair.SelectedIndex = 1;
        }

        //function panel
       
        private void btnLogOut_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            tabStudentAffair.SelectedIndex = 0;
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            tabStudentAffair.SelectedIndex = 2;
        }

       
    }
}