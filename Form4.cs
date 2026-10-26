using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace yefersonProject
{
    public partial class FormChart : Form
    {

        private List<Candlestick> candlesticks = new List<Candlestick>();
        public FormChart()
        {
            InitializeComponent();

        }

        public FormChart(string filePath, DateTime startDate, DateTime endDate)
        {
            InitializeComponent();
            candlesticks = CandlestickUtilities.ReadCandlestickFromFiles(filePath);
            candlesticks = CandlestickUtilities.FilterCandlestick(candlesticks, startDate, endDate);
            candlestickBindingSource.DataSource = candlesticks;
            dataGridView1.DataSource = candlestickBindingSource;


        }
    }

    public static class CandlestickUtilities
    {
        public static List<Candlestick> ReadCandlestickFromFiles(string filePath)
        {
            List<Candlestick> candlesticks = new List<Candlestick>();
            var lines = System.IO.File.ReadAllLines(filePath);
            foreach (var line in lines.Skip(1))
            {
                Candlestick candlestick = new Candlestick(line);
                candlesticks.Add(candlestick);
            }
            return candlesticks;
        }

        public static List<Candlestick> FilterCandlestick(List<Candlestick> candlesticks, DateTime startDate, DateTime endDate)
        {
            var filteredCandlesticks = candlesticks.Where(c => c.Date >= startDate && c.Date <= endDate).ToList();
            return filteredCandlesticks;
        }
    }


}
