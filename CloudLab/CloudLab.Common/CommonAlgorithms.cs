using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloudLab.Common
{
    /// <summary>
    /// Static class which includes Sinusoidal-Geographic algorithms 
    /// </summary>
    public class CommonAlgorithms
    {

        private static readonly int[] daysInCommonYearMonths = {31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31};
        private static readonly int[] daysInLeapYearMonths = { 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };


        private CommonAlgorithms()
        {
        }

        /// <summary>
        /// Get the number of days in a given year
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public static int GetDaysInYear(int year)
        {
            if (year % 4 != 0)
                return 365;

            if (year % 100 == 0 && year % 400 != 0)
                return 365;
            else
                return 366;
        }


        /// <summary>
        /// Get the date string representation of a given day (e.g. "2004.02.15")
        /// </summary>
        /// <param name="year"></param>
        /// <param name="dayOfYear"></param>
        /// <returns></returns>
        public static string GetDateString(int year, int dayOfYear)
        {
            int daysInYear = GetDaysInYear(year);
            if (dayOfYear > daysInYear)
                return null;

            int[] daysInMonths;
            if ( daysInYear == 365)
                daysInMonths = daysInCommonYearMonths;
            else
                daysInMonths = daysInLeapYearMonths;

            int month = 1;
            int remainedDays = dayOfYear;
            while (remainedDays > daysInMonths[month-1])
            {
                remainedDays -= daysInMonths[month-1];
                month++;
            }

            return year + "." + GetTwoDigitString(month) + "." + GetTwoDigitString(remainedDays);
        }


        public static int GetDayOfYear(int year, int month, int day)
        {
            if (month < 1 || month > 12)
                return -1;

            int[] daysInMonths;

            int daysInYear = GetDaysInYear(year);
            if (daysInYear == 365)
                daysInMonths = daysInCommonYearMonths;
            else
                daysInMonths = daysInLeapYearMonths;

            int currentMonth = 1;
            int doy = 0;
            while (currentMonth < month)
            {
                doy += daysInMonths[currentMonth - 1];
                currentMonth++;
            }

            doy += day;
            if (doy > daysInYear)
                return daysInYear;
            else
                return doy;
        }

        /// <summary>
        /// Get the 2-digit string representation of an integer in [1,99] 
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string GetTwoDigitString(int num)
        {
            if (num < 1 || num >= 100)
                return null;

            if (num < 10)
                return "0" + num.ToString();
            else
                return num.ToString();
        }


        /// <summary>
        /// Get the 3-digit string representation of the day (e.g."003")
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public static string GetThreeDigitDayString(int day)
        {
            if (day <= 0)
                return null;
            else if (day < 10)
                return "00" + day;
            else if (day < 100)
                return "0" + day;
            else if (day <= 366)
                return day.ToString();
            else
                return null;
        }

        /// <summary>
        /// Return a list of strings by spliting a string using the delimiter
        /// </summary>
        /// <param name="s"></param>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        public static List<string> GetSeparateStrings(string s, char delimiter)
        {
            List<string> res = new List<string>();
            if (String.IsNullOrEmpty(s))
                return res;

            string[] parts = s.Split(delimiter);
            foreach (string p in parts)
            {
                if (!String.IsNullOrEmpty(p))
                    res.Add(p);
            }
            return res;
        }

        
        /// <summary>
        /// Parse a string to get the list of numbers (e.g."1-10, 25, 36-78")
        /// Used mainly for parsing days
        /// </summary>
        /// <param name="dayString"></param>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        public static List<int> GetNumsFromString(string dayString, char delimiter)
        {
            dayString.Replace(" ", "");
            List<int> dayList = new List<int>();
            string[] daySet = dayString.Split(delimiter);
            foreach (string days in daySet)
            {
                if (!days.Contains("-"))  //A single day
                {
                    dayList.Add(Int32.Parse(days));
                }
                else    //A day period
                {
                    string[] ds = days.Split('-');
                    int dayStart = Int32.Parse(ds.First());
                    int dayEnd = Int32.Parse(ds.Last());
                    for (int i = dayStart; i <= dayEnd; i++)
                    {
                        dayList.Add(i);
                    }
                }
            }

            return dayList;
        }


        /// <summary>
        /// Get the separate tile locations from a tile string 
        /// </summary>
        /// <param name="tileString">The tile string representation (e.g."h08v05/h08v06/")</param>
        /// <returns></returns>
        /*
        public static List<SinusoidalTileLocation> GetTileLocationsFromString(string tileString)
        {
            List<SinusoidalTileLocation> tileList = new List<SinusoidalTileLocation>();
            string[] tiles = tileString.Split('/');
            foreach (string t in tiles)
            {
                if (!String.IsNullOrEmpty(t))
                {
                    tileList.Add(new SinusoidalTileLocation(t));
                }
            }
            return tileList;
        }
        */
 
        /// <summary>
        /// Get the separate satellite names from the string (e.g."Terra&Aqua")
        /// </summary>
        /// <param name="satelliteString"></param>
        /// <returns></returns>
        /*
        public static List<string> GetSatellitesFromString(string satelliteString)
        {
            List<string> satelliteList = new List<string>();

            if (satelliteString.Contains("Terra"))
                satelliteList.Add("Terra");

            if (satelliteString.Contains("Aqua"))
                satelliteList.Add("Aqua");

            return satelliteList;
        }
        */

        /// <summary>
        /// Decide if a finle name is HDF data file name
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool IsHdfFile(string fileName)
        {
            if (fileName.Length < 4)
                return false;

            if (fileName.Substring(fileName.Length - 4) == ".hdf")
                return true;
            else
                return false;
        }

        /// <summary>
        /// Decide whether a (longitude, latitude) point lays inside a given grand boundary rectangle
        /// </summary>
        /// <param name="p"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /*
        public static bool LonLatInGrandBoundary(LonLatPoint p, GrandBoundary b)
        {
            if (p.longitude > b.westBoundingCoord && p.longitude < b.eastBoundingCoord &&
                p.latitude > b.southBoundingCoord && p.latitude < b.northBoundingCoord)
                return true;
            else
                return false;
        }
        */
        
        /// <summary>
        /// Return the square of distance given the X- and Y-axis distance
        /// NOTE: For speed, sqrt() is not performed on the results
        /// </summary>
        /// <param name="xDist">X-axis distance</param>
        /// <param name="yDist">Y-axis distance</param>
        /// <returns></returns>
        public static double Distance2(double xDist, double yDist)
        {
            //return (Math.Abs(p1.longitude - p2.longitude) + Math.Abs(p1.latitude - p2.latitude));
            return xDist * xDist + yDist * yDist;
        }

        /// <summary>
        /// Return the square of distance of two (lon, lat) points
        /// NOTE: For speed, sqrt() is not performed on the results
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        /*
        public static double Distance2(LonLatPoint p1, LonLatPoint p2)
        {
            double xDist = p1.longitude - p2.longitude;
            double yDist = p1.latitude - p2.latitude;
            return xDist * xDist + yDist * yDist;
        }
        */
    }
}
