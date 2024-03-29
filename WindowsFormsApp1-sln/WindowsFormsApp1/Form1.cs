﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics; //stopwatch
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp1 {
    public partial class Form1 : Form {
      
        private Control.ControlCollection keyboard;
        private Control textBox;
        private Game newGame;
        private bool isGameOn;
        string[] words; //polje rijeci u tekstu koji trebamo pretipkati
        string[] wordsToDisplay;
        string text="";
        int spaceCtr;
        Stopwatch timer = new Stopwatch();
        DateTime today;

        /*---Konstruktor klase Form1.---*/
        public Form1()
        {
            InitializeComponent();
            setFirstExercise();
            initializeGame();
            loadUserExercises();
            printScores();
            changeFormAppearance();
        }


        /*---Metoda za ispisivanje korisnikovih vjezbi.---*/
        void loadUserExercises()
        {
            bool first = true;
            //put do foldera
            string[] paths = { Environment.CurrentDirectory, @"..\..\exercises\user_ex" };
            string fullPath = System.IO.Path.Combine(paths);
            //svi file-ovi u folderu
            string[] fileEntries = Directory.GetFiles(fullPath);
            if(fileEntries.Length == 0 || fileEntries == null)
            {
                this.loadUserEx.Enabled = false;
            }
            foreach (string fileName in fileEntries)
            {
                string name;
                name = Path.GetFileName(fileName);
                name = name.Remove(name.Length - 4); //mičemo .txt
                //stvaramo novi radiobutton
                RadioButton radioButton = new RadioButton();
                radioButton.Text = name;
                radioButton.AutoSize = true;
                if (first)
                {
                    radioButton.Checked = true;
                    first = false;
                }
                //dodajemo ga u odgovarajući container
                exPanel.Controls.Add(radioButton);
            }
        }

        /*---Postavi prvu učitanu vježbu na prvu laganu vježbu.---*/
        void setFirstExercise()
        {
            string[] paths = { Environment.CurrentDirectory, @"..\..\exercises\", "easy_ex_1.txt" };
            string fullPath = System.IO.Path.Combine(paths);
            try
            {
                text = System.IO.File.ReadAllText(fullPath);
            }
            catch 
            {
                text = "";
                Console.WriteLine("Prva lagana vježba nije pronađena!");
            }
            createAndDisplayWords(text);
        }
        /*---Metoda koji služi za ispis korisnikovih rezultata---*/
        void printScores() {
            string[] paths = { Environment.CurrentDirectory, @"..\..\scores.txt" };
            string fullPath = System.IO.Path.Combine(paths);
   
            try {
                string[] lines = System.IO.File.ReadAllLines(fullPath);
                foreach (string line in lines) {

                    Label label = new Label();
                    label.Text = line;
                    label.AutoSize = true;
                    label.Margin = new Padding(0, 0, 0, 4);

                    this.scoresPanel.Controls.Add(label); 

                }
            } catch {
                Console.WriteLine("Greška kod čitanja scores.txt");
            }

            this.scoresPanel.AutoScroll = true;
        }

        /*---Incijaliziraj igru, tj. postavi textBox, keyboard te kreiraj igru.---*/
        void initializeGame()
        {
            textBox = this.typedText;
            keyboard = this.panel1.Controls;
            newGame = new Game(this);
            isGameOn = false;

        }

        /*---Pritisak gumba 'Započni igru'.---*/
        private void startBtn_Click(object sender, EventArgs e) {

            isGameOn = true;
            changeFormAppearance();
            startNewGame();
            today = DateTime.Today;
        }

        /*---Pritisak gumba 'Ponovno pokreni'.---*/
        private void restartBtn_Click(object sender, EventArgs e) {

            isGameOn = false;
            resetKeyboard();
            changeFormAppearance();
        }

        /*---Promjena izgleda forme s obzirom na to je li igra trenutno traje ili ne.---*/
        void changeFormAppearance() {

            if (isGameOn) {
                this.startBtn.Enabled = false;
                this.restartBtn.Enabled = true;
                this.typedText.Enabled = true;
                this.typedText.Text = "";
                this.typedText.Focus();
                this.skipErrorCheckbox.Enabled = false;
            } else {

                resetKeyboard();
                this.startBtn.Enabled = true;
                this.restartBtn.Enabled = false;
                this.typedText.Enabled = false;
                this.typedText.Text = ""; //ispraznimo textbox ako je igra gotova
                this.skipErrorCheckbox.Enabled = true;
                this.textToType.Controls.Clear();
                createAndDisplayWords(text);
            }

            //ako je došlo do pogreške kod učitavanja teksta, ne želimo korisniku dopustiti da pokrene igru
            if (text == "") {
                this.startBtn.Enabled = false;
                this.restartBtn.Enabled = false;
            }
        }

        /*---Resetiranje tipkovnice - promjena pozadine tipki u bijelo.---*/
        private void resetKeyboard() {
            foreach (Label key in keyboard) {
                key.BackColor = Color.White;
            }
        }

        /*---Metoda koja započinje trenutno učitanu vježbu.---*/
        private void startNewGame() {

            isGameOn = true;
            var textArray = text.ToUpper().ToCharArray();
            string str = text.ToUpper();
            words = str.Split(' ');
            spaceCtr = 0;

            timer.Restart();
            if (textArray.Length > 0) {
                newGame.startGame(keyboard, textArray, words);
            } else {
                stopTyping();
            }
        }

        /*---Metoda koja završava započetu vježbu.---*/
        private void stopTyping() {
            
            isGameOn = false;
            timer.Stop();
            TimeSpan ts = timer.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            long time = timer.ElapsedMilliseconds;
            int wrong = newGame.getWrongLettersCounter();
            int skipped = newGame.getSkippedLettersCounter();
            int correct = newGame.getCorrectLettersCounter();
            int grossWPM = (int)((wrong + correct) / 5 / ((float)time / 1000 / 60));
            int accuracy = 0;
            if (wrong + correct != 0)
                accuracy = (int)((float)correct / (wrong + correct) * 100);

            MessageBox.Show("Vrijeme " + elapsedTime + "\nBroj pogrešno unesenih znakova: " + (wrong+skipped) 
                + "\nBroj točno unesenih znakova: " + correct + "\nWPM: " + grossWPM + "\nTočnost: " + accuracy +"%");

            string[] paths = { Environment.CurrentDirectory, @"..\..\scores.txt"};
            string fullPath = System.IO.Path.Combine(paths);
            Console.WriteLine(today.ToString("dd/MM/yyyy"));
           
            using (StreamWriter sw = File.AppendText(fullPath))
            {
                string score = today.ToString("dd.MM.yyyy") + " - " + grossWPM + " - " + accuracy + "%";
                sw.WriteLine(score);
                Label label = new Label();
                label.Text = score;
                label.AutoSize = true;
                this.scoresPanel.Controls.Add(label);
            }
            changeFormAppearance();
        }

        /*---Event pritiska tipke na tipkovnici.---*/
        private void textBox1_KeyDown(object sender, KeyEventArgs e) {
            
            int code = (int)e.KeyCode; //citamo kod pritisnute tipke
            char typedChar = Char.ToUpper(typedCharacter(code));     
            string typedText = this.typedText.Text.ToUpper() + typedChar;

            //ukoliko je opcija preskakanja greški upaljena
            if (this.skipErrorCheckbox.Checked)
            {
                //newGame.handleInputSkipErrorsOn(e,typedChar, typedText);
                if (newGame.handleInputSkipErrorsOn(e, typedChar, typedText, spaceCtr) == true)
                {
                    spaceCtr++;
                    nextWord();
                    this.typedText.Text = "";
                    e.SuppressKeyPress = true;
                }

            }
            //ukoliko je opcija preskakanja greški ugašena
            else
            {
                // ovdje zahtjevamo da se greske isprave
                if (newGame.handleInputSkipErrorsOff(e, typedChar) == true)
                {
                    spaceCtr++;
                    nextWord();
                    this.typedText.Text = "";
                    e.SuppressKeyPress = true;
                }

            }

            //svaki pritiskom tipke provjeravaj je li kraj igre, ako je, završi s tipkanjem
            if (newGame.getIsGameOver())
            {
                stopTyping();
            }
        }

        /*---Metoda za raspoznavanje hrvatskih dijakritičkih znakova.---*/
        private char typedCharacter(int code) {

            char letter;
            if (code == 186) letter = 'Č';
            else if (code == 219) letter = 'Š';
            else if (code == 220) letter = 'Ž';
            else if (code == 221) letter = 'Đ';
            else if (code == 222) letter = 'Ć';
            else if (code >= 65 && code <= 90) letter = (char)code;
            else if (code == 32) letter = ' '; //razmak
            else if (code == 8) letter = '\b'; //backspace
            else letter = '?'; // neki znak koji nije slovo

            return letter;
        }

        /*---Event pritiska gumba 'Učitaj vježbu'.---*/
        private void loadNewEx_Click(object sender, EventArgs e)
        {
            //omoguci odabir opcije preskakanja greske i onemoguci ponovno pokretanje
            this.skipErrorCheckbox.Enabled = true;
            this.restartBtn.Enabled = false;

            //dohvati odabrani nivo i vjezbu iz radio buttona
            var level = groupBoxLevel.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Name;
            var exercise = groupBoxEx.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Name;

            //dohvacamo putanju vjezbe koju ucitavamo
            var fileName = level + "_" + exercise + ".txt";
            string[] paths = { Environment.CurrentDirectory, @"..\..\exercises\", fileName };
            string fullPath = System.IO.Path.Combine(paths);

            //ukoliko datoteka s danom putanjom postoji, ucitaj je, inace ispisi poruku
            try
            {
                text = System.IO.File.ReadAllText(fullPath);
            }
            catch
            {
                Console.WriteLine("Odabrana vježba nije pronađena!");
                text = "";
                MessageBox.Show("Dogodila se pogreška - odabrana vježba ne postoji. Molimo odaberite drugu vježbu.");
            }
            createAndDisplayWords(text);

            //svaki put kad se ucita nova vjezba, resetiraj tipkovnicu i zaustavi igru ako traje
            isGameOn = false;
            resetKeyboard();
            changeFormAppearance();

        }

        /*---Event pritiska gumba 'Kreiraj svoju vježbu'.---*/
        private void createNewEx_Click(object sender, EventArgs e)
        {
            Form2 createEx = new Form2(); 
            createEx.ShowDialog(this); //tako da ne mozemo raditi na Form1
        }

        /*---Event promjene opcije preskakanja greške.---*/
        private void skipError_CheckedChanged(object sender, EventArgs e)
        {
            //opcija preskakanja greške je upaljena
            if(this.skipErrorCheckbox.Checked)
            {
                this.skipErrorCheckbox.Text = "Upaljeno";
                this.skipErrorCheckbox.BackColor = Color.LightBlue;
            }
            //opcija preskakanja greške je ugašena
            else
            {
                this.skipErrorCheckbox.Text = "Ugašeno";
                this.skipErrorCheckbox.BackColor = Color.Silver;
            }
        }

        /*---Event pritiska gumba 'Učitaj svoju vježbu'.---*/
        private void loadUserEx_Click(object sender, EventArgs e)
        {
            this.skipErrorCheckbox.Enabled = true;
            this.restartBtn.Enabled = false;
            
            //dohvacamo ime vjezbe iz odabranog radio buttona
            var nameex = exPanel.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Text;
            nameex = nameex + ".txt";
            string[] paths = { Environment.CurrentDirectory, @"..\..\exercises\user_ex\", nameex };
            string fullPath = System.IO.Path.Combine(paths);


            //ukoliko datoteka s danom putanjom postoji, ucitaj je, inace ispisi poruku
            try {
                text = System.IO.File.ReadAllText(fullPath);
            }
            catch
            {
                Console.WriteLine("Odabrana vježba nije pronađena!");
                text = "";
                MessageBox.Show("Dogodila se pogreška - odabrana vježba ne postoji. Molimo odaberite drugu vježbu.");
            }

            createAndDisplayWords(text);

            //svaki put kad se ucita nova vjezba, resetiraj tipkovnicu i zaustavi igru ako traje
            isGameOn = false;
            resetKeyboard();
            changeFormAppearance();

        }

        /*---Metoda koja ucitanu vjezbu dijeli na rijeci te kreira labele za svaku rijec.---*/
        private void createAndDisplayWords(string text)
        {
            bool first = true;
            wordsToDisplay = text.Split(' ');
            for (int i = 0; i < wordsToDisplay.Length; i++)
            { 
                Label label = new Label();
                label.Text = wordsToDisplay[i];
                label.AutoSize = true;
                if (first == true)
                {
                    label.BackColor = Color.LightBlue;
                    first = false;
                }
                this.textToType.Controls.Add(label);
            }
        }

        private void nextWord()
        {
            if (spaceCtr < words.Length)
            {
                var lbls = textToType.Controls.OfType<Label>().ToArray();
                lbls[1].BackColor = Color.LightBlue;
                textToType.Controls.Remove(lbls[0]);
            }
        }

        /*---Event promjene opcije svjetleće tipkovnice.---*/
        private void keyboardCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            //opcija svjetleće tipkovnice je upaljena
            if(this.keyboardCheckbox.Checked)
            {
                this.keyboardCheckbox.Text = "Upaljena";
                this.keyboardCheckbox.BackColor = Color.LightBlue;
                this.panel1.Visible = true;
                foreach (Label key in this.panel1.Controls.OfType<Label>())
                {
                    key.Visible = true;
                }
            }
            //opcija svjetleće tipkovnice je ugašena
            else
            {
                this.keyboardCheckbox.Text = "Ugašena";
                this.keyboardCheckbox.BackColor = Color.Silver;
                this.panel1.Visible = false;
                foreach (Label key in this.panel1.Controls.OfType<Label>())
                {
                    key.Visible = false;
                }
            }
        }
    }
}
