using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class Game {
        private char[] lettersInText;
        private char[] typedLetters;
        private int currentLetter; //pamtimo na kojem se slovu nalazimo
        private char wrongCharacter;
        private bool skipErrors;
        private bool isGameOver;
        Control.ControlCollection keyboard;

        public bool getIsGameOver() {
            return isGameOver;
        }
        public char getWrongCharacter() {
            return this.wrongCharacter;
        }
        public int getCurrentLetter() {
            return this.currentLetter;
        }
        public char[] getLettersInText() {
            return this.lettersInText;
        }
        public void setLettersInText(char[] value) {
            this.lettersInText = value;
        }
        public char[] getTypedLetters() {
            return this.typedLetters;
        }
        public void setTypedLetters(char[] value) {
            this.typedLetters = value;
        }
        public void setCurrentLetter(int value) {
            this.currentLetter = value;
        }
        public void setWrongCharacter(char value) {
            this.wrongCharacter = value;
        }

        public Game() {
            this.currentLetter = 0;
            this.wrongCharacter = '0';
            isGameOver = false;
            
        }

        public void startGame(Control.ControlCollection keyboard, char[] text, bool skipErrors) {
            this.lettersInText = text;
            this.wrongCharacter = '0';
            this.skipErrors = skipErrors;
            this.keyboard = keyboard;
            this.currentLetter = -1; //ovo samo da bi dobro radilo showNextLetter
            this.showNextLetterOnKeyboard();
            this.currentLetter = 0;
            isGameOver = false;
        }

        public void handleInput(char typedChar) {

            char currentLetterInText = lettersInText[currentLetter];

            if ((typedChar == Char.ToUpper(currentLetterInText)) || (typedChar == '-' && currentLetterInText == ' ')) {

                if (currentLetter < lettersInText.Length - 1) {

                    removeCurrentLetterFromKeyboard();
                    showNextLetterOnKeyboard();
                    currentLetter += 1;

                } else {

                    isGameOver = true;

                }

            } else {

                if (typedChar != '?') {

                    wrongCharacter = typedChar;
                    showWrongLetterOnKeyboard("" + currentLetterInText);
                }
            }
        }

        public void showNextLetterOnKeyboard()
        {
            char nextLetter = lettersInText[currentLetter + 1];
            string letter = "" + Char.ToUpper(nextLetter);

            if (nextLetter == ' ') {
                letter = "SPACE";
            }
             
            try
            {
                var nextKey = keyboard.Find(letter, true);
                nextKey[0].BackColor = Color.LightGreen;
            } catch (Exception ex)
            {
                Console.WriteLine("Nije nađeno slovo.");
                isGameOver = true;
            }  
        }

        public void showWrongLetterOnKeyboard(String letter)
        {
            if (letter == " ") {
                letter = "SPACE";
            }
            try
            {
                var key = keyboard.Find(letter, true);
                key[0].BackColor = Color.Red;

            } catch (Exception ex)
            {
                Console.WriteLine("Nije nađeno slovo.");
                isGameOver = true;
            }

        }

        public void removeCurrentLetterFromKeyboard()
        {
            char current = lettersInText[currentLetter];
            string letter = "" + Char.ToUpper(current); 

            if(current == ' ') {
                letter = "SPACE"; 
            }

            try
            {
                var currentKey = keyboard.Find(letter, true);
                currentKey[0].BackColor = Color.White;

            } catch (Exception ex)
            {
                Console.WriteLine("Nije nađeno slovo.");
                isGameOver = true;
            }

        }
    }
}
