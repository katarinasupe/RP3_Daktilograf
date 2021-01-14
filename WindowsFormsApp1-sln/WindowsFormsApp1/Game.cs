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
        private string[] wordsInText;
        private int expectedLetterIndex; //slovo koje ocekujemo iduce
        private char wrongCharacter;
        private bool isGameOver;
        private int spaceCtr;
        Control.ControlCollection keyboard;
        Control textBox;

        public bool getIsGameOver() {
            return isGameOver;
        }
        public char getWrongCharacter() {
            return this.wrongCharacter;
        }
        public int getExpectedLetterIndex() {
            return this.expectedLetterIndex;
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
        public void setExpectedLetterIndex(int value) {
            this.expectedLetterIndex = value;
        }
        public void setWrongCharacter(char value) {
            this.wrongCharacter = value;
        }

        public Game() {
            this.expectedLetterIndex = 0;
            this.wrongCharacter = '0';
            isGameOver = false;
            
        }

        public void startGame(Control textBox, Control.ControlCollection keyboard, char[] text, string[] words) {
            this.lettersInText = text;
            this.wordsInText = words;
            this.spaceCtr = 0;
            this.wrongCharacter = '0';
            this.textBox = textBox;
            this.keyboard = keyboard;
            this.expectedLetterIndex = -1; //ovo samo da bi dobro radilo showNextLetter
            this.showNextLetterOnKeyboard();
            this.expectedLetterIndex = 0;
            isGameOver = false;
        }

        public void handleInputSkipErrorsOn(char typedChar, string typedText) {

            removeExpectedLetterFromKeyboard();
            if(wrongCharacter != '0')
                removeWrongLetterFromKeyboard();

            if(typedChar != lettersInText[expectedLetterIndex] && typedChar != '\b' && typedChar != ' ') {

                wrongCharacter = typedChar;
                showWrongLetterOnKeyboard();
            }

            if (typedChar == ' ') {

                spaceCtr += 1;

                //expectedLetterIndex ovdje mora biti postavljen na prvo slovo iduce rijeci
                expectedLetterIndex = spaceCtr;
                for (int i = 0; i < wordsInText.Length && i<spaceCtr;  i++) {
                    expectedLetterIndex += wordsInText[i].Length;
                }
                if (spaceCtr == wordsInText.Length) {
                  
                    isGameOver = true;

                } else {
                    showExpectedLetterOnKeyboard();
                }
         
            } else {

                if (typedChar == '\b') {
                    //treba rucno maknuti char '\b' i zadnji znak koji ne obrise
                    if(typedText.Length >= 2) {
                        typedText = typedText.Substring(0, typedText.Length - 2);
                    } else {
                        typedText = "";
                    }
                }

                typedLetters = typedText.ToCharArray();
                int lengthOfTypedText = typedText.Length;
                string wordSubstr = "";
                bool longer = true;

                if (lengthOfTypedText <= wordsInText[spaceCtr].Length) { 
                    wordSubstr = wordsInText[spaceCtr].Substring(0, lengthOfTypedText);
                    longer = false;
                }

                if (wordSubstr == typedText && !longer) {

                    //prelazimo na sljedece slovo
                    expectedLetterIndex = spaceCtr + wordSubstr.Length;
                    for (int i = 0; i < wordsInText.Length && i < spaceCtr; i++) {
                        expectedLetterIndex += wordsInText[i].Length;
                    }

                    if (expectedLetterIndex < lettersInText.Length) {
                        showExpectedLetterOnKeyboard();
                    } else {
                        isGameOver = true;
                    }


                } else {

                    expectedLetterIndex = spaceCtr + typedText.Length;
                    for (int i = 0; i < wordsInText.Length && i < spaceCtr; i++) {
                        expectedLetterIndex += wordsInText[i].Length;
                    }
                    if (expectedLetterIndex < lettersInText.Length) {
                        showExpectedLetterOnKeyboard();
                    } else {
                        isGameOver = true;
                    }

                }
                
            }
            
        }

        public void handleInputSkipErrorsOff(KeyEventArgs e, char typedChar) {
            char expectedLetterIndexInText = lettersInText[expectedLetterIndex];

            if ((typedChar == Char.ToUpper(expectedLetterIndexInText)) || (typedChar == '-' && expectedLetterIndexInText == ' ')) {             
                if (expectedLetterIndex < lettersInText.Length - 1) {
                    if (wrongCharacter!='0')
                        removeWrongLetterFromKeyboard();

                    removeExpectedLetterFromKeyboard();
                    showNextLetterOnKeyboard();
                    expectedLetterIndex += 1;
                } 
                else 
                    isGameOver = true;  
            }
            else if (typedChar != '?') {
                e.SuppressKeyPress = true;
                if (wrongCharacter!='0')
                    removeWrongLetterFromKeyboard();
                wrongCharacter = typedChar;
                showWrongLetterOnKeyboard();   
            }
            
        }

        //ovo treba srediti - vec postoji fja showExpected koja se koristi pa bi ovu bilo dobro maknuti
        //nakon implementacije skipErrors = false;
        public void showNextLetterOnKeyboard()
        {
            char nextLetter = lettersInText[expectedLetterIndex + 1];
            string letter = "" + Char.ToUpper(nextLetter);

            if (nextLetter == ' ') {
                letter = "SPACE";
            }
             
            try {
                var nextKey = keyboard.Find(letter, true);
                nextKey[0].BackColor = Color.LightGreen;
            } catch (Exception ex) {
                Console.WriteLine("Nije nađeno slovo.");
                isGameOver = true;
            }  
        }

        public void showWrongLetterOnKeyboard()
        {
            string letter = wrongCharacter.ToString();

            if (letter == " ") {
                letter = "SPACE";
            }
            try  {
                var key = keyboard.Find(letter, true);
                key[0].BackColor = Color.Red;

            } catch (Exception ex) {
                Console.WriteLine("Nije nađeno slovo.");
                isGameOver = true;
            }

        }

        public void removeWrongLetterFromKeyboard() {

            string letter = wrongCharacter.ToString();

            if (letter == " ") {
                letter = "SPACE";
            }

            try {
                var key = keyboard.Find(letter, true);
                key[0].BackColor = Color.White;

            } catch (Exception ex) {
                Console.WriteLine("Nije nađeno slovo.");
                isGameOver = true;
            }

        }



        public void showExpectedLetterOnKeyboard() {
            char expectedLetter = lettersInText[expectedLetterIndex];
            string letter = "" + Char.ToUpper(expectedLetter);

            if (expectedLetter == ' ') {
                letter = "SPACE";
            }

            try {
                var nextKey = keyboard.Find(letter, true);
                nextKey[0].BackColor = Color.LightGreen;
            } catch (Exception ex) {
                Console.WriteLine("Nije nađeno slovo.");
                isGameOver = true;
            }
        }

        public void removeExpectedLetterFromKeyboard() {
            char current = lettersInText[expectedLetterIndex];
            string letter = "" + Char.ToUpper(current);

            if (current == ' ') {
                letter = "SPACE";
            }

            try {
                var currentKey = keyboard.Find(letter, true);
                currentKey[0].BackColor = Color.White;

            } catch (Exception ex) {
                Console.WriteLine("Nije nađeno slovo.");
                isGameOver = true;
            }

        }


        //public void handleInputSkipErrorsOff(char typedChar, string typedText)

    }
}
