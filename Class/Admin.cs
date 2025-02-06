using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace BookRental.Class
{
    internal class Admin : User
    {
        ConnectDatabase db = new ConnectDatabase();
        public void showAccounts(DataGridView d1, string type)
        {
            string query = "";
            db.cnOpen();
            if (type == "All")
            {
                query = $"select Name, Email, Type, CreatedDate from schoolAccounts where Type<>'Admin'";
            }
            else
            {
                query = $"select Name, Email, Type, CreatedDate from schoolAccounts where Type<>'Admin' and type='{type}'";
            }

            d1.DataSource = db.showDataGridView(query);
            db.cnClose();
        }

        public void showlibAccounts(DataGridView d1, string type)
        {
            string query = "";
            db.cnOpen();
            if (type == "All")
            {
                query = $"select Name, Email, Gmail, Phone, Type from user where Type<>'Admin'";
            }
            else
            {
                query = $"select Name, Email, Gmail, Phone, Type from user where Type<>'Admin' and type='{type}'";
            }

            d1.DataSource = db.showDataGridView(query);
            db.cnClose();
        }

        public void userCount(string type, Label lb)
        {
            string query;
            if (type != "All")
            {
                query = $"select count(*) as total{type} from schoolAccounts where type = '{type}'";
            }
            else
            {
                query = $"select count(*) from schoolAccounts";
            }
            db.cnOpen();
            MySqlDataReader dr = db.dataReader(query);
            while (dr.Read())
            {
                lb.Text = dr[0].ToString();
            }
            db.cnClose();
        }
        public void deleteAccount(string email)
        {
            db.cnOpen();
            string query = $"delete from schoolAccounts where email='{email}'";
            db.executeQuery(query);
            db.cnClose();
            MessageBox.Show("Account Delete Successfully","Deletion",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
        public void updateLibAccount(string n, string e, string g, string p, string pwd)
        {
            string query = "UPDATE user SET name='" + n + "', gmail='" + g + "', phone='" + p + "'";
            db.cnOpen();
            // Check if the password is 
            if (!string.IsNullOrEmpty(pwd))
            {
                string hashed = HashPassword(pwd);
                query += ", password='" + hashed + "'"; // Add password to the query
            }
            query += " WHERE email='" + e + "'"; 
            db.executeQuery(query);
            db.cnClose();
            MessageBox.Show("Update Successfully", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void deleteLibAccount(string email)
        {
            db.cnOpen();
            string query = $"delete from user where email='{email}'";
            db.executeQuery(query);
            db.cnClose();
            MessageBox.Show("Account Delete Successfully", "Deletion", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        public void createAccount(string n, string e, string t)
        {
            db.cnOpen();
            string query = $"insert into schoolAccounts (name,email,type) values ('{n}', '{e}', '{t}')";
            db.executeQuery(query);
            db.cnClose();
            MessageBox.Show("Account Create Successfully", "Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}
