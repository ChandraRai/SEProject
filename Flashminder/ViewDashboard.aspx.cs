using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Flashminder.Models;

namespace Flashminder
{
    public partial class ViewDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string user = HttpContext.Current.User.Identity.Name;
            if (string.IsNullOrEmpty(user))
            {
                Response.Redirect("~/Signin.aspx");
            }
            int userInt = Int32.Parse(user);
            using (DefaultConnection db = new DefaultConnection())
            {
                category_dropdownlist.DataSource = db.Categories.Where(cat => cat.UserId == userInt).ToList();
                category_dropdownlist.DataBind();
            }
        }

        protected void RedirectToCreateFlashcards(object sender, EventArgs e)
        {
            Response.Redirect("~/CreateFlashcard.aspx");
        }

        protected void RedirectToViewFlashcards(object sender, EventArgs e)
        {
            Response.Redirect("~/ViewFlashcards.aspx");
        }

        protected void RedirectToStartQuiz(object sender, EventArgs e)
        {
            Response.Redirect("~/ViewQuiz.aspx?CategoryName=" + category_dropdownlist.SelectedItem.ToString());
        }

        protected void RedirectToQuizSettings(object sender, EventArgs e)
        {
            // todo
        }
    }
}