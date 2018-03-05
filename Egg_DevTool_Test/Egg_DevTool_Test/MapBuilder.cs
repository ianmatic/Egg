using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Egg_DevTool_Test
{
    public partial class MapBuilder : Form
    {
        List<Button> tabletBtns = new List<Button>();


        #region Text Output

        // for each item in along the text file, have some variable saved off invisibly
        // so that we can make a big loop and send all of those into a series of strings
        // which we can then combine into the output and print to a text file.

        string output =
    "This is me testing writing a line of text into a text file." + Environment.NewLine +
    "This is hopefully a second line.";

        private void button1_Click(object sender, EventArgs e)
        {
            StreamWriter writer = new StreamWriter("eggTest.txt");
            writer.Write(output);
            writer.Close();
        }
        #endregion


        public MapBuilder()
        {
            InitializeComponent();
            #region Adding all the buttons to tabletBtns
            /*
            for (int i = 1; i <= 135; i++)
            {
                string tempBtn = "button";
                tempBtn += i.ToString();
                tabletBtns.Add((Button)tempBtn);
            }
            

            // Attempt 2
            for (int i = 0; i >= 150; i++)
            {
                var buttonName = string.Format("btnCalc{0}", i);
                var button = Controls.Find(buttonName, true);

                if (button != null)
                {
                    tabletBtns.Add(button);
                }
            }
            */
            #endregion


            //set up a default function to change it so every button in the placement container has no text
            //hook up the default constructor in each of the tablet's buttons to this so they default to nothing
        }

        // set up a single click event so that click is recognized by 



        #region Etcetera Clicked Functions
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //do this
        }



        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void button_click(object sender, EventArgs e)
        {

        }
        #endregion
    }
}
