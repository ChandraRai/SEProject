using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

using Flashminder.Models;

namespace Flashminder
{
    public class DatabaseMutators
    {
        const int MAX_CARD_TEXT = 500;

        static public int CreateDefaultCategory(int userId)
        {
            int ret = 0;
            using (DefaultConnection db = new DefaultConnection())
            {
                Category category = new Category();
                category.CategoryName = "Default";
                category.CreatedDate = DateTime.Now;
                category.UserId = userId;
                db.Categories.Add(category);
                ret = db.SaveChanges();
            }
            return ret;
        }

        static public int UpdateNextDate(Flashcard_Algorithm_Data data, float multiplier)
        {
            int ret = 0;
            using (DefaultConnection db = new DefaultConnection())
            {
                Flashcard_Algorithm_Data update = db.Flashcard_Algorithm_Data.Find(data.Id);
                Flashcard_Algorithm_Data temp = LearningAlgorithms.UpdateSM2Algorithm(data, multiplier);
                update.Easiness = temp.Easiness;
                update.Interval = temp.Interval;
                update.Repetitions = temp.Repetitions;
                update.Quality = temp.Quality;
                update.NextPratice = temp.NextPratice;
                ret = db.SaveChanges();
            }
            return ret;
        }

        static public int CreateFlashcard(int userID, string categoryName, string frontText, string backText, string frontImage, string backImage)
        {
            // Connect to EF
            using (DefaultConnection db = new DefaultConnection())
            {
                Flashcard flashcard = new Flashcard();
                Flashcard_Category relation = new Flashcard_Category(); // set flashcard 2 category too
                Flashcard_Algorithm_Data algData = new Flashcard_Algorithm_Data();
                algData.Easiness = 2.5;
                algData.Flashcard = flashcard;
                algData.Interval = 1;
                algData.Quality = 0;
                algData.Repetitions = 0;
                algData.NextPratice = DateTime.Now;

                relation.Flashcard = flashcard;
                int selectedCategory = db.Categories.Where(cat => (cat.CategoryName == categoryName)).FirstOrDefault().Id;
                relation.CategoryId = selectedCategory;
                relation.UserID = userID;
                flashcard.CardType = db.CardTypes.Find(1); // set to default right now, set for different types later
                flashcard.UserId = userID;
                flashcard.FrontImage = "";
                flashcard.BackImage = "";
                if (frontText.Length<= MAX_CARD_TEXT)
                {
                    flashcard.FrontText = frontText; // clean?
                }
                if (backText.Length <= MAX_CARD_TEXT)
                {
                    flashcard.BackText = backText;
                }
                flashcard.FrontImage = frontImage;
                flashcard.BackImage = backImage;
                flashcard.CreatedDate = DateTime.Now;
                db.Flashcards.Add(flashcard);
                db.Flashcard_Category.Add(relation);
                db.Flashcard_Algorithm_Data.Add(algData);
                int ret = db.SaveChanges();
                return ret;
            }
        }

        static public int CreateFlashcard(int userID, string categoryName, string frontText, string backText, Image frontImage, Image backImage)
        {
            // Connect to EF
            using (DefaultConnection db = new DefaultConnection())
            {
                Flashcard flashcard = new Flashcard();
                Flashcard_Category relation = new Flashcard_Category(); // set flashcard 2 category too
                Flashcard_Algorithm_Data algData = new Flashcard_Algorithm_Data();
                algData.Easiness = 2.5;
                algData.Flashcard = flashcard;
                algData.Interval = 1;
                algData.Quality = 0;
                algData.Repetitions = 0;
                algData.NextPratice = DateTime.Now;

                relation.Flashcard = flashcard;
                int selectedCategory = db.Categories.Where(cat => (cat.CategoryName == categoryName)).FirstOrDefault().Id;
                relation.CategoryId = selectedCategory;
                relation.UserID = userID;
                flashcard.CardType = db.CardTypes.Find(1); // set to default right now, set for different types later
                flashcard.UserId = userID;
                flashcard.FrontImage = "";
                flashcard.BackImage = "";
                if (frontText.Length <= MAX_CARD_TEXT)
                {
                    flashcard.FrontText = frontText; // clean?
                }
                if (backText.Length <= MAX_CARD_TEXT)
                {
                    flashcard.BackText = backText;
                }

                if (frontImage != null)
                {
                    string fileName = DateTime.Now.ToString("MM-dd-yyyy_HHmmss");
                    string filetype = frontImage.RawFormat.ToString().ToLower();
                    frontImage.Save("Images/" + userID + "_" + fileName + "_front_" + filetype);
                    flashcard.FrontImage = userID + "_" + fileName + "_front_" + filetype;
                }
                if (backImage != null)
                {
                    string fileName = DateTime.Now.ToString("MM-dd-yyyy_HHmmss");
                    string filetype = backImage.RawFormat.ToString().ToLower();
                    backImage.Save("Images/" + userID + "_" + fileName + "_back_" + filetype);
                    flashcard.BackImage = userID + "_" + fileName + "_back_" + filetype;
                }

                flashcard.CreatedDate = DateTime.Now;
                db.Flashcards.Add(flashcard);
                db.Flashcard_Category.Add(relation);
                db.Flashcard_Algorithm_Data.Add(algData);
                int ret = db.SaveChanges();
                return ret;
            }
        }

        static public bool DeleteFlashcard(int id)
        {
            bool success = false;
            using (DefaultConnection db = new DefaultConnection())
            {
                Flashcard toRemove = db.Flashcards.Where(card => (card.Id == id)).FirstOrDefault();
                Flashcard_Category relation = db.Flashcard_Category.Where(cat => (cat.FlashcardId == id)).FirstOrDefault();
                Flashcard_Algorithm_Data alg = db.Flashcard_Algorithm_Data.Where(algData => (algData.FlashcardId == id)).FirstOrDefault();
                if (toRemove != null)
                {
                    db.Flashcards.Remove(toRemove);
                    db.Flashcard_Category.Remove(relation);
                    db.Flashcard_Algorithm_Data.Remove(alg);
                }

                if (db.SaveChanges() == 3)
                {
                    success = true;
                }
            }
            return success;
        }

    }
}