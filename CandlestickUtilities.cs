using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yefersonProject
{
    /// <summary>
    /// This class provides utility functions for reading and filtering candlestick data from CSV files.
    /// It includes methods to read candlestick data from a specified file path and filter the data based on a given date range.
    /// </summary>
    public static class CandlestickUtilities
    {
        /// <summary>
        /// Manages the logic for reading candlestick data from a CSV file. It reads the file line by line, skipping the header, 
        /// and creates a list of aCandlestick objects.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static List<aCandlestick> ReadCandlestickFromFiles(string filePath)
        {
            // Create an empty list to store the candlesticks
            List<aCandlestick> candlesticks = new List<aCandlestick>();

            // Read every line from the CSV file
            var lines = System.IO.File.ReadAllLines(filePath);

            // Skip the header and process each stock data line
            foreach (var line in lines.Skip(1))
            {
                // Create a new aCandlestick object from the CSV line and add it to the list
                aCandlestick candlestick = new aCandlestick(line);
                candlesticks.Add(candlestick);
            }

            // Return the list of candlestick objects read from the file
            return candlesticks;
        }
        /// <summary>
        /// Manages the logic for filtering candlestick data based on a specified date range. 
        /// It uses LINQ to filter the list of aCandlestick objects by comparing their Date property to the provided start and end dates.
        /// </summary>
        /// <param name="candlesticks"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static List<aCandlestick> FilterCandlestick(List<aCandlestick> candlesticks, DateTime startDate, DateTime endDate)
        {
            // Use LINQ to filter the candlesticks based on the specified date range
            var filteredCandlesticks = candlesticks.Where(c => c.Date >= startDate && c.Date <= endDate).ToList();

            // Return the filtered list of candlestick objects that fall within the specified date range
            return filteredCandlesticks;
        }
        /// <summary>
        /// This method converts a list of aCandlestick objects into a list of aSmartCandlestick objects, adding additional properties such as 
        /// index, isPeak, and isValley to each smart candlestick based on its position in the list and its relationship to neighboring candlesticks.
        /// </summary>
        /// <param name="candlesticks"></param>
        /// <returns></returns>
        public static List<aSmartCandlestick> ConvertIntoSmartCandlestick(List<aCandlestick> candlesticks)
        {
            // Initialize a counter to keep track of the index of each candlestick in the list.
            int counter = 0;

            // Create a new list to hold the smart candlestick objects.
            List<aSmartCandlestick> smartCandlesticks = new List<aSmartCandlestick>();

            // Iterate through each candlestick in the input list and create a corresponding smart candlestick object.
            foreach (aCandlestick cs in candlesticks)
            {
                aSmartCandlestick scs = new aSmartCandlestick(cs);
                {
                    scs.index = counter;
                    smartCandlesticks.Add(scs);
                    // Determine if the current candlestick is a peak or a valley based on its relationship to the previous and next candlesticks in the list.
                    if (counter >= 2)
                    {
                        // A candlestick is considered a peak if its high value is greater than the high values of the previous and next candlesticks.
                        smartCandlesticks[counter - 1].isPeak = smartCandlesticks[counter - 2].High < smartCandlesticks[counter - 1].High && smartCandlesticks[counter - 1].High > smartCandlesticks[counter].High;
                        // A candlestick is considered a valley if its low value is less than the low values of the previous and next candlesticks.
                        smartCandlesticks[counter - 1].isValley = smartCandlesticks[counter - 2].Low > smartCandlesticks[counter - 1].Low && smartCandlesticks[counter - 1].Low < smartCandlesticks[counter].Low;
                    }
                    counter++;
                }
            }

            // Return the list of smart candlestick objects, which now includes index, isPeak, and isValley.
            return smartCandlesticks;
        }
    }
}