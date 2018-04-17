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
        //FIELDS
        int HorizontalTileCount = 9;    //eventually set this to be fed in by the text file
        int VerticalTileCount = 16;     //this one too
        int screenLength = 1920;        //Set this up to get fed in by whatever the current screen size is
        int screenHeight = 1080;        //same for this 
        string currentLevel;
        string[,] level;

        StreamReader interpreter;
        Tile[,] screenTiles;
        Dictionary<string, Tile.TileType> tileTypeDict = new Dictionary<string, Tile.TileType>();
        Dictionary<string, string> mapFileLocations = new Dictionary<string, string>();

        string[,] tempScreenArray;

        /// <summary>
        /// By default, the screen is populated with screenTiles to be an X by Y array filled with empty tiles
        /// </summary>
        public Screen(string map)
        {
            currentLevel = map;

            #region initializing tile array
            screenTiles = new Tile[HorizontalTileCount, VerticalTileCount];  //initializes screenTiles
            for (int row = 0; row < HorizontalTileCount; row++)
            {
                for (int column = 0; column < VerticalTileCount; column++) //poplates every row and column with a placeholder tile
                {
                    screenTiles[row, column] = new Tile(0, null, new Rectangle(0, 0, 0, 0), Tile.TileType.Normal);
                }
            }
            #endregion

            #region Tile Type Dictionary element adding
            tileTypeDict.Add("nt", Tile.TileType.Normal);
            tileTypeDict.Add("dm", Tile.TileType.Damaging);
            tileTypeDict.Add("mv", Tile.TileType.Moving);
            tileTypeDict.Add("nc", Tile.TileType.NoCollision);
            tileTypeDict.Add("00", Tile.TileType.NoCollision);
            #endregion

            #region Map Location Dictionary element adding
            //Add in an external trigger in tiles to send players to different maps.
            mapFileLocations.Add("mapDemo", @"..\..\..\..\Resources\levelExports\platformDemo");
            mapFileLocations.Add("demoTwo", @"..\..\..\..\Resources\levelExports\demoTwo");
            mapFileLocations.Add("variableSizeDemo", @"..\..\..\..\Resources\levelExports\nineByFifteen");
            #endregion

            tempScreenArray = new string[,]           
            {
                {null, "mapDemo", "demoTwo", "variableSizeDemo"},
                {null, null, null, null},
                { null, null, null, null},
                { null, null, null, null},
                { null, null, null, null}
            };

            
        }

        /// <summary>
        /// Updates and returns tile map
        /// </summary>
        public Tile[,] UpdateTiles(List<Texture2D> textures)
        {
            string[,] baseLevelMap = LevelInterpreter(mapFileLocations[currentLevel]);      //turn the text file into a 2d array
            Tile[,] tileMap = new Tile[VerticalTileCount, HorizontalTileCount];     //turn that 2d array into a 2d array of tiles
            tileMap = LoadTiles(baseLevelMap, textures);                            //populate those 2d arrays with 2d textures
            screenTiles = tileMap;
            return tileMap;
        }

        /// <summary>
        /// Public callable function to print all the tiles to the screen
        /// </summary>
        public void DrawTilesFromMap(SpriteBatch sb, string s, List<Texture2D> textures)
        {
            DrawLevel(screenTiles, sb); //draw everything tot he screen
        }

        /// <summary>
        /// Takes in the file in the parameter and returns it as a 2d array
        /// </summary>
        private string[,] LevelInterpreter(string s)
        {
            //fields & initialization logic
            string line;
            int row;
            int column;
            int c = 0;
            string tempString = "";
            string[] split;

            interpreter = new StreamReader(s + ".txt");

            //Setup for creating the level's 2d array
            line = interpreter.ReadLine(); //reads FIRST line only
            split = line.Split(','); //splits into array of 2
            row = int.Parse(split[0]); //reads rows of level array
            column = int.Parse(split[1]); //reads collumns of level array
            level = new string[row, column]; //creates level array with determined dimensions

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

            //update tile counts based on scraped information
            HorizontalTileCount = column;
            VerticalTileCount = row;

            //returns 2d array filled with scraped info
            return level;
        }

        /// <summary>
        /// Adds in the 2d textures to a 2d array of tiles then returns the array
        /// </summary>
        private Tile[,] LoadTiles(string[,] levelMap, List<Texture2D> textures)
        {
            //populate the array with whatever level map is being passed in
            for (int row = 0; row < VerticalTileCount; row++)
            {
                for (int column = 0; column < HorizontalTileCount; column++)
                {                    
                    Texture2D tempTexture = textures[1];                    //create temp texture 
                    int textureNumber = GetTexture(levelMap[row, column]);  //get told which texture to put in place from levelMap

                    if (textureNumber == 0)
                    {
                        levelMap[row, column] = null;
                    }
                    else
                    {
                        tempTexture = textures[textureNumber];                  //set texture into temp
                        screenTiles[row, column].DefaultSprite = tempTexture;   //update the tile using temp

                        string tagTemp = TagTileSplit(levelMap[row, column], false);    //pulls the tag for this tile
                        screenTiles[row, column].Type = tileTypeDict[tagTemp];          //updates screenTiles with the tag
                    }
                    
                }
            }
            return screenTiles; //return screentiles so it can be added to the collision check group
        }

        /// <summary>
        /// Draws the level to the screen using the level map array
        /// </summary>
        /// <param name="level">level map 2d array</param>
        private void DrawLevel(Tile[,] level, SpriteBatch sb)
        {
            int tileWidth = screenLength / HorizontalTileCount; //Set up as screen length divided into segments
            int tileHeight = screenHeight / VerticalTileCount;

            for (int row = 0; row < VerticalTileCount; row++)
            {
                for (int column = 0; column < HorizontalTileCount; column++)
                {
                    level[row, column].X = (column * tileWidth) - ((1 / 2) * tileWidth);    //determine placement then draw
                    level[row, column].Y = (row * tileHeight) - ((1 / 2) * tileHeight);     //each tile accordingly
                    level[row, column].Height = tileHeight;
                    level[row, column].Width = tileWidth;   
                    Tile temp = level[row, column]; //create a temporary copy of the given tile (for readability)

                    if (level[row, column].DefaultSprite != null)
                    {
                        sb.Draw(temp.DefaultSprite, temp.Hitbox, Color.White);
                    }
                    
                }
            }
        }

        /// <summary>
        /// Takes in a string and bool (t is tile and f is tag) to return either the tag or string
        /// from the text strings from the map
        /// </summary>
        private string TagTileSplit(string s, bool TagOrTile)
        {
            string temp;
            char[] splitUp = s.ToCharArray();   //split passed in string (4 characters) into a character array
            if (TagOrTile == true)              //if it's true, return the first two characters (the tile)
            {
                temp = splitUp[0].ToString() + splitUp[1].ToString();
            }       
            else                                //otherwise, return the third and fourth characters (the tag)
            {   
                temp = splitUp[2].ToString() + splitUp[3].ToString();
            }
            return temp;
        }

        /// <summary>
        /// Gets a texture based on the string (s) passed in
        /// In retrospect, this should probably be a dictionary but it's already done and it works
        /// </summary>
        private int GetTexture(string s)
        {
            s = TagTileSplit(s, true);
            switch (s)
            {
                //empty tile
                case "00": 
                    return 0;

                //light tiles
                case "b1":
                    return 1;
                case "b2":
                    return 2;
                case "b3":
                    return 3;
                case "b4":
                    return 4;
                case "b6":
                    return 6;
                case "b7":
                    return 7;
                case "b8":
                    return 8;
                case "b9":
                    return 9;

                //dark tiles
                case "i1":
                    return 10;
                case "i2":
                    return 11;
                case "i3":
                    return 12;
                case "i4":
                    return 13;
                case "i5":
                    return 14;
                case "i6":
                    return 15;
                case "i7":
                    return 16;
                case "i8":
                    return 17;
                case "i9":
                    return 18;

                //interior corner tiles
                case "n1":
                    return 19;
                case "n3":
                    return 20;
                case "n2":
                    return 21;
                case "n4":
                    return 22;

                default:    //failsafe case
                    return 0;
            }
        }

        /// <summary>
        /// function change levels
        /// </summary>
        /// <param name="newLevel"></param>
        public void ChangeLevel(string newLevel)
        {
            LevelMapClear();
            currentLevel = newLevel;
        }

        /// <summary>
        /// Function to fully clear out items in a map
        /// IMPORTANT: Call any time while changing levels
        /// </summary>
        private void LevelMapClear()
        {
            for (int row = 0; row < VerticalTileCount; row++)
            {
                for (int column = 0; column < HorizontalTileCount; column++)
                {
                    screenTiles[row, column] = new Tile(0, null, new Rectangle(0, 0, 0, 0), Tile.TileType.NoCollision);
                }
            }
        }
    }
}
