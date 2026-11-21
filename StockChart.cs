using Microsoft.Win32;
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
        private int startIndex = -1; // The index of the starting point of the selection in the smart candlestick data. Initialized to -1 to indicate that no selection has been made yet.
        private bool startsAtValley = false; // A flag to indicate whether the selection starts at a valley in the smart candlestick data. This is used to determine how to handle the selection when it is made.

        private Point snapStartPoint; // The snapped starting point of the selection on the chart, adjusted to align with the nearest candlestick data point.
        private bool startsAtSnappedValley = false; // A flag to indicate whether the snapped selection starts at a valley in the smart candlestick data. This is used to determine how to handle the snapped selection when it is made.    

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
            StockViewerChart.Series["PriceSeries"].XValueMember = "index";
            // Set the Y value members for the price series in the chart to the "High", "Low", "Open", and "Close" properties of the smart candlestick data.
            StockViewerChart.Series["PriceSeries"].YValueMembers = "High,Low,Open,Close";

            // Set the X value member for the volume series in the chart to the "Date" property of the smart candlestick data.
            StockViewerChart.Series["VolumeSeries"].XValueMember = "index";
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
            StockViewerChart.Annotations.Clear();

            // Create or reuse the marker series.
            Series peaks = StockViewerChart.Series.FindByName("PeakDots");
            Series valleys = StockViewerChart.Series.FindByName("ValleyDots");

            if (peaks == null)
                peaks = StockViewerChart.Series.Add("PeakDots");

            if (valleys == null)
                valleys = StockViewerChart.Series.Add("ValleyDots");

            // Configure the circular markers.
            peaks.ChartType = SeriesChartType.Point;
            peaks.ChartArea = "StockArea";
            peaks.MarkerStyle = MarkerStyle.Circle;
            peaks.MarkerSize = 6;
            peaks.Color = Color.DarkGreen;
            peaks.IsVisibleInLegend = false;

            valleys.ChartType = SeriesChartType.Point;
            valleys.ChartArea = "StockArea";
            valleys.MarkerStyle = MarkerStyle.Circle;
            valleys.MarkerSize = 6;
            valleys.Color = Color.DarkRed;
            valleys.IsVisibleInLegend = false;

            peaks.Points.Clear();
            valleys.Points.Clear();

            Series priceSeries = StockViewerChart.Series["PriceSeries"];

            foreach (aSmartCandlestick scs in smartCandlesticks)
            {
                if (scs.index < 0 || scs.index >= priceSeries.Points.Count)
                    continue;

                double x = priceSeries.Points[scs.index].XValue;

                if (scs.isPeak)
                {
                    peaks.Points.AddXY(x, (double)scs.High);
                }

                if (scs.isValley)
                {
                    valleys.Points.AddXY(x, (double)scs.Low);
                }
            }
        }


        private bool TrySnap(Point mouse, out Point snapped,
                             out int index, out bool isValley)
        {
            snapped = mouse;
            index = -1;
            isValley = false;

            try
            {
                ChartArea area = StockViewerChart.ChartAreas["StockArea"];
                Series series = StockViewerChart.Series["PriceSeries"];

                // Convert mouse pixels into chart values.
                double xValue = area.AxisX.PixelPositionToValue(mouse.X);
                double yValue = area.AxisY.PixelPositionToValue(mouse.Y);

                // Round to the nearest candlestick index.
                int nearest = (int)Math.Round(xValue);

                if (nearest < 0 || nearest >= smartCandlesticks.Count ||
                    nearest >= series.Points.Count)
                    return false;

                aSmartCandlestick candle = smartCandlesticks[nearest];

                double high = (double)candle.High;
                double low = (double)candle.Low;

                // Check if the mouse is within 1% of a peak or valley.
                bool nearPeak = candle.isPeak &&
                    Math.Abs(yValue - high) <= Math.Abs(high) * 0.01;

                bool nearValley = candle.isValley &&
                    Math.Abs(yValue - low) <= Math.Abs(low) * 0.01;

                if (!nearPeak && !nearValley)
                    return false;

                // Select the closest valid price.
                isValley = nearValley &&
                    (!nearPeak ||
                     Math.Abs(yValue - low) < Math.Abs(yValue - high));

                double price = isValley ? low : high;

                // Convert the snapped chart values back into pixels.
                int x = (int)area.AxisX.ValueToPixelPosition(
                    series.Points[nearest].XValue);

                int y = (int)area.AxisY.ValueToPixelPosition(price);

                snapped = new Point(x, y);
                index = nearest;

                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (InvalidOperationException)
            {
                return false;
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
            if (e.Button != MouseButtons.Left)
                return;

            snapStartPoint = e.Location;
            startPoint = snapStartPoint;
            currentPoint = snapStartPoint;

            startIndex = -1;
            startsAtValley = false;

            selectedRectangle = Rectangle.Empty;
            startsAtSnappedValley = true;
            isDragging = true;

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
                currentPoint = e.Location;
                selectedRectangle = GetRectangle(startPoint, currentPoint);

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

        /// <summary>
        /// Checks whether the selected rectangle represents
        /// a valid upward or downward stock movement.
        /// </summary>

        /// <summary>
        /// Checks whether the rectangle started at a peak or valley.
        /// </summary>
        private bool IsValidSegment(Point drawingEnd)
        {
            if (startIndex < 0 ||
                startIndex >= smartCandlesticks.Count ||
                selectedRectangle.Width <= 0 ||
                selectedRectangle.Height <= 0)
                return false;

            // The selection must move forward in time.
            if (drawingEnd.X <= startPoint.X)
                return false;

            // A valley must move upward, and a peak downward.
            return startsAtValley
                ? drawingEnd.Y < startPoint.Y
                : drawingEnd.Y > startPoint.Y;
        }


        /// <summary>
        /// Finds the highest peak or lowest valley inside the selected
        /// rectangle to determine the ending point of the price swing.
        /// </summary>
        private bool TryFindTrendEnd(Rectangle rectangle,
            out Point trendEnd, out double endingPrice)
        {
            trendEnd = Point.Empty;
            endingPrice = 0;

            if (startIndex < 0 || startIndex >= smartCandlesticks.Count)
                return false;

            ChartArea area = StockViewerChart.ChartAreas["StockArea"];
            Series series = StockViewerChart.Series["PriceSeries"];

            double startingPrice = startsAtValley
                ? (double)smartCandlesticks[startIndex].Low
                : (double)smartCandlesticks[startIndex].High;

            bool found = false;

            for (int i = startIndex + 1;
                 i < smartCandlesticks.Count && i < series.Points.Count;
                 i++)
            {
                aSmartCandlestick candle = smartCandlesticks[i];

                // An upward trend needs a peak; a downward trend needs a valley.
                if (startsAtValley && !candle.isPeak)
                    continue;

                if (!startsAtValley && !candle.isValley)
                    continue;

                double price = startsAtValley
                    ? (double)candle.High
                    : (double)candle.Low;

                // The opposing extreme must represent movement in
                // the correct price direction.
                if (startsAtValley && price <= startingPrice)
                    continue;

                if (!startsAtValley && price >= startingPrice)
                    continue;

                int x = (int)Math.Round(area.AxisX.ValueToPixelPosition(
                    series.Points[i].XValue));

                int y = (int)Math.Round(area.AxisY.ValueToPixelPosition(price));

                // Only consider extremes inside the selected rectangle.
                if (x < rectangle.Left || x > rectangle.Right ||
                    y < rectangle.Top || y > rectangle.Bottom)
                    continue;

                // Choose the highest peak or the lowest valley.
                if (!found ||
                    (startsAtValley && price > endingPrice) ||
                    (!startsAtValley && price < endingPrice))
                {
                    endingPrice = price;
                    trendEnd = new Point(x, y);
                    found = true;
                }
            }

            return found;
        }
        /// <summary>
        /// Determines whether a candlestick price visually touches
        /// a Fibonacci level, using a small pixel tolerance.
        /// </summary>
        private bool IsTouchAtLevel(double candlePrice,
                                    double levelPrice,
                                    Axis axisY)
        {
            double candleY = axisY.ValueToPixelPosition(candlePrice);
            double levelY = axisY.ValueToPixelPosition(levelPrice);

            // A small pixel tolerance avoids floating-point equality issues.
            return Math.Abs(candleY - levelY) <= 2.0;
        }

        /// <summary>
        /// Draws orange touch indicators for candlesticks whose
        /// OHLC prices touch the Fibonacci levels inside the rectangle.
        /// Returns the number of touch indicators drawn.
        /// </summary>
        private int DrawTouchIndicators(Graphics g,
                                        Rectangle rectangle,
                                        List<double> fibLevels,
                                        ChartArea area)
        {
            int touchCount = 0;

            Series series = StockViewerChart.Series["PriceSeries"];
            Axis axisY = area.AxisY;

            const int dotSize = 7;

            using (SolidBrush touchBrush = new SolidBrush(Color.Orange))
            {
                for (int i = startIndex;
                     i < smartCandlesticks.Count && i < series.Points.Count;
                     i++)
                {
                    aSmartCandlestick candle = smartCandlesticks[i];

                    float x = (float)area.AxisX.ValueToPixelPosition(
                        series.Points[i].XValue);

                    // Only check candlesticks whose X position is inside the rectangle.
                    if (x < rectangle.Left || x > rectangle.Right)
                        continue;

                    foreach (double levelPrice in fibLevels)
                    {
                        float y = (float)axisY.ValueToPixelPosition(levelPrice);

                        // Only draw touches that are inside the rectangle.
                        if (y < rectangle.Top || y > rectangle.Bottom)
                            continue;

                        // Count one touch per candlestick per Fibonacci level
                        // if any OHLC price touches that level.
                        bool touched =
                            IsTouchAtLevel((double)candle.Open, levelPrice, axisY) ||
                            IsTouchAtLevel((double)candle.High, levelPrice, axisY) ||
                            IsTouchAtLevel((double)candle.Low, levelPrice, axisY) ||
                            IsTouchAtLevel((double)candle.Close, levelPrice, axisY);

                        if (!touched)
                            continue;

                        g.FillEllipse(
                            touchBrush,
                            x - dotSize / 2f,
                            y - dotSize / 2f,
                            dotSize,
                            dotSize);

                        touchCount++;
                    }
                }
            }

            return touchCount;
        }
        /// <summary>
        /// Draws the selection rectangle, actual swing trend line,
        /// Fibonacci retracement levels and their price labels.
        /// The drawing updates continuously during rubberbanding.
        /// </summary>
        private void StockViewerChart_Paint(object sender, PaintEventArgs e)
        {
            if (!startsAtSnappedValley)
            {
                this.Text = "Stock Viewer";
                return;
            }

            // Identify the starting peak or valley.
            startPoint = snapStartPoint;
            startIndex = -1;
            startsAtValley = false;

            Point snappedStart;
            int index;
            bool valley;

            if (TrySnap(snapStartPoint, out snappedStart, out index, out valley))
            {
                startPoint = snappedStart;
                startIndex = index;
                startsAtValley = valley;
            }

            ChartArea area = StockViewerChart.ChartAreas["StockArea"];
            Series series = StockViewerChart.Series["PriceSeries"];

            Point drawingEnd = currentPoint;

            // After MouseUp, snap the rectangle horizontally.
            if (!isDragging)
            {
                try
                {
                    double xValue =
                        area.AxisX.PixelPositionToValue(currentPoint.X);

                    if (!double.IsNaN(xValue) && !double.IsInfinity(xValue))
                    {
                        int nearest = (int)Math.Round(xValue);

                        if (nearest >= 0 && nearest < series.Points.Count)
                        {
                            drawingEnd.X = (int)Math.Round(
                                area.AxisX.ValueToPixelPosition(
                                    series.Points[nearest].XValue));
                        }
                    }
                }
                catch (ArgumentException) { }
                catch (InvalidOperationException) { }
            }
            // Get the price area's pixel boundaries.
            int left = (int)area.AxisX.ValueToPixelPosition(area.AxisX.Minimum);
            int right = (int)area.AxisX.ValueToPixelPosition(area.AxisX.Maximum);

            int top = (int)area.AxisY.ValueToPixelPosition(area.AxisY.Maximum);
            int bottom = (int)area.AxisY.ValueToPixelPosition(area.AxisY.Minimum);

            // Prevent the rectangle from leaving the price plotting area.
            drawingEnd.X = Math.Max(left, Math.Min(right, drawingEnd.X));
            drawingEnd.Y = Math.Max(top, Math.Min(bottom, drawingEnd.Y));

            selectedRectangle = GetRectangle(startPoint, drawingEnd);

            if (selectedRectangle.Width <= 0 ||
                selectedRectangle.Height <= 0)
            {
                this.Text = "Stock Viewer";
                return;
            }

            // Determine whether the selected price movement is valid.
            bool valid = IsValidSegment(drawingEnd);

            Point trendEnd = Point.Empty;
            double endingPrice = 0;

            if (valid)
            {
                valid = TryFindTrendEnd(
                    selectedRectangle, out trendEnd, out endingPrice);
            }

            Color rectangleColor = valid
                ? (startsAtValley ? Color.Green : Color.Red)
                : Color.White;

            var dashStyle = isDragging
                ? System.Drawing.Drawing2D.DashStyle.Dash
                : System.Drawing.Drawing2D.DashStyle.Solid;

            // Draw the rubberband rectangle.
            using (Pen pen = new Pen(rectangleColor, 3))
            {
                pen.DashStyle = dashStyle;
                e.Graphics.DrawRectangle(pen, selectedRectangle);
            }

            // Invalid selections have no trend line or Fibonacci levels.
            if (!valid)
            {
                this.Text = "Stock Viewer - Touches: 0";
                return;
            }

            // Draw the diagonal between actual candlestick extremes.
            using (Pen trendPen = new Pen(rectangleColor, 2))
            {
                trendPen.DashStyle = dashStyle;
                e.Graphics.DrawLine(trendPen, startPoint, trendEnd);
            }

            // Starting price is the selected peak's High or valley's Low.
            double startingPrice = startsAtValley
                ? (double)smartCandlesticks[startIndex].Low
                : (double)smartCandlesticks[startIndex].High;

            double[] ratios = { 0.236, 0.382, 0.5, 0.618, 0.786 };
            List<double> fibLevels = new List<double>();

            using (Pen fibPen = new Pen(rectangleColor, 1.2f))
            using (Font fibFont = new Font("Segoe UI", 8))
            using (SolidBrush textBrush = new SolidBrush(rectangleColor))
            {
                fibPen.DashStyle = dashStyle;

                foreach (double ratio in ratios)
                {
                    // Professor's Fibonacci formula.
                    double levelPrice = startingPrice +
                        (endingPrice - startingPrice) * ratio;

                    fibLevels.Add(levelPrice);

                    float y = (float)area.AxisY.ValueToPixelPosition(levelPrice);

                    // Draw the Fibonacci level inside the rectangle.
                    e.Graphics.DrawLine(fibPen,
                        selectedRectangle.Left, y,
                        selectedRectangle.Right, y);

                    // Display the percentage and calculated price.
                    string label = $"{ratio * 100:0.#}% - ${levelPrice:F2}";

                    SizeF textSize = e.Graphics.MeasureString(label, fibFont);

                    float labelX = selectedRectangle.Right - textSize.Width - 4;
                    float labelY = y - textSize.Height - 2;

                    // Only draw labels that fit inside the selection.
                    if (labelX >= selectedRectangle.Left + 4 &&
                        labelY >= selectedRectangle.Top &&
                        labelY + textSize.Height <= selectedRectangle.Bottom)
                    {
                        e.Graphics.DrawString(
                            label, fibFont, textBrush, labelX, labelY);
                    }
                }
            }

            // Draw orange touch indicators and display their count.
            int touchCount = DrawTouchIndicators(
                e.Graphics, selectedRectangle, fibLevels, area);

            this.Text = $"Stock Viewer - Touches: {touchCount}";
        }



    }
}
