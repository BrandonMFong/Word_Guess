using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace Project2_Word_Guess
{
    public partial class Form1 : Form
    {
        int line = 0;
        ArrayList Already_Guessed_Letters = new ArrayList();
        bool flag0 = false; //for the fact that the user already guessed this letter
        bool UserWon = false;
        bool UserLost = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Start/Reset button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            UserWon = false;
            UserLost = false;
            flag0 = false;

            Already_Guessed_Letters.Clear();
            textBox1.Enabled = true;
            Game_Handler.Correct_Guesses = 0;
            submit.Visible = true;
            Game_Handler.Strikes = 0;
            dealloc_labels(Label_Handler.Label_Number);// going to go through with it but tests if label_number is 0

            //reseting form controls
            this.label3.Visible = false; // lose banner 
            this.submit.Enabled = true; //turn the submit button back on
            this.textBox1.Enabled = true;
            this.pictureBox1.Image = global::Project2_Word_Guess.Properties.Resources.hangman_0;

            // reset variables
            Label_Handler.X = Label_Handler.DEFAULT_LOCATION; //sets location back to the first place

            rst_button.Text         = "Reset"; // changes the title, this is originally the reset button 
            Random Number_Generator = new Random();
            
            foreach (string x in Game_Handler.words) { Game_Handler.Lines++; } // counts how many words was put into the string array
            line    = Number_Generator.Next(0, Game_Handler.Lines); // chooses a number 0 to the number words in the string array
            Game_Handler.Lines       = 0;
            Label_Handler.letter      = new char[Game_Handler.words[line].Length]; // making an array of a char that is the size of word we are guessing

            Make_Label(Game_Handler.words[line].ToUpper()); // converts to all uppercase
        }

        /// <summary>
        /// The Submit button, checks if letter is in word
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            Boolean match = false; //set to false when you first enter function
            if (this.textBox1.Text.Length == 1)
            {
                if (Already_Guessed_Letters.Contains(this.textBox1.Text))
                {
                    label2.Text = "Already guessed \nthis letter!";
                    flag0 = true;
                    goto already_guessed;
                }
                else Already_Guessed_Letters.Add(this.textBox1.Text);
                char guess_letter = Convert.ToChar((this.textBox1.Text).ToUpper()); // converting to upper case

                for (int j = 0; j < Label_Handler.letter.Length; j++)
                {
                    if ((Label_Handler.letter[j] == guess_letter))
                    {
                        
                        Label_Handler.labels[j].Text = Convert.ToString(Label_Handler.letter[j]);
                        Game_Handler.Correct_Guesses++;
                        // This for loop is for the case where there are more than one of the same letter
                        for(int k = j+1; k < Label_Handler.letter.Length; k++)
                        {
                            if ((Label_Handler.letter[k] == guess_letter))
                            {
                                Label_Handler.labels[k].Text = Convert.ToString(Label_Handler.letter[k]);
                                Game_Handler.Correct_Guesses++;
                            }
                        }
                        
                        match = true;
                        break;
                    }
                }
            }
            else
            {
                label2.Text = "Must be a single \nletter!";
                if (!flag0) Already_Guessed_Letters.RemoveAt(Already_Guessed_Letters.Count - 1); //case where user accidentally puts a space infront of the letter and submits that submits again with no space, basically taking that letter 
                
                return;
            }

        already_guessed://go here if the user already guessed this word

            
            if(!match)
            {
                if(!flag0) this.label2.Text = "Sorry, Try again!";
                if (Game_Handler.Strikes == 8) YOU_LOST();
                else
                {
                    Game_Handler.Strikes = (!flag0) ? Game_Handler.Strikes + 1 : Game_Handler.Strikes;
                }
            }
            else
            {
                if (Label_Handler.Label_Number == Game_Handler.Correct_Guesses) YOU_WON();
            }

            // Gives the user an image of how far they are in the game
            if (!UserWon && !UserLost)
            {
                switch (Game_Handler.Strikes)
                {
                    case 0: this.pictureBox1.Image = global::Project2_Word_Guess.Properties.Resources.hangman_0; break;
                    case 1: this.pictureBox1.Image = global::Project2_Word_Guess.Properties.Resources.hangman_1; break;
                    case 2: this.pictureBox1.Image = global::Project2_Word_Guess.Properties.Resources.hangman_2; break;
                    case 3: this.pictureBox1.Image = global::Project2_Word_Guess.Properties.Resources.hangman_3; break;
                    case 4: this.pictureBox1.Image = global::Project2_Word_Guess.Properties.Resources.hangman_4; break;
                    case 5: this.pictureBox1.Image = global::Project2_Word_Guess.Properties.Resources.hangman_5; break;
                    case 6: this.pictureBox1.Image = global::Project2_Word_Guess.Properties.Resources.hangman_6; break;
                    case 7: this.pictureBox1.Image = global::Project2_Word_Guess.Properties.Resources.hangman_7; break;
                    case 8: this.pictureBox1.Image = global::Project2_Word_Guess.Properties.Resources.hangman_8; break;
                    case 9: this.pictureBox1.Image = global::Project2_Word_Guess.Properties.Resources.hangman_9; break;
                }
            }
            flag0 = false;
        }

        /// <summary>
        /// Bringing back to default text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_TextChanged(object sender, EventArgs e) { this.label2.Text = "Guess the Letter!\r\n"; }
        /// <summary>
        /// Deletes labels used for the last word
        /// </summary>
        /// <param name="Label_Number"></param>
        public void dealloc_labels(int Label_Number)
        {
            if (Label_Number != 0)
            {
                for (int j = 0; j < Label_Number; j++)
                {
                    this.Controls.Remove(Label_Handler.labels[j]);
                }

            }

        }
        public void YOU_LOST()
        {
            this.label3.Visible = true;
            this.submit.Enabled = false;
            this.textBox1.Text = " ";
            this.textBox1.Enabled = false;
            this.rst_button.Text = "Restart";
            UserLost = true;
            switch (Game_Handler.words[line])
            {
                // TODO configure correct resources
                case "Adobo": { this.pictureBox1.Image = global::Project2_Word_Guess.Properties.Resources.Adobo; this.label3.Text += "\n Word: Adobo"; break; }
                case "Lechon": { this.pictureBox1.Image = global::Project2_Word_Guess.Properties.Resources.Lechon; this.label3.Text += "\n Word: Lechon"; break; }
                case "Sisig": { this.pictureBox1.Image = global::Project2_Word_Guess.Properties.Resources.Sisig; this.label3.Text += "\n Word: Sisig"; break; }
                case "Pancit": { this.pictureBox1.Image = global::Project2_Word_Guess.Properties.Resources.Pancit; this.label3.Text += "\n Word: Pancit"; break; }
                case "Kamaro": { this.pictureBox1.Image = global::Project2_Word_Guess.Properties.Resources.Kamaro; this.label3.Text += "\n Word: Kamaro"; break; }
                case "Sinigang": { this.pictureBox1.Image = global::Project2_Word_Guess.Properties.Resources.Sinigang; this.label3.Text += "\n Word: Sinigang"; break; }
                case "Dinuguan": { this.pictureBox1.Image = global::Project2_Word_Guess.Properties.Resources.Dinuguan; this.label3.Text += "\n Word: Dinuguan"; break; }
            }
        }
        public void YOU_WON()
        {
            this.label3.Visible = true;
            this.label3.Text = "You Won!";
            this.submit.Enabled = false;
            this.textBox1.Text = " ";
            this.textBox1.Enabled = false;
            this.rst_button.Text = "Start";
            UserWon = true;
            //give back probability of rights vs wrong out of hangman parts
            switch (Game_Handler.words[line])
            {
                // TODO configure correct resources
                case "Adobo": { this.pictureBox1.Image = global::Project2_Word_Guess.Properties.Resources.Adobo; this.label3.Text += "\n Word: Adobo"; break; }
                case "Lechon": { this.pictureBox1.Image = global::Project2_Word_Guess.Properties.Resources.Lechon; this.label3.Text += "\n Word: Lechon"; break; }
                case "Sisig": { this.pictureBox1.Image = global::Project2_Word_Guess.Properties.Resources.Sisig; this.label3.Text += "\n Word: Sisig"; break; }
                case "Pancit": { this.pictureBox1.Image = global::Project2_Word_Guess.Properties.Resources.Pancit; this.label3.Text += "\n Word: Pancit"; break; }
                case "Kamaro": { this.pictureBox1.Image = global::Project2_Word_Guess.Properties.Resources.Kamaro; this.label3.Text += "\n Word: Kamaro"; break; }
                case "Sinigang": { this.pictureBox1.Image = global::Project2_Word_Guess.Properties.Resources.Sinigang; this.label3.Text += "\n Word: Sinigang"; break; }
                case "Dinuguan": { this.pictureBox1.Image = global::Project2_Word_Guess.Properties.Resources.Dinuguan; this.label3.Text += "\n Word: Dinuguan"; break; }
            }
        }

        /// <summary>
        /// Makes the labels for each letter of the word we need to guess
        /// </summary>
        /// <param name="word"></param>
        public void Make_Label(string word)
        {
            int i;
            for (i = 0; i < word.Length; i++)
            {
                Label_Handler.labels[i] = new System.Windows.Forms.Label();
                Label_Handler.labels[i].AutoSize = true;
                Label_Handler.labels[i].BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                Label_Handler.labels[i].Font = new System.Drawing.Font
                                                ("Microsoft YaHei UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                Label_Handler.labels[i].Location = new System.Drawing.Point(Label_Handler.X, 461);
                Label_Handler.labels[i].Name = "Array_Label";
                Label_Handler.labels[i].Size = new System.Drawing.Size(84, 28);
                Label_Handler.labels[i].TabIndex = 6;
                Label_Handler.letter[i] = word[i]; // putting letter of the word in the global char array
                Label_Handler.labels[i].Text = "____"; // creating place holder
                Label_Handler.labels[i].Visible = true;
                this.Controls.Add(Label_Handler.labels[i]); // i need this to add to form
                Label_Handler.X = Label_Handler.X + Label_Handler.LABEL_INDEX;
            }
            Label_Handler.Label_Number = i;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
