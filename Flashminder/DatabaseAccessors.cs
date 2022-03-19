using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Flashminder.Models;

namespace Flashminder
{
    public class DatabaseAccessors
    {
        static public List<Flashcard> LoadFlashcardsByCategoryName(string user, string category)
        {
            List<Flashcard> flashcards = null;
            if (!string.IsNullOrEmpty(user))
            {
                int userInt = Int32.Parse(user);

                using (DefaultConnection db = new DefaultConnection())
                {
                    if (category == "All")
                    {

                        flashcards = db.Flashcards.Where(flashcard => (flashcard.UserId == userInt)).ToList();
                    }
                    else
                    {
                        IQueryable<Flashcard_Category> flashcardCategory = db.Flashcard_Category.Where(cat => (cat.Category.CategoryName == category));
                        flashcards = (from fc in db.Flashcards join combined in flashcardCategory on fc.Id equals combined.FlashcardId where fc.UserId == userInt select fc).ToList();

                    }
                }
            }
            return flashcards;
        }

        static public List<Flashcard_Algorithm_Data> LoadNextFlashcardData(List<Flashcard> where)
        {
            List<Flashcard_Algorithm_Data> data = null;

            using (DefaultConnection db = new DefaultConnection())
            {
                data = db.Flashcard_Algorithm_Data.ToList().Join(where, alg => alg.FlashcardId, fc => fc.Id, (alg, fc) => (alg)).OrderBy(date => date.NextPratice).ToList();
            }

            return data;
        }

        static public Flashcard LoadFlashcard(int flashcardId, int userId)
        {
            Flashcard flashcard = null;
            using (DefaultConnection db = new DefaultConnection())
            {
                flashcard = db.Flashcards.Where(id => id.Id == flashcardId && id.UserId == userId).FirstOrDefault();    
            }
            return flashcard;
        }


    }
}