using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Flashminder.Models;

namespace Flashminder
{
    public partial class ViewFlashcard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string flashcardId = Request.QueryString["FlashcardID"];
            string userId = HttpContext.Current.User.Identity.Name;
            Flashcard card = null;
            if (!string.IsNullOrEmpty(flashcardId))
            {
                card = DatabaseAccessors.LoadFlashcard(Int32.Parse(flashcardId), Int32.Parse(userId));
            }

            if (card!= null)
            {
                LoadFlashcard(card);
            }
        }


        public void LoadFlashcard(Flashcard flashcard)
        {
            string user = HttpContext.Current.User.Identity.Name;
            if (string.IsNullOrEmpty(user))
            {
                ///Response.Redirect("~/default.aspx");
            }
            else
            {
                //string flashcardID = Request.QueryString["FlashcardID"];
                if (flashcard != null)
                {
                    int userInt = Int32.Parse(user);
                    if (flashcard.UserId != userInt)
                    {
                        // issue, user doesn't match?
                    }
                    if (!string.IsNullOrEmpty(flashcard.FrontImage))
                    {
                        front_image.ImageUrl = "Images/" + flashcard.FrontImage;
                    }
                    if (!string.IsNullOrEmpty(flashcard.BackImage))
                    {
                        back_image.ImageUrl = "Images/" + flashcard.BackImage;
                    }
                    front_text.Text = flashcard.FrontText;
                    back_text.Text = flashcard.BackText;
                }
            }
        }
    }
}