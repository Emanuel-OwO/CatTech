using Project_CatTech.Layer.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_CatTech
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new frmPrincipal());

            frmLogin frmLogin = new frmLogin();

            frmLogin.ShowDialog();

            if (frmLogin.DialogResult == DialogResult.OK)
            {
                Application.Run(new frmPrincipal());
            }

        }
    }
}
