using System;
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
        bool flag;
        Stopwatch timer = new Stopwatch();

        /*---Konstruktor klase Form1.---*/
        public Form1()
        {
            InitializeComponent();
            setFirstExercise();
            initializeGame();
            initializeUserExercises();
        }

        /*------------getteri i setteri------------*/
        public string getTextToType()
        {
            return this.textToType.Text;
        }
        public void setTextToType(string value)
        {
            text = value;
            createAndDisplayWords(text);
        }
        //----------------------------------------


        /*---Metoda za ispisivanje korisnikovih vjezbi.---*/
        void initializeUserExercises()
        {
            bool first = true;
            //put do foldera
            string[] paths = { Environment.CurrentDirectory, @"..\..\exercises\user_ex" };
            string fullPath = System.IO.Path.Combine(paths);
            //svi file-ovi u folderu
            string[] fileEntries = Directory.GetFiles(fullPath);
            foreach (string fileName in fileEntries)
            {
                string name;
                name = Path.GetFileName(fileName);
                name = name.Remove(name.Length - 4); //mičemo .txt
                System.Diagnostics.Debug.WriteLine(name);
                //stvaramo novi radiobutton
                RadioButton radioButton = new RadioButton();
                radioButton.Text = name;
                radioButton.Width = 100;
                radioButton.Height = 25;
                //radioButton.Height = (TextRenderer.MeasureText(radioButton.Text, radioButton.Font)).Height + 30;
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
            if(System.IO.File.Exists(fullPath))
            {
                text = System.IO.File.ReadAllText(fullPath);
                createAndDisplayWords(text);
            }
            else
            {
                throw new Exception("Prva lagana vježba nije pronađena!");
            }  
        }

        /*---Incijaliziraj igru, tj. postavi textBox, keyboard te kreiraj igru.---*/
        void initializeGame()
        {
            textBox = this.typedText;
            keyboard = this.panel1.Controls;
            newGame = new Game(this);
            isGameOn = false;
           // skipError = true; //nije nuzno vracanje nakon greske
        }

        /*---Pritisak gumba 'Započni igru'.---*/
        private void startBtn_Click(object sender, EventArgs e) {

            isGameOn = true;
            changeFormAppearance();
            startNewGame(); 
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
            System.Diagnostics.Debug.WriteLine(text);
            if (textArray.Length > 0) {
                newGame.startGame(textBox, keyboard, textArray, words);
            } else {
                stopTyping();
            }
        }

        /*---Metoda koja završava započetu vježbu.---*/
        private void stopTyping() {
            
            isGameOn = false;
            timer.Stop();
            MessageBox.Show("Ovdje mozemo ispisati rezultat! :)\n" + timer.Elapsed + "\n Broj greški: " + newGame.getWrongLettersCounter());
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
                //string typedText = this.typedText.Text.ToUpper() + typedChar;
                newGame.handleInputSkipErrorsOn(e,typedChar, typedText);
                if (typedChar == ' ')
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
                //System.Diagnostics.Debug.WriteLine(typedText);
                newGame.handleInputSkipErrorsOff(e, typedChar, typedText);
                if (typedChar == ' ')
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

            //ukoliko datoteka s danom putanjom postoji, ucitaj je, inace baci iznimku
            if (System.IO.File.Exists(fullPath))
            {
                text = System.IO.File.ReadAllText(fullPath);
                //this.textToType.Text = text;
                createAndDisplayWords(text);

                //svaki put kad se ucita nova vjezba, resetiraj tipkovnicu i zaustavi igru ako traje
                isGameOn = false;
                resetKeyboard();
                changeFormAppearance();
            }
            else
            {
                throw new Exception("Odabrana vježba nije pronađena!");
            }
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
            System.Diagnostics.Debug.WriteLine(nameex);
            nameex = nameex + ".txt";
            string[] paths = { Environment.CurrentDirectory, @"..\..\exercises\user_ex\", nameex };
            string fullPath = System.IO.Path.Combine(paths);

            //provjera postoji li datoteka s danom putanjom
            if (System.IO.File.Exists(fullPath))
            {
                text = System.IO.File.ReadAllText(fullPath);
                //this.textToType.Text = text;
                createAndDisplayWords(text);
                isGameOn = false;
                resetKeyboard();
                changeFormAppearance();
            }
            else
            {
                throw new Exception("Kreirana vježba nije pronađena!");
            }
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
                //label.Width = 30;
                label.AutoSize = true;
                //label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            
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

        /*---Event promjene opcije svijetleće tipkovnice.---*/
        private void keyboardCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            //opcija svijetleće tipkovnice je upaljena
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
            //opcija svijetleće tipkovnice je ugašena
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








        /* 
         * PRIMJER FOREACH
             foreach (Label tipka  in this.panel1.Controls.OfType<Label>()) {
                 String label = "label" + tipka.Text;
                 tipka.Name = label;
             }
             
         */


    }
}
