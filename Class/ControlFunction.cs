using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookRental.Class
{
    internal class ControlFunction
    {
        public static void togglePasswordBox(TextBox txtPassword)
        {
            if (txtPassword.UseSystemPasswordChar == true)
            {
                txtPassword.UseSystemPasswordChar = false;
                return;
            }
            txtPassword.UseSystemPasswordChar = true;
        }

        public static void ClearInput(TextBox n, TextBox e, TextBox g, TextBox ph, TextBox pw,TextBox cpw, ComboBox t)
        {
            n.Clear();
            e.Clear();
            g.Clear();
            ph.Clear();
            pw.Clear();
            cpw.Clear();
            t.Text = "";
        }

    }
}
