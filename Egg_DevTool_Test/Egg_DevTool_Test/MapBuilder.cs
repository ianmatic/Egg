using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        public MapBuilder()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //do this
        }

        string output = 
            "This is me testing writing a line of text into a text file." + Environment.NewLine +
            "This is hopefully a second line.";

        private void button1_Click(object sender, EventArgs e)
        {
            StreamWriter writer = new StreamWriter("eggTest.txt");
            writer.Write(output);
            writer.Close();
        }
    }
}
