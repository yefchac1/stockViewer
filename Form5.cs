using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace yefersonProject
{
    public partial class StockChart : Form
    {

        private List<Candlestick> candlesticks = new List<Candlestick>();

        public StockChart()
        {
            InitializeComponent();
        }

        public StockChart(string filePath, DateTime startDate, DateTime endDate)
        {
            InitializeComponent();
            candlesticks = CandlestickUtilities.ReadCandlestickFromFiles(filePath);
            candlesticks = CandlestickUtilities.FilterCandlestick(candlesticks, startDate, endDate);
            StockViewerChart.DataSource = candlesticks ;
            

            // Set the X and Y value members for the candlestick series in the chart
            StockViewerChart.Series["PriceSeries"].XValueMember = "Date";
            StockViewerChart.Series["PriceSeries"].YValueMembers = "High,Low,Open,Close";

            // Set the X and Y value members for the volume series in the chart
            StockViewerChart.Series["VolumeSeries"].XValueMember = "Date";
            StockViewerChart.Series["VolumeSeries"].YValueMembers = "Volume";

            // Refresh the chart to reflect the updated data
            StockViewerChart.DataBind();

        }
    }
}




