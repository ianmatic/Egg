using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Egg
{
    //Represents a single screen within a level.
    class Screen
    {
        //interpreter fields
        string[,] level;
        StreamReader interpreter;


        Tile[,] tileList;
        Enemy[] listOfEnemies;
        int tileX;
        int tileY;

        int lengthX; // (Tiles in X direction)*(Length of a side of a single tile)
        int lengthY; // (Tiles in Y direction)*(Length of a side of a single tile)

        public Screen(string textFile)
        {

        }

        public void LevelInterpreter(string s)
        {
            //fields
            string line;
            int row;
            int col;
            int c = 0;
            string tempString = "";
            string[] split;
           

            interpreter = new StreamReader(s + ".txt");
            line = interpreter.ReadLine(); //reads FIRST line only
            split = line.Split(','); //splits into array of 2

            row = int.Parse(split[0]); //reads rows of level array
            col = int.Parse(split[1]); //reads collumns of level array
            level = new string[row, col]; //creates level array with determined dimensions
            string array = interpreter.ReadToEnd();
            split = array.Split(','); // for last one when drawing skip it


            for (int i = 0; i <= split.Length - 2; i++) //getting rid of \r\n
            {
                tempString = split[i]; //fixes everything going null 
                char[] temp = split[i].ToCharArray(); //creates a char array
                if (temp.Length > 4) //checks if tile ID is more than 4 charcters (\r\n count as 2 characters)
                {
                    tempString = ""; //clears tempString so we dont get \r\nb1nt b1nt
                    char space = ' ';
                    //make first two char spaces
                    temp[0] = space;
                    temp[1] = space;
                    foreach (var item in temp) //go through every char in temp
                    {
                        if (item != ' ') tempString += item.ToString(); //if char is not a space add it to tile ID temp string
                    }
                }
                split[i] = tempString;
            }





            for (int i = 0; i <= level.GetLength(0) - 1; i++)
            {

                for (int j = 0; j <= level.GetLength(1) - 1; j++)
                {

                    level[i, j] = split[c]; //sets tileID in level array 

                    c++; //increments c because it is seperate array of different dimensions
                }

            }


            interpreter.Close();
        }


        public void DrawLevel(string[,] level)
        {
            //fields
            
            
            // graphics device doesnt work for some reason

            //int tileWidth = GraphicsDevice.Viewport.Width / level.GetLength(0);
           // int tileHeight = GraphicsDevice.Viewport.Height / level.GetLength(1);
            int x = level.GetLength(0) - 1;
            int y = level.GetLength(1) - 1;
            List<Tile> tileList = new List<Tile>();

            string temp;
            char[] array;
            Tile tileTemp;
            for (int i = 0; i <= level.GetLength(0) - 1; i++)
            {

                for (int j = 0; j <= level.GetLength(1) - 1; j++)
                {
                    string tileID = "";
                    string tileProp = "";

                    temp = level[i, j];
                    array = temp.ToCharArray();
                    for (int k = 0; k <= array.Length; k++)
                    {

                        if (k >= array.Length - 2)
                        {
                            tileID += array[i].ToString();
                        }
                        else
                        {
                            tileProp += array[i].ToString();
                        }
                    }

                    //Texture2d's not loaded in screen, put them in later
                    /*
                    switch (tileID) //positions are not final, everything rectangle is same size with same position so far
                    {
                        //checks tileID and makes a new rectangle with corresponding texture
                        case "b1":
                            tileTemp = new Tile(0, LTopLeft, new Rectangle(0, 0, tileWidth, tileHeight), Tile.TileType.Normal);
                            tileList.Add(tileTemp);
                            break;
                        case "b2":
                            tileTemp = new Tile(0, LTopMid, new Rectangle(0, 0, tileWidth, tileHeight), Tile.TileType.Normal);
                            tileList.Add(tileTemp);
                            break;
                        case "b3":
                            tileTemp = new Tile(0, LTopRight, new Rectangle(0, 0, tileWidth, tileHeight), Tile.TileType.Normal);
                            tileList.Add(tileTemp);
                            break;
                        case "b4":
                            tileTemp = new Tile(0, LMidRight, new Rectangle(0, 0, tileWidth, tileHeight), Tile.TileType.Normal);
                            tileList.Add(tileTemp);
                            break;
                        case "b5": //no b5 tile sprite
                            break;
                        case "b6":
                            tileTemp = new Tile(0, LMidLeft, new Rectangle(0, 0, tileWidth, tileHeight), Tile.TileType.Normal);
                            tileList.Add(tileTemp);
                            break;
                        case "b7":
                            tileTemp = new Tile(0, LBotLeft, new Rectangle(0, 0, tileWidth, tileHeight), Tile.TileType.Normal);
                            tileList.Add(tileTemp);
                            break;
                        case "b8":
                            tileTemp = new Tile(0, LBotMid, new Rectangle(0, 0, tileWidth, tileHeight), Tile.TileType.Normal);
                            tileList.Add(tileTemp);
                            break;
                        case "b9":
                            tileTemp = new Tile(0, LBotRight, new Rectangle(0, 0, tileWidth, tileHeight), Tile.TileType.Normal);
                            tileList.Add(tileTemp);
                            break;
                        case "i1":
                            tileTemp = new Tile(0, dTopLeft, new Rectangle(0, 0, tileWidth, tileHeight), Tile.TileType.Normal);
                            tileList.Add(tileTemp);
                            break;
                        case "i2":
                            tileTemp = new Tile(0, dTopMid, new Rectangle(0, 0, tileWidth, tileHeight), Tile.TileType.Normal);
                            tileList.Add(tileTemp);
                            break;
                        case "i3":
                            tileTemp = new Tile(0, dTopRight, new Rectangle(0, 0, tileWidth, tileHeight), Tile.TileType.Normal);
                            tileList.Add(tileTemp);
                            break;
                        case "i4":
                            tileTemp = new Tile(0, dMidLeft, new Rectangle(0, 0, tileWidth, tileHeight), Tile.TileType.Normal);
                            tileList.Add(tileTemp);
                            break;
                        case "i5":
                            tileTemp = new Tile(0, dSolid, new Rectangle(0, 0, tileWidth, tileHeight), Tile.TileType.Normal);
                            tileList.Add(tileTemp);
                            break;
                        case "i6":
                            tileTemp = new Tile(0, dMidRight, new Rectangle(0, 0, tileWidth, tileHeight), Tile.TileType.Normal);
                            tileList.Add(tileTemp);
                            break;
                        case "i7":
                            tileTemp = new Tile(0, dBotLeft, new Rectangle(0, 0, tileWidth, tileHeight), Tile.TileType.Normal);
                            tileList.Add(tileTemp);
                            break;
                        case "i8":
                            tileTemp = new Tile(0, dBotMid, new Rectangle(0, 0, tileWidth, tileHeight), Tile.TileType.Normal);
                            tileList.Add(tileTemp);
                            break;
                        case "i9":
                            tileTemp = new Tile(0, dBotRight, new Rectangle(0, 0, tileWidth, tileHeight), Tile.TileType.Normal);
                            tileList.Add(tileTemp);
                            break;
                        case "n1":
                            tileTemp = new Tile(0, nLeftTop, new Rectangle(0, 0, tileWidth, tileHeight), Tile.TileType.Normal);
                            tileList.Add(tileTemp);
                            break;
                        case "n2":
                            tileTemp = new Tile(0, nRightTop, new Rectangle(0, 0, tileWidth, tileHeight), Tile.TileType.Normal);
                            tileList.Add(tileTemp);
                            break;
                        case "n3":
                            tileTemp = new Tile(0, nLeftBot, new Rectangle(0, 0, tileWidth, tileHeight), Tile.TileType.Normal);
                            tileList.Add(tileTemp);
                            break;
                        case "n4":
                            tileTemp = new Tile(0, nRightBot, new Rectangle(0, 0, tileWidth, tileHeight), Tile.TileType.Normal);
                            tileList.Add(tileTemp);
                            break;
                        case "#### TRANSLATOR BROKEN #####":
                            break;
                    }
                    */
                }

            }
        }


            
        }

        public void LoadTiles(Player p)
        {
            const int SCREEN_LENGTH = 16;
            const int SCREEN_WIDTH = 9;

            //Calculate player's tile
            int playerTileX = (int)Math.Round((double)p.Hitbox.X / SCREEN_LENGTH);
            int playerTileY = (int)Math.Round((double)p.Hitbox.Y / SCREEN_WIDTH);

            //the loop

            for (int row = 0; row < tileY; row++)
            {               
                for(int column = 0; column < tileX; column++)
                {
                    if (row > (playerTileY - 6) && row < (playerTileY + 6))
                    {
                        if (column > (playerTileX - 9) && column < (playerTileX + 9))
                        {
                            tileList[row, column].IsActive = true;
                        }
                        else
                        {
                            tileList[row, column].IsActive = false;
                        }
                    }
                    else
                    {
                        tileList[row, column].IsActive = false;
                    }
                }
            } //End loop

        }
    }
}
