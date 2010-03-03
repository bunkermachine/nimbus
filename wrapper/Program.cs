using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace testapp
{
    class Program
    {
        static void Main(string[] args)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = "C:\\readhdf.exe";
            
            proc.StartInfo.Arguments = "C:\\sample.he5";
            try
            {
                proc.Start();
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
