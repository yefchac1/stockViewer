using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace yefersonProject
{
    /// <summary>
    /// Represents the main form of the stock data viewer application. This form allows users to open a CSV file 
    /// containing stock data, filter the data based on a specified date range, and display the filtered results 
    /// in a DataGridView.
    /// </summary>
    public partial class StockForm : Form
    {
        /// <summary>
        /// A list to hold the candlestick data read from the CSV file. Each Candlestick object represents 
        /// a single candlestick entry with properties for date, open, high, low, close, and volume.
        /// </summary>
        private List<Candlestick> candlesticks = new List<Candlestick>();

        /// <summary>
        /// Initializes a new instance of the stockForm class and sets up the form's components.
        /// </summary>
        public StockForm(string filePath)
        {
            InitializeComponent();
            candlesticks = ReadStockData(filePath);
            FilterCandlestickDate();

        }

        /// <summary>
        /// Reads stock data from a CSV file and returns a list of aCandlestick objects.
        /// </summary>
        /// <param name="filePath">The path of the CSV file.</param>
        
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
        /// <summary>
        /// Handles the click event of the "Open File" button. Opens a file dialog to select a CSV file containing stock data, 
        /// reads the data, and filters it based on the selected date range.
        /// </summary>


        private void OpenFileClick(object sender, EventArgs e)
        {
            // Set the initial directory to the "Stock Data" folder relative to the application startup path
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

        /// <summary>
        /// Filters the candlestick data based on the selected start and end dates from the date pickers and
        /// updates the DataGridView and chart to display only the filtered results. If the end date is earlier than
        /// the start date, a message box is shown to inform the user.
        /// </summary>
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

        /// <summary>
        /// Handles the click event of the "Update Date" button. Calls the filterCandlestickDate
        /// method to filter and update the displayed candlestick data based on the selected date range.
        /// </summary>

        private void UpdateDate_Click(object sender, EventArgs e)
        {
            FilterCandlestickDate();
        }
    }
}
