using Microsoft.VisualBasic.ApplicationServices;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookRental.Class
{
    internal class Student : User
    {
        ConnectDatabase connectDB = new ConnectDatabase();
        Book b = new Book();
        public int bookTotal()
        {
            int total = 0;
            string query = "select count(*) as Total from books";
            connectDB.cnOpen();
            MySqlDataReader dr = connectDB.dataReader(query);
            if (dr.Read())
            {
                total += Convert.ToInt32(dr["Total"]);
            }
            connectDB.cnClose();
            return total;
        }

        public int LikedbookTotal(string e)
        {
            this.Email = e;
            int total = 0;
            string query = $"select count(*) as Total from liked where email='{this.Email}'";

            connectDB.cnOpen();
            MySqlDataReader dr = connectDB.dataReader(query);
            if (dr.Read())
            {
                total = Convert.ToInt32(dr["Total"]);
            }
            connectDB.cnClose();
            return total;
        }

        public MySqlDataReader viewBook(string type, string e)
        {
            string query = "";
            connectDB.cnOpen();
            this.Email = e;
            // Sanitize the input to prevent SQL injection
            type = type.Replace("'", "''"); // Escape single quotes

            if (type == "all")
            {
                query = "SELECT bookId, title, author, genre.name AS genre, year_published, isbn, image, description, qty, isAvailable FROM books JOIN genre ON genre.gid = books.genreId ORDER BY RAND()";
            }
            else if (type == "liked")
            {
                query = $"select liked.bookId,title, author, genre.name AS genre, year_published, isbn, image, description, qty, isAvailable from liked, books JOIN genre ON genre.gid = books.genreId where books.bookId=liked.bookId and liked.email='{this.Email}'";
            }
            else
            {
                query = $"SELECT bookId, title, author, genre.name AS genre, year_published, isbn, image, description, qty, isAvailable FROM books JOIN genre ON genre.gid = books.genreId WHERE title like '%{type}%' or author like '%{type}%'";
            }

            MySqlDataReader dr = connectDB.dataReader(query);
            connectDB.cnClose();
            return dr;
        }

        public bool checkLiked(string e, int id)
        {
            this.Email = e;
            b.BookId = id;
            string query = $"select * from liked where email='{this.Email}' and bookId='{b.BookId}'";
            connectDB.cnOpen();
            MySqlDataReader dr = connectDB.dataReader(query);
            connectDB.cnClose();
            if (dr.Read())
            {
                return true;
            }
            return false;
        }

        public MySqlDataReader viewRental(string email)
        {
            this.Email = email;
            connectDB.cnOpen();
            string query = $"select books.title, books.author, books.image, user.email, rental.borrowDate, rental.returnDate, rental.isReturned from rental join books on books.bookId=rental.bookId join user on user.userId=rental.uid where user.email='{Email}'";
            MySqlDataReader dr = connectDB.dataReader(query);
            connectDB.cnClose();
            return dr;
        }

        public void createRentalPanels(Panel panel, MySqlDataReader dr)
        {
            panel.Controls.Clear();
            int i = 0;
            while (dr.Read())
            {
                b.Title = dr["title"].ToString();
                b.Author = dr["author"].ToString();
                b.Image = dr["image"].ToString();
                int isReturned = Convert.ToInt32(dr["isReturned"]);
                DateTime borrowDateValue = Convert.ToDateTime(dr["borrowDate"]);
                DateTime returnDateValue = Convert.ToDateTime(dr["returnDate"]);
                string borrowDate = borrowDateValue.ToString("yyyy-MM-dd");
                string returnDate = returnDateValue.ToString("yyyy-MM-dd");
                Label title = new Label();
                Label author = new Label();
                Label lblBorrow = new Label();
                Label  lblReturn= new Label();
                Panel p = new Panel();
                PictureBox pbo = new PictureBox();
                string destinationFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
                pbo.Image = Image.FromFile(Path.Combine(destinationFolder, b.Image));
                pbo.SizeMode = PictureBoxSizeMode.StretchImage;
                pbo.Location = new Point(5, 7);
                pbo.Size = new Size(50, 80);

                //title
                title.Text = $"{b.Title}";
                title.ForeColor = Color.White;
                Font currentFont = title.Font;
                title.Font = new Font(currentFont.FontFamily, 18);
                title.Location = new Point(pbo.Width + 8, 10);
                title.AutoSize = true;

                //author
                author.Text = $"{b.Author}";
                smallLabel(author, 225, 225, 225);
                author.Location = new Point(pbo.Width + 11, 40);

                //dates
                lblBorrow.Text = "Borrow Date : "+borrowDate;
                lblReturn.Text = "Return Date : "+returnDate;
                smallLabel(lblBorrow, 220, 220, 220);
                lblBorrow.Location = new Point(panel.Width-200,20);
                lblReturn.Location = new Point(panel.Width-200, 50);
                smallLabel(lblReturn, 220, 220, 220);

                p.Controls.Add(title);
                p.Controls.Add(author);
                p.Controls.Add(lblBorrow);
                p.Controls.Add(lblReturn);

                p.Controls.Add(pbo);
                p.Size = new Size(panel.Width, 100);
                if (DateTime.Now > returnDateValue && isReturned == 0)
                {
                    p.BackColor = Color.FromArgb(120, 20, 20); //red
                }
                else if (DateTime.Now<returnDateValue)
                {
                    p.BackColor = Color.FromArgb(200, 180, 60); //yellow
                }
                if (isReturned == 1)
                {
                    p.BackColor = Color.FromArgb(40, 100, 60); //green
                }
                p.BorderStyle = BorderStyle.FixedSingle;
                p.Location = new Point(1,+ (p.Height + 3) * i);

                panel.Controls.Add(p);
                p.MouseEnter += (sender, e) => P_MouseEnter(sender, e, p);
                p.Click += (sender, e) => P_Click(sender, e);
                p.MouseLeave += (sender, e) => P_MouseLeave(sender, e, p);
                i++;
            }

        }

        public void smallLabel(Label l, int r,int g, int b)
        {
            
            Font currentFont2 = l.Font;
            l.Font = new Font(currentFont2.FontFamily, 10);
            l.ForeColor = Color.FromArgb(r,g,b);
            l.AutoSize = true;
        }

        private void P_MouseLeave(object sender, EventArgs e, Panel p)
        {
            //p.BackColor = Color.FromArgb(40, 100, 60);

        }

        private void P_MouseEnter(object sender, EventArgs e, Panel p)
        {
            //p.BackColor = Color.FromArgb(60, 120, 80);
        }
        private void P_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(name);
        }


        public void ControlLike(string e, int id, string operation)
        {
            this.Email = e;
            b.BookId = id;
            string query = "";
            connectDB.cnOpen();
            if (operation == "like")
            {
                query = $"insert into liked (bookId,email) values('{b.BookId}', '{this.Email}')";
            }
            else if (operation == "dislike")
            {
                query = $"delete from liked where bookId='{b.BookId}' and email='{this.Email}'";
            }
            connectDB.executeQuery(query);
            connectDB.cnClose();
        }
    }
}
