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


        Tile[,] screenTiles = new Tile[16, 9];


        public Tile[,] LoadTiles(string[,] levelMap, List<Texture2D> textures)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    screenTiles[j, i].DefaultSprite = textures[0]; 
                }
            }

            for (int h = 0; h < 9; h++)
            {
                for (int w = 0; w < 16; w++)
                {
                    screenTiles[w, h].DefaultSprite = textures[GetTexture(levelMap[w, h])];
                }
            }

            return screenTiles;
        }



        //Gets a texture based on the string (s) passed in
        public int GetTexture(string s)
        {
            switch (s)
            {
                case "LTopLeft":
                    return 0;
                case "LTopMid":
                    return 1;
                case "LTopRight":
                    return 2;
                case "LMidLeft":
                    return 3;
                case "LMidRight":
                    return 4;
                case "LBotLeft":
                    return 5;
                case "LBotMid":
                    return 6;
                case "LBotRight":
                    return 7;

                case "dTopLeft":
                    return 8;
                case "dTopMid":
                    return 9;
                case "dTopRight":
                    return 10;
                case "dMidLeft":
                    return 11;
                case "dSolid":
                    return 12;
                case "dMidRight":
                    return 13;
                case "dBotLeft":
                    return 14;
                case "dBotMid":
                    return 15;
                case "dBotRight":
                    return 16;

                case "nLeftTop":
                    return 17;
                case "nLeftBot":
                    return 18;
                case "nRightTop":
                    return 19;
                case "nRightBot":
                    return 20;

                default:    //failsafe case
                    return 0;
            }
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

            interpreter = new StreamReader(s + ".txt");

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
        public void DrawLevel(Tile[,] level)
        {
            int xPos = 0;
            int yPos = 0;
            int tileLength = 0; //Set up as screen length divided into segments
            int tileHeight = 0;
            int screenLength = 0;
            int screenHeight = 0;

            for (int r = 0; r < 9; r++)
            {
                for (int c = 0; c < 16; c++)
                {
                    xPos = ((screenLength / 16) * c) - ((1 / 2) * tileLength);
                    yPos = ((screenHeight / 9) * r) - ((1 / 2) * tileHeight);
                    level[c, r].X = xPos;
                    level[c, r].Y = yPos;
                }
            }
        }       
    }
}
