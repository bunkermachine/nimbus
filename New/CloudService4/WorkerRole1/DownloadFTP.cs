using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Net;

namespace WorkerRole1
{
    class DownloadFTP
    {
        // The main entry point for the application.
        [STAThread]
        public byte[] getDataFromFTP()
        {
            //string FTPAddress = args[0];
            //string filename = args[1];
            //string savefilename = args[2];
            string FTPAddress = "ftp://e4ftl01u.ecs.nasa.gov/MOLT/MOD09A1.005/2000.02.18/";
            string filename = "BROWSE.MOD09A1.A2000049.h00v08.005.2006268222533.1.jpg	";
            string savefilename = "C://BROWSE.MOD09A1.A2000049.h00v08.005.2006268222533.1.jpg	";

            return downloadFile(FTPAddress, filename, savefilename, "anonymous", "guest");

        }

        
        //Connects to the FTP server and downloads the file
        private byte[] downloadFile(string FTPAddress, string filename, string savefilename, string username, string password)
        {
            byte[] downloadedData = new byte[0];

            try
            {
                FtpWebRequest request = FtpWebRequest.Create(FTPAddress + "/" + filename) as FtpWebRequest;

                //get the data
                request = FtpWebRequest.Create(FTPAddress + "/" + filename) as FtpWebRequest;
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = new NetworkCredential(username, password);
                request.UsePassive = true;
                request.UseBinary = true;
                request.KeepAlive = false; //close the connection when done

                //Streams
                FtpWebResponse response = request.GetResponse() as FtpWebResponse;
                Stream reader = response.GetResponseStream();

                //Download to memory
                //Note: adjust the streams here to download directly to the hard drive
                MemoryStream memStream = new MemoryStream();
                byte[] buffer = new byte[1024]; //downloads in chuncks

                while (true)
                {

                    //Try to read the data
                    int bytesRead = reader.Read(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                    {
                        //Nothing was read, finished downloading

                        break;
                    }
                    else
                    {
                        //Write the downloaded data
                        memStream.Write(buffer, 0, bytesRead);

                    }
                }

                //Convert the downloaded stream to a byte array
                downloadedData = memStream.ToArray();

                //Clean up
                reader.Close();
                memStream.Close();
                response.Close();

                Trace.TraceInformation(string.Format("Downloaded Successfully"));

                //Save the file to hard drive
                /*
                if (downloadedData != null && downloadedData.Length != 0)
                {
                    //Write the bytes to a file
                    FileStream newFile = new FileStream(savefilename, FileMode.Create);
                    newFile.Write(downloadedData, 0, downloadedData.Length);
                    newFile.Close();

                    Trace.TraceInformation(string.Format("Saved Successfully"));

                }
                else
                    Trace.TraceInformation(string.Format("No files downloaded yet"));
                */
            }

            catch (Exception)
            {
                Trace.TraceInformation(string.Format("Error connecting to the FTP server"));

            }
            

            username = string.Empty;
            password = string.Empty;
            return downloadedData;
        }
    }
}
