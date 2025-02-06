using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookRental.Class
{
    internal class ConnectDatabase
    {
        public static MySqlConnection cn;
        public static MySqlConnection dataSource()
        {
            cn = new MySqlConnection("Server=localhost;Database=book;Uid=root;Pwd=root;");
            return cn;
        }

        public void cnOpen()
        {
            dataSource();
            cn.Open();
        }

        public void cnClose()
        {
            dataSource();
            cn.Close();
        }

        public void executeQuery(string query)
        {
            MySqlCommand cmd = new MySqlCommand(query, cn);
            cmd.ExecuteNonQuery();
        }

        public MySqlDataReader dataReader(string query)
        {
            MySqlCommand cmd = new MySqlCommand(query, cn);
            MySqlDataReader dr = cmd.ExecuteReader();
            return dr;
        }

        public object showDataGridView(string query)
        {
            MySqlCommand cmd = new MySqlCommand(query, cn);
            cmd.Connection = ConnectDatabase.dataSource();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = cmd;
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            BindingSource bs = new BindingSource();
            return bs.DataSource = dt;
        }


    }
}
