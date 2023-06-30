using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShipGame
{
    public partial class startscreen : Form
    {
        public startscreen()
        {
            InitializeComponent();
        }

        private void loadGame(object sender, EventArgs e)
        {
            Form1 GameWindow = new Form1();

            GameWindow.Show();
        }

        private void LoadHelp(object sender, EventArgs e)
        {
            HelpScreen helpWindow = new HelpScreen();
            helpWindow.Show();
        }

        private void LoadExit(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
