using BookRental.Class;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookRental
{
    public partial class frmStudent : Form
    {
        public frmStudent()
        {
            InitializeComponent();

        }

        private void frmStudent_Load(object sender, EventArgs e)
        {
            int points = Program.currentPoints;
            int total = s.bookTotal();
            lblTotal.Text = total.ToString();
            MySqlDataReader dr = s.viewBook("all", "");
            CreatePictureBoxes(dr,pBooks);
            this.WindowState = FormWindowState.Maximized;
            progressBar.Left += 140 * points / 20;
            lblPoints.Text=points.ToString();
            btnLikeBack.Visible = false;
            lblNavName.Text = Program.currentName;
            lblName.Text = Program.currentName;
            lblEmail.Text = Program.currentEmail;
            lblPhoneNumber.Text = Program.currentPhone;
        }
        Student s = new Student();
        
        private void CreatePictureBoxes(MySqlDataReader dr, Panel p)
        {
            
            p.Controls.Clear();
            p.SuspendLayout(); // Suspend layout to improve performance
            p.AutoScroll = true;
            int columns = 5; // Number of columns
            int pictureBoxWidth = 190; // Width of each PictureBox
            int pictureBoxHeight = 300; // Height of each PictureBox
            int spacing = 20; // Spacing between PictureBoxes
            int topSpace = 10; // Top space before the first PictureBox
            int i = 0;
            while (dr.Read())
            {
                Book b = new Book();
                PictureBox pictureBox = new PictureBox();
                pictureBox.Size = new Size(pictureBoxWidth, pictureBoxHeight); // Set size of PictureBox
                pictureBox.BorderStyle = BorderStyle.FixedSingle; // Optional: Add border
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage; // Optional: Adjust size mode
                b.Image = dr["image"].ToString(); // Assuming images are named Profile1, Profile2, etc.
                //pictureBox.Image = (Image)Properties.Resources.ResourceManager.GetObject(b.Image);
                string destinationFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
                string imagePath = Path.Combine(destinationFolder, b.Image);
                pictureBox.Image = Image.FromFile(imagePath);
                // Calculate the location based on the current index
                int row = i / columns; // row number
                int column = i % columns; // column number
                pictureBox.Location = new Point(column * (pictureBoxWidth + spacing) + 15,
                                                 topSpace + row * (pictureBoxHeight + spacing)); // Add top space and vertical spacing

                b.BookId = Convert.ToInt32(dr["bookId"]);
                b.Title = dr["title"].ToString();
                b.Author = dr["author"].ToString();
                b.Genre = dr["genre"].ToString();
                b.Isbn = dr["isbn"].ToString();
                b.Year_published = dr["year_published"].ToString();
                b.Description = dr["description"].ToString();

                // Click event
                pictureBox.Click += (sender, e) => PictureBox_Click(sender, e, b.BookId, b.Title, b.Author, b.Genre, b.Isbn, b.Year_published, imagePath, b.Description);
                p.Controls.Add(pictureBox);
                i++;
            }
            p.ResumeLayout(); // Resume layout after adding all controls
        }
        int bid;
        private void PictureBox_Click(object sender, EventArgs e, int id, string bName, string author, string genre, string isbn, string year, string image, string desc)
        {
            bool liked = s.checkLiked(Program.currentEmail, id);
            if (liked == true)
            {
                pboHearted.Visible = true;
                pboHeart.Visible = false;
            }
            else
            {
                pboHeart.Visible=true;
                pboHearted.Visible = false;
            }
            tabStudent.SelectedIndex = 1;
            bid = id;
            lblBookName.Text = bName;
            lblAuthor.Text = author;
            lblGenre.Text = genre;
            lblIsbn.Text = isbn;
            lblYear.Text = year;
            lblDescription.Text = desc;
            pboBookCover.Image = Image.FromFile(image);
        }
        private void btnHome_Click(object sender, EventArgs e)
        {
            btnLikeBack.Visible= false;
            tabStudent.SelectedIndex = 0;
            MySqlDataReader dr = s.viewBook("all","");
            CreatePictureBoxes(dr, pBooks);

        }

        private void btnLiked_Click(object sender, EventArgs e)
        {
            btnLikeBack.Visible = true;
            tabStudent.SelectedIndex = 2;
            int total=s.LikedbookTotal(Program.currentEmail);
            lblLiked.Text = total.ToString();
            MySqlDataReader dr = s.viewBook("liked", Program.currentEmail);
            CreatePictureBoxes(dr, pLiked);
        }

        private void btnRental_Click(object sender, EventArgs e)
        {
            tabStudent.SelectedIndex = 3;
            MySqlDataReader dr= s.viewRental(Program.currentEmail);
            s.createRentalPanels(pRental,dr);
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            tabStudent.SelectedIndex = 4;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            tabStudent.SelectedIndex = 0;
        }

        private void textSearch_Enter(object sender, EventArgs e)
        {

            tabStudent.SelectedIndex = 0;

        }

        private void textSearch_Leave(object sender, EventArgs e)
        {
            if (textSearch.Text == "")
            {
                lblSearch.Show();
            }

        }

        private void textSearch_TextChanged(object sender, EventArgs e)
        {
            string _type = textSearch.Text.Trim(); // Trim whitespace
            if (!string.IsNullOrEmpty(_type))
            {
                // Clear previous PictureBoxes
                pBooks.Controls.Clear();
                MySqlDataReader reader = s.viewBook(_type,"");
                CreatePictureBoxes(reader, pBooks);
            }
        }

        private void lblSearch_Click(object sender, EventArgs e)
        {
            lblSearch.Hide();
            textSearch.Focus();
        }

        private void lblSearch_Enter(object sender, EventArgs e)
        {
            tabStudent.SelectedIndex = 0;
            lblSearch.Hide();
        }

        private void pboHearted_Click(object sender, EventArgs e)
        {
            s.ControlLike(Program.currentEmail,bid,"dislike");
            pboHeart.Visible = true;
            pboHearted.Visible=false;
        }

        private void pboHeart_Click(object sender, EventArgs e)
        {
            s.ControlLike(Program.currentEmail, bid, "like");
            pboHeart.Visible = false;
            pboHearted.Visible = true;
            
        }

        private void btnLikeBack_Click(object sender, EventArgs e)
        {
            tabStudent.SelectedIndex = 2;
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmLogin l = new frmLogin();
            l.ShowDialog();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if(txtName.Text!="" && txtPhone.Text != "")
            {
                lblName.Text = txtName.Text;
                lblPhoneNumber.Text = txtPhone.Text;
                s.Name = txtName.Text;
                s.Phone = txtPhone.Text;
                s.updateInfo(Program.currentEmail, s.Name, s.Phone);
                MessageBox.Show("Information Updated Successfully", "Update Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Clear();
                txtPhone.Clear();
            }
            else
            {
                MessageBox.Show("Please Enter Full Information","Empty Information",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
        }
    }
}
