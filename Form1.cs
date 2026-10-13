using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace yefersonProject
{
    /// <summary>
    /// Represents the main form of the stock data viewer application. This form allows users to open a CSV file 
    /// containing stock data, filter the data based on a specified date range, and display the filtered results 
    /// in a DataGridView.
    /// </summary>
    public partial class stockForm : Form
    {
        /// <summary>
        /// A list to hold the candlestick data read from the CSV file. Each aCandlestick object represents 
        /// a single candlestick entry with properties for date, open, high, low, close, and volume.
        /// </summary>
        private List<aCandlestick> candlesticks = new List<aCandlestick>();

        /// <summary>
        /// Initializes a new instance of the stockForm class and sets up the form's components.
        /// </summary>
        public stockForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Reads stock data from a CSV file and returns a list of aCandlestick objects.
        /// </summary>
        /// <param name="filePath">The path of the CSV file.</param>
        
        private List<aCandlestick> ReadStockData(string filePath)
        {
            // Create an empty list to store the candlesticks
            List<aCandlestick> candlesticks = new List<aCandlestick>();

            // Read every line from the CSV file
            var lines = System.IO.File.ReadAllLines(filePath);

            // Skip the header and process each stock data line
            foreach (var line in lines.Skip(1))
            {
                aCandlestick candlestick = new aCandlestick(line);

                candlesticks.Add(candlestick);
            }

            return candlesticks;
        }
        /// <summary>
        /// Handles the click event of the "Open File" button. Opens a file dialog to select a CSV file containing stock data, 
        /// reads the data, and filters it based on the selected date range.
        /// </summary>


        private void openFileClick(object sender, EventArgs e)
        {
            // Set the initial directory to the "Stock Data" folder relative to the application startup path
            stockFileDialog.InitialDirectory = System.IO.Path.GetFullPath(
            System.IO.Path.Combine(Application.StartupPath, @"..\..\..\Stock Data")
            );

            // Show the file dialog and check if the user selected a file
            if (stockFileDialog.ShowDialog() == DialogResult.OK)
            {
                candlesticks = ReadStockData(stockFileDialog.FileName);

                filterCandlestickDate();
            }
        }

        /// <summary>
        /// Filters the candlestick data based on the selected start and end dates from the date pickers 
        /// and updates the DataGridView with the filtered results.
        /// </summary>
        private void filterCandlestickDate()
        {
            // Filter the candlesticks based on the selected start and end dates
            DateTime startDate = startDatePicker.Value.Date;
            DateTime endDate = endDatePicker.Value.Date;
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
            dataStockViewer.DataSource = filteredCandlesticks;
        }

        /// <summary>
        /// Handles the click event of the "Update Date" button. Calls the filterCandlestickDate
        /// method to filter and update the displayed candlestick data based on the selected date range.
        /// </summary>

        private void updateDate_Click(object sender, EventArgs e)
        {
            filterCandlestickDate();
        }
    }
}
