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
    public partial class Form3 : Form
    {
        private List<Candlestick> candlesticks = new List<Candlestick>();

        public Form3()
        {
            InitializeComponent();
        }
        private List<Candlestick> ReadStockData(string filePath)
        {
            // Create an empty list to store the candlesticks
            List<Candlestick> candlesticks = new List<Candlestick>();

            // Read every line from the CSV file
            var lines = System.IO.File.ReadAllLines(filePath);

            // Skip the header and process each stock data line
            foreach (var line in lines.Skip(1))
            {
                Candlestick candlestick = new Candlestick(line);

                candlesticks.Add(candlestick);
            }

            return candlesticks;
        }

        private void OpenStockFile_Click(object sender, EventArgs e)
        {
            StockFileDialog.InitialDirectory = System.IO.Path.GetFullPath(
            System.IO.Path.Combine(Application.StartupPath, @"..\..\..\Stock Data")
            );

            // Show the file dialog and check if the user selected a file
            if (StockFileDialog.ShowDialog() == DialogResult.OK)
            {
                candlesticks = ReadStockData(StockFileDialog.FileName);

                FilterCandlestickDate();
            }

        }
        private void FilterCandlestickDate()
        {
            // Filter the candlesticks based on the selected start and end dates
            DateTime startDate = StartDatePicker.Value.Date;
            DateTime endDate = EndDatePicker.Value.Date;
            // Ensure that the end date is not earlier than the start date
            if (endDate < startDate)
            {
                MessageBox.Show("End date cannot be earlier than start date.");
                return;
            }
            // Filter the candlesticks to include only those within the specified date range
            var filteredCandlesticks = candlesticks
                .Where(c => c.Date.Date >= startDate &&
                            c.Date.Date <= endDate)

            // Convert the filtered results to a list for binding to the DataGridView
                .ToList();

            // Bind the filtered candlesticks to the DataGridView for display
            DataStockViewer.DataSource = filteredCandlesticks;

            // Bind the filtered candlesticks to the chart for visualization
            StockDataChartViewer.DataSource = filteredCandlesticks;

            // Set the X and Y value members for the candlestick series in the chart
            StockDataChartViewer.Series["PriceSeries"].XValueMember = "Date";
            StockDataChartViewer.Series["PriceSeries"].YValueMembers = "High, Low, Open, Close";

            // Set the X and Y value members for the volume series in the chart
            StockDataChartViewer.Series["VolumeSeries"].XValueMember = "Date";
            StockDataChartViewer.Series["VolumeSeries"].YValueMembers = "Volume";

            // Refresh the chart to reflect the updated data
            StockDataChartViewer.DataBind();

        }
    }
}
