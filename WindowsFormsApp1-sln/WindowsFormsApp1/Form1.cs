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

namespace WindowsFormsApp1 {
    public partial class Form1 : Form {
      
        private Control.ControlCollection keyboard;
        private String letter;

        private Game newGame;
        private bool skipError;
        private bool isGameOn;

        Stopwatch timer = new Stopwatch();

        //getters, setters
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
            initializeGame();
       }

        void initializeGame()
        {
            keyboard = this.panel1.Controls;
            newGame = new Game();
            isGameOn = false;
        }

        private void startBtn_Click(object sender, EventArgs e) {

            isGameOn = true;
            changeFormAppearance();
            startNewGame(); 
        }

        private void restartBtn_Click(object sender, EventArgs e) {

            isGameOn = true;
            resetKeyboard();
            changeFormAppearance();
            startNewGame();
        }

        void changeFormAppearance() {

            if (isGameOn) {
                this.startBtn.Enabled = false;
                this.restartBtn.Enabled = true;
                this.typedText.Enabled = true;
                this.typedText.Text = "";
                this.typedText.Focus();
            } else {
                this.startBtn.Enabled = true;
                this.restartBtn.Enabled = true;
                this.typedText.Enabled = false;
                this.typedText.Text = ""; //ispraznimo textbox ako je igra gotova
            }


            /*//ne mozemo vise mijenjati opciju preskakanje greške
            this.skipErrorCheckbox.Enabled = false; !!!!!!!!!!!!!!!!!
            */
        }

        private void startNewGame() {

            isGameOn = true;
            timer.Restart();

            var text = this.textToType.Text.ToCharArray();

            if (text.Length > 0) {
                newGame.startGame(keyboard, text);
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
            if (typedChar == '?') {
               
                //todo - handlanje greske

            } else if (typedChar == Char.ToUpper(newGame.getLettersInText()[newGame.getCurrentLetter()])) {
                newGame.removeCurrentLetterFromKeyboard(keyboard, letter);
                if (newGame.getWrongCharacter() != '0') {
                    //todo
                    // removeWrongLetterFromKeyboard();
                    //wrongCharacter = '0';
                }
                newGame.setCurrentLetter(newGame.getCurrentLetter() + 1);
                if(newGame.getCurrentLetter() < newGame.getLettersInText().Length) 
                {
                    letter = "" + Char.ToUpper(newGame.getLettersInText()[newGame.getCurrentLetter()]) + "";
                    newGame.showNextLetterOnKeyboard(keyboard, letter);
                }
                else
                {
                    stopTyping();
                }
             
            } else {
                newGame.setWrongCharacter(typedChar);
                newGame.showWrongLetterOnKeyboard(keyboard, letter);
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
            string text = System.IO.File.ReadAllText(
                System.IO.Path.Combine(Environment.CurrentDirectory, fileName));
            Console.WriteLine(text);
            this.textToType.Text = text;

            //svaki put kad se ucita nova vjezba, resetiraj tipkovnicu i zaustavi igru ako traje
            isGameOn = false;
            resetKeyboard();
            changeFormAppearance();
        }

        private void resetKeyboard()
        {
            foreach (Label key in keyboard)
            {
                key.BackColor = Color.White;
            }
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
        





        /* 
         * PRIMJER FOREACH
             foreach (Label tipka  in this.panel1.Controls.OfType<Label>()) {
                 String label = "label" + tipka.Text;
                 tipka.Name = label;
             }
             
         */


    }
}
