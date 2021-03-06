﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Net;

namespace CloudLab.Common
{
    /*Called from run() in WorkerRole.cs*/
    public class DownloadFTP
    {
        [STAThread]

        public static byte[] getDataFromFTP(string FTPAddress, string fileName)
        {
            return downloadFile(FTPAddress, fileName, "anonymous", "guest");
        }

        public static byte[] getDataFromFTP(string productName, int year, int day, string tile)
        {
            /*
             * Function called when dataset is to be downloaded 
             */
            
            /* Find ftp URL from productName, year and day */
            string FTPAddress = SourceInfo.GetFtpUrl(productName, year, day);
            FTPAddress = "ftp://" + FTPAddress;
            //downloadSource(FTPAddress, downloadPath, "anonymous", "guest");

            string yr = Convert.ToString(year);
            string dy = Convert.ToString(day);
            // Find relevant file and return it
            List<string> possibleFileNames = findFileName(FTPAddress, productName, yr, dy, tile);
            string fileName = "MOD04_L2.A2000055.0010.005.2006253050115.hdf";
            return downloadFile(FTPAddress, fileName, /*downloadPath,*/ "anonymous", "guest");
        }

        public static byte[] getDataFromFTP()
        {
            /*
             * Function called when dataset is to be downloaded 
             */
            string productName = "MOD04_L2";
            int year = 2000;
            int day = 55;
            string tile = "h00v10";
            //string downloadPath = "";

            /* Find ftp URL from productName, year and day */
            string FTPAddress = SourceInfo.GetFtpUrl(productName, year, day);
            FTPAddress = "ftp://" + FTPAddress;
            
            string fileName = "MOD04_L2.A2000055.0010.005.2006253050115.hdf";
            return downloadFile(FTPAddress, fileName, /*downloadPath,*/ "anonymous", "guest");
        }

        // Connects to the FTP server and downloads the specified file
        public static byte[] downloadFile(string FTPAddress, string fileName, /*string downloadPath,*/ string username, string password)
        {
            
            byte[] downloadedData = new byte[0];

            try
            {
                FtpWebRequest request = FtpWebRequest.Create(FTPAddress + "/" + fileName) as FtpWebRequest;

                //get the data
                request = FtpWebRequest.Create(FTPAddress + "/" + fileName) as FtpWebRequest;
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
                byte[] buffer = new byte[1024]; //downloads in chunks

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

            }

            catch (Exception)
            {
                Trace.TraceInformation(string.Format("Error connecting to the FTP server"));

            }

            username = string.Empty;
            password = string.Empty;
            return downloadedData;
        }


        // Connects to the FTP server and downloads the entire data source and stores it on the cloud
        public static void downloadSource(string FTPAddress, string downloadPath, string username, string password)
        {
            List<string> fileList = null;

            // Get container name from storage architecture
            //string containerName = "SourceContainerName";

            // Get list of tiles
            // List<string> tileList = CommonAlgorithms.GetSeparateStrings(locationList, '/');

            // For every tile in list, download file from ftp site
            //foreach (string t in tileList)
            //{
            //string blobName = "SourceBlobName";
            // Need to download from external FTP site
            //get file list

            if (fileList == null)
                fileList = GetFileList(FTPAddress, "anonymous", "guest");
            //fileList = GetFileList(false);

            if (fileList != null)
            {
                foreach (string fileName in fileList)
                    //for every file in list, download file
                    downloadFile(FTPAddress, fileName, "anonymous", "guest");
                //store the downloaded file to blobName/containerName; this is happening in WorkerRole.cs as of now; might need to change that
            }
            return;
            //}

        }


        //Connects to the FTP server and request the list of available files
        public static List<string> GetFileList(string FTPAddress, string username, string password)
        {
            List<string> files = new List<string>();

            try
            {
                //Create FTP request
                FtpWebRequest request = FtpWebRequest.Create(FTPAddress) as FtpWebRequest;

                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.Credentials = new NetworkCredential(username, password);
                request.UsePassive = true;
                request.UseBinary = true;
                request.KeepAlive = false;

                //Read the server's response
                FtpWebResponse response = request.GetResponse() as FtpWebResponse;
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);

                while (!reader.EndOfStream)
                {
                    files.Add(reader.ReadLine());
                }


                //Clean-up
                reader.Close();
                responseStream.Close(); //redundant
                response.Close();
                return files;
            }
            catch (Exception)
            {
            }

            username = string.Empty;
            password = string.Empty;

            return null;
        }

        public static List<string> findFileName(string FTPAddress, string productName, string year, string day, string tile)
        {
            List<string> list = GetFileList(FTPAddress, "anonymous", "guest");
            List<string> searchResults = new List<string>();
            if (day.Length == 2)
                day = "0" + day;

            // split the string and check for matches with each file in the list            
            foreach (string fn in list)
            {
                String[] subString = fn.Split('.');

                if ((String.Compare(subString[0], productName) == 0) && (String.Compare(subString[2], tile) == 0) && (String.Compare(subString[1], "A" + year + day) == 0))
                {
                    searchResults.Add(fn);
                }

            }
            return searchResults;

        }


    }
}