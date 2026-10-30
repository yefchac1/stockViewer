using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace yefersonProject
{
    public partial class FormChart : Form
    {
        private List<aCandlestick> candlesticks = new List<aCandlestick>(); // List to hold the candlestick data read from the CSV file. Each aCandlestick object represents a single candlestick entry with properties for date, open, high, low, close, and volume.
        private List<aSmartCandlestick> smartCandlesticks = new List<aSmartCandlestick>(); // List to hold the smart candlestick data, which includes additional properties such as index, isPeak, and isValley based on the position of each candlestick in the list and its relationship to neighboring candlesticks.
        public FormChart()
        {
            InitializeComponent();
        }
        public FormChart(string filePath, DateTime startDate, DateTime endDate)
        {
            InitializeComponent();
            candlesticks = CandlestickUtilities.ReadCandlestickFromFiles(filePath);
            candlesticks = CandlestickUtilities.FilterCandlestick(candlesticks, startDate, endDate);
            smartCandlesticks = aSmartCandlestick.ConvertIntoSmartCandlestick(candlesticks);
            candlestickBindingSource.DataSource = smartCandlesticks;
            dataGridView1.DataSource = candlestickBindingSource;
        }
    }
    public static class CandlestickUtilities
    {
        public static List<aCandlestick> ReadCandlestickFromFiles(string filePath)
        {
            List<aCandlestick> candlesticks = new List<aCandlestick>();
            var lines = System.IO.File.ReadAllLines(filePath);
            foreach (var line in lines.Skip(1))
            {
                aCandlestick candlestick = new aCandlestick(line);
                candlesticks.Add(candlestick);
            }
            return candlesticks;
        }
        public static List<aCandlestick> FilterCandlestick(List<aCandlestick> candlesticks, DateTime startDate, DateTime endDate)
        {
            var filteredCandlesticks = candlesticks.Where(c => c.Date >= startDate && c.Date <= endDate).ToList();
            return filteredCandlesticks;
        }
    }
}
