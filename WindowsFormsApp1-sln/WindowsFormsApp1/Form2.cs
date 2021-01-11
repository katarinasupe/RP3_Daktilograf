using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
            char[] delimiterChars = {','};
            //array slova za generiranje vjezbe
            string[] letters = this.practiceLetters.Text.Split(delimiterChars);
            foreach (var letter in letters)
            {
                Console.WriteLine(letter.ToString());
            }
            var lengthOfExercise = this.lenOfEx.Value;
            var lengthOfWords = this.lenOfWords.Value;
            Console.WriteLine(lengthOfExercise);
            Console.WriteLine(lengthOfWords);
            generateNewExercise(letters, lengthOfExercise, lengthOfWords);
            this.Close();
        }

        private void generateNewExercise(string[] letters, decimal lenOfEx, decimal lenOfWords)
        {
            //TODO
            //generiraj vj, kreiraj txt file, sejvaj ga u trenutni i prikazi na Form1.typedText.Text

            /*ovako bi se postavljao tekst u parent Form1, samo ne na letters[0], vec na novu generiranu vj
            to je ok jer san napravila get i set za textToType, a Form2 san pozvala sa .ShowDialog(this)
           sto govori da je Form1 parent od Form2*/
            ((Form1)this.Owner).setTextToType(letters[0]);
        }
    }
}
