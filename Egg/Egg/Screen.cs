using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egg
{
    //Represents a single screen within a level.
    class Screen
    {
        Tile[,] tileList;
        Enemy[] listOfEnemies;
        int tileX;
        int tileY;

        int lengthX; // (Tiles in X direction)*(Length of a side of a single tile)
        int lengthY; // (Tiles in Y direction)*(Length of a side of a single tile)

        public Screen(string textFile)
        {
            
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
