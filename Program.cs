using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Project2_Word_Guess
{
    public class Program
    {
        // TODO - put a case for each letter that you have already submitted this letter


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            /*** DON'T TOUCH ***/
            //Label_Handler label = new Label_Handler();
            //Game_Handler game = new Game_Handler();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            
        }
    }
}
