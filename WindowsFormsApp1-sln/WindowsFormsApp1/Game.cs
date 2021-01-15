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
        private Form1 form;
        Control.ControlCollection keyboard;
        Control textBox;
        bool bl = true;
        private int expectedLetterIndexFirstMistake=-1;
        private int wrongLettersCounter = 0;
        private int correctLettersCounter = 0;
        private int allLettersCounter = 0;
        private int correctWordsCounter = 0;

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

        public Game(Form1 form1) {
            this.expectedLetterIndex = 0;
            this.wrongCharacter = '0';
            isGameOver = false;
            form = form1;
        }

        public void startGame(Control textBox, Control.ControlCollection keyboard, char[] text, string[] words) {
            this.lettersInText = text;
            this.wordsInText = words;
            this.spaceCtr = 0;
            this.wrongCharacter = '0';
            this.textBox = textBox;
            this.keyboard = keyboard;
            this.expectedLetterIndex = 0;
            this.showExpectedLetterOnKeyboard();
            isGameOver = false;
        }

        
        public void handleInputSkipErrorsOn(KeyEventArgs e,char typedChar, string typedText)
        {

            if (expectedLetterIndex < lettersInText.Length)
                removeExpectedLetterFromKeyboard();
            if (wrongCharacter != '0')
                removeWrongLetterFromKeyboard();
            if (typedChar == '?')
            {
                e.SuppressKeyPress = true;
                if (expectedLetterIndex < lettersInText.Length)
                    showExpectedLetterOnKeyboard();
                return;
            }
            else if ((expectedLetterIndex >= lettersInText.Length || typedChar != lettersInText[expectedLetterIndex]) && typedChar != '\b' && typedChar != ' ')
            {

                wrongCharacter = typedChar;
                showWrongLetterOnKeyboard();
                wrongLettersCounter++;
                System.Diagnostics.Debug.WriteLine("krivih: " + wrongLettersCounter);

                if (bl == true)
                {
                    var lbls = form.textToType.Controls.OfType<Label>().ToArray();
                    lbls[0].BackColor = Color.Red;
                    bl = false;
                    expectedLetterIndexFirstMistake = expectedLetterIndex;
                }
            }

            else if (typedChar == ' ')
            {
                System.Diagnostics.Debug.WriteLine("Typed text: " + typedText);
                System.Diagnostics.Debug.WriteLine("Word in text: " + wordsInText[spaceCtr]);
                if (typedText.Substring(0, typedText.Length - 1) == wordsInText[spaceCtr])
                {
                    correctWordsCounter++;
                    System.Diagnostics.Debug.WriteLine("Točna rič: " + correctWordsCounter);
                }
                spaceCtr += 1;
                //expectedLetterIndex ovdje mora biti postavljen na prvo slovo iduce rijeci
                expectedLetterIndex = spaceCtr; //jer moramo ubrojiti i razmake
                handleNextExpectedLetter();
                bl = true;
                expectedLetterIndexFirstMistake = -1;
                return;
            }

            else if (typedChar == '\b')
            {
                //treba rucno maknuti char '\b' i zadnji znak koji ne obrise
                if (typedText.Length >= 2)
                    typedText = typedText.Substring(0, typedText.Length - 2);
                else
                    typedText = "";

            }
            else
            {
                correctLettersCounter++;
                System.Diagnostics.Debug.WriteLine("točnih: " + correctLettersCounter);
            }


            typedLetters = typedText.ToCharArray();
            int lengthOfTypedText = typedText.Length;
            string wordSubstr = "";
            bool longer = true;

            if (lengthOfTypedText <= wordsInText[spaceCtr].Length)
            {
                wordSubstr = wordsInText[spaceCtr].Substring(0, lengthOfTypedText);
                longer = false;
            }

            if (wordSubstr == typedText && !longer)
            {
                //prelazimo na sljedece slovo
                expectedLetterIndex = spaceCtr + wordSubstr.Length;
                handleNextExpectedLetter();
                if (expectedLetterIndexFirstMistake == expectedLetterIndex)
                {
                    System.Diagnostics.Debug.WriteLine(expectedLetterIndexFirstMistake + " ");
                    System.Diagnostics.Debug.WriteLine(expectedLetterIndex);
                    var lbls = form.textToType.Controls.OfType<Label>().ToArray();
                    lbls[0].BackColor = Color.LightBlue;
                    bl = true;
                    expectedLetterIndexFirstMistake = -1;
                }
            }
            else if((wordSubstr != typedText && lengthOfTypedText == wordsInText[spaceCtr].Length) || !longer)
            {
                if (typedChar != '\b')
                    expectedLetterIndex++;

                else if (typedChar == '\b' && lengthOfTypedText < wordsInText[spaceCtr].Length)
                {
                    expectedLetterIndex--;

                }
                    
                if (expectedLetterIndex < lettersInText.Length)
                    showExpectedLetterOnKeyboard();


            }
            else if(expectedLetterIndex<lettersInText.Length)
                showExpectedLetterOnKeyboard();
            
        }

 

        //sa stopiranjem unosa
        public void handleInputSkipErrorsOff(KeyEventArgs e, char typedChar, string typedText)
        {
            if (expectedLetterIndex < lettersInText.Length)
                removeExpectedLetterFromKeyboard();
            if (wrongCharacter != '0')
                removeWrongLetterFromKeyboard();
            if (typedChar == '?')
            {
                e.SuppressKeyPress = true;
                if (expectedLetterIndex < lettersInText.Length)
                    showExpectedLetterOnKeyboard();
                return;
            }
            else if ((expectedLetterIndex >= lettersInText.Length || typedChar != lettersInText[expectedLetterIndex]))
            {
                e.SuppressKeyPress = true;
                wrongCharacter = typedChar;
                showWrongLetterOnKeyboard();
                showExpectedLetterOnKeyboard();
                var lbls = form.textToType.Controls.OfType<Label>().ToArray();
                lbls[0].BackColor = Color.Red;
                if (typedChar != '\b')
                {
                    wrongLettersCounter++;
                    System.Diagnostics.Debug.WriteLine("krivih: " + wrongLettersCounter);
                }
            }
            else
            {
                correctLettersCounter++;
                System.Diagnostics.Debug.WriteLine("točnih: " + correctLettersCounter);
            }

            if (typedChar == lettersInText[expectedLetterIndex] && expectedLetterIndex < lettersInText.Length)
            {
                var lbls = form.textToType.Controls.OfType<Label>().ToArray();
                lbls[0].BackColor = Color.LightBlue;
                expectedLetterIndex++;
                if (expectedLetterIndex < lettersInText.Length)
                    showExpectedLetterOnKeyboard();
                else
                    isGameOver = true;
            }
            else if (expectedLetterIndex < lettersInText.Length)
                showExpectedLetterOnKeyboard();
            
        }

        private void handleNextExpectedLetter()
        {
            for (int i = 0; i < wordsInText.Length && i < spaceCtr; i++)
                expectedLetterIndex += wordsInText[i].Length;
            if (expectedLetterIndex < lettersInText.Length)
                showExpectedLetterOnKeyboard();
            else
                isGameOver = true;
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
                //isGameOver = true;

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
                //isGameOver = true;
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
