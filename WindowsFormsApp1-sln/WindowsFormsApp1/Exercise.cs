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
            
            string word="";
            var rand = new Random();
            int mIndex;
            bool first = true;
            for (int j = 0; j < mlenOfExercise; j++)
			{
                word="";
                for (int i = 0; i < mlenOfWords; i++)
			    {
                    mIndex = rand.Next(mlettersToExercise.Length);
                    word+=mlettersToExercise[mIndex];
			    }
                if (first)
                {
                    ex = word;
                    first = false;
                }
                else
                    ex = ex + ' ' + word;
            }

            
            System.Diagnostics.Debug.WriteLine(mname);
            saveExercise();
            return ex;

        }

        private void saveExercise()
        {
            string[] paths = { Environment.CurrentDirectory, @"..\..\exercises\user_ex", mname + ".txt"};
            string fullPath = System.IO.Path.Combine(paths);
            System.IO.File.WriteAllText(fullPath, ex);
        }
    }
}
