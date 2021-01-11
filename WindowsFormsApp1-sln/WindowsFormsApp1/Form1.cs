using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics; //stopwatch
using System.Globalization; //localtime
using System.Windows.Forms;

namespace WindowsFormsApp1 {
    public partial class Form1 : Form {

        private char[] lettersInText;
        private int currentLetter = 0; //pamtimo na kojem se slovu nalazimo
        private char wrongCharacter = '0';
        Stopwatch timer = new Stopwatch();

        public string getTextToType()
        {
            return this.textToType.Text;
        }
        public void setTextToType(string value)
        {
            this.textToType.Text = value;
        }


        public Form1() {

            InitializeComponent();
            createTextArray();
            this.typedText.Enabled = false;
            this.startBtn.Enabled = true;
        }

        void createTextArray()
        {
            lettersInText = this.textToType.Text.ToCharArray();
            foreach (char ch in lettersInText)
            {
                Console.WriteLine(Char.ToUpper(ch));
            }
            currentLetter = 0;
        }

        private void startBtn_Click(object sender, EventArgs e) {

            this.startBtn.Enabled = false;
            this.typedText.Enabled = true;
            this.typedText.Text = "";
            this.typedText.Focus();

            startTyping(); 
        }

        private void startTyping() {

            timer.Start();
            createTextArray();
            showNextLetterOnKeyboard();
        }

        private void stopTyping() {

            timer.Stop();
            MessageBox.Show("Ovdje mozemo ispisati rezultat! :)\n" + timer.Elapsed);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e) {

            int code = (int)e.KeyCode;
            char typedChar = Char.ToUpper(typedCharacter(code)); //typedChar je u uppercase-u
            
            if (typedChar == '?') {
               
                //todo - handlanje greske

            } else if (typedChar == Char.ToUpper(lettersInText[currentLetter])) {

                removeCurrentLetterFromKeyboard();
                if (wrongCharacter != '0') {
                    //todo
                    // removeWrongLetterFromKeyboard();
                    //wrongCharacter = '0';
                }
                currentLetter += 1;
                showNextLetterOnKeyboard();
             
            } else {
                wrongCharacter = typedChar;
                showWrongLetterOnKeyboard();
            }
            //todo - slucaj kad je pritisnut backspace (srediti wrongCharacter)
        }


        private char typedCharacter(int code) {

            char letter;
            if (code == 186) letter = 'Č';
            else if (code == 219) letter = 'Š';
            else if (code == 220) letter = 'Ž';
            else if (code == 221) letter = 'Đ';
            else if (code == 222) letter = 'Ć';
            else if (code >= 65 && code <= 90) letter = (char)code;
            else if (code == 32) letter = '-'; //razmak
            else if (code == 8) letter = '.'; //todo backspace
            else letter = '?'; // mozda neki drugi znak????

            return letter;
        }


        private void removeCurrentLetterFromKeyboard() {

            var keyboard = this.panel1.Controls;
            String letter = "" + Char.ToUpper(lettersInText[currentLetter]) + "";
            try {
                var currentKey = keyboard.Find(letter, true);
                //vraca kolekciju elemenata (ovdje ce biti samo 1 element zbog jedinstvenog imena)
                currentKey[0].BackColor = Color.White;

            } catch (Exception ex) {

                Console.WriteLine("Nije nađeno slovo.");
                //DODATI NEKI PREKID IGRE (ZAUSTAVITI VRIJEME I BLOKIRATI UNOS)
            }

        }

        private void showNextLetterOnKeyboard() {

            if (currentLetter < lettersInText.Length) {
                var keyboard = this.panel1.Controls;
                String letter = "" + Char.ToUpper(lettersInText[currentLetter]) + "";
                try {
                    var nextKey = keyboard.Find(letter, true);
                    nextKey[0].BackColor = Color.LightGreen;

                } catch (Exception ex) {

                    Console.WriteLine("Nije nađeno slovo.");
                    //DODATI NEKI PREKID IGRE (ZAUSTAVITI VRIJEME I BLOKIRATI UNOS)
                }
            } else {

                this.typedText.Enabled = false;
                this.startBtn.Enabled = true;

                stopTyping();
                
            }
        }

        private void showWrongLetterOnKeyboard() {
            
            var keyboard = this.panel1.Controls;
            String letter = "" + Char.ToUpper(wrongCharacter) + "";

            try {
                var key = keyboard.Find(letter, true);
                key[0].BackColor = Color.Red;

            } catch (Exception ex) {

                Console.WriteLine("Nije nađeno slovo.");
            }

        }

        private void removeWrongLetterFromKeyboard() {

            var keyboard = this.panel1.Controls;
            String letter = "" + Char.ToUpper(wrongCharacter) + "";
            try {
                var currentKey = keyboard.Find(letter, true);
                //vraca kolekciju elemenata (ovdje ce biti samo 1 element zbog jedinstvenog imena)
                currentKey[0].BackColor = Color.White;

            } catch (Exception ex) {

                Console.WriteLine("Nije nađeno slovo.");
                //DODATI NEKI PREKID IGRE (ZAUSTAVITI VRIJEME I BLOKIRATI UNOS)
            }
        }


        private void loadNewEx_Click(object sender, EventArgs e)
        {
            var level = groupBoxLevel.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Name;
            var exercise = groupBoxEx.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Name;

            Console.WriteLine(level);
            Console.WriteLine(exercise);

            var fileName = level + "_" + exercise + ".txt";
            //cita iz Debug foldera 
            string text = System.IO.File.ReadAllText(
                System.IO.Path.Combine(Environment.CurrentDirectory, fileName));
            Console.WriteLine(text);
            this.textToType.Text = text;

            //svaki put kad se ucita nova vjezba, resetiraj tipkovnicu 
            resetKeyboard();
            createTextArray();
        }

        private void resetKeyboard()
        {
            var keyboard = this.panel1.Controls;
            foreach (Label key in keyboard)
            {
                key.BackColor = Color.White;
            }
            this.typedText.Enabled = false;
            this.startBtn.Enabled = true;
        }

        private void createNewEx_Click(object sender, EventArgs e)
        {
            Form2 createEx = new Form2();
            //samo .Show() dozvoljava rad na Form1, a mi zelimo samo na Form2 pa koristimo ShowDialog()
            createEx.ShowDialog(this);
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
