using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Flashminder.Models;

namespace Flashminder
{
    public partial class CreateCategory : System.Web.UI.Page
    {
        const int MAX_CATEGORYNAME = 100;
        const int MAX_CATEGORYDESC = 500;

        //show message on client
        //https://stackoverflow.com/questions/35823379/bootstrap-alert-in-button-event-on-asp-net
        public void ShowMessage(string message, WarningType warning)
        {
            message_lbl.Text = message;
            message_pnl.CssClass = string.Format("alert alert-{0} alert-dismissable", warning.ToString().ToLower());
            message_pnl.Attributes.Add("role", "alert");
            message_pnl.Visible = true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string user = HttpContext.Current.User.Identity.Name;
            if (string.IsNullOrEmpty(user))
            {
                Response.Redirect("~/Signin.aspx");
            }
        }

        private bool ValidateForm()
        {
            bool ret = true;
            
            if (string.IsNullOrEmpty(categoryName_txtbx.Text))
            {
                ret = false;
            }
            if (categoryName_txtbx.Text.Length > MAX_CATEGORYNAME)
            {
                ret = false;
            }
            if (categoryDesc_txtbx.Text.Length > MAX_CATEGORYDESC)
            {
                ret = false;    
            }

            return ret;
        }

        protected void CreateCategoryDB(object sender, EventArgs e)
        {
            using (DefaultConnection db = new DefaultConnection())
            {
                if (!ValidateForm())
                {
                    return;
                }
                string user = HttpContext.Current.User.Identity.Name;
                if (!string.IsNullOrEmpty(user))
                {
                    int userInt = int.Parse(user);
                    Category category = new Category();
                    // check if name already exists
                    if (db.Categories.Any( c => c.CategoryName == categoryName_txtbx.Text && c.UserId == userInt))
                    {
                        ShowMessage("Category name is already in the database", WarningType.Warning);
                        return;
                    }
                    category.CategoryName = categoryName_txtbx.Text;
                    category.CategoryDesc = categoryDesc_txtbx.Text;
                    category.UserId = userInt;
                    category.CreatedDate = DateTime.Now;
                    db.Categories.Add(category);
                   int ret =  db.SaveChanges();
                    if (ret > 0)
                    {
                        ShowMessage("Successfully created category", WarningType.Success);
                        categoryDesc_lbl.Visible = false;
                        categoryDesc_txtbx.Visible = false;
                        categoryName_lbl.Visible = false;
                        categoryName_txtbx.Visible = false;
                        create_btn.Visible = false;
                        success_lbl.Visible = true;
                    }
                    else
                    {
                        ShowMessage("Unable to create flashcard", WarningType.Warning);
                    }
                }
                else
                {
                    ShowMessage("User is not logged in.", WarningType.Danger);
                }
            }
        }
    }
}