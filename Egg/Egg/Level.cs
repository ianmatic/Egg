using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Egg
{
    //Represents a level of the game containing a variable amount of screens
    class Level
    {
        Dictionary<string, string> mapFileLocations = new Dictionary<string, string>();
        Screen[,] screenArray;
        Screen currentScreen;

        int currentTempArrayC;
        int currentTempArrayR;

        public Level(int levelNum)
        {
            #region Map Location Dictionary element adding
            //Add in an external trigger in tiles to send players to different maps.
            mapFileLocations.Add("mapDemo", @"..\..\..\..\Resources\levelExports\platformDemo");
            mapFileLocations.Add("demo2", @"..\..\..\..\Resources\levelExports\demo2");
            mapFileLocations.Add("demo3", @"..\..\..\..\Resources\levelExports\demo3");
            mapFileLocations.Add("demo4", @"..\..\..\..\Resources\levelExports\demo4");
            mapFileLocations.Add("variableSizeDemo", @"..\..\..\..\Resources\levelExports\nineByFifteen");
            mapFileLocations.Add("collisionTest", @"..\..\..\..\Resources\levelExports\collisionTestMap");
            //.Add("key", @"..\..\..\..\Resources\levelExports\(exported file in levelExports)
            #endregion

            screenArray = new Screen[5, 5];
            
        }

        private void FillScreenArray(int level)
        {
            string filePath = @"..\..\..\..\Resources\Levels\level" + level;

            #region Start Folder
            string startFolderPath = filePath + @"\startScreen";
            string[] startFolderArray = Directory.GetFiles(startFolderPath);

            string temp = startFolderArray[0];

            //subsplit the filename, then Parse ints to get index. Then create a new screen object with the filepath and place it at the index
            
            #endregion
        }

       
        //BUgged, fix me later
        /// <summary>
        /// function change levels, returns true if successful
        /// </summary>
        /// <param name="newLevel"></param>
        public bool ChangeLevel(string direction)
        {
            switch (direction)
            {
                case "left":
                    if (currentTempArrayC == 0)
                    {
                        return false;
                    }
                    else if (tempScreenArray[currentTempArrayR, currentTempArrayC - 1] == null)
                    {
                        return false;
                    }
                    currentScreen.LevelMapClear();
                    currentScreen = tempScreenArray[currentTempArrayR, currentTempArrayC - 1];
                    currentTempArrayC -= 1;
                    break;
                case "right":
                    if (currentTempArrayC == tempScreenArray.GetLength(1) - 1)
                    {
                        return false;
                    }
                    else if (tempScreenArray[currentTempArrayR, currentTempArrayC + 1] == null)
                    {
                        return false;
                    }
                    currentScreen.LevelMapClear();
                    currentScreen = tempScreenArray[currentTempArrayR, currentTempArrayC + 1];
                    currentTempArrayC += 1;
                    break;
                case "up":
                    if (currentTempArrayR == 0)
                    {
                        return false;
                    }
                    else if (tempScreenArray[currentTempArrayR - 1, currentTempArrayC] == null)
                    {
                        return false;
                    }
                    LevelMapClear();
                    currentLevel = tempScreenArray[currentTempArrayR - 1, currentTempArrayC];
                    currentTempArrayR -= 1;
                    break;
                case "down":
                    if (currentTempArrayR == tempScreenArray.GetLength(0) - 1)
                    {
                        return false;
                    }
                    else if (tempScreenArray[currentTempArrayR + 1, currentTempArrayC] == null)
                    {
                        return false;
                    }
                    LevelMapClear();
                    currentLevel = tempScreenArray[currentTempArrayR + 1, currentTempArrayC];
                    currentTempArrayR += 1;
                    break;
            }

            return true;
        }

    }
}
