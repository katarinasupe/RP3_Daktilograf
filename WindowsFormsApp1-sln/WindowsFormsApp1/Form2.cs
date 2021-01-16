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
    /*---Klasa koja predstavlja dijalog za kreiranje nove vježbe.---*/
    public partial class Form2 : Form
    {
        /*---Konstruktor klase Form2.---*/
        public Form2()
        {
            InitializeComponent();
            toolTip1.SetToolTip(practiceLetters, "Upišite slova bez razmaka i bez ponavljanja.");
            toolTip1.SetToolTip(practiceLettersLabel, "Upišite slova bez razmaka i bez ponavljanja.");
            toolTip2.SetToolTip(exNameLabel, "Ime vježbe mora biti različito od postojećih vježbi.");
            toolTip2.SetToolTip(exNameTextBox, "Ime vježbe mora biti različito od postojećih vježbi.");
        }

        /*---Event pritiska gumba 'Generiraj i spremi'.---*/
        private void generateExButton_Click(object sender, EventArgs e)
        {
            //array slova za generiranje vjezbe
            string[] letters = this.practiceLetters.Text.Select(c => c.ToString()).ToArray();

            //kreiraj novu vjezbu s danim parametrima
            var lengthOfExercise = this.lenOfEx.Value;
            var lengthOfWords = this.lenOfWords.Value;
            var name = this.exNameTextBox.Text;
            Exercise ex = new Exercise(letters, lengthOfExercise, lengthOfWords, name);

            //generiranje i spremanje vjezbe
            string newEx = ex.generateExercise(); 

            //kreiranje radio buttona za novu vjezbu
            RadioButton radioButton = new RadioButton();
            radioButton.Text = name;
            radioButton.AutoSize = true;
            ((Form1)this.Owner).exPanel.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Checked = false;
            radioButton.Checked = true;
            ((Form1)this.Owner).exPanel.Controls.Add(radioButton);

            //((Form1)this.Owner).setTextToType(newEx);
            ((Form1)this.Owner).loadUserEx.PerformClick(); //ucitaj generiranu vjezbu
            this.Close(); //zatvori dijalog
        }

        /*---Metoda koja provjerava korektnost unosa slova za vjezbanje.---*/
        private bool checkPracticeLetters()
        {
            //dopustamo samo unos slova
            string pattern = @"^[a-z]+$"; //ne dozvoljava prazan string
            //neovisno o caseu
            Match m = Regex.Match(this.practiceLetters.Text, pattern, RegexOptions.IgnoreCase);
            bool isRegexOk = m.Success;
            //provjera da su sva unesena slova razlicita
            bool areLettersDistinct = this.practiceLetters.Text.Distinct().Count() == this.practiceLetters.Text.Length;
            
            if (isRegexOk && areLettersDistinct)
                return true;
            else
                return false;
        }

        /*---Metoda koja provjerava korektnost unosa naziva nove vjezbe.---*/
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

        /*---Event promjene teksta u tektualnom okviru za unos slova za vjezbu.---*/
        private void practiceLetters_TextChanged(object sender, EventArgs e)
        {
            //gumb 'Generiraj i spremi' je omogucen ako su oba tekstualna okvira korektna
            if (checkExName() && checkPracticeLetters())
            {
                this.generateExButton.Enabled = true; 
            }
            else
            {
                this.generateExButton.Enabled = false;
            }
        }

        /*---Event promjene teksta u tektualnom okviru za unos naziva nove vjezbe.---*/
        private void exNameTextBox_TextChanged(object sender, EventArgs e)
        {
            //gumb 'Generiraj i spremi' je omogucen ako su oba tekstualna okvira korektna
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
