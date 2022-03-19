using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Flashminder.Models;

namespace Flashminder
{
    public class LearningAlgorithms
    {
        //int repetitions = 0; // how many times a user has seen the flashcard
        //int interval = 1; // length of time between repetitions 
        ////int quality = 0; // 0-5     user defined difficulty 
        //float easiness = 2.5f; //  Easiness factor, used to increase space in spaced repetition
        //DateTime nextPractice; // the next time the flashcard will be shown 

        static public DateTime CalculateSM2Alg(int quality, double easiness, int interval, int repetitions, float multiplier = 1)
        {

            // quality need to be in bounds
            if (quality < 0 || quality > 5)
            {
                return new DateTime();
            }

            //EF
            easiness = Math.Max((1.3f), (double)(easiness + 0.1 - (5.0 - quality) * (0.08f + (5.0 - quality) * 0.02)));
            if (quality < 3)
            {
                repetitions = 0;
            }
            else
            {
                repetitions += 1;
            }

            if (repetitions <= 1)
            {
                interval = 1;
            }
            else if (repetitions == 2)
            {
                interval = 6;
            }
            else
            {
                interval = (int) (Math.Round(interval * easiness));
            }

            int millisecsInDay = 60 * 60 * 24 * 1000;
            long now = DateTime.Now.Ticks;
            long nextPracticeDate = now + (long)(TimeSpan.FromMilliseconds(millisecsInDay).Ticks * interval* multiplier);
            DateTime nextPractice;
            try
            {
                nextPractice = new DateTime(nextPracticeDate);
            }
            catch(ArgumentOutOfRangeException )
            {
                nextPractice= new DateTime(DateTime.MaxValue.Ticks);
            }
            // update next practice date for card
            return nextPractice;
        }

        static public Flashcard_Algorithm_Data UpdateSM2Algorithm(Flashcard_Algorithm_Data data, float multiplier)
        {

            // quality need to be in bounds
            if (data.Quality < 0 || data.Quality > 5)
            {
                return data;
            }

            //EF
            data.Easiness = Math.Max((1.3f), (double)(data.Easiness + 0.1 - (5.0 - data.Quality) * (0.08f + (5.0 - data.Quality) * 0.02)));
            if (data.Quality < 3)
            {
                data.Repetitions = 0;
            }
            else
            {
                data.Repetitions += 1;
            }

            if (data.Repetitions <= 1)
            {
                data.Interval = 1;
            }
            else if (data.Repetitions == 2)
            {
                data.Interval= 6;
            }
            else
            {
                data.Interval = (int) (Math.Round(data.Interval * data.Easiness) * multiplier);
            }

            int millisecsInDay = 60 * 60 * 24 * 1000;
            long now = DateTime.Now.Ticks;
            long nextPracticeDate = now + (long)(TimeSpan.FromMilliseconds(millisecsInDay).Ticks * data.Interval * multiplier);
            DateTime nextPractice;
            try
            {
                nextPractice = new DateTime(nextPracticeDate);
            }
            catch (ArgumentOutOfRangeException )
            {
                nextPractice = new DateTime(DateTime.MaxValue.Ticks);
            }
            data.NextPratice = nextPractice;
            // update next practice date for card
            return data;
        }

    }
}