using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.IO;

namespace HDFFileReceiver
{
    public class Program
    {
        public StreamReader parseHDF(String hdfFileName)
        {
            char[] data;
            data = new char[1000];
     
            Process process = new Process();
            process.StartInfo.FileName = "C:\\rdHDF.exe";
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

            return process.StandardOutput;
        }
    }
}
