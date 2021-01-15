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

        //getters, setters
        public string getTextToType()
        {
            return this.textToType.Text;
        }
        public void setTextToType(string value)
        {
            text = value;
            createAndDisplayWords(text);
        }
        public Form1() {
            InitializeComponent();
            //za pocetnu vjezbu postavi prvu laku vjezbu
            setFirstExercise();
            initializeGame();

            initializeUserExercises();        
        }

        //ispisivanje korisnikovih vježbi
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
                radioButton.Height = 20;
                if (first)
                {
                    radioButton.Checked = true;
                    first = false;
                }
                //dodajemo ga u odgovarajući container
                exPanel.Controls.Add(radioButton);
            }
        }
        void setFirstExercise()
        {
            string[] paths = { Environment.CurrentDirectory, @"..\..\exercises\", "easy_ex_1.txt" };
            string fullPath = System.IO.Path.Combine(paths);
            text = System.IO.File.ReadAllText(fullPath);
            //this.textToType.Text = text;
            createAndDisplayWords(text);
        }

        void initializeGame()
        {
            textBox = this.typedText;
            keyboard = this.panel1.Controls;
            newGame = new Game(this);
            isGameOn = false;
           // skipError = true; //nije nuzno vracanje nakon greske
        }

        private void startBtn_Click(object sender, EventArgs e) {

            isGameOn = true;
            changeFormAppearance();
            startNewGame(); 
        }

        private void restartBtn_Click(object sender, EventArgs e) {

            isGameOn = false;
            resetKeyboard();
            changeFormAppearance();
        }

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

        private void resetKeyboard() {
            foreach (Label key in keyboard) {
                key.BackColor = Color.White;
            }
        }

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

        private void stopTyping() {
            
            isGameOn = false;
            timer.Stop();
            MessageBox.Show("Ovdje mozemo ispisati rezultat! :)\n" + timer.Elapsed);
            changeFormAppearance();
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e) {
            
            int code = (int)e.KeyCode;
            char typedChar = Char.ToUpper(typedCharacter(code)); //typedChar je u uppercase-u      
            string typedText = this.typedText.Text.ToUpper() + typedChar;
            if (this.skipErrorCheckbox.Checked)
            {
                //u ovom slucaju ne zahtjevamo ispravljanje gresaka tj. mozemo ih preskociti

                //string typedText = this.typedText.Text.ToUpper() + typedChar;
                newGame.handleInputSkipErrorsOn1(e,typedChar, typedText);
                if (typedChar == ' ')
                {
                    spaceCtr++;
                    nextWord();
                    this.typedText.Text = "";
                    e.SuppressKeyPress = true;
                }

            }
            else
            {
                // ovdje zahtjevamo da se greske isprave
                System.Diagnostics.Debug.WriteLine(typedText);
                newGame.handleInputSkipErrorsOff1(e, typedChar, typedText);
                if (typedChar == ' ')
                {
                    spaceCtr++;
                    nextWord();
                    this.typedText.Text = "";
                    e.SuppressKeyPress = true;

                }

            }

            if (newGame.getIsGameOver())
            {
                stopTyping();
            }


        }


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

        private void loadNewEx_Click(object sender, EventArgs e)
        {
            this.skipErrorCheckbox.Enabled = true;
            this.restartBtn.Enabled = false;
            var level = groupBoxLevel.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Name;
            var exercise = groupBoxEx.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Name;

            Console.WriteLine(level);
            Console.WriteLine(exercise);

            var fileName = level + "_" + exercise + ".txt";
            //cita iz Debug foldera 
            string[] paths = { Environment.CurrentDirectory, @"..\..\exercises\", fileName };
            string fullPath = System.IO.Path.Combine(paths);
            text = System.IO.File.ReadAllText(fullPath);
            Console.WriteLine(text);
            //this.textToType.Text = text;
            createAndDisplayWords(text);

            //svaki put kad se ucita nova vjezba, resetiraj tipkovnicu i zaustavi igru ako traje
            isGameOn = false;
            resetKeyboard();
            changeFormAppearance();
        }

  
        private void createNewEx_Click(object sender, EventArgs e)
        {
            Form2 createEx = new Form2(); 
            //samo .Show() dozvoljava rad na Form1, a mi zelimo samo na Form2 pa koristimo ShowDialog()
            createEx.ShowDialog(this);
        }

        private void skipError_CheckedChanged(object sender, EventArgs e)
        {
            if(this.skipErrorCheckbox.Checked)
            {
                this.skipErrorCheckbox.Text = "Upaljeno";
                this.skipErrorCheckbox.BackColor = Color.LightBlue;
            }
            else
            {
                this.skipErrorCheckbox.Text = "Ugašeno";
                this.skipErrorCheckbox.BackColor = Color.Silver;
            }
        }

        //učitavanje korisnikove vježbe na klik
        private void loadUserEx_Click(object sender, EventArgs e)
        {

            this.skipErrorCheckbox.Enabled = true;
            this.restartBtn.Enabled = false;
            var nameex = exPanel.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Text;
            System.Diagnostics.Debug.WriteLine(nameex);
            nameex = nameex + ".txt";

            string[] paths = { Environment.CurrentDirectory, @"..\..\exercises\user_ex\", nameex };
            string fullPath = System.IO.Path.Combine(paths);
            text = System.IO.File.ReadAllText(fullPath);
            //this.textToType.Text = text;
            createAndDisplayWords(text);


            isGameOn = false;
            resetKeyboard();
            changeFormAppearance();

        }

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

        private void keyboardCheckbox_CheckedChanged(object sender, EventArgs e)
        {
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
