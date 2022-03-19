using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;

using Flashminder.Models;

namespace Flashminder
{
    public class SupermemoController : ApiController
    {
        // GET
        // api/supermemo/<id>
        public string Get(int id, int multiplier)
        {
            DateTime nextDate = new DateTime();
            using (DefaultConnection db = new DefaultConnection())
            {
                Flashcard_Algorithm_Data data = db.Flashcard_Algorithm_Data.Where(alg => (alg.FlashcardId == id)).FirstOrDefault();
                nextDate = LearningAlgorithms.CalculateSM2Alg(data.Quality, data.Easiness, data.Interval, data.Repetitions, multiplier);
            }

            return nextDate.ToString();
        }

        //POST
        // api/supermemo/<id>
        // not sure if we want an auto generating update or custom update
        public HttpResponseMessage Post([FromBody] Flashcard_Algorithm_Data data)
        {
            if (data != null)
            {
                using (DefaultConnection db = new DefaultConnection())
                {
                    Flashcard_Algorithm_Data updated = db.Flashcard_Algorithm_Data.Where(a => (data.Id == a.Id)).FirstOrDefault();
                    updated.Interval = data.Interval;
                    updated.NextPratice = data.NextPratice;
                    updated.Quality = data.Quality;
                    updated.Repetitions = data.Repetitions;
                    int changes = db.SaveChanges();
                    if (changes >= 1)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, updated);
                    }
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }
    }   
}