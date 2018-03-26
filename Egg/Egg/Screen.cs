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

        public Screen(string textFile, Player p)
        {
            string[,] pleaseWork = LevelInterpreter(textFile);
            LoadTiles(p, pleaseWork);
        }

        /// <summary>
        /// Loads the tiles based on the player's position in the context of the loaded textMap
        /// </summary>
        /// <param name="p"></param>
        /// <param name="textMap"></param>
        public void LoadTiles(Player p, string[,] textMap)
        {
            // #################################
            // ## Something in here is broken ##
            // #################################

            const int SCREEN_TILES_L = 16;  //How many tiles across to draw on screen
            const int SCREEN_TILES_H = 9;   //How many tiles high to draw on screen
            string[,] screenTiles = new string[SCREEN_TILES_L, SCREEN_TILES_H];

            //Calculate player's tile
            //int playerTileX = (int)Math.Round((double)p.Hitbox.X / SCREEN_TILES_H); //player's x in relation to tiles
            //int playerTileY = (int)Math.Round((double)p.Hitbox.Y / SCREEN_TILES_L); //player's y in relation to tiles
            int playerTileX = 5;
            int playerTileY = 5;
            const int P_BUFFER_X = 3;   
            const int P_BUFFER_Y = 2;

            int drawStartTileX = 0;     //assume x drawing starts at textMap x of 0
            int drawStartTileY = 0;     //assume y drawing starts at textMap y of 0

            //determine if the player is far enough over along the xMap to move the drawing start tile to the left
            if (drawStartTileX < (playerTileX - (1 / 2) * SCREEN_TILES_L))    
                drawStartTileY = playerTileX - (1 / 2) * SCREEN_TILES_L;
            //determine if the player is far enough over to move the drawing start tile to the right
            if (drawStartTileX > SCREEN_TILES_L - P_BUFFER_X)
                drawStartTileX = SCREEN_TILES_L - P_BUFFER_X;
            //determine if the player is far enough over to move the drawing start tile down
            if (drawStartTileY < (playerTileY - (1 / 2) * SCREEN_TILES_H))
                drawStartTileY = playerTileY - (1 / 2) * SCREEN_TILES_H;
            //determine if the player is far enough over to move the drawing start tile up
            if (drawStartTileY > SCREEN_TILES_H - P_BUFFER_Y)
                drawStartTileY = SCREEN_TILES_H - P_BUFFER_Y;

            //main drawing loop
            for (int row = 0; row < SCREEN_TILES_L; row++)
            {
                for (int column = 0; column < SCREEN_TILES_H; column++)
                {
                    
                    screenTiles[column, row] = Translator(textMap[drawStartTileX + column, drawStartTileY + row]);

                    #region temporarily removed to try and implement screen tile drawing
                    /*
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
                    */
                    #endregion
                }
            } //End loop

        }

        /// <summary>
        /// Takes in the file in the parameter and returns it as a 2d array
        /// </summary>
        public string[,] LevelInterpreter(string s)
        {
            //fields
            string line;
            int row;
            int col;
            int c = 0;
            string tempString = "";
            string[] split;
           
            interpreter = new StreamReader(@"..\..\..\..\Resources\levelExports\" + s + ".txt");

            //Setup for creating the level's 2d array
            line = interpreter.ReadLine(); //reads FIRST line only
            split = line.Split(','); //splits into array of 2
            row = int.Parse(split[0]); //reads rows of level array
            col = int.Parse(split[1]); //reads collumns of level array
            level = new string[row, col]; //creates level array with determined dimensions

            //Reading in the tiles from the file and placing them into the array
            string array = interpreter.ReadToEnd();
            split = array.Split(','); // for last one when drawing skip it
            
            //getting rid of "\r\n"
            for (int i = 0; i <= split.Length - 2; i++)
            {
                tempString = split[i]; //fixes everything going null 
                char[] temp = split[i].ToCharArray(); //creates a char array

                //cleaning tile names which contain '\r\n'
                if (temp.Length > 4) 
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
            
            //actually sets all of the individual tiles into the 2d array
            for (int i = 0; i <= level.GetLength(0) - 1; i++)
            {

                for (int j = 0; j <= level.GetLength(1) - 1; j++)
                {

                    level[i, j] = split[c]; //sets tileID in level array 

                    c++; //increments c because it is seperate array of different dimensions
                }

            }
            interpreter.Close();
            return level;
        }


        /// <summary>
        /// Draws the level to the screen using the level map array
        /// </summary>
        /// <param name="level">level map 2d array</param>
        public List<Tile> DrawLevel(string[,] level)
        {
            //fields


            // graphics device doesnt work for some reason
            //int tileWidth = Game1.Viewport("w") / level.GetLength(0);
            //int tileHeight = Game1.Viewport("h")/ level.GetLength(1);
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
                    
                    switch (tileID) //positions are not final, everything rectangle is same size with same position so far
                    {
                        //checks tileID and makes a new rectangle with corresponding texture
                        //returns the texture2D that should come outta these
                        /*
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
                            */
                    }
                }

            }

            return tileList;
        }

        /// <summary>
        /// Translates text file names for tiles back to tiles names
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public string Translator(string s)
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
    }
}
