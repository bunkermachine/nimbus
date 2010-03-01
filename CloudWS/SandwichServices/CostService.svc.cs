using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Diagnostics;


namespace SandwichServices
{
    [ServiceContract(Namespace = "SandwichServices")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class CostService
    {
        // Add [WebGet] attribute to use HTTP GET
        [OperationContract]
        public void getSandwich()
        {
            //HttpWebRequest Request = 
            // Add your operation implementation here
            //string album = Request.QueryString["album"];
            try
            {
                Process.Start("C:\\My Stuff\\My Work\\Stanford\\nimbus\\hdfread\\readhdf.exe");
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
            }
                
            //return "Here's your foot-long subway!";
        }

        // Add more operations here and mark them with [OperationContract]
    }
}
