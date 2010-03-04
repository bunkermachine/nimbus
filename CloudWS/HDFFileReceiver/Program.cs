using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

namespace HDFFileReceiver
{
    public class Program
    {
        public void parseHDF(String hdfFileName)
        {
            Process process = new Process();
            process.StartInfo.FileName = "C:\\RdHdf.exe";
            process.StartInfo.Arguments = hdfFileName;
            try
            {
                process.Start();
                System.Threading.Thread.Sleep(10000);
                Console.WriteLine("");
                Console.Read();

            }
            catch (Exception E)
            {
                Console.WriteLine("{0} Exception caught ", E);
                System.Threading.Thread.Sleep(10000);
                Console.WriteLine("");

            }
        }
    }
}
