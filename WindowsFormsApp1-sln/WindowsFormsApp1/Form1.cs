using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1 {
    public partial class Form1 : Form {

        private char[] lettersInText;
        private int currentLetter = 0; //pamtimo na kojem se slovu nalazimo

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
        }

        private void removeCurrentLetterOnKeyboard() {

            var keybord = this.panel1.Controls;
            String letter = "" + Char.ToUpper(lettersInText[currentLetter]) + "";
            try {
                var currentKey = keybord.Find(letter, true); //vraca kolekciju elemenata (ali uvijek ce biti samo 1 element)
                currentKey[0].BackColor = Color.White;

            } catch (Exception ex) {

                Console.WriteLine("Nije nađeno slovo.");
                //DODATI NEKI PREKID IGRE (ZAUSTAVITI VRIJEME I BLOKIRATI UNOS)
            }
          
        }

        private void showNextLetterOnKeyboard() {

            if (currentLetter < lettersInText.Length) {
                var keybord = this.panel1.Controls;
                String letter = "" + Char.ToUpper(lettersInText[currentLetter]) + "";
                try {
                    var nextKey = keybord.Find(letter, true); 
                    nextKey[0].BackColor = Color.LightGreen;

                } catch (Exception ex) {

                    Console.WriteLine("Nije nađeno slovo.");
                    //DODATI NEKI PREKID IGRE (ZAUSTAVITI VRIJEME I BLOKIRATI UNOS)
                }
            } else {
                this.typedText.Enabled = false;
                MessageBox.Show("Ovdje mozemo ispisati rezultat! :)");
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
            else if (code == 32) letter = '-'; //razmak
            else if (code == 8) letter = '.'; //todo backspace
            else letter = '?'; // OVO TREBA SREDITI!


            return letter;
        }


        private void textBox1_KeyDown(object sender, KeyEventArgs e) {

            int code = (int)e.KeyCode;
            char typedChar = Char.ToUpper(typedCharacter(code)); //typedChar je u uppercase-u
            
            if (typedChar == '?') {
               
                //todo - handlanje greske

            } else if (typedChar == Char.ToUpper(lettersInText[currentLetter])) {

                removeCurrentLetterOnKeyboard();
                currentLetter += 1;
                showNextLetterOnKeyboard();
             

            } else {
                var letter = "" + typedChar + "";
                var keybord = this.panel1.Controls;
                try {
                    var key = keybord.Find(letter, true); 
                    key[0].BackColor = Color.Red;

                } catch (Exception ex) {

                    Console.WriteLine("Nije nađeno slovo.");
                }
            }
        }


        private void startBtn_Click(object sender, EventArgs e) {

            this.startBtn.Enabled = false;
            this.typedText.Enabled = true;
            this.typedText.Focus();

            showNextLetterOnKeyboard();

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
