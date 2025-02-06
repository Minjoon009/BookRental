using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRental.Class
{
    internal class Book
    {
        private int bookId;
        private string title;
        private string author;
        private string genre;
        private string year_published;
        private string isbn;
        private string description;
        private string image;
        private int qty;
        private int isAvailable;

        public int BookId
        {
            get { return bookId; }
            set { bookId = value; }
        }
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        public string Author
        {
            get { return author; }
            set { author = value; }
        }
        public string Genre
        {
            get { return genre; }
            set { genre = value; }
        }
        public string Year_published
        {
            get { return year_published; }
            set { year_published = value; }
        }
        public string Isbn
        {
            get { return isbn; }
            set { isbn = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public string Image
        {
            get { return image; }
            set { image = value; }
        }

        public int Qty
        {
            get { return qty; }
            set { qty = value; }
        }
        public int IsAvailable
        {
            get { return isAvailable; }
            set { isAvailable = value; }
        }

    }
}
