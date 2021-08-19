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
            //If the label has not been revealed yet, reveal it
            //by changing the ForeColor to Color.Black
            Label clickedLabel = sender as Label;
            if (clickedLabel != null)
            {
                if(clickedLabel.ForeColor == Color.Black)
                {
                    return;
                }
                clickedLabel.ForeColor = Color.Black;
            }
        }
    }//END class matchingGame_Form
}//END namespace MatchingGame
