using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace yefersonProject
{
    /// <summary>
    /// This class represents a form that displays a table of candlestick data. 
    /// It allows users to view and filter stock data based on a specified date range.
    /// </summary>
    public partial class FormChart : Form
    {
        private List<aCandlestick> candlesticks = new List<aCandlestick>(); // A list to hold the filtered candlestick data.
        private List<aCandlestick> totalCandlesticks = new List<aCandlestick>(); // A list to hold all candlestick data read from the CSV file.
        private List<aSmartCandlestick> smartCandlesticks = new List<aSmartCandlestick>(); // A list to hold the smart candlestick data, which includes additional properties such as index, isPeak, and isValley.

        /// <summary>
        /// Initializes a new instance of the FormChart class. This constructor is used when no initial data is provided.
        /// </summary>
        public FormChart()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This class uses functions from CandlestickUtilities to read candlestick data from a CSV file, filter it based on a specified date range, 
        /// and convert it into smart candlestick data.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        public FormChart(string filePath, DateTime startDate, DateTime endDate)
        {
            // Initialize the form's components.
            InitializeComponent();

            // Read all candlestick data from the specified CSV file using the ReadCandlestickFromFiles method from CandlestickUtilities.
            totalCandlesticks = CandlestickUtilities.ReadCandlestickFromFiles(filePath);

            // Filter the candlestick data based on the specified start and end dates using the FilterCandlestick method from CandlestickUtilities.
            candlesticks = CandlestickUtilities.FilterCandlestick(totalCandlesticks, startDate, endDate);

            // Convert the filtered candlestick data into smart candlestick data, which includes additional properties such as index, isPeak, and isValley.
            smartCandlesticks = CandlestickUtilities.ConvertIntoSmartCandlestick(candlesticks);

            // Bind the smart candlestick data to the DataGridView for display.
            candlestickBindingSource.DataSource = smartCandlesticks;

            // Refresh the DataGridView to reflect the updated data.
            dataGridView1.DataSource = candlestickBindingSource;
        }

        /// <summary>
        /// Takes the updated dates from StockForm and filters the candlestick data based on the specified start and end dates.
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        public void UpdateTable(DateTime startDate, DateTime endDate)
        {
            // Filter the candlestick data based on dates using the FilterCandlestick method from CandlestickUtilities.
            candlesticks = CandlestickUtilities.FilterCandlestick(totalCandlesticks, startDate, endDate);

            // Convert the filtered candlestick data into smart candlestick data.
            smartCandlesticks = CandlestickUtilities.ConvertIntoSmartCandlestick(candlesticks);

            // Bind the updated smart candlestick data to the DataGridView for display.
            candlestickBindingSource.DataSource = smartCandlesticks;

            // Refresh the DataGridView to reflect the updated data.
            candlestickBindingSource.ResetBindings(false);
        }
    }
}