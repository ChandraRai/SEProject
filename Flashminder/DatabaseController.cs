using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Flashminder.Models;

namespace Flashminder
{
    public class DatabaseController : ApiController
    {
        public class Flashcard_DBInfo
        {
            public List<Flashcard> flashcards;
            public List<Category> categories;
            public List<Flashcard_Category> cardsToCategories;
            public List<Flashcard_Algorithm_Data> algorithmData;

            public Flashcard_DBInfo(List<Flashcard> inCards, List<Category>inCats, List<Flashcard_Category> inCardsToCats, List<Flashcard_Algorithm_Data> inAlgData)
            {
                flashcards = inCards;
                categories = inCats;
                cardsToCategories = inCardsToCats;
                algorithmData = inAlgData;
            }
        }

        // GET api/db
        public Flashcard_DBInfo Get()
        {
            Flashcard_DBInfo dbInfo;
            using(DefaultConnection db = new DefaultConnection())
            {
                List<Flashcard> cards = new List<Flashcard>();
                foreach (Flashcard card in db.Flashcards.ToList())
                {
                    Flashcard newCard = new Flashcard();
                    newCard.Id = card.Id;
                    newCard.BackImage = card.BackImage;
                    newCard.FrontImage = card.FrontImage;
                    newCard.FrontText = card.FrontText;
                    newCard.BackText = card.BackText;
                    newCard.CreatedDate = card.CreatedDate;
                    newCard.fk_CardType = card.fk_CardType;
                    newCard.UserId = card.UserId;
                    newCard.ModifiedDate = card.ModifiedDate;
                    cards.Add(newCard);
                }
                List<Category> cats = new List<Category>();
                foreach (Category cat in db.Categories.ToList())
                {
                    Category newCat = new Category();
                    newCat.CategoryDesc = cat.CategoryDesc;
                    newCat.CategoryName = cat.CategoryName;
                    newCat.CreatedDate = cat.CreatedDate;
                    newCat.Id = cat.Id;
                    newCat.UserId = cat.UserId;
                    newCat.ModifiedDate = cat.ModifiedDate;
                    cats.Add(newCat);
                }
                List<Flashcard_Category> cardToCats = new List<Flashcard_Category>();
                foreach (Flashcard_Category cardToCat in db.Flashcard_Category.ToList())
                {
                    Flashcard_Category newCardToCat = new Flashcard_Category();
                    newCardToCat.UserID = cardToCat.UserID;
                    newCardToCat.FlashcardId = cardToCat.FlashcardId;
                    newCardToCat.CategoryId = cardToCat.CategoryId;
                    cardToCats.Add(newCardToCat);
                }
                List<Flashcard_Algorithm_Data> algDatas = new List<Flashcard_Algorithm_Data>();
                foreach (Flashcard_Algorithm_Data algData in db.Flashcard_Algorithm_Data.ToList())
                {
                    Flashcard_Algorithm_Data newAlgData = new Flashcard_Algorithm_Data();
                    newAlgData.Easiness = algData.Easiness;
                    newAlgData.FlashcardId = algData.FlashcardId;
                    newAlgData.Id = algData.Id;
                    newAlgData.Interval = algData.Interval;
                    newAlgData.NextPratice = algData.NextPratice;
                    newAlgData.Quality = algData.Quality;
                    newAlgData.Repetitions = algData.Repetitions;
                    algDatas.Add(newAlgData);
                }
                dbInfo = new Flashcard_DBInfo(cards, cats, cardToCats, algDatas);
            }

            return dbInfo;
        }

        // GET api/db
        public Flashcard_DBInfo Get(DateTime modifiedDate)
        {
            Flashcard_DBInfo dbInfo;
            using (DefaultConnection db = new DefaultConnection())
            {
                List<Flashcard> cards = new List<Flashcard>();
                foreach (Flashcard card in db.Flashcards.Where(card => (card.ModifiedDate >= modifiedDate)).ToList())
                {
                    Flashcard newCard = new Flashcard();
                    newCard.Id = card.Id;
                    newCard.BackImage = card.BackImage;
                    newCard.FrontImage = card.FrontImage;
                    newCard.FrontText = card.FrontText;
                    newCard.BackText = card.BackText;
                    newCard.CreatedDate = card.CreatedDate;
                    newCard.fk_CardType = card.fk_CardType;
                    newCard.UserId = card.UserId;
                    newCard.ModifiedDate = card.ModifiedDate;
                    cards.Add(newCard);
                }
                List<Category> cats = new List<Category>();
                foreach (Category cat in db.Categories.Where(cat => (cat.ModifiedDate >= modifiedDate)).ToList())
                {
                    Category newCat = new Category();
                    newCat.CategoryDesc = cat.CategoryDesc;
                    newCat.CategoryName = cat.CategoryName;
                    newCat.CreatedDate = cat.CreatedDate;
                    newCat.Id = cat.Id;
                    newCat.UserId = cat.UserId;
                    newCat.ModifiedDate = cat.ModifiedDate;
                    cats.Add(newCat);
                }
                List<Flashcard_Category> cardToCats = new List<Flashcard_Category>();
                foreach (Flashcard_Category cardToCat in db.Flashcard_Category.Where(cardToCat => (cardToCat.ModifiedDate >= modifiedDate)).ToList())
                {
                    Flashcard_Category newCardToCat = new Flashcard_Category();
                    newCardToCat.UserID = cardToCat.UserID;
                    newCardToCat.FlashcardId = cardToCat.FlashcardId;
                    newCardToCat.CategoryId = cardToCat.CategoryId;
                    cardToCats.Add(newCardToCat);
                }
                List<Flashcard_Algorithm_Data> algDatas = new List<Flashcard_Algorithm_Data>();
                foreach (Flashcard_Algorithm_Data algData in db.Flashcard_Algorithm_Data.Where(data => (data.ModifiedDate>=modifiedDate)).ToList())
                {
                    Flashcard_Algorithm_Data newAlgData = new Flashcard_Algorithm_Data();
                    newAlgData.Easiness = algData.Easiness;
                    newAlgData.FlashcardId = algData.FlashcardId;
                    newAlgData.Id = algData.Id;
                    newAlgData.Interval = algData.Interval;
                    newAlgData.NextPratice = algData.NextPratice;
                    newAlgData.Quality = algData.Quality;
                    newAlgData.Repetitions = algData.Repetitions;
                    algDatas.Add(newAlgData);
                }
                dbInfo = new Flashcard_DBInfo(cards, cats, cardToCats, algDatas);
            }

            return dbInfo;
        }

        // POST api/db
        public HttpResponseMessage Post([FromBody] Flashcard_DBInfo info)
        {
            int count = 0;


            using (DefaultConnection db = new DefaultConnection())
            {

                foreach (Category category in info.categories)
                {
                    if (!db.Categories.Where(cat => (cat.Id == category.Id)).Any())
                    {
                        db.Categories.Add(category);                    
                        count++;
                    }
                }

                foreach (Flashcard flashcard in info.flashcards)
                {
                    if (!db.Flashcards.Where(card => (card.Id == flashcard.Id)).Any())
                    {
                        db.Flashcards.Add(flashcard);
                        count++;

                    }
                }

                foreach (Flashcard_Category flashCategory in info.cardsToCategories)
                {
                    if (!db.Flashcard_Category.Where(flashCat => (flashCat.FlashcardId == flashCategory.FlashcardId)).Any())
                    {
                        db.Flashcard_Category.Add(flashCategory);
                        count++;
                    }
                }

                foreach (Flashcard_Algorithm_Data algData in info.algorithmData)
                {
                    if (!db.Flashcard_Algorithm_Data.Where(alg => (alg.Id == algData.Id)).Any())
                    {
                        db.Flashcard_Algorithm_Data.Add(algData);
                        count++;
                    }
                }

                int changes = db.SaveChanges();
                if (changes != count)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
        }
    }
}