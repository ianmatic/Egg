using System;
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

namespace Egg_DevTool_Test
{
    public partial class MapBuilder : Form
    {
        List<Button> tabletBtns = new List<Button>();

        #region Text Output

        // for each item in along the text file, have some variable saved off invisibly
        // so that we can make a big loop and send all of those into a series of strings
        // which we can then combine into the output and print to a text file.

        string output =
    "This is me testing writing a line of text into a text file." + Environment.NewLine +
    "This is hopefully a second line.";

        private void button1_Click(object sender, EventArgs e)
        {
            StreamWriter writer = new StreamWriter("eggTest.txt");
            writer.Write(output);
            writer.Close();
        }
        #endregion


        public MapBuilder()
        {
            InitializeComponent();
            string desperation = boxSelect.Text.ToString();
            #region Adding all the buttons to tabletBtns
            /*
            for (int i = 1; i <= 135; i++)
            {
                string tempBtn = "button";
                tempBtn += i.ToString();
                tabletBtns.Add((Button)tempBtn);
            }
            

            // Attempt 2
            for (int i = 0; i >= 150; i++)
            {
                var buttonName = string.Format("btnCalc{0}", i);
                var button = Controls.Find(buttonName, true);

                if (button != null)
                {
                    tabletBtns.Add(button);
                }
            }
            */
            List<Button> tabletBtns = new List<Button>();

            #region grossly hand-inputting all the buttons
            tabletBtns.Add(button1);
            tabletBtns.Add(button2);
            tabletBtns.Add(button3);
            tabletBtns.Add(button4);
            tabletBtns.Add(button5);
            tabletBtns.Add(button6);
            tabletBtns.Add(button7);
            tabletBtns.Add(button8);
            tabletBtns.Add(button9);
            tabletBtns.Add(button10);
            tabletBtns.Add(button11);
            tabletBtns.Add(button12);
            tabletBtns.Add(button13);
            tabletBtns.Add(button14);
            tabletBtns.Add(button15);
            tabletBtns.Add(button16);
            tabletBtns.Add(button17);
            tabletBtns.Add(button18);
            tabletBtns.Add(button19);
            tabletBtns.Add(button20);
            tabletBtns.Add(button21);
            tabletBtns.Add(button22);
            tabletBtns.Add(button23);
            tabletBtns.Add(button24);
            tabletBtns.Add(button25);
            tabletBtns.Add(button26);
            tabletBtns.Add(button27);
            tabletBtns.Add(button28);
            tabletBtns.Add(button29);
            tabletBtns.Add(button30);
            tabletBtns.Add(button31);
            tabletBtns.Add(button32);
            tabletBtns.Add(button33);
            tabletBtns.Add(button34);
            tabletBtns.Add(button35);
            tabletBtns.Add(button36);
            tabletBtns.Add(button37);
            tabletBtns.Add(button38);
            tabletBtns.Add(button39);
            tabletBtns.Add(button40);
            tabletBtns.Add(button41);
            tabletBtns.Add(button42);
            tabletBtns.Add(button43);
            tabletBtns.Add(button44);
            tabletBtns.Add(button45);
            tabletBtns.Add(button46);
            tabletBtns.Add(button47);
            tabletBtns.Add(button48);
            tabletBtns.Add(button49);
            tabletBtns.Add(button50);
            tabletBtns.Add(button51);
            tabletBtns.Add(button52);
            tabletBtns.Add(button53);
            tabletBtns.Add(button54);
            tabletBtns.Add(button55);
            tabletBtns.Add(button56);
            tabletBtns.Add(button57);
            tabletBtns.Add(button58);
            tabletBtns.Add(button59);
            tabletBtns.Add(button60);
            tabletBtns.Add(button61);
            tabletBtns.Add(button62);
            tabletBtns.Add(button63);
            tabletBtns.Add(button64);
            tabletBtns.Add(button65);
            tabletBtns.Add(button66);
            tabletBtns.Add(button67);
            tabletBtns.Add(button68);
            tabletBtns.Add(button69);
            tabletBtns.Add(button70);
            tabletBtns.Add(button71);
            tabletBtns.Add(button72);
            tabletBtns.Add(button73);
            tabletBtns.Add(button74);
            tabletBtns.Add(button75);
            tabletBtns.Add(button76);
            tabletBtns.Add(button77);
            tabletBtns.Add(button78);
            tabletBtns.Add(button79);
            tabletBtns.Add(button80);
            tabletBtns.Add(button81);
            tabletBtns.Add(button82);
            tabletBtns.Add(button83);
            tabletBtns.Add(button84);
            tabletBtns.Add(button85);
            tabletBtns.Add(button86);
            tabletBtns.Add(button87);
            tabletBtns.Add(button88);
            tabletBtns.Add(button89);
            tabletBtns.Add(button90);
            tabletBtns.Add(button91);
            tabletBtns.Add(button92);
            tabletBtns.Add(button93);
            tabletBtns.Add(button94);
            tabletBtns.Add(button95);
            tabletBtns.Add(button96);
            tabletBtns.Add(button97);
            tabletBtns.Add(button98);
            tabletBtns.Add(button99);
            tabletBtns.Add(button100);
            tabletBtns.Add(button101);
            tabletBtns.Add(button102);
            tabletBtns.Add(button103);
            tabletBtns.Add(button104);
            tabletBtns.Add(button105);
            tabletBtns.Add(button106);
            tabletBtns.Add(button107);
            tabletBtns.Add(button108);
            tabletBtns.Add(button109);
            tabletBtns.Add(button110);
            tabletBtns.Add(button111);
            tabletBtns.Add(button112);
            tabletBtns.Add(button113);
            tabletBtns.Add(button114);
            tabletBtns.Add(button115);
            tabletBtns.Add(button116);
            tabletBtns.Add(button117);
            tabletBtns.Add(button118);
            tabletBtns.Add(button119);
            tabletBtns.Add(button120);
            tabletBtns.Add(button121);
            tabletBtns.Add(button122);
            tabletBtns.Add(button123);
            tabletBtns.Add(button124);
            tabletBtns.Add(button125);
            tabletBtns.Add(button126);
            tabletBtns.Add(button127);
            tabletBtns.Add(button128);
            tabletBtns.Add(button129);
            tabletBtns.Add(button130);
            tabletBtns.Add(button131);
            tabletBtns.Add(button132);
            tabletBtns.Add(button133);
            tabletBtns.Add(button134);
            tabletBtns.Add(button135);
            tabletBtns.Add(button136);
            tabletBtns.Add(button137);
            tabletBtns.Add(button138);
            tabletBtns.Add(button139);
            tabletBtns.Add(button140);
            tabletBtns.Add(button141);
            tabletBtns.Add(button142);
            tabletBtns.Add(button143);
            tabletBtns.Add(button144);
            tabletBtns.Add(button145);
            tabletBtns.Add(button146);
            tabletBtns.Add(button147);
            tabletBtns.Add(button148);
            tabletBtns.Add(button149);
            tabletBtns.Add(button150);
            #endregion

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
            }

            #endregion


            //set up a default function to change it so every button in the placement container has no text
            //hook up the default constructor in each of the tablet's buttons to this so they default to nothing
        }

        // set up a single click event so that click is recognized by 


        #region Tablet Functionality
        //The current tile
        string currentTile = "";

        //Enumerator to keep track of which tiles is in which place

        /// <summary>
        /// Change the designer based on which box index selection was made
        /// </summary>
        private void BoxIndexChanged(object sender, EventArgs e)
        {
            currentTile = boxSelect.Text.ToString();
            tileView.Image = ImageSelect(currentTile);
        }


        private Image ImageSelect (string s)
        {
            // modify the image to fit properly inside the button's image. 
            Image test = Image.FromFile(@"..\..\..\..\Resources\" + s  + ".png");
            return test;
        }
        /*
        private Image ImageSelect(string s)
        {
            switch (s)
            {
                case "D Top Left":
                    break;
                case "D Top Mid":
                    break;
                case "D Top Right":
                    break;
                case "D Mid Left":
                    break;
                case "D Mid Right":
                    break;
                case "D Bot Left":
                    break;
                case "D Bot Mid":
                    break;
                case "D Bot Right":
                    break;
                default:
                    break;
            }
        }
        */

        private void tabletClick(object sender, EventArgs e)
        {
            button1.Image = ImageSelect(currentTile);
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

        private void MapBuilder_Load(object sender, EventArgs e)
        {

        }


    }
}
