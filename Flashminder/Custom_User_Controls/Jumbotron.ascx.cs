using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Flashminder.Custom_User_Controls
{
    public partial class Jumbotron : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            JumbotronH5.InnerText = "This is a group project required for the partial fulfillment of Senior Engineering Project course (SFWRTECH 4FD3).";
        }
    }
}