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
            Exercise ex = new Exercise(letters, lengthOfExercise, lengthOfWords);
            string newEx = ex.generateExercise();
            ((Form1)this.Owner).setTextToType(newEx);
            this.Close();
        }
    }
}
