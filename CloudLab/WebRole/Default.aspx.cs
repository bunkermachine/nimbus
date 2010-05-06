using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using System.Net;
using AjaxControlToolkit;
using System.Web.Security;

namespace WebRole
{
  public partial class Default : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Write("Hello, " + Server.HtmlEncode(User.Identity.Name));
    }

    protected void SubmitBtn_Click(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
    }


  }
}
