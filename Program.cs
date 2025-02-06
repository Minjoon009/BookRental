using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookRental
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static string currentName = "";
        public static string currentPhone = "";
        public static string currentEmail = "aungheinhtet@dragon.edu.mm";
        public static string currentGmail = "";
        public static int currentPoints = 0;
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new frmRegister());
            //Application.Run(new frmStudentAffair());
            //Application.Run(new frmStudent());
            Application.Run(new frmLibrarian());
            //Application.Run(new frmAdmin());
            //Application.Run(new frmSA());



        }
    }
}
