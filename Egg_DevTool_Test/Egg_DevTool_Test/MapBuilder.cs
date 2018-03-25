using System;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Input;

namespace Egg_DevTool_Test
{
    public partial class Mappy : Form
    {
        List<PictureBox> tabletButts = new List<PictureBox>();

        #region Text Output

        // for each item in along the text file, have some variable saved off invisibly
        // so that we can make a big loop and send all of those into a series of strings
        // which we can then combine into the output and print to a text file.

        string output = "" + Environment.NewLine;

        private void button1_Click(object sender, EventArgs e)
        {
            StreamWriter writer = new StreamWriter("eggTest.txt");
            writer.Write(output);
            writer.Close();
        }
        #endregion

        public Mappy()
        {
            InitializeComponent();
            string desperation = boxSelect.Text.ToString();

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
            List<PictureBox> tabletBtns = new List<PictureBox>();

            #region grossly hand-inputting all the buttons
            /*
            tabletBtns.Add(button1);
            tabletBtns.Add(button2);
            tabletBtns.Add(button3);
            tabletBtns.Add(button4);
            tabletBtns.Add(button5);
            tabletBtns.Add(button6);
            tabletBtns.Add(button7);
            tabletBtns.Add(button8);
            tabletBtns.Add(button9);
            tabletBtns.Add(button10);
            tabletBtns.Add(button11);
            tabletBtns.Add(button12);
            tabletBtns.Add(button13);
            tabletBtns.Add(button14);
            tabletBtns.Add(button15);
            tabletBtns.Add(button16);
            tabletBtns.Add(button17);
            tabletBtns.Add(button18);
            tabletBtns.Add(button19);
            tabletBtns.Add(button20);
            tabletBtns.Add(button21);
            tabletBtns.Add(button22);
            tabletBtns.Add(button23);
            tabletBtns.Add(button24);
            tabletBtns.Add(button25);
            tabletBtns.Add(button26);
            tabletBtns.Add(button27);
            tabletBtns.Add(button28);
            tabletBtns.Add(button29);
            tabletBtns.Add(button30);
            tabletBtns.Add(button31);
            tabletBtns.Add(button32);
            tabletBtns.Add(button33);
            tabletBtns.Add(button34);
            tabletBtns.Add(button35);
            tabletBtns.Add(button36);
            tabletBtns.Add(button37);
            tabletBtns.Add(button38);
            tabletBtns.Add(button39);
            tabletBtns.Add(button40);
            tabletBtns.Add(button41);
            tabletBtns.Add(button42);
            tabletBtns.Add(button43);
            tabletBtns.Add(button44);
            tabletBtns.Add(button45);
            tabletBtns.Add(button46);
            tabletBtns.Add(button47);
            tabletBtns.Add(button48);
            tabletBtns.Add(button49);
            tabletBtns.Add(button50);
            tabletBtns.Add(button51);
            tabletBtns.Add(button52);
            tabletBtns.Add(button53);
            tabletBtns.Add(button54);
            tabletBtns.Add(button55);
            tabletBtns.Add(button56);
            tabletBtns.Add(button57);
            tabletBtns.Add(button58);
            tabletBtns.Add(button59);
            tabletBtns.Add(button60);
            tabletBtns.Add(button61);
            tabletBtns.Add(button62);
            tabletBtns.Add(button63);
            tabletBtns.Add(button64);
            tabletBtns.Add(button65);
            tabletBtns.Add(button66);
            tabletBtns.Add(button67);
            tabletBtns.Add(button68);
            tabletBtns.Add(button69);
            tabletBtns.Add(button70);
            tabletBtns.Add(button71);
            tabletBtns.Add(button72);
            tabletBtns.Add(button73);
            tabletBtns.Add(button74);
            tabletBtns.Add(button75);
            tabletBtns.Add(button76);
            tabletBtns.Add(button77);
            tabletBtns.Add(button78);
            tabletBtns.Add(button79);
            tabletBtns.Add(button80);
            tabletBtns.Add(button81);
            tabletBtns.Add(button82);
            tabletBtns.Add(button83);
            tabletBtns.Add(button84);
            tabletBtns.Add(button85);
            tabletBtns.Add(button86);
            tabletBtns.Add(button87);
            tabletBtns.Add(button88);
            tabletBtns.Add(button89);
            tabletBtns.Add(button90);
            tabletBtns.Add(button91);
            tabletBtns.Add(button92);
            tabletBtns.Add(button93);
            tabletBtns.Add(button94);
            tabletBtns.Add(button95);
            tabletBtns.Add(button96);
            tabletBtns.Add(button97);
            tabletBtns.Add(button98);
            tabletBtns.Add(button99);
            tabletBtns.Add(button100);
            tabletBtns.Add(button101);
            tabletBtns.Add(button102);
            tabletBtns.Add(button103);
            tabletBtns.Add(button104);
            tabletBtns.Add(button105);
            tabletBtns.Add(button106);
            tabletBtns.Add(button107);
            tabletBtns.Add(button108);
            tabletBtns.Add(button109);
            tabletBtns.Add(button110);
            tabletBtns.Add(button111);
            tabletBtns.Add(button112);
            tabletBtns.Add(button113);
            tabletBtns.Add(button114);
            tabletBtns.Add(button115);
            tabletBtns.Add(button116);
            tabletBtns.Add(button117);
            tabletBtns.Add(button118);
            tabletBtns.Add(button119);
            tabletBtns.Add(button120);
            tabletBtns.Add(button121);
            tabletBtns.Add(button122);
            tabletBtns.Add(button123);
            tabletBtns.Add(button124);
            tabletBtns.Add(button125);
            tabletBtns.Add(button126);
            tabletBtns.Add(button127);
            tabletBtns.Add(button128);
            tabletBtns.Add(button129);
            tabletBtns.Add(button130);
            tabletBtns.Add(button131);
            tabletBtns.Add(button132);
            tabletBtns.Add(button133);
            tabletBtns.Add(button134);
            tabletBtns.Add(button135);
            tabletBtns.Add(button136);
            tabletBtns.Add(button137);
            tabletBtns.Add(button138);
            tabletBtns.Add(button139);
            tabletBtns.Add(button140);
            tabletBtns.Add(button141);
            tabletBtns.Add(button142);
            tabletBtns.Add(button143);
            tabletBtns.Add(button144);
            tabletBtns.Add(button145);
            tabletBtns.Add(button146);
            tabletBtns.Add(button147);
            tabletBtns.Add(button148);
            tabletBtns.Add(button149);
            tabletBtns.Add(button150);
            */
            #endregion

            int xInc = 0;   //x incrementer
            int yInc = 1;   //y incrementer
            foreach (var btn in tabletBtns)
            {
                xInc++;
                if (xInc > 15)
                {
                    yInc++;
                    xInc = 1;
                }
                btn.Text = "(" + xInc.ToString() + ", " + yInc + ")";
                btn.ForeColor = Color.White;
            }

            tabletButts = tabletBtns;
            #endregion
        }

        /// <summary>
        /// Function to test button creation, no longer in use
        /// </summary>
        private void ButtonTest(object sender, EventArgs e)
        {
            int top = 200;
            int left = 400;

            PictureBox button = new PictureBox();
            button.Left = left;
            button.Top = top;
            tabPage1.Controls.Add(button);
        }
        #region Tablet Setup on Text Change
         /// <summary>
         /// Updates Tablet Buttons as it's changed
         /// </summary>
        private void HeightWidthChange(object sender, EventArgs e)
        {
            const int BASEX = 220;  // Top left corner of the container
            const int BASEY = 40;   // Top right corner of the container
            const int BASEW = 830;  // The width of the container
            const int BASEH = 550;  // The height of the container

            int btnX;       // X Position field
            int btnY;       // Y Position field
            int btnWidth;   // How wide the buttons are
            int btnHeight;  // How tall the buttons are

            int btnStacks; // How many buttons tall (pulled from form)
            int.TryParse(tabletHeight.Text, out btnStacks);
            int btnRows;  // How many buttons wide (pulled from form)
            int.TryParse(tabletWidth.Text, out btnRows);

            if (btnRows == 0 || btnStacks == 0)  //Check if the tablet's height and width will yeild a result
            {
                return; // Break the function if nothing it's not going to work
            }
            else // If there will be a result, change the tablet's setup
            {
                // First, clear all currently existing buttons in tabletButts
                foreach (var button in tabletButts)
                {
                    tabPage1.Controls.Remove(button);
                }
                for (int buttonNumber = tabletButts.Count-1; buttonNumber >= 0; buttonNumber--)
                {
                    tabletButts.Remove(tabletButts[buttonNumber]);
                } 

                 // Next, recreate all of the buttons according to the new height and widths
                for (int h = 1; h <= btnStacks; h++)
                {
                    for (int w = 1; w <= btnRows; w++)
                    {
                        btnWidth = BASEW / btnRows;
                        btnHeight = BASEH / btnStacks;
                        btnX = BASEX + ((w - 1) * btnWidth);
                        btnY = BASEY + ((h - 1) * btnHeight);

                        ImageBox temp = new ImageBox();
                        temp.Height = btnHeight;
                        temp.Width = btnWidth;
                        temp.Left = btnX;
                        temp.Top = btnY;
                        temp.Visible = true;
                        temp.Image = ImageSelect("blankTile");
                        temp.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                        temp.SizeMode = PictureBoxSizeMode.Zoom;
                        temp.Click += TabletClick;

                        tabletButts.Add(temp);
                        tabPage1.Controls.Add(temp);
                    }
                }
            }
        }
        #endregion

        #region Tablet Functionality
        //The current tile
        string currentTile = "";

        //Enumerator to keep track of which tiles is in which place

        /// <summary>
        /// Change the designer based on which box index selection was made
        /// </summary>
        private void BoxIndexChanged(object sender, EventArgs e)
        {
            currentTile = boxSelect.Text.ToString();
            tileView.Image = ImageSelect(currentTile);
        }

        /// <summary>
        /// Takes a string S from the current dropdown list then returns
        /// any image that has that as the name from the resources folder.
        /// 
        /// Also catches empty inputs and displays an error screen
        /// </summary>
        private Image ImageSelect (string s)
        {
            // modify the image to fit properly inside the button's image. 
            // also put in a catch to check if that image actually exists
            Image test;
            try
            {
                test = Image.FromFile(@"..\..\..\..\Resources\" + s + ".png");
            }
            catch (System.IO.FileNotFoundException)
            {
                Form broken = new Form() { Width = 300 , Height = 20};
                MessageBox.Show("Please select an image from the drop-list to the left.");
                test = Image.FromFile(@"..\..\..\..\Resources\dSolid.png");
            }
            return test;
        }


        /// <summary>
        /// The base function called every time a tablet button
        /// is clicked by the user
        /// </summary>
        private void TabletClick(object sender, EventArgs e)
        {
            string radioTag;
            if (rad1.Checked == true)
                radioTag = "dm";            //damaging
            else if (rad2.Checked == true)
                radioTag = "nd";            //non-damaging
            else if (rad3.Checked == true)
                radioTag = "nc";            //non-collision
            else if (rad4.Checked == true)
                radioTag = "mv";            //moving
            else
                radioTag = "nt";            //neutral/normal

            PictureBox tempCopy = (PictureBox)sender;
            // Drop the an image equal to the current drop 
            if (chkDeleter.Checked == false)    
            {
                tempCopy.Image = ImageSelect(currentTile);
                tempCopy.Tag = currentTile + radioTag;
            }
            // Clear the button if the delete is checked
            else if (chkDeleter.Checked == true) 
            {
                tempCopy.Image = null;
                tempCopy.Tag = null;
            }
            sender = tempCopy;
        }


        int incrementer = 0;
        string outputTest = "";
        /// <summary>
        /// The function called to export the current tile positions
        /// that exist on the tablet to a text file.
        /// </summary>
        private void Export(object sender, EventArgs e)
        {
            int width;
            int.TryParse(tabletWidth.Text, out width);
            width -= 1;

            int height;
            int.TryParse(tabletHeight.Text, out height);

            foreach (var btn in tabletButts)
            {
                string btnTag = "";
                string btnRad = "";
                if (btn.Tag != null) //Splits up the tag into the tile and the radio tag
                {
                    char[] tagSplit = btn.Tag.ToString().ToCharArray();
                    for (int i = 0; i < tagSplit.Length; i++)
                    {
                        if (i >= tagSplit.Length-2)
                            btnRad += tagSplit[i].ToString();
                        else
                            btnTag += tagSplit[i].ToString();
                    }
                }

                if (incrementer >= width)  //Check if a new line is needed and add if it is
                {
                    if (btn.Tag != null)  //Make sure the button has a tag
                        outputTest += Translator(btnTag.ToString()) + btnRad + "," + Environment.NewLine;
                    else  //If there's no tag, add 00
                        outputTest += "0000" + "," + Environment.NewLine;
                    incrementer = 0;
                }
                else if (0 <= incrementer && incrementer <= width) //If a new line isn't needed, run regularly
                {
                    if (btn.Tag != null)  //Still make sure the button has a tag
                        outputTest += Translator(btnTag.ToString()) + btnRad + ",";
                    else  //If there's no tag, add 00
                        outputTest += "0000" + ",";
                    incrementer++;
                }
            }

            //Reset incrementer when the export is complete
            incrementer = 0;
            width += 1;

            // Actually export to a text file
            string fileName = txtFile.Text;
            try
            {
                ClearTextFile(@"..\..\..\..\Resources\levelExports\" + fileName + ".txt");  // Clears any text currently in the file
            }
            catch (System.IO.DirectoryNotFoundException)
            {
                Console.WriteLine("Nothing to see here, just trying to debug.");
            }
            StreamWriter writer = new StreamWriter(@"..\..\..\..\Resources\levelExports\" + fileName + ".txt");
            writer.Write(height + ", " + width + ", " + Environment.NewLine);
            writer.Write(outputTest);         // Overwrites with new text
            writer.Close();
            outputTest = "";

            MessageBox.Show("Successfully Exported");
            
        }

        /// <summary>
        /// Takes in a string parameter "s" which is then run through a switch
        /// statement to convert and return it in it's encoded form for the exporter
        /// </summary>
        private string Translator(string s)
        {
            switch (s)
            {
                    // Light grass tiles
                case "LTopLeft":
                    return "b1";
                case "LTopMid":
                    return "b2";
                case "LTopRight":
                    return "b3";
                case "LMidRight":
                    return "b4";
                    // b5 doesn't exist as there's no middle tile for the 
                    // exterior/light wrapping tiles
                case "LMidLeft":
                    return "b6";
                case "LBotLeft":
                    return "b7";
                case "LBotMid":
                    return "b8";
                case "LBotRight":
                    return "b9";

                // Light interior tiles
                case "nLeftTop":
                    return "n1";
                case "nRightTop":
                    return "n2";
                case "nLeftBot":
                    return "n3";
                case "nRightBot":
                    return "n4";

                    // Dark grass tiles
                case "dTopLeft":
                    return "i1";
                case "dTopMid":
                    return "i2";
                case "dTopRight":
                    return "i3";
                case "dMidLeft":
                    return "i4";
                case "dSolid":
                    return "i5";
                case "dMidRight":
                    return "i6";
                case "dBotLeft":
                    return "i7";
                case "dBotMid":
                    return "i8";
                case "dBotRight":
                    return "i9";


                default:
                    return "#### TRANSLATOR BROKEN #####";
            }
        }

        /// <summary>
        /// Helper function to clear the entire file of any text
        /// </summary>
        private void ClearTextFile(string path)
        {
            File.Delete(path);
        }
        #endregion

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


        private void MapBuilder_Load(object sender, EventArgs e)
        {
        }
    }
}
