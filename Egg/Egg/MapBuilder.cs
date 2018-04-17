﻿using System;
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

namespace Egg
{
    public partial class Mappy : Form
    {
        List<PictureBox> tabletButts = new List<PictureBox>();
        ImageBox chosenTile;

        public Mappy()
        {
            InitializeComponent();

            this.Height = 860;
            this.Width = 1550;

            InitializeTileBox();
            
            #region Initializing Tablet
            List<PictureBox> tabletBtns = new List<PictureBox>();

            
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

        #region Tablet Setup on Text Change
         /// <summary>
         /// Updates Tablet Buttons as it's changed
         /// </summary>
        private void HeightWidthChange(object sender, EventArgs e)
        {
            const int BASEX = 300;  // Top left corner of the container
            const int BASEY = 25;   // Top right corner of the container
            const int BASEW = 1200;  // The width of the container
            const int BASEH = 770;  // The height of the container

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
                    Controls.Remove(button);
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
                        Controls.Add(temp);
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
            //currentTile = boxSelect.Text.ToString();
            chosenTile.Image = ImageSelect(currentTile);
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
                radioTag = "nt";            //non-damaging
            else if (rad3.Checked == true)
                radioTag = "nc";            //non-collision
            else if (rad4.Checked == true)
                radioTag = "mv";            //moving
            else
                radioTag = "nt";            //neutral/normal

            PictureBox tempCopy = (PictureBox)sender;

            
            // Drop the an image equal to the current drop 

            // Clear the button if the delete is checked
            //else if (chkDeleter.Checked == true) 
            if (currentTile == "blank")
            {
                tempCopy.Image = null;
                tempCopy.Tag = null;
            }
            else
            {
                tempCopy.Image = ImageSelect(currentTile);
                tempCopy.Tag = currentTile + radioTag;
            }
            sender = tempCopy;
            
        }

        /// <summary>
        /// The function called to export the current tile positions
        /// that exist on the tablet to a text file.
        /// </summary>
        private void Export(object sender, EventArgs e)
        {
            int incrementer = 0;
            string output = "";
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
                        output += Translator(btnTag.ToString()) + btnRad + "," + Environment.NewLine;
                    else  //If there's no tag, add 00
                        output += "0000" + "," + Environment.NewLine;
                    incrementer = 0;
                }
                else if (0 <= incrementer && incrementer <= width) //If a new line isn't needed, run regularly
                {
                    if (btn.Tag != null)  //Still make sure the button has a tag
                        output += Translator(btnTag.ToString()) + btnRad + ",";
                    else  //If there's no tag, add 00
                        output += "0000" + ",";
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
                ClearTextFile(fileName);  // Clears any text currently in the file
            }
            catch (System.IO.DirectoryNotFoundException)
            {
                Console.WriteLine("Nothing to see here, just trying to debug.");
            }
            StreamWriter writer = new StreamWriter(@"..\..\..\..\Resources\levelExports\" + fileName + ".txt");
            writer.Write(height + ", " + width + ", " + Environment.NewLine);
            writer.Write(output);         // Overwrites with new text
            writer.Close();
            output = "";

            MessageBox.Show("Successfully Exported");
            
        }

        /// <summary>
        /// Helper function to clear the entire file of any text
        /// </summary>
        private void ClearTextFile(string path)
        {
            File.Delete(@"..\..\..\..\Resources\levelExports\" + path + ".txt");
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

        #region Translators

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
        /// Translates text file names for tiles back to tiles names
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public string Retranslator(string s)
        {
            switch (s)
            {
                case "b1":
                    return "LTopLeft";
                case "b2":
                    return "LTopMid";
                case "b3":
                    return "LTopRight";
                case "b4":
                    return "LMidLeft";
                case "b6":
                    return "LMidRight";
                case "b7":
                    return "LBotLeft";
                case "b8":
                    return "LBotMid";
                case "b9":
                    return "LBotRight";

                case "i1":
                    return "dTopLeft";
                case "i2":
                    return "dTopMid";
                case "i3":
                    return "dTopRight";
                case "i4":
                    return "dMidLeft";
                case "i5":
                    return "dSolid";
                case "i6":
                    return "dMidRight";
                case "i7":
                    return "dBotLeft";
                case "i8":
                    return "dBotMid";
                case "i9":
                    return "dBotRight";

                case "n1":
                    return "nLeftTop";
                case "n3":
                    return "nLeftBot";
                case "n2":
                    return "nRightTop";
                case "n4":
                    return "nRightBot";

                default:    //failsafe case
                    return "i5";
            }

        }

        #endregion

        private void MapBuilder_Load(object sender, EventArgs e)
        {
            InitialMap();
        }

        private void InitialMap()
        {
            const int BASEX = 300;  // Top left corner of the container
            const int BASEY = 25;   // Top right corner of the container
            const int BASEW = 1200;  // The width of the container
            const int BASEH = 770;  // The height of the container

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
                    Controls.Remove(button);
                }
                for (int buttonNumber = tabletButts.Count - 1; buttonNumber >= 0; buttonNumber--)
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
                        Controls.Add(temp);
                    }
                }
            }
        }

        #region Visual Tile Selector



        private void InitializeTileBox()
        {
            //fields for tile setup
            int tileTop = 10;
            int tileLeft = 0;
            int tileWidth = (boxTiles.Width - 24) / 3;
            int tileHeight = tileWidth;
            int tileBuffer = 2;
            int topBuffer = 15;

            string[,] LTiles = new string[,]
            {
                {"LTopLeft", "LTopMid",   "LTopRight" },
                {"LMidLeft", "blankTile", "LMidRight" },
                {"LBotLeft", "LBotMid",   "LBotRight" }
            };

            string[,] dTiles = new string[,]
            {
                {"dTopLeft", "dTopMid",  "dTopRight" },
                {"dMidLeft", "dSolid",   "dMidRight" },
                {"dBotLeft", "dBotMid",  "dBotRight" }
            };

            string[,] nTiles = new string[,]
            {
                {"nLeftTop", "nRightTop" },
                {"nLeftBot", "nRightBot" }
            };

            #region Adding 3x3 Tile Sets
            //loop that populates boxTiles container with first set of clickable tiles
            for (int column = 0; column < 3; column++)
            {
                for (int row = 0; row < 3; row++)
                {
                    ImageBox tempTile = new ImageBox();
                    tempTile.Height = tileHeight;
                    tempTile.Width = tileWidth;
                    tempTile.Top = (row * tileHeight) + (row * tileBuffer) + topBuffer;
                    tempTile.Left = (column * tileWidth) + (column * tileBuffer) + 4;

                    tempTile.Visible = true;
                    tempTile.Image = ImageSelect(LTiles[row, column]);
                    tempTile.Name = LTiles[row, column];


                    tempTile.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                    tempTile.SizeMode = PictureBoxSizeMode.Zoom;
                    tempTile.Click += TileClicked;

                    boxTiles.Controls.Add(tempTile);
                }
            }

            //loop that populates boxTiles container with second set of clickable tiles
            for (int column = 0; column < 3; column++)
            {
                for (int row = 0; row < 3; row++)
                {
                    ImageBox tempTile = new ImageBox();
                    tempTile.Height = tileHeight;
                    tempTile.Width = tileWidth;
                    tempTile.Top = (row * tileHeight) + (row * tileBuffer) + (topBuffer * 2) + (3 * tileHeight);
                    tempTile.Left = (column * tileWidth) + (column * tileBuffer) + 4;

                    tempTile.Visible = true;
                    tempTile.Image = ImageSelect(dTiles[row, column]);
                    tempTile.Name = dTiles[row, column];


                    tempTile.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                    tempTile.SizeMode = PictureBoxSizeMode.Zoom;
                    tempTile.Click += TileClicked;

                    boxTiles.Controls.Add(tempTile);
                }
            }
            #endregion

            #region Adding 2x2 Tile Sets

            //loop that populates boxTiles container with third set of clickable tiles
            for (int column = 0; column < 2; column++)
            {
                for (int row = 0; row < 2; row++)
                {
                    ImageBox tempTile = new ImageBox();
                    tempTile.Height = tileHeight;
                    tempTile.Width = tileWidth;
                    tempTile.Top = (row * tileHeight) + (row * tileBuffer) + (topBuffer * 6) + (6 * tileHeight);
                    tempTile.Left = (column * tileWidth) + (column * tileBuffer) + 4 + (boxTiles.Width / 6);

                    tempTile.Visible = true;
                    tempTile.Image = ImageSelect(nTiles[row, column]);
                    tempTile.Name = nTiles[row, column];


                    tempTile.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                    tempTile.SizeMode = PictureBoxSizeMode.Zoom;
                    tempTile.Click += TileClicked;

                    boxTiles.Controls.Add(tempTile);
                }
            }
            #endregion

            #region Indicator Box

            ImageBox indicator = new ImageBox();
            
            indicator.Height = tileHeight;
            indicator.Width = tileWidth;
            indicator.Top = (0 * tileHeight) + (0 * tileBuffer) + (topBuffer * 6) + (6 * tileHeight);
            indicator.Left = (0 * tileWidth) + (0 * tileBuffer) + 4 + (boxTiles.Width / 6);

            indicator.Visible = true;
            indicator.Image = ImageSelect("indicator");
            indicator.Name = "indicator";


            indicator.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            indicator.SizeMode = PictureBoxSizeMode.Zoom;
            indicator.Click += TileClicked;

            boxTiles.Controls.Add(indicator);

            #endregion
        }

        private void TileClicked(object sender, EventArgs e)
        {
            ImageBox temp = (ImageBox)sender;
            currentTile = temp.TileName;
        }


        #endregion
    }
}
