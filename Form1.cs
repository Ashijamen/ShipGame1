using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;
using ShipGame;

namespace ShipGame
{

    public partial class Form1 : Form
    {


        List<Point> PozycjaGraczaButtonPoint;

        List<Point> PozycjaPrzeciwnikaButtonPoint;

        Random rand = new Random();

        int TotalShip = 5;
        int round = 10;
        int playerScore;
        int EnemyScore;




        public Form1()
        {
            InitializeComponent();
            RestartGame();
        }
        public class Point
        {
            public int? x { get; set; }
            public int? y { get; set; }
            public bool Enabled { get; set; }
            public string Tag { get; set; }
            public Color BackColor { get; set; }
            public string Text { get; set; }

            public object BackgroundImage { get; internal set; }

            public string Name { get; set; }
        }

        private void EnemyPlayTimeEvent(object sender, EventArgs e)
        {

            if (PozycjaPrzeciwnikaButtonPoint.Count > 0 && round > 0)
            {
                round -= 1;

                txtRundy.Text = "Round: " + round;

                int index = rand.Next(PozycjaGraczaButtonPoint.Count);

                if ((string)PozycjaGraczaButtonPoint[index].Tag == "playerShip")
                {
                    PozycjaGraczaButtonPoint[index].BackgroundImage = Properties.Resources.fire;
                    ruchWroga.Text = PozycjaGraczaButtonPoint[index].Text;
                    PozycjaGraczaButtonPoint[index].Enabled = false;
                    PozycjaGraczaButtonPoint[index].BackColor = Color.DarkBlue;
                    PozycjaGraczaButtonPoint.RemoveAt(index);
                    EnemyScore += 1;
                    txtPrzeciwnik.Text = EnemyScore.ToString();
                    CzasGryWroga.Stop();
                }
                else
                {
                    PozycjaGraczaButtonPoint[index].BackgroundImage = Properties.Resources.shotx;
                    ruchWroga.Text = PozycjaGraczaButtonPoint[index].Text;
                    PozycjaGraczaButtonPoint[index].Enabled = false;
                    PozycjaGraczaButtonPoint[index].BackColor = Color.DarkBlue;
                    PozycjaGraczaButtonPoint.RemoveAt(index);
                    CzasGryWroga.Stop();

                }
            }

            if (round < 1 || EnemyScore > 2 || playerScore > 2)
            {

                if (playerScore > EnemyScore)
                {
                    MessageBox.Show("WYGRAŁEŚ!!");
                    RestartGame();
                }
                else if (EnemyScore > playerScore)
                {
                    MessageBox.Show("PRZEGRAŁEŚ");
                    RestartGame();
                }
                else if (EnemyScore == playerScore)
                {
                    MessageBox.Show("NIKT NIE WYGRAŁ TEJ GRY");
                    RestartGame();
                }
            }



        }

        private void AtakButtonEvent(object sender, EventArgs e)
        {
            if (PolePrzeciwnikaListaBox.Text != "")
            {

                var attackPosition = PolePrzeciwnikaListaBox.Text.ToLower();

                int index = PozycjaPrzeciwnikaButtonPoint.FindIndex(a => a.Name == attackPosition);

                if (PozycjaPrzeciwnikaButtonPoint[index].Enabled && round > 0)
                {
                    round -= 1;
                    txtRundy.Text = "Round: " + round;


                    if ((string)PozycjaPrzeciwnikaButtonPoint[index].Tag == "enemyShip")
                    {

                        PozycjaPrzeciwnikaButtonPoint[index].Enabled = false;
                        PozycjaPrzeciwnikaButtonPoint[index].BackgroundImage = Properties.Resources.fire;
                        PozycjaPrzeciwnikaButtonPoint[index].BackColor = Color.DarkBlue;
                        playerScore += 1;
                        txtGracz.Text = playerScore.ToString();
                        CzasGryWroga.Start();

                    }
                    else
                    {
                        PozycjaPrzeciwnikaButtonPoint[index].Enabled = false;
                        PozycjaPrzeciwnikaButtonPoint[index].BackgroundImage = Properties.Resources.shotx;
                        PozycjaPrzeciwnikaButtonPoint[index].BackColor = Color.DarkBlue;
                        CzasGryWroga.Start();
                    }


                }


            }
            else
            {
                MessageBox.Show("Wybierz pozycje z listy rozwijanej, a następnie Atakuj");
            }
        }

        private void PozycjaGraczaButtonEvents(object sender, EventArgs e)
        {

            if (TotalShip > 0)
            {
                var button = (Button)sender;

                button.Enabled = false;
                button.Tag = "playerShip";
                button.BackColor = Color.Orange;
                TotalShip -= 1;
            }

            if (TotalShip == 0)
            {
                btnAtak.Enabled = true;
                btnAtak.BackColor = Color.Red;
                btnAtak.ForeColor = Color.White;


            }


        }


        private void RestartGame()
        {

            //wyswietlenie listy pozycji gracza//
            List<Point> PozycjaGraczaButtonPoint = new List<Point>();
            PozycjaGraczaButtonPoint.Add(new Point() { x = 1, y = 1 }); /*a1*/
            PozycjaGraczaButtonPoint.Add(new Point() { x = 2, y = 1 }); /*a2*/
            PozycjaGraczaButtonPoint.Add(new Point() { x = 3, y = 1 }); /*a3*/
            PozycjaGraczaButtonPoint.Add(new Point() { x = 4, y = 1 }); /*a4*/

            PozycjaGraczaButtonPoint.Add(new Point() { x = 1, y = 2 }); /*a1*/
            PozycjaGraczaButtonPoint.Add(new Point() { x = 2, y = 2 }); /*a2*/
            PozycjaGraczaButtonPoint.Add(new Point() { x = 3, y = 2 }); /*a3*/
            PozycjaGraczaButtonPoint.Add(new Point() { x = 4, y = 2 }); /*a4*/

            PozycjaGraczaButtonPoint.Add(new Point() { x = 1, y = 3 }); /*a1*/
            PozycjaGraczaButtonPoint.Add(new Point() { x = 2, y = 3 }); /*a2*/
            PozycjaGraczaButtonPoint.Add(new Point() { x = 3, y = 3 }); /*a3*/
            PozycjaGraczaButtonPoint.Add(new Point() { x = 4, y = 3 }); /*a4*/

            PozycjaGraczaButtonPoint.Add(new Point() { x = 1, y = 4 }); /*a1*/
            PozycjaGraczaButtonPoint.Add(new Point() { x = 2, y = 4 }); /*a2*/
            PozycjaGraczaButtonPoint.Add(new Point() { x = 3, y = 4 }); /*a3*/
            PozycjaGraczaButtonPoint.Add(new Point() { x = 4, y = 4 }); /*a4*/
            //wyswietlenie listy pozycji przeciwnika//
            List<Point> PozycjaPrzeciwnikaButtonPoint = new List<Point>();
            PozycjaPrzeciwnikaButtonPoint.Add(new Point() { x = 1, y = 1 }); /*a1*/
            PozycjaPrzeciwnikaButtonPoint.Add(new Point() { x = 1, y = 2 }); /*a2*/
            PozycjaPrzeciwnikaButtonPoint.Add(new Point() { x = 1, y = 3 }); /*a3*/
            PozycjaPrzeciwnikaButtonPoint.Add(new Point() { x = 1, y = 4 }); /*a4*/

            PozycjaPrzeciwnikaButtonPoint.Add(new Point() { x = 2, y = 2 }); /*a1*/
            PozycjaPrzeciwnikaButtonPoint.Add(new Point() { x = 2, y = 2 }); /*a2*/
            PozycjaPrzeciwnikaButtonPoint.Add(new Point() { x = 2, y = 2 }); /*a3*/
            PozycjaPrzeciwnikaButtonPoint.Add(new Point() { x = 2, y = 2 }); /*a4*/

            PozycjaPrzeciwnikaButtonPoint.Add(new Point() { x = 3, y = 3 }); /*a1*/
            PozycjaPrzeciwnikaButtonPoint.Add(new Point() { x = 3, y = 3 }); /*a2*/
            PozycjaPrzeciwnikaButtonPoint.Add(new Point() { x = 3, y = 3 }); /*a3*/
            PozycjaPrzeciwnikaButtonPoint.Add(new Point() { x = 3, y = 3 }); /*a4*/

            PozycjaPrzeciwnikaButtonPoint.Add(new Point() { x = 4, y = 4 }); /*a1*/
            PozycjaPrzeciwnikaButtonPoint.Add(new Point() { x = 4, y = 4 }); /*a2*/
            PozycjaPrzeciwnikaButtonPoint.Add(new Point() { x = 4, y = 4 }); /*a3*/
            PozycjaPrzeciwnikaButtonPoint.Add(new Point() { x = 4, y = 4 }); /*a4*/

            /*
                        PolePrzeciwnikaListaBox.Items.Clear();

                        PolePrzeciwnikaListaBox.Text = null;*/



            PozycjaPrzeciwnikaButtonPoint = PozycjaPrzeciwnikaButtonPoint.Distinct().ToList();

            PolePrzeciwnikaListaBox.Items.Clear();
            PolePrzeciwnikaListaBox.Text = null;

            for (int i = 0; i < PozycjaPrzeciwnikaButtonPoint.Count; i++)
            {
                // The Point class does not have an 'Enabled' property, so this line will throw an error
                // PozycjaPrzeciwnikaButtonPoint[i].Enabled = true;

                PozycjaPrzeciwnikaButtonPoint[i].Tag = null;
                PozycjaPrzeciwnikaButtonPoint[i].BackColor = Color.White;
                PozycjaPrzeciwnikaButtonPoint[i].BackgroundImage = null;
                PolePrzeciwnikaListaBox.Items.Add(PozycjaPrzeciwnikaButtonPoint[i].ToString());
            }

            for (int i = 0; i < PozycjaGraczaButtonPoint.Count; i++)
            {
                PozycjaGraczaButtonPoint[i].Enabled = true;
                PozycjaGraczaButtonPoint[i].Tag = null;
                PozycjaGraczaButtonPoint[i].BackColor = Color.White;
                PozycjaPrzeciwnikaButtonPoint[i].BackgroundImage = null;
            }

            playerScore = 0;
            EnemyScore = 0;
            round = 10;
            TotalShip = 3;

            txtGracz.Text = playerScore.ToString();
            txtPrzeciwnik.Text = EnemyScore.ToString();
            ruchWroga.Text = "A1";

            btnAtak.Enabled = false;

            EnemyLocationPicker();


        }

        private void EnemyLocationPicker()
        {
            for (int i = 0; i < 5; i++)
            {
                int index = rand.Next(PozycjaPrzeciwnikaButtonPoint.Count);

                if (PozycjaPrzeciwnikaButtonPoint[index].Enabled == true && (string)PozycjaPrzeciwnikaButtonPoint[index].Tag == null)
                {
                    PozycjaPrzeciwnikaButtonPoint[index].Tag = "EnemyShip";

                    Debug.WriteLine("Pozycja Przeciwnika: " + PozycjaPrzeciwnikaButtonPoint[index].Text);
                }

                else
                {
                    index = rand.Next(PozycjaPrzeciwnikaButtonPoint.Count);
                }
            }

        }
    }
}

