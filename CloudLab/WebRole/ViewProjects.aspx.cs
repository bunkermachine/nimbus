using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebRole
{
    public partial class ViewProjects : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ArrayList projects = new ArrayList();
            projects.Add("Global Evapotranspiration");
            ProjectList.DataSource = projects;
            ProjectList.DataBind();
        }

        protected void NewProjectBtn_Click(object sender, EventArgs e)
        {
            
        }
    }
}