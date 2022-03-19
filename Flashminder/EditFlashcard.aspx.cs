using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Flashminder.Models;

namespace Flashminder
{
    public partial class EditFlashcard : System.Web.UI.Page
    {
        Flashcard currentFlashcard = null;
        public enum WarningType
        {
            Success,
            Info,
            Warning,
            Danger
        }

        const int MAX_CARD_TEXT = 500;

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
            int userInt = int.Parse(user);

            // if no category exists, make a default category
            // populate drop down

                using (DefaultConnection db = new DefaultConnection())
                {
                    List<Category> categories = db.Categories.Where(cat => (cat.UserId == userInt)).ToList();
                    category_dropdownlist.DataSource = db.Categories.Where(cat => (cat.UserId == userInt)).ToList();
                    category_dropdownlist.DataBind();
                }
                string flashcardId = Request.QueryString["FlashcardID"];
                if (string.IsNullOrEmpty(flashcardId))
                {
                    ShowMessage("Unable to load flashcard, try again", WarningType.Warning);
                }
                else
                {
                    int flashcardInt = int.Parse(flashcardId);
                    Flashcard flashcard = null;
                    using (DefaultConnection db = new DefaultConnection())
                    {
                        flashcard = db.Flashcards.Where(card => (card.Id == flashcardInt && userInt == card.UserId)).FirstOrDefault();
                    }
                    if (flashcard != null)
                {
                        if (!IsPostBack)
                        {
                            front_txtbx.Text = flashcard.FrontText;
                            back_txtbx.Text = flashcard.BackText;
                            current_front_img.ImageUrl = flashcard.FrontImage;
                            current_back_img.ImageUrl = flashcard.BackImage;
                        }
                    currentFlashcard = flashcard;
                        
                    }
                    else
                    {
                        ShowMessage("Unable to load flashcard, try again", WarningType.Warning);
                    }
                }
        }

        // validate the flashcard has valid information
        private bool ValidateForm()
        {
            if ((string.IsNullOrEmpty(front_txtbx.Text) && string.IsNullOrEmpty(front_upload.FileName)) ||
                (string.IsNullOrEmpty(back_txtbx.Text) && string.IsNullOrEmpty(back_upload.FileName)))
            {
                ShowMessage("Each card side needs text or an image.", WarningType.Warning);
                return false;
            }
            if (front_txtbx.Text.Length > MAX_CARD_TEXT || back_txtbx.Text.Length > MAX_CARD_TEXT)
            {
                ShowMessage("Max text length is 500 characters", WarningType.Warning);
                return false;
            }
            return true;
        }

        protected void UpdateFlashcard(object sender, EventArgs e)
        {
                using (DefaultConnection db = new DefaultConnection())
                {
                    Flashcard flashcard = db.Flashcards.Where(card => (card.Id == currentFlashcard.Id)).FirstOrDefault();
                    if (!string.IsNullOrEmpty(front_upload.FileName))
                    {
                        if (File.Exists(flashcard.FrontImage))
                        {
                            File.Delete(flashcard.FrontImage);
                        }

                        string fileName = DateTime.Now.ToString("MM-dd-yyyy_HHmmss");
                        string filetype = Path.GetExtension(front_upload.FileName).ToString().ToLower();
                        front_upload.SaveAs(Server.MapPath("~/Images/" + currentFlashcard.UserId + "_" + fileName + "_front_" + filetype));
                        flashcard.FrontImage = currentFlashcard.UserId + "_" + fileName + "_front_" + filetype;
                        
                    }
                    if (!string.IsNullOrEmpty(back_upload.FileName))
                    {
                        if (File.Exists(flashcard.BackImage))
                        {
                            File.Delete(flashcard.BackImage);
                        }

                        string fileName = DateTime.Now.ToString("MM-dd-yyyy_HHmmss");
                        string filetype = Path.GetExtension(back_upload.FileName).ToString().ToLower();
                        back_upload.SaveAs(Server.MapPath("~/Images/" + flashcard.UserId + "_" + fileName + "_back_" + filetype));
                        flashcard.BackImage = flashcard.UserId + "_" + fileName + "_back_" + filetype;
                    }
                    flashcard.FrontText = front_txtbx.Text;
                    flashcard.BackText = back_txtbx.Text;
                    Category selectedCategory = db.Categories.Where(cat => (cat.CategoryName == category_dropdownlist.SelectedItem.Text && cat.UserId == flashcard.UserId )).FirstOrDefault();
                    int categoryID = flashcard.Flashcard_Category.First().CategoryId;
                    Flashcard_Category flashcard_category = db.Flashcard_Category.Where(fc => fc.CategoryId == categoryID && fc.FlashcardId == flashcard.Id).FirstOrDefault();
                    db.Flashcard_Category.Remove(flashcard_category);
                    Flashcard_Category newRelation = new Flashcard_Category();
                    newRelation.Category = selectedCategory;
                    newRelation.Flashcard = flashcard;
                    newRelation.UserID = flashcard.UserId;
                    db.Flashcard_Category.Add(newRelation);
                    db.SaveChanges();                    
                }
        }

        protected void RemoveFrontImage(object sender, EventArgs e)
        {
            if (currentFlashcard != null)
            {
                using (DefaultConnection db = new DefaultConnection())
                {
                    Flashcard flashcard = db.Flashcards.Where(card => (card.Id == currentFlashcard.Id)).FirstOrDefault();
                    if (File.Exists(flashcard.FrontImage))
                    {
                        File.Delete(flashcard.FrontImage);
                    }
                    flashcard.FrontImage = "";
                    db.SaveChanges();
                }
            }
        }

        protected void RemoveBackImage(object sender, EventArgs e)
        {
            if (currentFlashcard != null)
            {
                using (DefaultConnection db = new DefaultConnection())
                {
                    Flashcard flashcard = db.Flashcards.Where(card => (card.Id == currentFlashcard.Id)).FirstOrDefault();
                    if (File.Exists(flashcard.BackImage))
                    {
                        File.Delete(flashcard.BackImage);
                    }
                    flashcard.BackImage = "";
                    db.SaveChanges();
                }
            }
        }

        protected void ShowChangeFrontPanel(object sender, EventArgs e)
        {
            front_img_change_pnl.Visible = true;
            front_img_change_btn.Visible = false;
        }

        protected void ShowChangeBackPanel(object sender, EventArgs e)
        {
            back_img_change_pnl.Visible = true;
            back_img_change_btn.Visible = false;
        }
    }
}