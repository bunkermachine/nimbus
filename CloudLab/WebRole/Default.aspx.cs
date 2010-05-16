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
using CloudLab.Common;

namespace WebRole
{
  public partial class Default : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void UpdateSidebar(object sender, EventArgs e)
    {
        ProjectListView.DataSource = UserState.GetProjects("David");
        ProjectListView.DataBind();
    }

    protected void SubmitBtn_Click(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
    }

    protected void OpenProject(object sender, ListViewCommandEventArgs e)
    {
        //UserState.CurrentProject = e.CommandArgument.ToString();
        Server.Transfer("NewTask.aspx");
    }

    protected void CreateProject(object sender, EventArgs e)
    {
        //UserState.CurrentProject = NewProjectName.Text;
        Server.Transfer("NewTask.aspx");
    }
  }
}
