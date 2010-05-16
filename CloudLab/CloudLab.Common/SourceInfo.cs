using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Diagnostics;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Net;

namespace CloudLab.Common
{
    
    public enum SatelliteType
    {
        Aqua,
        Terra,
        Unknown
    }

    public enum ProductType
    {
        Atmos,
        Land,
        Ocean,
        Unknown
    }

    public enum ProjectType
    {
        Swath,
        Sinusoidal,
        Unknown
    }

    /// <summary>
    /// MODIS Product Class
    /// </summary>
    public class ModisSourceProduct
    {
        public string productName { get; set; }
        public string baseFtpUrl { get; set; }
        public SatelliteType satellite { get; set; }
        public ProductType productType { get; set; }
        public ProjectType projectType { get; set; }
        public int observeInterval { get; set; }
        public ArrayList FileList { get; set; }
        public ModisSourceProduct(string name, string baseUrl, SatelliteType sateType, 
                                    ProductType prodType, ProjectType projType, int days)
        {
            productName = name;
            baseFtpUrl = baseUrl;
            satellite = sateType;
            productType = prodType;
            projectType = projType;
            observeInterval = days;
            FileList = new ArrayList();
            FileList = DownloadFTP.GetFileList("ftp://" + baseFtpUrl + "/", "anonymous", "guest");
        }

        public string GetFtpUrl(int year, int day)
        {
            if (productType == ProductType.Atmos)
            {
                return baseFtpUrl + "/" + year + "/" + CommonAlgorithms.GetThreeDigitDayString(day);
            }
            else if (productType == ProductType.Land)
            {
                return baseFtpUrl + "/" + CommonAlgorithms.GetDateString(year, day);
            }

            return null;
        }
    }


    /// <summary>
    /// FTP source products info class 
    /// </summary>
    public class SourceInfo
    {
        public static readonly ModisSourceProduct[] products = {
                            new ModisSourceProduct("MOD04_L2", "ladsweb.nascom.nasa.gov/allData/5/MOD04_L2", SatelliteType.Terra, ProductType.Atmos, ProjectType.Swath, 1),
                            new ModisSourceProduct("MOD05_L2", "ladsweb.nascom.nasa.gov/allData/5/MOD05_L2", SatelliteType.Terra, ProductType.Atmos, ProjectType.Swath, 1),
                            new ModisSourceProduct("MOD06_L2", "ladsweb.nascom.nasa.gov/allData/5/MOD06_L2", SatelliteType.Terra, ProductType.Atmos, ProjectType.Swath, 1),
                            new ModisSourceProduct("MOD07_L2", "ladsweb.nascom.nasa.gov/allData/5/MOD07_L2", SatelliteType.Terra, ProductType.Atmos, ProjectType.Swath, 1),
                            new ModisSourceProduct("MOD11_L2", "e4ftl01u.ecs.nasa.gov/MOLT/MOD11_L2.005", SatelliteType.Terra, ProductType.Land, ProjectType.Swath, 1),

                            new ModisSourceProduct("MYD04_L2", "ladsweb.nascom.nasa.gov/allData/5/MYD04_L2", SatelliteType.Aqua, ProductType.Atmos, ProjectType.Swath, 1),
                            new ModisSourceProduct("MYD05_L2", "ladsweb.nascom.nasa.gov/allData/5/MYD05_L2", SatelliteType.Aqua, ProductType.Atmos, ProjectType.Swath, 1),
                            new ModisSourceProduct("MYD06_L2", "ladsweb.nascom.nasa.gov/allData/5/MYD06_L2", SatelliteType.Aqua, ProductType.Atmos, ProjectType.Swath, 1),
                            new ModisSourceProduct("MYD07_L2", "ladsweb.nascom.nasa.gov/allData/5/MYD07_L2", SatelliteType.Aqua, ProductType.Atmos, ProjectType.Swath, 1),
                            new ModisSourceProduct("MYD11_L2", "e4ftl01u.ecs.nasa.gov/MOLA/MYD11_L2.005", SatelliteType.Aqua, ProductType.Land, ProjectType.Swath, 1),

                            new ModisSourceProduct("MCD15A2", "e4ftl01u.ecs.nasa.gov/MOTA/MCD15A2.005", SatelliteType.Unknown, ProductType.Land, ProjectType.Sinusoidal, 8),
                            new ModisSourceProduct("MOD15A2", "e4ftl01u.ecs.nasa.gov/MOLT/MOD15A2.005", SatelliteType.Unknown, ProductType.Land, ProjectType.Sinusoidal, 8),
                            new ModisSourceProduct("MCD43B2", "e4ftl01u.ecs.nasa.gov/MOTA/MCD43B2.005", SatelliteType.Unknown, ProductType.Land, ProjectType.Sinusoidal, 8),
                            new ModisSourceProduct("MCD43B3", "e4ftl01u.ecs.nasa.gov/MOTA/MCD43B3.005", SatelliteType.Unknown, ProductType.Land, ProjectType.Sinusoidal, 8),
                            new ModisSourceProduct("MOD13A2", "e4ftl01u.ecs.nasa.gov/MOLT/MOD13A2.005", SatelliteType.Unknown, ProductType.Land, ProjectType.Sinusoidal, 16),
                            new ModisSourceProduct("MCD12Q1", "e4ftl01u.ecs.nasa.gov/MOTA/MCD12Q1.005", SatelliteType.Unknown, ProductType.Land, ProjectType.Sinusoidal, 366),
                            new ModisSourceProduct("MOD44B", "e4ftl01u.ecs.nasa.gov/MOLT/MOD44B.003", SatelliteType.Unknown, ProductType.Land, ProjectType.Sinusoidal, 366),
                            new ModisSourceProduct("MOD09A1", "e4ftl01u.ecs.nasa.gov/MOLT/MOD09A1.005", SatelliteType.Unknown, ProductType.Land, ProjectType.Sinusoidal, 366)
                                                          };

        private static Dictionary<string, ModisSourceProduct> dicProducts; //<productName, ModisProduct>

        private SourceInfo()
        {
        }
        

        public static string GetFtpUrl(string productName, int year, int day)
        {
            if (dicProducts == null)
            {
                BuildDictionary();
            }

            if (dicProducts.Keys.Contains(productName))
            {
                return dicProducts[productName].GetFtpUrl(year, day);
            }
            else
                return null;
        }

        public static SatelliteType GetSatellite(string productName)
        {
            if (dicProducts == null)
            {
                BuildDictionary();
            }

            if (dicProducts.Keys.Contains(productName))
            {
                return dicProducts[productName].satellite;
            }
            else
                return SatelliteType.Unknown;
        }

        public static ProductType GetProductType(string productName)
        {
            if (dicProducts == null)
            {
                BuildDictionary();
            }

            if (dicProducts.Keys.Contains(productName))
            {
                return dicProducts[productName].productType;
            }
            else
                return ProductType.Unknown;
        }


        public static ProjectType GetProjectType(string productName)
        {
            if (dicProducts == null)
            {
                BuildDictionary();
            }

            if (dicProducts.Keys.Contains(productName))
            {
                return dicProducts[productName].projectType;
            }
            else
                return ProjectType.Unknown;
        }


        public static int GetObserveInterval(string productName)
        {
            if (dicProducts == null)
            {
                BuildDictionary();
            }

            if (dicProducts.Keys.Contains(productName))
            {
                return dicProducts[productName].observeInterval;
            }
            else
                return -1;
        }


        private static void BuildDictionary()
        {
            dicProducts = new Dictionary<string, ModisSourceProduct>();
            for (int i = 0; i < products.Length; i++)
                dicProducts.Add(products[i].productName, products[i]);
        }
    }
}
