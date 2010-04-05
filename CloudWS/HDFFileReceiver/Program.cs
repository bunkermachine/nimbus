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
<<<<<<< HEAD
            process.StartInfo.Arguments = hdfFileName;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
=======
            process.StartInfo.Arguments = hdfFileName;
>>>>>>> d4a516dcb613988c559097705c11de866ced072d

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
