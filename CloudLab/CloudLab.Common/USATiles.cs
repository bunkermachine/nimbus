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
    public class USATiles
    {

        public string tileId { get; set; }
        public double LatMin { get; set; }
        public double LatMax { get; set; }
        public double LonMin { get; set; }
        public double LonMax { get; set; }

        public USATiles(string Id, double LtMin, double LtMax, double LnMin, double LnMax)
        {
            tileId = Id;
            LatMin = LtMin;
            LatMax = LtMax;
            LonMin = LtMin;
            LonMax = LtMax;
        }
    }

    public class TileFilter
    {
        public static USATiles[] Tiles = {
                            new USATiles("h08v04", -155.5515, -117.4887, 40, 50),
                            new USATiles("h09v04", -139.9957, -104.4338, 40, 50),
                            new USATiles("h10v04", -124.4399,  -91.3789, 40, 50),
                            new USATiles("h11v04", -108.8841,  -78.324,	 40, 50),
                            new USATiles("h12v04",  -93.3283,  -65.2691, 40, 50),
                            new USATiles("h13v04",  -77.7725,  -52.2142, 40, 50),
                            new USATiles("h14v04",	-62.2167,  -39.1593, 40, 50),
	                        new USATiles("h07v05", -143.5812, -115.4692, 30, 40),
                            new USATiles("h08v05", -130.5279, -103.9218, 30, 40),
                            new USATiles("h09v05", -117.4745,  -92.3744, 30, 40),
                            new USATiles("h10v05", -104.4212,  -80.827,  30, 40),
                            new USATiles("h11v05",	-91.3679,  -69.2796, 30, 40),
                            new USATiles("h12v05",  -78.3145,  -57.7322, 30, 40),
                            new USATiles("h07v06", -127.0113, -106.4197, 20, 30),
                            new USATiles("h08v06", -115.4648,  -95.7777, 20, 30),
                            new USATiles("h09v06", -103.9183,  -85.1357, 20, 30),
                            new USATiles("h10v06",	-92.3718,  -74.4938, 20, 30),
                            new USATiles("h11v06",  -80.8254,  -63.8518, 20, 30)
               
                                   };
        public TileFilter()
        {
        }

        public static ArrayList findTiles(string IpLatMin, string IpLatMax, string IpLonMin, string IpLonMax)
        {
            ArrayList tileList = new ArrayList();
            bool exists = false;

            for (int i = 0; i < 18; i++)
            {
                if ((Convert.ToDouble(IpLatMin) < Tiles[i].LatMax) && (Convert.ToDouble(IpLatMin) > Tiles[i].LatMin))
                {
                    foreach (string str in tileList)
                    {
                        if (String.Compare(str, Tiles[i].tileId) == 0)
                        {
                            exists = true;
                            break;
                        }
                    }

                    if (exists == false)
                        tileList.Add(Tiles[i].tileId);
                }
            }

            return tileList;
        }

        public static ArrayList filterFileList(ArrayList FileList, ArrayList tileList)
        {
            ArrayList FilteredFileList = new ArrayList();

            foreach (string fn in FileList)
            {
                String[] subString = fn.Split('.');

                foreach (string tile in tileList)
                {
                    if (String.Compare(subString[2], tile) == 0)
                    {
                        FilteredFileList.Add(fn);
                    }
                }
            }

            return FilteredFileList;
        }
    }


}
