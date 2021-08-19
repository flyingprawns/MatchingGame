using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatchingGame
{
    public partial class matchingGame_Form : Form
    {
        // --------
        // Fields
        // --------
        private Random random = new Random();
        private List<string> icons = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "k", "k",
            "b", "b", "v", "v", "w", "w", "z", "z"
        };
        private Label firstClicked = null; private Label secondClicked = null;

        // ---------
        // Methods
        // ---------
        private void AssignIconsToSquares()
        {
            foreach (Control c in matchingGame_TabelLayoutPanel.Controls)
            {
                Label iconLabel = c as Label;
                if (iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    iconLabel.ForeColor = iconLabel.BackColor; //hides the icon!
                    icons.RemoveAt(randomNumber);
                }
            }
        }
        private void CheckForWinner()
        {
            //Check all icons. If any remain hidden, do nothing.
            foreach (Control c in matchingGame_TabelLayoutPanel.Controls)
            {
                Label iconLabel = c as Label;
                if (iconLabel != null)
                {
                    bool iconHidden = iconLabel.ForeColor == iconLabel.BackColor;
                    if (iconHidden)
                    {
                        return;
                    }
                }
            }

            //All icons are revealed. Display congratulatory message.
            MessageBox.Show("You matched all the icons!", "Winner :)");
            Close();
        }

        // ---------------------------
        // Windows Form Designer Code
        // ---------------------------
        public matchingGame_Form()
        {
            InitializeComponent();
            AssignIconsToSquares();
        }

        /// <summary>
        /// Handles the click event of every cell_Label
        /// </summary>
        /// <param name="sender">The label that was clicked</param>
        /// <param name="e"></param>
        private void cell_Label_Click(object sender, EventArgs e)
        {
            //Ignore the click if the program is busy resetting a mismatch
            if(resetMismatch_Timer.Enabled == true)
            {
                return;
            }

            Label clickedLabel = sender as Label;
            if (clickedLabel != null)
            {
                //Ignore the click if label is already revealed (ForeColor == Black)
                if(clickedLabel.ForeColor == Color.Black)
                {
                    return;
                }

                //Set firstClicked to clickedLabel if applicable
                if(firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;
                    return;
                }

                //Set secondClicked to clickedLabel (At this point we know we are on the second click)
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;

                //At this point, both labels clicked by the user have been registered. CheckForWinner() ends the game if it succeeds.
                CheckForWinner();

                //If icons match, keep them revealed. Reset trackers
                if(firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }

                //Reset the mismatched icons
                resetMismatch_Timer.Start();
            }
        }

        /// <summary>
        /// When user selects a pair that doesn't match, this timer
        /// resets that pair after 0.75 seconds.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void resetMismatch_Timer_Tick(object sender, EventArgs e)
        {
            resetMismatch_Timer.Stop();

            //Hide both icons and reset trackers
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;
            firstClicked = null;
            secondClicked = null;
        }

    }//END class matchingGame_Form
}//END namespace MatchingGame
