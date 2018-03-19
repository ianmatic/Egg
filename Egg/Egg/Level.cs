using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egg
{
    //Represents a level of the game containing a variable amount of screens
    class Level
    {
        List<Screen> screenList;
        int remainingChickens;

        public int RemainingChickens
        {
            get { return remainingChickens; }
        }

        public Level(List<Screen> listOfScreens, int totalChickens)
        {
            this.screenList = listOfScreens;
            this.remainingChickens = totalChickens;
        }

    }
}
