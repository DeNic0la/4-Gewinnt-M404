using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace VierGewinntM404
{
    public partial class Form1 : Form
    {
        Boolean ValidInputs;
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnStartGame_Click(object sender, EventArgs e)
        {
            // Entgegegenname der Spielerstrings
            String PlayerOne = txtEingabeSpielerEins.Text;
            String PlayerTwo = txtSpielerEingabeZwei.Text;
            

            // Überprüfung der Eingabe
            try
            {
                TestInputs(PlayerOne,PlayerTwo);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Die eingen sind ungültig: " + ex, "Ungpltige eingabe", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (ValidInputs)
            {
                Form2 Game = new Form2(PlayerOne, PlayerTwo);
                Game.Show();
                this.WindowState = FormWindowState.Minimized;

            }

        }

        //Diese Funktion überprüft ob Ungültige eingaben vorhanden sind
        private void TestInputs(String One, String Two)
        {
            //Überprüfen ob selber Name
            if (One == Two)
            {
                ValidInputs = false;
                throw new System.ArgumentException("Die Spieler müssen unterschiedliche Namen haben.");
            }
            // Überprüfen ob Zahlen vorhanden sind
            if (Regex.IsMatch(One, "^[0 - 9] +$") || Regex.IsMatch(Two, "^[0 - 9] +$"))
            {
                ValidInputs = false;
                throw new System.ArgumentException("Es Dürfen keine Zahlen verwendet werden.");
            }            
            // Wenn keine Regel verstossen wurde ist alles richtig und das Spiel kann gestartett werden
            ValidInputs = true;
        }
    }
}
