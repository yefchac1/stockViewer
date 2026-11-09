using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace yefersonProject
{
    /// <summary>
    /// This class represents a form that displays a candlestick chart for stock data. It allows users to view and filter stock data
    /// based on a specified date range, and it annotates peaks and valleys in the candlestick data.
    /// </summary>
    public partial class StockChart : Form
    {
        private List<aCandlestick> candlesticks = new List<aCandlestick>(); // A list to hold the filtered candlestick data.
        private List<aCandlestick> totalCandlesticks = new List<aCandlestick>(); // A list to hold all candlestick data read from the CSV file.
        private List<aSmartCandlestick> smartCandlesticks = new List<aSmartCandlestick>(); // A list to hold the smart candlestick data, which includes additional properties such as index, isPeak, and isValley.
        private FormChart stockDataTableForm; // A reference to the FormChart instance that displays the candlestick data in a table format. This allows for updating the table when the chart is updated.
        private bool isDragging = false; // A flag to indicate whether the user is currently selecting a range on the chart.
        private Point startPoint; // The starting point of the selection on the chart.
        private Point currentPoint; // The ending point of the selection on the chart.
        private Rectangle selectedRectangle; // A rectangle representing the selected area on the chart, defined by the start and end points of the selection.

        /// <summary>
        /// Initializes the StockChart form and sets up the chart with the provided stock data, date range, and a reference to the FormChart instance for updating the table.
        /// </summary>
        public StockChart()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Takes the file path, start date, end date, and a reference to the FormChart instance to initialize the StockChart form. 
        /// It reads the candlestick data from the specified file, filters it based on the provided date range, converts it into 
        /// smart candlestick data, and binds it to the chart for display. It also updates annotations for peaks and valleys in 
        /// the chart.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="updatedForm"></param>
        public StockChart(string filePath, DateTime startDate, DateTime endDate, FormChart updatedForm)
        {
            InitializeComponent();

            // Read all candlestick data from the specified CSV file using the ReadCandlestickFromFiles method from CandlestickUtilities.
            totalCandlesticks = CandlestickUtilities.ReadCandlestickFromFiles(filePath);
            // Filter the candlestick data based on the specified start and end dates using the FilterCandlestick method from CandlestickUtilities.
            candlesticks = CandlestickUtilities.FilterCandlestick(totalCandlesticks, startDate, endDate);
            // Convert the filtered candlestick data into smart candlestick data, which includes additional properties such as index, isPeak, and isValley.
            smartCandlesticks = CandlestickUtilities.ConvertIntoSmartCandlestick(candlesticks);
            // Store the reference to the FormChart instance for updating the table when the chart is updated.
            this.stockDataTableForm = updatedForm;

            // Bind the smart candlestick data to the chart for display.
            StockViewerChart.DataSource = smartCandlesticks;

            // Set the X value member for the price series in the chart to the "Date" property of the smart candlestick data.
            StockViewerChart.Series["PriceSeries"].XValueMember = "Date";
            // Set the Y value members for the price series in the chart to the "High", "Low", "Open", and "Close" properties of the smart candlestick data.
            StockViewerChart.Series["PriceSeries"].YValueMembers = "High,Low,Open,Close";

            // Set the X value member for the volume series in the chart to the "Date" property of the smart candlestick data.
            StockViewerChart.Series["VolumeSeries"].XValueMember = "Date";
            // Set the Y value member for the volume series in the chart to the "Volume" property of the smart candlestick data.
            StockViewerChart.Series["VolumeSeries"].YValueMembers = "Volume";

            // Refresh the chart to reflect the updated data and settings.
            StockViewerChart.DataBind();

            // Update the annotations for peaks and valleys in the chart based on the smart candlestick data.
            UpdateAnnotations(); 
        }
        /// <summary>
        /// Updates the annotations on the StockViewerChart to display peaks and valleys in the smart candlestick data.
        /// </summary>
        private void UpdateAnnotations()
        {
            // Clear any existing annotations on the chart to prepare for adding new annotations based on the current smart candlestick data.
            StockViewerChart.Annotations.Clear();

            // Iterate through each smart candlestick in the list to check for peaks and valleys.
            foreach (aSmartCandlestick scs in smartCandlesticks)
            {
                // If the current smart candlestick is identified as a peak, create a text annotation to mark it on the chart.
                if (scs.isPeak)
                {   
                    // Properties of the annotation are set to display "P" in dark green color, anchored to the corresponding data point on the chart.
                    TextAnnotation isPeakAnnotation = new TextAnnotation(); 
                    isPeakAnnotation.Text = "P";
                    isPeakAnnotation.ForeColor = Color.DarkGreen;
                    isPeakAnnotation.AnchorAlignment = ContentAlignment.BottomCenter;
                    isPeakAnnotation.AnchorDataPoint = StockViewerChart.Series["PriceSeries"].Points[scs.index]; // The annotation is anchored to the data point corresponding to the peak candlestick.
                    isPeakAnnotation.AnchorY = (double)scs.High;

                    // Now we add the annotation to the chart's Annotations collection, which will display it on the chart.
                    StockViewerChart.Annotations.Add(isPeakAnnotation);
                }

                // If the current smart candlestick is identified as a valley, create a text annotation to mark it on the chart.
                if (scs.isValley)
                {
                    // Properties of the annotation are set to display "V" in dark red color, anchored to the corresponding data point on the chart.
                    TextAnnotation isValleyAnnotation = new TextAnnotation();
                    isValleyAnnotation.Text = "V";
                    isValleyAnnotation.ForeColor = Color.DarkRed;
                    isValleyAnnotation.AnchorAlignment = ContentAlignment.TopCenter;
                    isValleyAnnotation.AnchorDataPoint = StockViewerChart.Series["PriceSeries"].Points[scs.index];
                    isValleyAnnotation.AnchorY = (double)scs.Low;

                    // Just like with peaks, we add the valley annotation to the chart's Annotations collection to display it on the chart.
                    StockViewerChart.Annotations.Add(isValleyAnnotation);
                }
            }
        }
        /// <summary>
        /// When the "Update Date" button is clicked, this method retrieves the new start and end dates from the date pickers, 
        /// filters the total candlestick data based on the new date range, updates the smart candlestick data, and refreshes 
        /// the chart and annotations. If the FormChart instance is still open, it also updates the table in that form with the 
        /// new date range.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateDate_Click(object sender, EventArgs e)
        {
            // Retrieve the new start and end dates from the date pickers on the form.
            DateTime newStartDate = newStartDatePicker.Value.Date;
            DateTime newEndDate = newEndDatePicker.Value.Date;

            // Filters the total candlestick data based on the new start and end dates using the FilterCandlestick method from CandlestickUtilities.
            candlesticks = CandlestickUtilities.FilterCandlestick(totalCandlesticks, newStartDate, newEndDate);

            // Convert the filtered candlestick data into smart candlestick data.
            smartCandlesticks = CandlestickUtilities.ConvertIntoSmartCandlestick(candlesticks);

            // Bind the updated smart candlestick data to the chart for display and refresh the chart to reflect the changes.
            StockViewerChart.DataSource = smartCandlesticks;
            StockViewerChart.DataBind();

            // Update the annotations on the chart to reflect any new peaks and valleys based on the updated smart candlestick data.
            UpdateAnnotations();

            // Error handling: Check if the FormChart instance is still open and not disposed before attempting to update its table with the new date range.
            if (stockDataTableForm != null && !stockDataTableForm.IsDisposed)
            {
                stockDataTableForm.UpdateTable(newStartDate, newEndDate);
            }
        }
        private void StockViewerChart_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            startPoint = e.Location;
            currentPoint = e.Location;
            selectedRectangle = Rectangle.Empty;
            StockViewerChart.Invalidate(); 
        }
        private void StockViewerChart_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                currentPoint = e.Location;
                selectedRectangle = GetRectangle(startPoint, currentPoint);
                StockViewerChart.Invalidate();
            }
        }
        private void StockViewerChart_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                isDragging = false;
                StockViewerChart.Invalidate(); 
            }
        }
        private Rectangle GetRectangle(Point p1, Point p2)
        {
            int x = Math.Min(p1.X, p2.X);
            int y = Math.Min(p1.Y, p2.Y);
            int w = Math.Abs(p1.X - p2.X);
            int h = Math.Abs(p1.Y - p2.Y);
            return new Rectangle(x, y, w, h);
        }

        private void StockViewerChart_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);

            if(selectedRectangle != Rectangle.Empty)
            {
                using (Pen pen = new Pen(Color.Red, 1))
                {
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    e.Graphics.DrawRectangle(pen, selectedRectangle);
                }
            }
        }
    }
}