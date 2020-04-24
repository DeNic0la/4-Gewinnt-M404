using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace VierGewinntM404
{
    public partial class Form2 : Form
    {
        Button[] Row1 = new Button[5];
        Button[] Row2 = new Button[5];
        Button[] Row3 = new Button[5];
        Button[] Row4 = new Button[5];
        Button[] Row5 = new Button[5];
        Button[] Row6 = new Button[5];
        Button[] Row7 = new Button[5];
        Button[] Row8 = new Button[5];
        Button[] Row9 = new Button[5];
        Button[] Row10 = new Button[5];
        /* Button Array, pro Reihe ein Array*/
        
        Button[,] Feld;
        /*2D Buttom Array des gesamten feldes*/

        String Turn;
        /*Spieler welcher aktuell am zug ist*/

        struct player
        {
            public String Name;
            public Color Color;
        }
        player pEins;
        player pZwei;
        /*Struktur um Spieler mit Farbe zu verknüpfen*/
        
        public Form2(string PlayerOne, string PlayerTwo) // Form nimmt die Namen der 2 Spieler als Input
        {
            InitializeComponent();
            Button[,] Field = { { btn1_1, btn1_2, btn1_3, btn1_4, btn1_5 }, { btn2_1, btn2_2, btn2_3, btn2_4, btn2_5 }, { btn3_1, btn3_2, btn3_3, btn3_4, btn3_5 }, { btn4_1, btn4_2, btn4_3, btn4_4, btn4_5 }, { btn5_1, btn5_2, btn5_3, btn5_4, btn5_5 }, { btn6_1, btn6_2, btn6_3, btn6_4, btn6_5 }, { btn7_1, btn7_2, btn7_3, btn7_4, btn7_5 }, { btn8_1, btn8_2, btn8_3, btn8_4, btn8_5 }, { btn9_1, btn9_2, btn9_3, btn9_4, btn9_5 }, { btn10_1, btn10_2, btn10_3, btn10_4, btn10_5 } };
            /* fülle 2D Button Array mit Buttons*/

            for (int R = 0; R < 5; R++)
            {
                Row1[R] = Field[0, R];
                Row1[R].Text = "";
                Row1[R].BackColor = Color.White;
            }
            for (int R = 0; R < 5; R++)
            {
                Row2[R] = Field[1, R];
                Row2[R].Text = "";
                Row2[R].BackColor = Color.White;
            }
            for (int R = 0; R < 5; R++)
            {
                Row3[R] = Field[2, R];
                Row3[R].Text = "";
                Row3[R].BackColor = Color.White;
            }
            for (int R = 0; R < 5; R++)
            {
                Row4[R] = Field[3, R];
                Row4[R].Text = "";
                Row4[R].BackColor = Color.White;
            }
            for (int R = 0; R < 5; R++)
            {
                Row5[R] = Field[4, R];
                Row5[R].Text = "";
                Row5[R].BackColor = Color.White;
            }
            for (int R = 0; R < 5; R++)
            {
                Row6[R] = Field[5, R];
                Row6[R].Text = "";
                Row6[R].BackColor = Color.White;
            }
            for (int R = 0; R < 5; R++)
            {
                Row7[R] = Field[6, R];
                Row7[R].Text = "";
                Row7[R].BackColor = Color.White;
            }
            for (int R = 0; R < 5; R++)
            {
                Row8[R] = Field[7, R];
                Row8[R].Text = "";
                Row8[R].BackColor = Color.White;
            }
            for (int R = 0; R < 5; R++)
            {
                Row9[R] = Field[8, R];
                Row9[R].Text = "";
                Row9[R].BackColor = Color.White;
            }
            for (int R = 0; R < 5; R++)
            {
                Row10[R] = Field[9, R];
                Row10[R].Text = "";
                Row10[R].BackColor = Color.White;
            }
            Feld = Field;
            /* Setze alle Felder auf Leer*/

            Turn = PlayerOne; // Lasse den Spieler 1 Anfangen
            lblTurn.Text = "Spieler: " + Turn + " ist am Zug"; // Update das Turn Label

            //Fülle die Spieler stukturen
            pEins.Name = PlayerOne;
            pZwei.Name = PlayerTwo;
            pEins.Color = Color.Red;
            pZwei.Color = Color.Blue;

            /*Fülle die Labels welche die Farben der Spieler beschriften*/
            lblPlayerOne.Text = "" + pEins.Name + ": Rot";
            lblPlayerTwo.Text = "" + pZwei.Name + ": Blau";

        }

        // Diese Funktion sorgt dafür dass wenn dieses Fenster geschlossen wird die Gesamte applikation geschlossen wird
        private void Form2_FormClosing(object sender, FormClosingEventArgs e) 
        {
            System.Windows.Forms.Application.Exit(); 
        }
        //Diese Funktion gibt die Farbe des Spielers welcher am zug ist aus
        public Color GetColorOfCurrentPlayer()
        {
            if (Turn == pEins.Name)
            {
                return pEins.Color;
            }
            else
            {
                return pZwei.Color;
            }
        }

        //Diese Funktion setzt den Spieler an den zug welcher zuvor nicht am zug war. In dieser Funktion wird zugleich auch überprüft ob ein spieler gewonnen hat
        public void switchTurn()
        {
            if (Turn == pEins.Name)
            {
                Turn = pZwei.Name;
            }
            else
            {
                Turn = pEins.Name;
            }
            lblTurn.Text = "Spieler: " + Turn + " ist am Zug";

            string tfw = TestForWin();
            if (tfw != "N0")
            {
                var arguments = DialogResult.No;
                if (tfw == "U1")
                {
                    arguments = MessageBox.Show("Es Steht Unentschiden da das Feld Voll ist. \nNochmals Spielen ?", "Spiel Fertig", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                }
                else
                {
                    arguments = MessageBox.Show("Der Spieler " + tfw + " hat Gewonnen! \nNochmals Spielen ?", "Spiel Gewonnen", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                }
                if (arguments == DialogResult.Yes)
                {
                    ClearField();                    
                }
                else
                {
                    System.Windows.Forms.Application.Exit();
                }
            }
        }
        //Setzt in der Ausgewählten reihe einen Stein der ausgewählten farbe an der tiefst möglichen position
        public bool SetStoneInRow(Button[] row, Color toSet)
        {        
                      
            if (row[0].BackColor == Color.White)
            {
                goDown(row,toSet,0);
                return true;
            }
            else
            {
                MessageBox.Show("Diese Reihe ist Bereits Voll", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            
        }
        //Diese Funktion wird Rekusiv aufgerufen, es wird versucht den stein 1 feld nach unten "fallen zu lassen"
        public void goDown(Button[] row, Color toSet, int Pos)
        {
            try { 
                if (row[Pos].BackColor == Color.White)
                {
                    row[Pos].BackColor = toSet;
                    try
                    {
                        row[Pos - 1].BackColor = Color.White;
                    }
                    catch (Exception ex)
                    {
                        //Console.WriteLine(ex);
                    }
                    //Thread.Sleep(1000);
                    goDown(row, toSet, Pos + 1);
                }
            }
            catch(Exception ex)
            {
                
            }
        }

        /*Diese funktionen nehmen die eingabe der Buttons entgegen*/
        public void row1Click(object sender, EventArgs e)
        {
            if (SetStoneInRow(Row1, GetColorOfCurrentPlayer())){
                switchTurn();
            }
        }
        public void row2Click(object sender, EventArgs e)
        {
            if (SetStoneInRow(Row2, GetColorOfCurrentPlayer()))
            {
                switchTurn();
            }
        }
        public void row3Click(object sender, EventArgs e)
        {
            if (SetStoneInRow(Row3, GetColorOfCurrentPlayer()))
            {
                switchTurn();
            }
        }
        public void row4Click(object sender, EventArgs e)
        {
            if (SetStoneInRow(Row4, GetColorOfCurrentPlayer()))
            {
                switchTurn();
            }
        }
        public void row5Click(object sender, EventArgs e)
        {
            if (SetStoneInRow(Row5, GetColorOfCurrentPlayer()))
            {
                switchTurn();
            }
        }
        public void row6Click(object sender, EventArgs e)
        {
            if (SetStoneInRow(Row6, GetColorOfCurrentPlayer()))
            {
                switchTurn();
            }
        }
        public void row7Click(object sender, EventArgs e)
        {
            if (SetStoneInRow(Row7, GetColorOfCurrentPlayer()))
            {
                switchTurn();
            }
        }
        public void row8Click(object sender, EventArgs e)
        {
            if (SetStoneInRow(Row8, GetColorOfCurrentPlayer()))
            {
                switchTurn();
            }
        }
        public void row9Click(object sender, EventArgs e)
        {
            if (SetStoneInRow(Row9, GetColorOfCurrentPlayer()))
            {
                switchTurn();
            }
        }
        public void row10Click(object sender, EventArgs e)
        {
            if (SetStoneInRow(Row10, GetColorOfCurrentPlayer()))
            {
                switchTurn();
            }
        }

        //Diese Funktion überprüft ob es ein Gewinner gibt, gibt N0 für nein, U1 für unentschiden und den Namen des Gewinners für ein Gewinner zurück
        public String TestForWin()
        {
            bool fieldIsFull = true;
            for(int i = 0; i < 10; i++)
            {
                for(int B = 0; B < 5; B++)
                {                    
                    Color PossibleWinner = Feld[i, B].BackColor;
                    if (PossibleWinner == Color.White) { fieldIsFull = false; }
                    else if (TestRows(i, B, PossibleWinner))
                    {
                        if (pEins.Color == PossibleWinner)
                        {
                            return pEins.Name;
                        }
                        else
                        {
                            return pZwei.Name;
                        }
                    }
                }
            }
            if (fieldIsFull) { return "U1"; } // Unentschiden
            return "N0";
        }
        // Nimmt kordinaten eines Feldes und eine Spielerfarbe und überprüft ob diese dieser Stein teil einer reihe ist, es werden nicht alle möglichkeiten überprüft da diese Funktion für jedes feld einmal aufgerufen wurd
        public bool TestRows(int A, int B, Color toTestFor)
        {
            //Test for stones underneath
            try
            {
                if(Feld[A,B+1].BackColor == toTestFor && Feld[A, B + 2].BackColor == toTestFor&& Feld[A, B + 3].BackColor == toTestFor)
                {
                    return true;
                }
            }
            catch (Exception ex) { }

            //Test for stones Row direction right
            try
            {
                if (Feld[A+1, B].BackColor == toTestFor && Feld[A+2, B].BackColor == toTestFor && Feld[A+3, B].BackColor == toTestFor)
                {
                    return true;
                }
            }
            catch (Exception ex) { }

            //Test for row to bot Right
            try
            {
                if (Feld[A+1, B + 1].BackColor == toTestFor && Feld[A+2, B + 2].BackColor == toTestFor && Feld[A+2, B + 3].BackColor == toTestFor)
                {
                    return true;
                }
            }
            catch (Exception ex) { }

            //Test for row to Bot Left
            try
            {
                if (Feld[A-1, B + 1].BackColor == toTestFor && Feld[A-2, B + 2].BackColor == toTestFor && Feld[A-3, B + 3].BackColor == toTestFor)
                {
                    return true;
                }
            }
            catch (Exception ex) { }
            return false;
        }

        //Setzt das gesamte feld zurück
        public void ClearField()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int B = 0; B < 5; B++)
                {
                    Feld[i, B].BackColor = Color.White;
                }
            }        
        }

        // Nimmt eingaben per Textfeld entgegen
        private void Btn_Send_Click(object sender, EventArgs e)
        {
            string input = txtInput.Text;
            try
            {
                int inp = Int32.Parse(input);
                switch (inp)
                {
                    case 1:
                        row1Click(sender,e);
                        break;
                    case 2:
                        row2Click(sender, e);
                        break;
                    case 3:
                        row3Click(sender, e);
                        break;
                    case 4:
                        row4Click(sender, e);
                        break;
                    case 5:
                        row5Click(sender, e);
                        break;
                    case 6:
                        row6Click(sender, e);
                        break;
                    case 7:
                        row7Click(sender, e);
                        break;
                    case 8:
                        row8Click(sender, e);
                        break;
                    case 9:
                        row9Click(sender, e);
                        break;
                    case 10:
                        row10Click(sender, e);
                        break;
                    default:
                        MessageBox.Show("Geben sie eine Gültig, Geben sie eine Gültige zahl ein oder klicken sie auf eine Reihe", "Fehlerhafte eingabe", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception ex) { MessageBox.Show("Geben sie eine Gültig, Geben sie eine Gültige zahl ein oder klicken sie auf eine Reihe", "Fehlerhafte eingabe", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
