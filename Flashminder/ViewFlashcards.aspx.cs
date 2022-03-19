using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Flashminder.Models;

namespace Flashminder
{
    public partial class ViewFlashcards : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string user = HttpContext.Current.User.Identity.Name;

            if (!string.IsNullOrEmpty(user))
            {
                int userInt = Int32.Parse(user);
                if (!IsPostBack)
                {
                    using (DefaultConnection db = new DefaultConnection())
                    {
                        category_dropdownlist.DataSource = db.Categories.Where(cat => cat.UserId == userInt).ToList();
                        category_dropdownlist.DataBind();
                    }
                }

                LoadFlashcards(user, category_dropdownlist.SelectedValue);
            }
        }

        protected void SwitchCategory(object sender, EventArgs e)
        {
            LoadFlashcards(HttpContext.Current.User.Identity.Name, category_dropdownlist.SelectedItem.Text);
        }

        private void LoadFlashcards(string user, string category)
        {
            List<Flashcard> list = DatabaseAccessors.LoadFlashcardsByCategoryName(user, category).ToList();
            if (list != null)
            {
                flashcards_datalist.DataSource = list;
                flashcards_datalist.DataBind();
            }
        }

    }
}