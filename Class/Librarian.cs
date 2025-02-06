using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookRental.Class
{
    internal class Librarian : User
    {
        ConnectDatabase db = new ConnectDatabase();

        public void viewBooks(DataGridView d1,string type)
        {
            db.cnOpen();
            string query;
            if (type == "All")
            {
                query = $"select Title, Author, Genre.name as Genre, Year_Published, ISBN, Qty from books join genre on books.genreId=genre.gid order by books.bookId";
            }
            else
            {
                query = $"SELECT Title, Author, Genre.name AS Genre, Year_Published, ISBN, Qty FROM books JOIN genre ON genre.gid = books.genreId WHERE title like '%{type}%' or author like '%{type}%'";
            }
            d1.DataSource=db.showDataGridView(query);
            db.cnClose();
        }
        public string booksCount()
        {
            db.cnOpen();
            string query = $"select count(*) from books";
            MySqlDataReader dr = db.dataReader(query);
            string count = "";
            while (dr.Read())
            {
                count = dr[0].ToString();
            }
            db.cnClose();

            return count;
        }
        public void borrowBook(string e,string t, string isbn,string b, string r)
        {
            db.cnOpen();
            string query = $"INSERT INTO rental (uid, bookId, borrowDate, returnDate, isSend, isPhoned, isReturned) VALUES ((SELECT userId FROM user WHERE email = '{e}' LIMIT 1), (SELECT bookId FROM books WHERE isbn = '{isbn}' LIMIT 1), '{b}', '{r}', '0', '0', '0')";
            db.executeQuery(query);
            db.cnClose();
        }
        public void updateQty(string isbn, string type)
        {
            db.cnOpen();
            string query = "";
            if (type == "borrow")
            {
                query = $"update books set qty=qty-1 where isbn='{isbn}'";
            }
            else if(type == "return")
            {
                query = $"update books set qty=qty+1 where isbn='{isbn}'";
            }
            db.executeQuery(query);
            db.cnClose();
        }
        public void deleteBook(string isbn)
        {
            db.cnOpen();
            string query =$"delete from books where isbn='{isbn}'";
            db.executeQuery(query);
            db.cnClose();
        }
        
    }
}
