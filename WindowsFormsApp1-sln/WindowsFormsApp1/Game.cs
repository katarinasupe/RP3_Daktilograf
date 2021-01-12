using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class Game
    { 
        private char[] lettersInText;
        private int currentLetter; //pamtimo na kojem se slovu nalazimo
        private char wrongCharacter;


        public char getWrongCharacter()
        {
            return this.wrongCharacter;
        }
        public int getCurrentLetter()
        {
            return this.currentLetter;
        }
        public char[] getLettersInText()
        {
            return this.lettersInText;
        }
        public void setLettersInText(char[] value)
        {
            this.lettersInText = value;
        }
        public void setCurrentLetter(int value)
        {
            this.currentLetter = value;
        }
        public void setWrongCharacter(char value)
        {
            this.wrongCharacter = value;
        }

        public Game(int mcurrentLetter, char mwrongCharacter)
        {
            this.currentLetter = mcurrentLetter;
            this.wrongCharacter = mwrongCharacter;
        }

        public void startGame(Control.ControlCollection keyboard, String letter)
        {
            this.showNextLetterOnKeyboard(keyboard, letter);
        }


        public void showNextLetterOnKeyboard(Control.ControlCollection keyboard, String letter)
        {          
            try
            {
                var nextKey = keyboard.Find(letter, true);
                nextKey[0].BackColor = Color.LightGreen;
            }
            catch (Exception ex)
            {

                Console.WriteLine("Nije nađeno slovo.");
                //DODATI NEKI PREKID IGRE (ZAUSTAVITI VRIJEME I BLOKIRATI UNOS)
            }  
        }

        public void showWrongLetterOnKeyboard(Control.ControlCollection keyboard, String letter)
        {
            try
            {
                var key = keyboard.Find(letter, true);
                key[0].BackColor = Color.Red;

            }
            catch (Exception ex)
            {

                Console.WriteLine("Nije nađeno slovo.");
            }

        }

        public void removeCurrentLetterFromKeyboard(Control.ControlCollection keyboard, String letter)
        {
            try
            {
                var currentKey = keyboard.Find(letter, true);
                //vraca kolekciju elemenata (ovdje ce biti samo 1 element zbog jedinstvenog imena)
                currentKey[0].BackColor = Color.White;

            }
            catch (Exception ex)
            {

                Console.WriteLine("Nije nađeno slovo.");
                //DODATI NEKI PREKID IGRE (ZAUSTAVITI VRIJEME I BLOKIRATI UNOS)
            }

        }
    }
}
