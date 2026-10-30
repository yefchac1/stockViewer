using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yefersonProject
{
    public class aSmartCandlestick: aCandlestick
    {
        public int index; // Index of the candlestick in the list. Set to public to allow access from other classes, such as FormChart and StockChart.
        public bool isPeak; // Indicates if the candlestick is a peak (local maximum). Set to public to allow access from other classes, such as FormChart and StockChart.
        public bool isValley; // Indicates if the candlestick is a valley (local minimum). Set to public to allow access from other classes, such as FormChart and StockChart.

        /// <summary>
        /// This cons
        /// </summary>
        /// <param name="cs"></param>
        public aSmartCandlestick(aCandlestick cs) : base(cs)
        {
        }

        /// <summary>
        /// This method converts a list of aCandlestick objects into a list of aSmartCandlestick objects, adding additional properties such as 
        /// index, isPeak, and isValley to each smart candlestick based on its position in the list and its relationship to neighboring candlesticks.
        /// </summary>
        /// <param name="candlesticks"></param>
        /// <returns></returns>
        public static List<aSmartCandlestick> ConvertIntoSmartCandlestick(List<aCandlestick> candlesticks)
        {

            int counter = 0;
            List<aSmartCandlestick> smartCandlesticks = new List<aSmartCandlestick>();
            foreach (aCandlestick cs in candlesticks)
            {
                aSmartCandlestick scs = new aSmartCandlestick(cs);
                {
                    scs.index = counter;
                    smartCandlesticks.Add(scs);
                    if(counter >= 2)
                    {
                        smartCandlesticks[counter-1].isPeak = smartCandlesticks[counter-2].High < smartCandlesticks[counter - 1].High && smartCandlesticks[counter - 1].High > smartCandlesticks[counter].High;
                        smartCandlesticks[counter-1].isValley = smartCandlesticks[counter - 2].Low > smartCandlesticks[counter - 1].Low && smartCandlesticks[counter - 1].Low < smartCandlesticks[counter].Low;
                    }
                    counter++; // USE ANCHOR DATA POINTS FOR ANNOTATIONS ON CHART-- DONT FORGET. PEAK LETTER P, VALLEY LETTER V AT BOTTOM

                }
            }
            
            return smartCandlesticks;
        }
    }
}
