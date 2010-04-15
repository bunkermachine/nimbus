using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.IO;

namespace WebRole1
{
    public class Program
    {
        public StreamReader parseHDF(String exeFileName, String hdfFileName)
        {
            Process process = new Process();
            process.StartInfo.FileName = exeFileName;
            process.StartInfo.Arguments = hdfFileName;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;

            try
            {
                process.Start();

            }
            catch (Exception E)
            {
                Console.WriteLine("{0} Exception caught ", E);

            }

            return process.StandardOutput;
        }
    }
}
