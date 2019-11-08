using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Project2_Word_Guess
{
    public class Game_Handler : Form1
    {
        static private int strikes = 0;
        static public string[] words = File.ReadAllLines(@"B:\COLLEGE\19_20\Fall_19\CompE_361\Projects\Project_2\Project2_Word_Guess\Words.txt"); // reads file and puts all words in words string array
        static private int lines = 0; // number elements the array has, will define when user presses the start/reset button
        static int correct_guesses = 0;

        public Game_Handler()
        {

        }

        static public int Lines
        {
            get
            {
                return lines;
            }
            set
            {
                lines = value;
            }
        }

        static public int Strikes
        {
            get { return strikes; }
            set { strikes = value; }
        }

        static public int Correct_Guesses
        {
            get { return correct_guesses; }
            set { correct_guesses = value; }
        }
    }
}
