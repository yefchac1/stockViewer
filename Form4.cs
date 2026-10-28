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

        private List<aCandlestick> candlesticks = new List<aCandlestick>();

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

        private void UpdateDate_Click(object sender, EventArgs e)
        {
            // We will attach the event handler to the UpdateDate button click event, which will filter the candlestick data based on the selected date range and update the chart accordingly.
            //Now we will refresh 
            candlesticks = CandlestickUtilities.FilterCandlestick(candlesticks, newStartDatePicker.Value.Date, newEndDatePicker.Value.Date);
            StockViewerChart.Refresh();
        }
    }
}




