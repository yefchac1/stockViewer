using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yefersonProject
{
    /// <summary>
    /// This class takes the aCandlestick class and adds additional properties to create a "smart" candlestick, which includes 
    /// an index, and boolean flags to indicate whether the candlestick is a peak or a valley.
    /// </summary>
    public class aSmartCandlestick: aCandlestick
    {
        public int index; // Index of the candlestick in the list. Set to public to allow access from other classes, such as FormChart and StockChart.
        public bool isPeak; // Indicates if the candlestick is a peak (local maximum). Set to public to allow access from other classes, such as FormChart and StockChart.
        public bool isValley; // Indicates if the candlestick is a valley (local minimum). Set to public to allow access from other classes, such as FormChart and StockChart.

        /// <summary>
        /// This constructor initializes a new instance of the aSmartCandlestick class by copying the properties of an existing 
        /// aCandlestick object using the base class constructor. It allows for the creation of a smart candlestick from 
        /// a regular candlestick.
        /// </summary>
        /// <param name="cs"></param>
        public aSmartCandlestick(aCandlestick cs) : base(cs)
        {
        }
    }
}
