using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebRole
{
    public partial class CreateProject : System.Web.UI.Page
    {
        ArrayList datasets;

        protected void Page_Load(object sender, EventArgs e)
        {
            datasets = new ArrayList();
            datasets.Add("ftp://e4ftl01u.ecs.nasa.gov/MOLT/MOD09A1.005/2000.02.18/");
        }

        protected void CreateProjectBtn_Click(object sender, System.EventArgs e)
        {
        }
    }
}