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
        Screen startScreen;
        Screen endScreen;

        int totalChickensInLevel;

        public Screen CurrentScreen
        {
            get { return currentScreen; }
        }
        public int TotalChickensInLevel
        {
            get { return totalChickensInLevel; }
            set { totalChickensInLevel = value; }
        }

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

            FillScreenArray(levelNum);

            currentScreen = startScreen;

            //temporary
            totalChickensInLevel = 0;
        }

        private void FillScreenArray(int level)
        {
            string filePath = @"..\..\..\..\Resources\Levels\level" + level;

            #region Start Folder
            string startFolderPath = filePath + @"\startScreen";
            string[] startFolderArray = Directory.GetFiles(startFolderPath);

            string temp = startFolderArray[0];

            string tempDigit2 = temp[temp.Length - 5] + "";
            string tempDigit1 = temp[temp.Length - 6] + "";

            int indexRow;
            int indexColumn;

            bool tryParse = int.TryParse(tempDigit1, out indexRow);
            tryParse = int.TryParse(tempDigit2, out indexColumn);

            screenArray[indexRow, indexColumn] = new Screen(temp);

            startScreen = screenArray[indexRow, indexColumn];
            //subsplit the filename, then Parse ints to get index. Then create a new screen object with the filepath and place it at the index

            #endregion

            #region End Folder
            string endFolderPath = filePath + @"\endScreen";
            string[] endFolderArray = Directory.GetFiles(endFolderPath);

            temp = endFolderArray[0];

            tempDigit2 = temp[temp.Length - 5] + "";
            tempDigit1 = temp[temp.Length - 6] + "";

            tryParse = int.TryParse(tempDigit1, out indexRow);
            tryParse = int.TryParse(tempDigit2, out indexColumn);

            screenArray[indexRow, indexColumn] = new Screen(temp);
            endScreen = screenArray[indexRow, indexColumn];
            #endregion

            #region Other Screens

            foreach (string file in Directory.GetFiles(filePath))
            {
                string rowString = file[file.Length - 6] + "";
                string columnString = file[file.Length - 5] + "";

                bool getInts;
                int row;
                int column;

                getInts = int.TryParse(rowString, out row);
                getInts = int.TryParse(columnString, out column);

                screenArray[row, column] = new Screen(file);

            }

            #endregion

        }

       
        
        /// <summary>
        /// function change screens, returns 1 if successful, 0 if null, or -1 if new level
        /// </summary>
        /// <param name="newLevel"></param>
        public int ChangeScreen(string direction)
        {
            switch (direction)
            {
                case "left":
                    if (currentTempArrayC == 0)
                    {
                        return 0;
                    }
                    else if (screenArray[currentTempArrayR, currentTempArrayC - 1] == null)
                    {
                        if (screenArray[currentTempArrayR, currentTempArrayC] == endScreen)
                        {
                            return -1;
                        }
                        return 0;
                    }
                    currentScreen.LevelMapClear();
                    currentScreen = screenArray[currentTempArrayR, currentTempArrayC - 1];
                    currentTempArrayC -= 1;
                    break;
                case "right":
                    if (currentTempArrayC == screenArray.GetLength(1) - 1)
                    {
                        return 0;
                    }
                    else if (screenArray[currentTempArrayR, currentTempArrayC + 1] == null)
                    {
                        if (screenArray[currentTempArrayR, currentTempArrayC] == endScreen)
                        {
                            return -1;
                        }
                        return 0;
                    }
                    currentScreen.LevelMapClear();
                    currentScreen = screenArray[currentTempArrayR, currentTempArrayC + 1];
                    currentTempArrayC += 1;
                    break;
                case "up":
                    if (currentTempArrayR == 0)
                    {
                        return 0;
                    }
                    else if (screenArray[currentTempArrayR - 1, currentTempArrayC] == null)
                    {
                        if (screenArray[currentTempArrayR, currentTempArrayC] == endScreen)
                        {
                            return -1;
                        }
                        return 0;
                    }
                    currentScreen.LevelMapClear();
                    currentScreen = screenArray[currentTempArrayR - 1, currentTempArrayC];
                    currentTempArrayR -= 1;
                    break;
                case "down":
                    if (currentTempArrayR == screenArray.GetLength(0) - 1)
                    {
                        return 0;
                    }
                    else if (screenArray[currentTempArrayR + 1, currentTempArrayC] == null)
                    {
                        if (screenArray[currentTempArrayR, currentTempArrayC] == endScreen)
                        {
                            return -1;
                        }
                        return 0;
                    }
                    currentScreen.LevelMapClear();
                    currentScreen = screenArray[currentTempArrayR + 1, currentTempArrayC];
                    currentTempArrayR += 1;
                    break;
            }

            return 1;
        }
        public int ChickensInLevel()
        {
            int result = 0;
            foreach(Screen currentScreen in screenArray)
            {

            }
            return result;
        }

    }
}
