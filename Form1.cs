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
using static ShipGame.Form1;


namespace ShipGame
{

    public partial class Form1 : Form
    {
        public List<Point> PozycjaGraczaButtonPoint;
        public List<Point> PozycjaPrzeciwnikaButtonPoint;
         



        Random rand = new Random();

        int TotalShip = 3;
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
            public int? x { get; set; } // Współrzędne x i y punktu.
            public int? y { get; set; }
            public bool Enabled { get; set; } //Określa, czy punkt jest włączony (aktywny) na planszy.
            public string Tag { get; set; } //Dowolna etykieta lub tag przypisany do punktu.
            public Color BackColor { get; set; } //Kolor tła punktu.
            public string Text { get; set; } //Tekst wyświetlany na punkcie.
            public object BackgroundImage { get; internal set; } //Obrazek tła punktu.
            public string Name { get; set; } //Nazwa punktu.
        }
        public class AddPlayer
        {
            public string tableName { get; set; }
            public string playerName { get; set; }
            public List<Point> ships { get; set; }

        }

        private void EnemyPlayTimeEvent(object sender, EventArgs e)
        {



            if (PozycjaPrzeciwnikaButtonPoint.Count > 0 && round > 0)
            {
                round -= 1;
                txtRundy.Text = "Round: " + round;

                int index = rand.Next(PozycjaGraczaButtonPoint.Count); //Losuje liczbę całkowitą z zakresu od 0 do liczby dostępnych pozycji gracza minus 1
                //

                if ((string)PozycjaGraczaButtonPoint[index].Tag == "playerShip") // Sprawdza tag wybranej pozycji gracza i porównuje go z ciągiem znaków "playerShip".
                {
                    PozycjaGraczaButtonPoint[index].BackgroundImage = Properties.Resources.fire;
                    ruchWroga.Text = PozycjaGraczaButtonPoint[index].Text;
                    PozycjaGraczaButtonPoint[index].Enabled = false; //Wyłącza możliwość interakcji z wybraną pozycją gracza
                    PozycjaGraczaButtonPoint[index].BackColor = Color.DarkBlue;
                    PozycjaGraczaButtonPoint.RemoveAt(index); // Usuwa wybraną pozycję gracza z listy dostępnych pozycji.
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
            if (PolePrzeciwnikaListaBox.Text != "") //
            {
                var attackPosition = PolePrzeciwnikaListaBox.Text.ToLower(); // Konwertuje tekst z PolePrzeciwnikaListaBox na małe litery i przypisuje do zmiennej attackPosition.

                int index = PozycjaPrzeciwnikaButtonPoint.FindIndex(a => a.Name == attackPosition); // Wyszukuje indeks elementu na liście PozycjaPrzeciwnikaButtonPoint, który ma wartość Name równą attackPosition. 

                if (index >= 0 && index < PozycjaPrzeciwnikaButtonPoint.Count && PozycjaPrzeciwnikaButtonPoint[index].Enabled && round > 0) // sprawdza czy znaleziono index w pozycjaprzeciwnika, czy pozycja ataku istnieje
                {
                    round -= 1;
                    txtRundy.Text = "Round: " + round;

                    if ((string)PozycjaPrzeciwnikaButtonPoint[index].Tag == "enemyShip")
                    {
                        PozycjaPrzeciwnikaButtonPoint[index].Enabled = false; //przycisk jest nie akyuwny
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
                MessageBox.Show("Najpierw wybierz lokalizację z listy rozwijanej", "Informacja");
            }

        }

        private void PozycjaGraczaButtonEvents(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            if (TotalShip > 0)
            {
                btn.BackColor = Color.Orange;
                btn.Enabled = false;
                btn.Tag = "playerShip";
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
            PozycjaGraczaButtonPoint = new List<Point>();
            PozycjaGraczaButtonPoint.Add(new Point() { x = 1, y = 1 }); /*a1*/
            PozycjaGraczaButtonPoint.Add(new Point() { x = 1, y = 2 }); /*a2*/
            PozycjaGraczaButtonPoint.Add(new Point() { x = 1, y = 3 }); /*a3*/
            PozycjaGraczaButtonPoint.Add(new Point() { x = 1, y = 4 }); /*a4*/

            PozycjaGraczaButtonPoint.Add(new Point() { x = 2, y = 1 }); /*a1*/
            PozycjaGraczaButtonPoint.Add(new Point() { x = 2, y = 2 }); /*a2*/
            PozycjaGraczaButtonPoint.Add(new Point() { x = 2, y = 3 }); /*a3*/
            PozycjaGraczaButtonPoint.Add(new Point() { x = 2, y = 4 }); /*a4*/

            PozycjaGraczaButtonPoint.Add(new Point() { x = 3, y = 1 }); /*a1*/
            PozycjaGraczaButtonPoint.Add(new Point() { x = 3, y = 2 }); /*a2*/
            PozycjaGraczaButtonPoint.Add(new Point() { x = 3, y = 3 }); /*a3*/
            PozycjaGraczaButtonPoint.Add(new Point() { x = 3, y = 4 }); /*a4*/

            PozycjaGraczaButtonPoint.Add(new Point() { x = 4, y = 1 }); /*a1*/
            PozycjaGraczaButtonPoint.Add(new Point() { x = 4, y = 2 }); /*a2*/
            PozycjaGraczaButtonPoint.Add(new Point() { x = 4, y = 3 }); /*a3*/
            PozycjaGraczaButtonPoint.Add(new Point() { x = 4, y = 4 }); /*a4*/

            // Dodawanie punktów do listy ships
            AddPlayer addPlayer = new AddPlayer();
            addPlayer.ships = new List<Point>();
            addPlayer.ships.Add(new Point() { x = 1, y = 2 });
            addPlayer.ships.Add(new Point() { x = 3, y = 4 });
            addPlayer.ships.Add(new Point() { x = 5, y = 6 });



            //wyswietlenie listy pozycji przeciwnika//
            PozycjaPrzeciwnikaButtonPoint = new List<Point>();
            PozycjaPrzeciwnikaButtonPoint.Add(new Point() { x = 1, y = 1}); /*a1*/
            PozycjaPrzeciwnikaButtonPoint.Add(new Point() { x = 2, y = 1}); /*ax2*/
            PozycjaPrzeciwnikaButtonPoint.Add(new Point() { x = 3, y = 1 }); /*ax3*/
            PozycjaPrzeciwnikaButtonPoint.Add(new Point() { x = 4, y = 1 }); /*ax4*/

            PozycjaPrzeciwnikaButtonPoint.Add(new Point() { x = 1, y = 2 }); /*a1*/
            PozycjaPrzeciwnikaButtonPoint.Add(new Point() { x = 2, y = 2 }); /*a2*/
            PozycjaPrzeciwnikaButtonPoint.Add(new Point() { x = 3, y = 2 }); /*a3*/
            PozycjaPrzeciwnikaButtonPoint.Add(new Point() { x = 4, y = 2 }); /*a4*/

            PozycjaPrzeciwnikaButtonPoint.Add(new Point() { x = 1, y = 3 }); /*a1*/
            PozycjaPrzeciwnikaButtonPoint.Add(new Point() { x = 2, y = 3 }); /*a2*/
            PozycjaPrzeciwnikaButtonPoint.Add(new Point() { x = 3, y = 3 }); /*a3*/
            PozycjaPrzeciwnikaButtonPoint.Add(new Point() { x = 4, y = 3 }); /*a4*/

            PozycjaPrzeciwnikaButtonPoint.Add(new Point() { x = 1, y = 4 }); /*a1*/
            PozycjaPrzeciwnikaButtonPoint.Add(new Point() { x = 2, y = 4 }); /*a2*/
            PozycjaPrzeciwnikaButtonPoint.Add(new Point() { x = 3, y = 4 }); /*a3*/
            PozycjaPrzeciwnikaButtonPoint.Add(new Point() { x = 4, y = 4 }); /*a4*/


            PolePrzeciwnikaListaBox.Items.Clear();

            PolePrzeciwnikaListaBox.Text = null;

            /*           PolePrzeciwnikaListaBox.Items.Clear();

                       foreach (Point p in PozycjaPrzeciwnikaButtonPoint)
                       {
                           char columnLetter = (char)('A' + p.x - 1);
                           string positionString = $"{columnLetter}{p.y}";

                           PolePrzeciwnikaListaBox.Items.Add(positionString);
                       }*/

            for (int i = 0; i < PozycjaPrzeciwnikaButtonPoint.Count; i++)
            {
                PozycjaPrzeciwnikaButtonPoint[i].Enabled = true;
                PozycjaPrzeciwnikaButtonPoint[i].Tag = null; //wlasciwosc przycisku o indeksie i 
                PozycjaPrzeciwnikaButtonPoint[i].BackColor = Color.White;
                PozycjaPrzeciwnikaButtonPoint[i].BackgroundImage = null;
                if (PozycjaPrzeciwnikaButtonPoint[i].Text == null)
                {
                    PozycjaPrzeciwnikaButtonPoint[i].Text = $"({PozycjaPrzeciwnikaButtonPoint[i].x}, {PozycjaPrzeciwnikaButtonPoint[i].y})";
                }

                PolePrzeciwnikaListaBox.Items.Add(PozycjaPrzeciwnikaButtonPoint[i].Text);
            }

            for (int i = 0; i < PozycjaGraczaButtonPoint.Count; i++)
            {
                PozycjaGraczaButtonPoint[i].Enabled = true;
                PozycjaGraczaButtonPoint[i].Tag = null;
                PozycjaGraczaButtonPoint[i].BackColor = Color.White;
                PozycjaGraczaButtonPoint[i].BackgroundImage = null;

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

            for (int i = 0; i < 3; i++)
            {
                int index = rand.Next(PozycjaPrzeciwnikaButtonPoint.Count);

                if (PozycjaPrzeciwnikaButtonPoint[index].Enabled == true && (string)PozycjaPrzeciwnikaButtonPoint[index].Tag == null)
                {
                    PozycjaPrzeciwnikaButtonPoint[index].Tag = "enemyShip";

                    Debug.WriteLine("Enemy Position: " + PozycjaPrzeciwnikaButtonPoint[index].Text);
                }
                else
                {
                    index = rand.Next(PozycjaPrzeciwnikaButtonPoint.Count);
                }
            }


        }
    }
}