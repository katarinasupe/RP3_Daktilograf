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
        private string mname;
        private string ex;

        public Exercise(string[] lettersToExercise, decimal lenOfExercise, decimal lenOfWords, string name)
        {
            this.mlettersToExercise = lettersToExercise;
            this.mlenOfExercise = lenOfExercise;
            this.mlenOfWords = lenOfWords;
            this.mname = name;
            this.ex = "";
        }
        public string generateExercise()
        {
            //TODO
            //generiranje nove vjezbe string newEx nekim algoritmom na temelju danih parametara
            //this.mlettersToExercise itd. su parametri
            //spremanje te vjezbe
            
            string word="";
            var rand = new Random();
            int mIndex;
            for (int j = 0; j < mlenOfExercise; j++)
			{
                word="";
                for (int i = 0; i < mlenOfWords; i++)
			    {
                    mIndex = rand.Next(mlettersToExercise.Length);
                    word+=mlettersToExercise[mIndex];
			    }
                ex = ex + word + ' ';
			}

            
            System.Diagnostics.Debug.WriteLine(mname);

            saveExercise();
            return ex;

        }

        private void saveExercise()
        {
            //TODO
            //sto zapravo znaci spremati i ponovno ucitavati za stalno?

            string[] paths = { Environment.CurrentDirectory, @"..\..\vjezbe\user_ex", mname + ".txt"};
            string fullPath = System.IO.Path.Combine(paths);
            System.IO.File.WriteAllText(fullPath, ex);
        }
    }
}
