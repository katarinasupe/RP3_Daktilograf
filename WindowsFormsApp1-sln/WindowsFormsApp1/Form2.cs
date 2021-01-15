using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void generateExButton_Click(object sender, EventArgs e)
        {
            //char[] delimiterChars = {','};
            //string[] letters = this.practiceLetters.Text.Split(delimiterChars);
            //array slova za generiranje vjezbe
            string[] letters = this.practiceLetters.Text.Select(c => c.ToString()).ToArray();
            foreach (var letter in letters)
            {
                Console.WriteLine(letter.ToString());
            }
            var lengthOfExercise = this.lenOfEx.Value;
            var lengthOfWords = this.lenOfWords.Value;
            var name = this.exNameTextBox.Text;

            Console.WriteLine(lengthOfExercise);
            Console.WriteLine(lengthOfWords);
            Exercise ex = new Exercise(letters, lengthOfExercise, lengthOfWords, name);
            string newEx = ex.generateExercise();
            RadioButton radioButton = new RadioButton();
            radioButton.Text = name;
            radioButton.Width = 100;
            radioButton.Height = 25;
            ((Form1)this.Owner).exPanel.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Checked = false;
            radioButton.Checked = true;

            ((Form1)this.Owner).exPanel.Controls.Add(radioButton);

            //((Form1)this.Owner).setTextToType(newEx);
            ((Form1)this.Owner).loadUserEx.PerformClick();
            this.Close();
        }

        private bool checkPracticeLetters()
        {
            //dopustamo samo unos slova
            string pattern = @"^[a-z]+$"; //ne dozvoljava prazan string
            //neovisno o caseu
            Match m = Regex.Match(this.practiceLetters.Text, pattern, RegexOptions.IgnoreCase);
            bool isRegexOk = m.Success;
            bool areLettersDistinct = this.practiceLetters.Text.Distinct().Count() == this.practiceLetters.Text.Length;
            
            if (isRegexOk && areLettersDistinct)
                return true;
            else
                return false;
        }

        private bool checkExName()
        {
            //dozvoljavamo slova, brojke, underscore i dash u imenu vjezbe (ne moze biti prazno)
            string pattern = @"^[a-z0-9_-]+$";
            //neovisno o caseu
            Match m = Regex.Match(this.exNameTextBox.Text, pattern, RegexOptions.IgnoreCase);
            bool isRegexOk = m.Success;
            //provjera postoji li vec vjezba s unesenim imenom
            string[] paths = { Environment.CurrentDirectory, @"..\..\exercises\user_ex", this.exNameTextBox.Text + ".txt" };
            string fullPath = System.IO.Path.Combine(paths);
            bool isSaved = System.IO.File.Exists(fullPath);

            if (isRegexOk && !isSaved)
                return true;
            else
                return false;
        }

        private void practiceLetters_TextChanged(object sender, EventArgs e)
        {
            if (checkExName() && checkPracticeLetters())
            {
                this.generateExButton.Enabled = true; 
            }
            else
            {
                this.generateExButton.Enabled = false;
            }
        }

        private void exNameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (checkExName() && checkPracticeLetters())
            {
                this.generateExButton.Enabled = true;
            }
            else
            {
                this.generateExButton.Enabled = false;
            }
        }
    }
}
