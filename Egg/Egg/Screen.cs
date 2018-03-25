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
        int lengthX; // (Tiles in X direction)*(Length of a side of a single tile)
        int lengthY; // (Tiles in Y direction)*(Length of a side of a single tile)

        public Screen(string textFile)
        {
            
        }
    }
}
