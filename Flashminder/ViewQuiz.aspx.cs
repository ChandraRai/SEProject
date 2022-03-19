using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Flashminder.Models;

namespace Flashminder
{
    public partial class ViewQuiz : System.Web.UI.Page
    {
        Flashcard_Algorithm_Data curData;

        protected void Page_Load(object sender, EventArgs e)
        {
            string userId = HttpContext.Current.User.Identity.Name;
            if (string.IsNullOrEmpty(userId))
            {
                Response.Redirect("~/Signin.aspx");
            }

            if (!IsPostBack)
            {
                string categoryName = Request.QueryString["CategoryName"];
                category.Text = categoryName;
            }
            if (string.IsNullOrEmpty(category.Text))
            {
                category.Text = "All";
            }
            LoadFlashcard(userId, category.Text);
            SetButtons();

        }


        protected Flashcard LoadFlashcard(string userId, string categoryName)
        {
            List<Flashcard> flashcards = DatabaseAccessors.LoadFlashcardsByCategoryName(userId, categoryName);
            List<Flashcard_Algorithm_Data> data = null;
            Flashcard ret = null;
            if (flashcards != null)
            {
                data = DatabaseAccessors.LoadNextFlashcardData(flashcards);
            }

            if ( data != null)
            {
                Flashcard_Algorithm_Data dataObj = data.FirstOrDefault();
                Flashcard card = null;
                if (dataObj != null)
                {
                    card = DatabaseAccessors.LoadFlashcard((int)dataObj.FlashcardId, Int32.Parse(userId));
                    curData = dataObj;
                }
                if (card != null)
                {
                    currentFlashcardId_label.Text = card.Id.ToString();
                    ret = card;
                }
            }

            return ret;
        }

        protected void SetButtons()
        {
            // set very easy
            float multiplier = float.Parse(multiplier_dropdown.SelectedValue);
            if (curData != null)
            {
                double days = (LearningAlgorithms.CalculateSM2Alg(5, curData.Easiness, curData.Interval, curData.Repetitions, multiplier) - DateTime.Now ).TotalDays;
                very_easy_btn.Text = "Very Easy (" + days.ToString("0.0") + ")";
                days = (LearningAlgorithms.CalculateSM2Alg(4, curData.Easiness, curData.Interval, curData.Repetitions, multiplier) - DateTime.Now).TotalDays;
                easy_btn.Text = "Easy (" + days.ToString("0.0") + ")";
                days = (LearningAlgorithms.CalculateSM2Alg(3, curData.Easiness, curData.Interval, curData.Repetitions, multiplier) - DateTime.Now).TotalDays;
                moderate_btn.Text = "Moderate ("+ days.ToString("0.0") + ")";
                days = (LearningAlgorithms.CalculateSM2Alg(2, curData.Easiness, curData.Interval, curData.Repetitions, multiplier) - DateTime.Now).TotalDays;
                hard_btn.Text = "Hard (" + days.ToString("0.0") + ")";
                days = (LearningAlgorithms.CalculateSM2Alg(1, curData.Easiness, curData.Interval, curData.Repetitions, multiplier) - DateTime.Now).TotalDays;
                very_hard_btn.Text = "Very Hard (" + days.ToString("0.0") + ")";
            }
        }

        protected void ButtonPressed( object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (curData != null)
            {
                if(btn.Text.StartsWith("Very Easy"))
                {
                    curData.Quality = 5;
                    DatabaseMutators.UpdateNextDate(curData, float.Parse(multiplier_dropdown.SelectedValue));
                }
                else if (btn.Text.StartsWith("Easy"))
                {
                    curData.Quality = 4;
                    DatabaseMutators.UpdateNextDate(curData, float.Parse(multiplier_dropdown.SelectedValue));
                }
                else if (btn.Text.StartsWith("Moderate"))
                {
                    curData.Quality = 3;
                    DatabaseMutators.UpdateNextDate(curData, float.Parse(multiplier_dropdown.SelectedValue));
                }
                else if (btn.Text.StartsWith("Hard"))
                {
                    curData.Quality = 2;
                    DatabaseMutators.UpdateNextDate(curData, float.Parse(multiplier_dropdown.SelectedValue));
                }
                else if (btn.Text.StartsWith("Very Hard"))
                {
                    curData.Quality = 1;
                    DatabaseMutators.UpdateNextDate(curData, float.Parse(multiplier_dropdown.SelectedValue));
                }
            }
            
        }

        protected void DropdownChanged(object sender, EventArgs e)
        {
            SetButtons();
        }
    }
}