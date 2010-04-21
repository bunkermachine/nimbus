using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebRole
{
    public partial class NewProject : System.Web.UI.UserControl
    {
        ArrayList datasets;

        protected void Page_Load(object sender, EventArgs e)
        {
            datasets = new ArrayList();
            datasets.Add("ftp://e4ftl01u.ecs.nasa.gov/MOLT/MOD09A1.005/2000.02.18/");
        }

        protected void CreateProject(object sender, System.EventArgs e)
        {
        }
    }
}