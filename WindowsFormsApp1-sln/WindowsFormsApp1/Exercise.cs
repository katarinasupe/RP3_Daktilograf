using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Exercise
    {
        private string[] mlettersToExercise;
        private decimal mlenOfExercise;
        private decimal mlenOfWords;

        public Exercise(string[] lettersToExercise, decimal lenOfExercise, decimal lenOfWords)
        {
            this.mlettersToExercise = lettersToExercise;
            this.mlenOfExercise = lenOfExercise;
            this.mlenOfWords = lenOfWords;
        }
        public string generateExercise()
        {
            //TODO
            //generiranje nove vjezbe string newEx nekim algoritmom na temelju danih parametara
            //this.mlettersToExercise itd. su parametri
            //spremanje te vjezbe


            saveExercise(this);
            return string.Join(" ", this.mlettersToExercise);

        }

        private void saveExercise(Exercise ex)
        {
            //TODO
            //sto zapravo znaci spremati i ponovno ucitavati za stalno?
        }
    }
}
