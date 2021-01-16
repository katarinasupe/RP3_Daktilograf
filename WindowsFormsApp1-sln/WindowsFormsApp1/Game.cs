using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    /*---Klasa koja predstavlja jednu igru za neku učitanu vježbu u daktilografu.---*/
    class Game {
        private char[] lettersInText;
        private string[] wordsInText;
        private int expectedLetterIndex; //slovo koje ocekujemo iduce
        private char wrongCharacter;
        private bool isGameOver;
        private Form1 form;
        Control.ControlCollection keyboard;
        bool firstError = true;
        private int expectedLetterIndexFirstMistake;

        private int wrongLettersCounter;
        private int correctLettersCounter;
        private int skippedLettersCounter;


        /*---Konstruktor klase Game.---*/
        public Game(Form1 form1)
        {
            this.expectedLetterIndex = 0;
            this.wrongCharacter = '0';
            isGameOver = false;
            form = form1;
        }

        /*------------getteri i setteri------------*/
        public int getWrongLettersCounter()
        {
            return this.wrongLettersCounter;
        }
        public int getCorrectLettersCounter()
        {
            return this.correctLettersCounter;
        }
        public int getSkippedLettersCounter()
        {
            return this.skippedLettersCounter;
        }
        public bool getIsGameOver() {
            return isGameOver;
        }

        //----------------------------------------


        /*---Metoda za početak igre - odgovara na pritisak gumba 'Započni vježbu'.---*/
        public void startGame( Control.ControlCollection keyboard, char[] text, string[] words) {
            this.lettersInText = text;
            this.wordsInText = words;
            this.wrongCharacter = '0';
            this.keyboard = keyboard;
            this.expectedLetterIndex = 0;
            this.showExpectedLetterOnKeyboard();
            isGameOver = false;

            this.expectedLetterIndexFirstMistake = -1;
            this.wrongLettersCounter = 0;
            this.correctLettersCounter = 0;
            this.skippedLettersCounter = 0;
        }

        /*---Metoda za igru kada je opcija preskakanja greški upaljena.---*/
        //funkcija vraća true ukoliko je unesen SPACE, odnosno ukoliko je potrebno prijeći na sljedeću riječ
        public bool handleInputSkipErrorsOn(KeyEventArgs e,char typedChar, string typedText, int spaceCtr)
        {
            int lengthOfTypedText = typedText.Length;

            if (expectedLetterIndex < lettersInText.Length)
                removeExpectedLetterFromKeyboard();
            if (wrongCharacter != '0')
                removeWrongLetterFromKeyboard();

            //nedopušteni znak
            if (typedChar == '?')
            {
                e.SuppressKeyPress = true;
                if (expectedLetterIndex < lettersInText.Length)
                    showExpectedLetterOnKeyboard();
                return false;
            }

            //krivo slovo, osim backspace i space
            else if ((expectedLetterIndex >= lettersInText.Length ||  typedChar != lettersInText[expectedLetterIndex]) && typedChar != '\b' && typedChar != ' ')
            {
 
                wrongCharacter = typedChar;
                showWrongLetterOnKeyboard();
                wrongLettersCounter++;
                //pamti se pozicija prve greške u riječi i riječ se boja u crveno
                if (firstError == true)
                {
                    var lbls = form.textToType.Controls.OfType<Label>().ToArray();
                    lbls[0].BackColor = Color.PaleVioletRed;
                    firstError = false;
                    expectedLetterIndexFirstMistake = expectedLetterIndex;
                }

                if (lengthOfTypedText <= wordsInText[spaceCtr].Length)
                {
                    expectedLetterIndex++; 
                    if(expectedLetterIndex < lettersInText.Length)
                    {
                        showExpectedLetterOnKeyboard();
                    }
                }
            }

            //space
            else if (typedChar == ' ')
            {
                if (typedText.Length-1 == wordsInText[spaceCtr].Length  && typedChar == lettersInText[expectedLetterIndex])
                {
                    correctLettersCounter++;
                }

                 if(typedText.Length <= wordsInText[spaceCtr].Length)
                {
                    skippedLettersCounter += (wordsInText[spaceCtr].Length - typedText.Length + 1);
                }

                //expectedLetterIndex postavljamo na prvo slovo iduce rijeci
                expectedLetterIndex = spaceCtr+1; //jer moramo ubrojiti i razmake
                for (int i = 0; i < wordsInText.Length && i < spaceCtr+1; i++)
                    expectedLetterIndex += wordsInText[i].Length;
                if (expectedLetterIndex < lettersInText.Length)
                    showExpectedLetterOnKeyboard();
                else
                    isGameOver = true;

                firstError = true;
                expectedLetterIndexFirstMistake = -1;
                return true;
            }
            //backspace
            else if (typedChar == '\b')
            {
                //treba rucno maknuti char '\b' i zadnji znak koji ne obrise
                if (typedText.Length >= 2)
                {
                    typedText = typedText.Substring(0, typedText.Length - 2);
                    lengthOfTypedText -= 2;

                    if (lengthOfTypedText < wordsInText[spaceCtr].Length)
                        if (expectedLetterIndex > 0)
                            expectedLetterIndex--;
                }
                else
                {
                    typedText = "";
                    lengthOfTypedText = 0;
                }
                //ako smo došli do pozicije prve greške bojimo riječ u plavo
                if (lengthOfTypedText <= wordsInText[spaceCtr].Length && expectedLetterIndexFirstMistake == expectedLetterIndex)
                {
                    var lbls = form.textToType.Controls.OfType<Label>().ToArray();
                    lbls[0].BackColor = Color.LightBlue;
                    firstError = true;
                    expectedLetterIndexFirstMistake = -1;
                }

                if (expectedLetterIndex < lettersInText.Length && lengthOfTypedText <= wordsInText[spaceCtr].Length)
                {
                        showExpectedLetterOnKeyboard();
                }
            }
            //točno slovo
            else
            {
                correctLettersCounter++;

                if (expectedLetterIndex < lettersInText.Length-1)
                {
                    expectedLetterIndex++;
                    showExpectedLetterOnKeyboard();
                }
                else
                    isGameOver = true;
            }

            return false;
        }


        //sa stopiranjem unosa
        /*---Metoda za igru kada je opcija preskakanja greški ugašena.---*/
        //funkcija vraća true ukoliko je unesen SPACE, odnosno ukoliko je potrebno prijeći na sljedeću riječ
        public bool handleInputSkipErrorsOff(KeyEventArgs e, char typedChar)
        {
            
            if (expectedLetterIndex < lettersInText.Length)
                removeExpectedLetterFromKeyboard();
            if (wrongCharacter != '0')
                removeWrongLetterFromKeyboard();
            //ako je pritisnut nedopušteni znak
            if (typedChar == '?' || typedChar == '\b')
            {
                e.SuppressKeyPress = true;
                if (expectedLetterIndex < lettersInText.Length)
                    showExpectedLetterOnKeyboard();
                return false;
            }
            //ako je pritisnuto slovo krivo
            else if (typedChar != lettersInText[expectedLetterIndex])
            {
                e.SuppressKeyPress = true;
                wrongCharacter = typedChar;
                showWrongLetterOnKeyboard();
                showExpectedLetterOnKeyboard();
                var lbls = form.textToType.Controls.OfType<Label>().ToArray();
                lbls[0].BackColor = Color.PaleVioletRed;
                wrongLettersCounter++;
                return false;
            }
            //pritisnuto slovo je točno
            else
            {
                correctLettersCounter++;

                var lbls = form.textToType.Controls.OfType<Label>().ToArray();
                lbls[0].BackColor = Color.LightBlue;
                expectedLetterIndex++;
                if (expectedLetterIndex < lettersInText.Length)
                    showExpectedLetterOnKeyboard();
                else
                    isGameOver = true;
                if (typedChar == ' ')
                    return true;
                return false;
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
                key[0].BackColor = Color.PaleVioletRed;

            } catch (Exception ex) {
                Console.WriteLine("Nije nađeno slovo.");
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
            }

        }

        public void showExpectedLetterOnKeyboard() {
            char expectedLetter = lettersInText[expectedLetterIndex];
            string letter = "" + Char.ToUpper(expectedLetter);

            if (expectedLetter == ' ')
            {
                letter = "SPACE";
            }

            try
            {
                var nextKey = keyboard.Find(letter, true);
                nextKey[0].BackColor = Color.LightGreen;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Nije nađeno slovo.");
                isGameOver = true;
            }
        }

        public void removeExpectedLetterFromKeyboard() {
            char current = lettersInText[expectedLetterIndex];
            string letter = "" + Char.ToUpper(current);

            if (current == ' ')
            {
                letter = "SPACE";
            }

            try
            {
                var currentKey = keyboard.Find(letter, true);
                currentKey[0].BackColor = Color.White;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Nije nađeno slovo.");
                isGameOver = true;
            }

        }

    }
}
