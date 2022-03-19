using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;

// Using statements that are required to connect to EF DB
using Flashminder.Models;
using System.Web.ModelBinding;

namespace Flashminder
{
    public partial class Signup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // WebForms UnobtrusiveValidationMode requires a ScriptResourceMapping for jquery
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        }

        protected void RegisterButton_Click(object sender, EventArgs e)
        {
            if (!ValidateUser(txtUsername.Text))
            {
                // Connect to EF
                using (DefaultConnection db = new DefaultConnection())
                {
                    // Use Flashminder to  create a new user object and save a new record
                    USERS newUser = new USERS();

                    // add data to record
                    newUser.Username = txtUsername.Text;
                    newUser.Email = txtEmail.Text;
                    newUser.Password = Sha1(Salt(txtPassword.Text));

                    // user LINQ to ADO.NET to save record
                    db.USERS.Add(newUser);

                    // Save
                    db.SaveChanges();

                    // Redirect back to Login Page
                    Response.Redirect("~/Signin.aspx");
                }
            }
            else
            {                
                RequiredFieldValidator1.IsValid = false;
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
            // Redirect to About Page
            Response.Redirect("~/About.aspx");
        } 
        
        // Method to validate user from db
        public bool ValidateUser(string username)
        {
            bool flag = false;

            // Connect to EF
            using (DefaultConnection db = new DefaultConnection())
            {
                // query USERS table using EF and LINQ
                var Users = (from allUsers in db.USERS select allUsers);

                foreach (var user in Users)
                {
                    if (user.Username == txtUsername.Text)
                    {
                        return flag = true;
                    }                    
                }
            }
            return flag;
        }
    }
}