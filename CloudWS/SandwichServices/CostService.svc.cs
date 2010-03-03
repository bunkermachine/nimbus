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
            //Process.Start("C:\\My Stuff\\My Work\\Stanford\\nimbus\\hdfread\\readhdf.exe");
            //return "Here's your foot-long subway!";
            Program HDFParser = new Program();
            String hdfFileName = "C:\\My Stuff\\My Work\\Stanford\\nimbus\\hdfop\\sample.he5";
            Console.WriteLine("Sucess entering!");
            HDFParser.parseHDF(hdfFileName);
        }

        // Add more operations here and mark them with [OperationContract]
    }
}
