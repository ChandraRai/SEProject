using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Security.Cryptography;

// Using statements that are required to connect to EF DB
using Flashminder.Models;
using System.Web.ModelBinding;

namespace Flashminder
{
    public partial class Signin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // WebForms UnobtrusiveValidationMode requires a ScriptResourceMapping for jquery
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        }

        protected void SendButton_Click(object sender, EventArgs e)
        {
            using (DefaultConnection db = new DefaultConnection())
            {
                // query USERS table using EF and LINQ
                var allUsers = (from Users in db.USERS select Users);               

                foreach (var user in allUsers)
                {

                    // Verify Username and Password
                    if (user.Username == UserNameTextBox.Text
                        && user.Password == Sha1(Salt(PasswordTextBox.Text)))
                    {
                        System.Web.Security.FormsAuthentication.SetAuthCookie(user.Id.ToString(), false);
                        Session["CurrentUser"] = UserNameTextBox.Text;
                        Response.Cookies["userName"].Value = UserNameTextBox.Text;

                        // Redirect to Home Page
                        Response.Redirect("~/Home.aspx");
                    }
                    else
                    {
                        RequiredFieldValidator1.IsValid = false;
                        RequiredFieldValidator2.IsValid = false;
                    }
                }                
            }
        }

        public static string Sha1(string text)
        {
            byte[] clear = System.Text.Encoding.UTF8.GetBytes(text);
            byte[] hash = new SHA1CryptoServiceProvider().ComputeHash(clear);
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }

        // Password Encryption        
        public static string Salt(string text)
        {
            return
              "zu5QnKrH4NJfOgV2WWqV5Oc1l" +
              text +
              "1DMuByokGSDyFPQ0DbXd9rAgW";
        }       

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/About.aspx");
        }
      
    }
}