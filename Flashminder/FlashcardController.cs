using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Flashminder
{
    public class FlashcardController : ApiController
    {
        // DELETE api/flashcard/5
        public HttpResponseMessage Delete(int id)
        {
            if (DatabaseMutators.DeleteFlashcard(id))
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }
    }
}