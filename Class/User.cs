using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace BookRental.Class
{
    internal class User
    {
        private int userId;
        private string name;
        private string email;
        private string gmail;
        private string phone;
        private string password;
        private string type;
        private int points;
        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        public string Gmail
        {
            get { return gmail; }
            set { gmail = value; }
        }
        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public int Points
        {
            get { return points; }
            set { points = value; }
        }
        ConnectDatabase connectDB = new ConnectDatabase();

        public string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower(); // Convert to hex string
            }
        }
        public void Register(string n, string e, string g, string ph, string pw, string t)
        {
            string query = "";
            this.name = n;
            this.email = e;
            this.gmail = g;
            this.phone = ph;
            this.password = HashPassword(pw);
            this.type = t;
            this.points = 10;
            connectDB.cnOpen();

            if (this.type != "Student")
            {
                query = $"insert into user (name,email,gmail,phone,password,type) values('{this.name}', '{this.email}', '{this.gmail}', '{this.phone}', '{this.password}', '{this.type}')";
            }
            else
            {
                query = $"insert into user (name,email,gmail,phone,password,type,points) values('{this.name}', '{this.email}', '{this.gmail}', '{this.phone}', '{this.password}', '{this.type}', '{this.points}')";
            }
            connectDB.executeQuery(query);
            connectDB.cnClose();
        }

        public MySqlDataReader Login(string e, string pw)
        {
            this.email = e;
            this.password = HashPassword(pw);
            connectDB.cnOpen();
            string query = $"select * from user where email='{this.email}' and password='{this.password}'";
            MySqlDataReader dr = connectDB.dataReader(query);
            connectDB.cnClose();
            return dr;
        }

        public bool isSchoolAcc(string e)
        {
            this.email = e;
            connectDB.cnOpen();
            string query = "Select * from schoolAccounts where email='" + this.email + "'";
            MySqlDataReader dr = connectDB.dataReader(query);
            if (dr.Read())
            {
                dr.Close();
                connectDB.cnClose();
                return true;
            }
            return false;
        }

        public bool isAlready(string e)
        {
            this.email = e;
            connectDB.cnOpen();
            string query = $"select * from schoolAccounts where email='{this.email}'";
            MySqlDataReader dr = connectDB.dataReader(query);
            if (dr.Read())
            {
                dr.Close();
                connectDB.cnClose();
                return true;
            }
            return false;
        }
        public bool checkType(string e, string t)
        {
            this.email = e;
            this.type = t;
            connectDB.cnOpen();
            string query = $"Select * from schoolAccounts where email='{this.email}' and type='{this.type}'";
            MySqlDataReader dr = connectDB.dataReader(query);
            if (dr.Read())
            {
                dr.Close();
                connectDB.cnClose();
                return true;
            }
            return false;
        }
        public void updateInfo(string e, string n, string p)
        {
            this.email = e;
            this.name = n;
            this.phone = p;

            connectDB.cnOpen();
            string query = $"update user set name='{this.name}',phone='{this.phone}' where email='{this.email}'";
            connectDB.executeQuery(query);
            connectDB.cnClose();
        }
    }
}
