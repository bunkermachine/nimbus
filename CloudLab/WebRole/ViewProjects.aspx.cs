using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using CloudLab.Common;

namespace WebRole
{
    public partial class ViewProjects : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ArrayList projects = new ArrayList();
                projects.Add("Global Evapotranspiration");
                ProjectListView.DataSource = projects;
                ProjectListView.DataBind();
            }
        }

        protected void OpenProject(object sender, ListViewCommandEventArgs e)
        {
            UserState.CurrentProject = e.CommandArgument.ToString();
            Server.Transfer("NewTask.aspx");
        }

        protected void CreateProject(object sender, EventArgs e)
        {
            UserState.CurrentProject = NewProjectName.Text;
            Server.Transfer("NewTask.aspx");
        }
    }
}