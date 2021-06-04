using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laba5
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form3 form3 = new Form3();
            DateTime end = DateTime.Now + TimeSpan.FromSeconds(10);
            form3.Show();
            while (end > DateTime.Now)
            {
                Application.DoEvents();
            }
            form3.Close();
            form3.Dispose();
            Application.Run(new Form1());
        }
    }
}
