using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Project2_Word_Guess
{
    public class Label_Handler : Form
    {
        static public Label[] labels = new Label[30]; // Don't think I will need 30+ labels
        static public char[] letter;
        public const int LABEL_INDEX = 100;
        public const int DEFAULT_LOCATION = 12;
        static private int x = DEFAULT_LOCATION;
        static private int label_number = 0;

        static public int Label_Number
        {
            get { return label_number; }
            set { label_number = value; }
        }
        static public int X
        {
            get { return x; }
            set { x = value; }
        }
        static public char[] Letter
        {
            get { return letter; }
            set { letter = value; }
        }
        public Label_Handler()
        { }
        
        

        
    }
}
