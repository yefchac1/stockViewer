using System;

namespace yefersonProject
{

    /// <summary>
    /// Represents a single candlestick entry in stock data, containing properties for 
    /// date, open, high, low, close, and volume.
    /// </summary>

    public class Candlestick
    {

        public DateTime Date { get; set; }      // Gets or sets the date of the candlestick entry.
        public decimal Open { get; set; }       // Gets or sets the open price of the candlestick entry.
        public decimal High { get; set; }       // Gets or sets the high price of the candlestick entry.
        public decimal Low { get; set; }        // Gets or sets the low price of the candlestick entry.
        public decimal Close { get; set; }      // Gets or sets the close price of the candlestick entry.
        public long Volume { get; set; }        // Gets or sets the volume of the candlestick entry.

        /// <summary>
        /// Initializes a new instance of the Candlestick class by parsing a CSV line containing candlestick data.
        /// </summary>
        /// <param name="csvLine"></param>
        public Candlestick(string csvLine)
        {
            // Split the CSV line into values using comma and double quote as delimiters
            char[] delimiterChars = { ',', '"' };

            // Split the line into values, removing empty entries
            string[] values = csvLine.Split(
                delimiterChars,
                StringSplitOptions.RemoveEmptyEntries
            );
            // Check if the line has the expected number of values (6)
            if (values.Length == 6)
            {
                // Parse the values and assign them to the properties
                Date = DateTime.Parse(values[0]);   // Date on the first column
                Open = decimal.Parse(values[1]);    // Open price on the second column
                High = decimal.Parse(values[2]);    // High price on the third column
                Low = decimal.Parse(values[3]);     // Low price on the fourth column
                Close = decimal.Parse(values[4]);   // Close price on the fifth column
                Volume = long.Parse(values[5]);     // Volume on the sixth column
            }
        }
    }

}
