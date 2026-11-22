using System;
using System.Collections.Generic;
using System.Drawing;
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
        private bool isDragging = false; // A flag to indicate whether the user is currently dragging the mouse to select a region on the chart. This is used to control the drawing of the selection rectangle and to determine when to finalize the selection.
        private Point startPoint; // The starting point of the selection on the chart, which is set when the user presses the mouse button down. This point is used to define the initial corner of the selection rectangle.
        private Point currentPoint; // The current point of the mouse cursor on the chart, which is updated as the user moves the mouse while dragging. This point is used to define the opposite corner of the selection rectangle and to provide real-time feedback on the selection area.
        private Rectangle selectedRectangle; // A rectangle representing the selected area on the chart, defined by the start and end points of the selection.
        private int startIndex = -1; // The index of the starting point of the selection in the smart candlestick data. Initialized to -1 to indicate that no selection has been made yet.
        private bool startsAtValley = false; // A flag to indicate whether the selection starts at a valley in the smart candlestick data. This is used to determine how to handle the selection when it is made.
        private Point snapStartPoint; // The snapped starting point of the selection on the chart, adjusted to align with the nearest candlestick data point.

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

            // Set the X value member for the price series in the chart to the "index" property of the smart candlestick data.
            StockViewerChart.Series["PriceSeries"].XValueMember = "index";
            // Set the Y value members for the price series in the chart to the "High", "Low", "Open", and "Close" properties of the smart candlestick data.
            StockViewerChart.Series["PriceSeries"].YValueMembers = "High,Low,Open,Close";

            // Set the X value member for the volume series in the chart to the "index" property of the smart candlestick data.
            StockViewerChart.Series["VolumeSeries"].XValueMember = "index";
            // Set the Y value member for the volume series in the chart to the "Volume" property of the smart candlestick data.
            StockViewerChart.Series["VolumeSeries"].YValueMembers = "Volume";

            // Refresh the chart to reflect the updated data and settings.
            StockViewerChart.DataBind();

            // Update the annotations for peaks and valleys in the chart based on the smart candlestick data.
            UpdateAnnotations();
        }

        /// <summary>
        /// Updates the annotations on the chart to reflect the peaks and valleys in the smart candlestick data. 
        /// It clears any existing annotations, adds new annotations for peaks and valleys based on the smart candlestick data, 
        /// and ensures that the annotations are correctly positioned on the chart.
        /// </summary>
        private void UpdateAnnotations()
        {
            // Clear any existing annotations on the chart to prepare for updating with new annotations.
            StockViewerChart.Annotations.Clear();

            // Get the series for peaks and valleys from the chart to add new data points for annotations.
            Series peaks = StockViewerChart.Series["PeakDots"];
            Series valleys = StockViewerChart.Series["ValleyDots"];

            // Clear any existing data points in the peaks and valleys series to prepare for adding new data points based on the smart candlestick data.
            peaks.Points.Clear();
            valleys.Points.Clear();

            // Get the price series from the chart to access the X values for the smart candlestick data points.
            Series priceSeries = StockViewerChart.Series["PriceSeries"];

            // Iterate through the smart candlestick data to identify peaks and valleys and add corresponding data points to the peaks and valleys series for annotation.
            foreach (aSmartCandlestick scs in smartCandlesticks)
            {
                // Ensure that the index of the smart candlestick is within the valid range of the price series points to avoid out-of-bounds errors.
                if (scs.index < 0 || scs.index >= priceSeries.Points.Count)
                    continue;

                // Retrieve the X value for the current smart candlestick from the price series using its index. This X value represents the position of the candlestick on the chart.
                double x = priceSeries.Points[scs.index].XValue;

                // If the current smart candlestick is identified as a peak, add a data point to the peaks series at the corresponding X value and the High price of the candlestick.
                if (scs.isPeak)
                    peaks.Points.AddXY(x, (double)scs.High);

                if (scs.isValley)
                    valleys.Points.AddXY(x, (double)scs.Low);
            }
        }

        /// <summary>
        /// This method attempts to snap the mouse position to the nearest peak or valley in the smart candlestick data. 
        /// It converts the mouse pixel coordinates into chart values, finds the nearest candlestick index, and checks if the 
        /// mouse is within a certain tolerance of a peak or valley. If successful, it returns the snapped point, index, and whether it is a valley.
        /// </summary>
        /// <param name="mouse"></param>
        /// <param name="snapped"></param>
        /// <param name="index"></param>
        /// <param name="isValley"></param>
        /// <returns></returns>
        private bool TrySnap(Point mouse, out Point snapped, out int index, out bool isValley)
        {
            snapped = mouse;    // Initialize the snapped point to the original mouse position.
            index = -1;         // Initialize the index to -1 to indicate that no valid index has been found yet.
            isValley = false;   // Initialize the isValley flag to false to indicate that the snapped point is not a valley by default.

            // If there are no smart candlesticks available, snapping is not possible, so return false.
            try
            {
                // Get the chart area and series for the stock viewer chart to perform calculations related to snapping.
                ChartArea area = StockViewerChart.ChartAreas["StockArea"];
                Series series = StockViewerChart.Series["PriceSeries"];

                // Convert mouse pixels into chart values.
                double xValue = area.AxisX.PixelPositionToValue(mouse.X);
                double yValue = area.AxisY.PixelPositionToValue(mouse.Y);

                // Round to the nearest candlestick index.
                int nearest = (int)Math.Round(xValue);

                // Check if the nearest index is valid and within the bounds of the smart candlestick list and series points.
                // If not, return false to indicate that snapping is not possible.
                if (nearest < 0 || nearest >= smartCandlesticks.Count ||
                    nearest >= series.Points.Count)
                    return false;

                // Get the smart candlestick at the nearest index to check its properties for snapping.
                aSmartCandlestick candle = smartCandlesticks[nearest];

                // Convert the High and Low prices of the candlestick to double for comparison with the mouse Y value.
                double high = (double)candle.High;
                double low = (double)candle.Low;

                // Check if the mouse is within 1% of a peak or valley.
                bool nearPeak = candle.isPeak &&
                    Math.Abs(yValue - high) <= Math.Abs(high) * 0.01;

                // Check if the mouse is within 1% of a valley.
                bool nearValley = candle.isValley &&
                    Math.Abs(yValue - low) <= Math.Abs(low) * 0.01;

                // If the mouse is not near a peak or valley, snapping is not possible, so return false.
                if (!nearPeak && !nearValley)
                    return false;

                // Select the closest valid price.
                isValley = nearValley &&
                    (!nearPeak ||
                     Math.Abs(yValue - low) < Math.Abs(yValue - high));

                // Determine the price to snap to based on whether it is a valley or a peak.
                double price = isValley ? low : high;

                // Convert the snapped chart values back into pixels.
                int x = (int)area.AxisX.ValueToPixelPosition(
                    series.Points[nearest].XValue);

                // Convert the snapped price value back into pixels for the Y coordinate.
                int y = (int)area.AxisY.ValueToPixelPosition(price);

                // Set the snapped point to the calculated pixel coordinates and update the index to the nearest candlestick index.
                snapped = new Point(x, y);

                // Update the index to the nearest candlestick index for reference in other methods.
                index = nearest;

                // Return true to indicate that snapping was successful and the snapped point, index, and isValley flag have been set correctly.
                return true;
            }

            // Catch any exceptions that may occur during the snapping process, such as invalid arguments or operations, and return false to indicate that snapping was not successful.
            catch (ArgumentException)
            {
                return false;
            }
            // Catch any exceptions that may occur during the snapping process, such as invalid operations, and return false to indicate that snapping was not successful.
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
        /// <summary>
        /// This method handles the MouseDown event on the StockViewerChart. It checks if the left mouse button is pressed, and if so, 
        /// it initializes the starting point of the selection rectangle, sets the dragging flag to true, and invalidates the chart to 
        /// trigger a repaint. This allows the user to start selecting a region on the chart for further analysis.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StockViewerChart_MouseDown(object sender, MouseEventArgs e)
        {
            // Only respond to left mouse button clicks; ignore other buttons.
            if (e.Button != MouseButtons.Left)
                return;

            // Reset the touch count display to "Touches: 0" to indicate that no touches have been found yet in the new selection.
            BoxTouches.Text = "Touches: 0";

            // Store the initial mouse position as the starting point for the selection rectangle.
            snapStartPoint = e.Location;

            // Initialize the current point to the starting point to prepare for drawing the selection rectangle.
            startPoint = snapStartPoint;

            // Initialize the current point to the starting point to prepare for drawing the selection rectangle.
            currentPoint = snapStartPoint;

            // Reset the starting index and valley flag to indicate that no selection has been made yet.
            startIndex = -1;

            // Reset the startsAtValley flag to false to indicate that the selection does not start at a valley by default.
            startsAtValley = false;

            // Reset the selected rectangle to an empty rectangle to prepare for drawing a new selection.
            selectedRectangle = Rectangle.Empty;



            // Set the isDragging flag to true to indicate that the user is currently dragging the mouse to select a region on the chart.
            isDragging = true;

            StockViewerChart.Invalidate();
        }
        private void StockViewerChart_MouseMove(object sender, MouseEventArgs e)
        {
            // If the user is currently dragging the mouse to select a region on the chart, update the current point to the new mouse location,
            // calculate the selection rectangle based on the starting point and current point, and invalidate the chart to trigger a repaint.
            if (isDragging)
            {
                // Update the current point to the new mouse location as the user moves the mouse while dragging.
                currentPoint = e.Location;

                // Calculate the selection rectangle based on the starting point and current point to provide real-time feedback on the selected area.
                selectedRectangle = GetRectangle(startPoint, currentPoint);

                // Invalidate the chart to trigger a repaint, which will draw the selection rectangle and any other visual elements based on the current state.
                StockViewerChart.Invalidate();
            }
        }

        /// <summary>
        /// This method handles the MouseUp event on the StockViewerChart. It checks if the user was dragging the mouse to select a region, 
        /// and if so, it finalizes the selection by updating the current point, calculating the final selection rectangle, resetting the dragging 
        /// flag, and invalidating the chart to trigger a repaint. This allows the user to complete their selection on the chart for further analysis.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StockViewerChart_MouseUp(object sender, MouseEventArgs e)
        {
            // Only respond to left mouse button releases; ignore other buttons.
            if (isDragging)
            {
                // Update the current point to the final mouse location when the user releases the left mouse button.
                currentPoint = e.Location;

                // Calculate the final selection rectangle based on the starting point and current point to finalize the selected area.
                selectedRectangle = GetRectangle(startPoint, currentPoint);

                // Reset the isDragging flag to false to indicate that the user has completed their selection and is no longer dragging the mouse.
                isDragging = false;

                // Invalidate the chart to trigger a repaint, which will draw the final selection rectangle and any other visual elements based on the completed selection.
                StockViewerChart.Invalidate();
            }
        }

        /// <summary>
        /// This method calculates and returns a rectangle defined by two points, p1 and p2. It determines the top-left corner of the rectangle and 
        /// its width and height based on the minimum and maximum coordinates of the two points. This is useful for creating a selection rectangle 
        /// on the chart based on user input.
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        private Rectangle GetRectangle(Point p1, Point p2)
        {
            // Calculate the top-left corner of the rectangle by taking the minimum X and Y coordinates of the two points.
            int x = Math.Min(p1.X, p2.X);
            int y = Math.Min(p1.Y, p2.Y);
            int w = Math.Abs(p1.X - p2.X);
            int h = Math.Abs(p1.Y - p2.Y);

            // Return a new Rectangle object defined by the calculated top-left corner (x, y) and the width (w) and height (h).
            return new Rectangle(x, y, w, h);
        }

        /// <summary>
        /// This method checks whether the selected segment on the chart is valid based on the starting index, selection rectangle dimensions, 
        /// and the direction of the selection. It ensures that the selection moves forward in time and that a valley selection moves upward while 
        /// a peak selection moves downward. This validation is important for determining whether the selected price movement is meaningful for
        /// further analysis.
        /// </summary>
        /// <param name="drawingEnd"></param>
        /// <returns></returns>
        private bool IsValidSegment(Point drawingEnd)
        {
            // Check if the starting index is valid and if the selection rectangle has positive width and height. If not, return false to indicate that the segment is invalid.
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
        /// This method attempts to find the end point of a trend within the selected rectangle on the chart. It checks for peaks or valleys 
        /// in the smart candlestick data, and ensures that the opposing extreme represents movement in the correct price direction. If a 
        /// valid trend end is found, it returns true and provides the trend end point and ending price; otherwise, it returns false.
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="trendEnd"></param>
        /// <param name="endingPrice"></param>
        /// <returns></returns>
        private bool TryFindTrendEnd(Rectangle rectangle, out Point trendEnd, out double endingPrice)
        {
            // Initialize the output parameters to default values.
            trendEnd = Point.Empty;

            // Initialize the ending price to 0 to indicate that no valid trend end has been found yet.
            endingPrice = 0;

            // Check if the starting index is valid and within the bounds of the smart candlestick list.
            // If not, return false to indicate that finding a trend end is not possible.
            if (startIndex < 0 || startIndex >= smartCandlesticks.Count)
                return false;

            // Get the chart area and series for the stock viewer chart to perform calculations related to finding the trend end.
            ChartArea area = StockViewerChart.ChartAreas["StockArea"];
            Series series = StockViewerChart.Series["PriceSeries"];

            // Determine the starting price based on whether the selection starts at a valley or a peak. If it starts at a valley,
            // use the Low price; if it starts at a peak, use the High price.
            double startingPrice = startsAtValley
                ? (double)smartCandlesticks[startIndex].Low
                : (double)smartCandlesticks[startIndex].High;

            // Initialize a flag to indicate whether a valid trend end has been found during the iteration through the smart candlestick data.
            bool found = false;

            // Iterate through the smart candlestick data starting from the index after the starting index, checking for peaks or valleys that
            // meet the criteria for a valid trend end.
            for (int i = startIndex + 1;
                 i < smartCandlesticks.Count && i < series.Points.Count;
                 i++)
            {
                // Get the smart candlestick at the current index to check its properties for determining if it represents a valid trend end.
                aSmartCandlestick candle = smartCandlesticks[i];

                // An upward trend needs a peak; a downward trend needs a valley.
                if (startsAtValley && !candle.isPeak)
                    continue;

                // A downward trend needs a valley; an upward trend needs a peak.
                if (!startsAtValley && !candle.isValley)
                    continue;

                double price = startsAtValley
                    ? (double)candle.High
                    : (double)candle.Low;


                // Ensure that the opposing extreme represents movement in the correct price direction.
                if (startsAtValley && price <= startingPrice)
                    continue;
                if (!startsAtValley && price >= startingPrice)
                    continue;

                // Convert the candlestick's X value and the price to pixel positions on the chart to check if they are within the selected rectangle.
                int x = (int)Math.Round(area.AxisX.ValueToPixelPosition(
                    series.Points[i].XValue));

                // Convert the price to pixel position on the Y axis of the chart.
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
            // Return true if a valid trend end was found, along with the trend end point and ending price; otherwise, return false.
            return found;
        }

        /// <summary>
        /// This method checks whether a given candlestick price (Open, High, Low, or Close) touches a specified Fibonacci level within a small pixel tolerance.
        /// </summary>
        /// <param name="candlePrice"></param>
        /// <param name="levelPrice"></param>
        /// <param name="axisY"></param>
        /// <returns></returns>
        private bool IsTouchAtLevel(double candlePrice,double levelPrice,Axis axisY)
        {
            // Convert the candlestick price and Fibonacci level price to pixel positions on the Y axis of the chart.
            double candleY = axisY.ValueToPixelPosition(candlePrice);

            // Convert the Fibonacci level price to pixel position on the Y axis of the chart.
            double levelY = axisY.ValueToPixelPosition(levelPrice);

            // Check if the absolute difference between the candlestick price pixel position and the Fibonacci level pixel position is within a small tolerance (2 pixels).
            return Math.Abs(candleY - levelY) <= 2.0;
        }

        /// <summary>
        /// This method draws touch indicators on the chart for each candlestick that touches any of the specified Fibonacci levels within the selected rectangle.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rectangle"></param>
        /// <param name="fibLevels"></param>
        /// <param name="area"></param>
        /// <returns></returns>
        private int DrawTouchIndicators(Graphics g, Rectangle rectangle, List<double> fibLevels, ChartArea area)
        {
            // Initialize a counter to keep track of the number of touches found within the selected rectangle and Fibonacci levels.
            int touchCount = 0;

            // Get the series for the price data from the chart to access the candlestick data points.
            Series series = StockViewerChart.Series["PriceSeries"];
            Axis axisY = area.AxisY;

            // Define a constant for the size of the touch indicator dots to be drawn on the chart.
            const int dotSize = 7;

            // Use a SolidBrush with an orange color to draw the touch indicators on the chart. The brush is disposed of automatically after use.
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

                        // Check if any of the candlestick's Open, High, Low, or Close prices touch the Fibonacci level within a small
                        // pixel tolerance.
                        bool touched =
                            IsTouchAtLevel((double)candle.Open, levelPrice, axisY) ||
                            IsTouchAtLevel((double)candle.High, levelPrice, axisY) ||
                            IsTouchAtLevel((double)candle.Low, levelPrice, axisY) ||
                            IsTouchAtLevel((double)candle.Close, levelPrice, axisY);

                        // If the candlestick does not touch the Fibonacci level, continue to the next level without drawing an indicator.
                        if (!touched)
                            continue;

                        // Draw a small orange dot at the position of the touch indicator on the chart to visually indicate that the candlestick touches the Fibonacci level.
                        g.FillEllipse(
                            touchBrush,
                            x - dotSize / 2f,
                            y - dotSize / 2f,
                            dotSize,
                            dotSize);

                        // Increment the touch count to keep track of the number of touches found within the selected rectangle and Fibonacci levels.
                        touchCount++;
                    }
                }
            }

            return touchCount;
        }

        /// <summary>
        /// This method handles the Paint event of the StockViewerChart. It is responsible for drawing the selection rectangle, trend line, 
        /// Fibonacci levels, and touch indicators on the chart based on the user's selection and the smart candlestick data.
        /// It also updates the form's title to display the number of touches found within the selected area.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StockViewerChart_Paint(object sender, PaintEventArgs e)
        {
            // If the selection rectangle has non-positive width or height, return early, as there is no valid selection to analyze.
            if (selectedRectangle.Width <= 0 || selectedRectangle.Height <= 0)
            {
                return;
            }

            // If the user is currently dragging the mouse to select a region on the chart, update the current point to the new mouse location,
            startPoint = snapStartPoint;

            // Reset the starting index and valley flag to indicate that no selection has been made yet.
            startIndex = -1;

            // Reset the startsAtValley flag to false to indicate that the selection does not start at a valley by default.
            startsAtValley = false;

            // Store the snapped starting point of the selection on the chart, adjusted to align with the nearest candlestick data point.
            Point snappedStart;

            // Variables to hold the index of the snapped candlestick and whether it is a valley.
            int index;

            // Variable to hold whether the snapped candlestick is a valley.
            bool valley;

            // Attempt to snap the starting point of the selection to the nearest peak or valley in the smart candlestick data.
            if (TrySnap(snapStartPoint, out snappedStart, out index, out valley))
            {
                startPoint = snappedStart;
                startIndex = index;
                startsAtValley = valley;
            }

            // Get the chart area and series for the stock viewer chart to perform calculations related to drawing the selection rectangle, trend line, and Fibonacci levels.
            ChartArea area = StockViewerChart.ChartAreas["StockArea"];

            // Get the series for the price data from the chart to access the candlestick data points.
            Series series = StockViewerChart.Series["PriceSeries"];

            // Store the current mouse position as the drawing end point for the selection rectangle.
            Point drawingEnd = currentPoint;

            // If the user is not currently dragging the mouse, attempt to snap the drawing end point to the nearest peak or valley in the smart candlestick data.
            if (!isDragging)
            {
                
                // Convert the current mouse pixel position into a chart value on the X axis to find the nearest candlestick index.
                double xValue =
                area.AxisX.PixelPositionToValue(currentPoint.X);

                // Check if the calculated X value is valid (not NaN or Infinity) before proceeding to find the nearest candlestick index.
                if (!double.IsNaN(xValue) && !double.IsInfinity(xValue))
                    {
                        // Round the X value to the nearest candlestick index to find the corresponding candlestick in the smart candlestick data.
                        int nearest = (int)Math.Round(xValue);

                        // Check if the nearest index is valid and within the bounds of the smart candlestick list and series points.
                        if (nearest >= 0 && nearest < series.Points.Count)
                        {
                            // Convert the nearest candlestick's X value back into pixel position on the chart to update the drawing end point.
                            drawingEnd.X = (int)Math.Round(
                                area.AxisX.ValueToPixelPosition(
                                    series.Points[nearest].XValue));
                        }
                    }
            }

            // Get the pixel positions for the left, right, top, and bottom boundaries of the chart area to ensure that the selection rectangle does
            // not extend beyond the chart's plotting area.
            int left = (int)area.AxisX.ValueToPixelPosition(area.AxisX.Minimum);
            int right = (int)area.AxisX.ValueToPixelPosition(area.AxisX.Maximum);

            // Get the pixel positions for the top and bottom boundaries of the chart area to ensure that the selection rectangle does not extend beyond the chart's plotting area.
            int top = (int)area.AxisY.ValueToPixelPosition(area.AxisY.Maximum);
            int bottom = (int)area.AxisY.ValueToPixelPosition(area.AxisY.Minimum);

            // Clamp the drawing end point to ensure that it stays within the boundaries of the chart area. This prevents the selection rectangle from extending outside the visible chart area.
            drawingEnd.X = Math.Max(left, Math.Min(right, drawingEnd.X));
            drawingEnd.Y = Math.Max(top, Math.Min(bottom, drawingEnd.Y));

            // Calculate the selection rectangle based on the starting point and the clamped drawing end point. This rectangle represents the area selected by the user for further analysis.
            selectedRectangle = GetRectangle(startPoint, drawingEnd);

            // If the selection rectangle has non-positive width or height, reset the form's title to "Stock Viewer" and return early, as there is no valid selection to analyze.
            if (selectedRectangle.Width <= 0 || selectedRectangle.Height <= 0)
            {
                return;
            }

            // Determine whether the selected price movement is valid.
            bool valid = IsValidSegment(drawingEnd);

            // Initialize variables to hold the trend end point and ending price for the selected trend. These will be used to draw the trend line and calculate Fibonacci levels.
            Point trendEnd = Point.Empty;

            // Initialize a variable to hold the ending price of the selected trend. This will be used to calculate Fibonacci levels and draw the trend line.
            double endingPrice = 0;

            // If the selected price movement is valid, attempt to find the trend end point and ending price based on the selected rectangle and smart candlestick data.
            if (valid)
            {
                valid = TryFindTrendEnd(selectedRectangle, out trendEnd, out endingPrice);
            }

            // Determine the color of the selection rectangle based on whether the selection is valid and whether it starts at a valley or a peak.
            Color rectangleColor = valid
                ? (startsAtValley ? Color.Green : Color.Red)
                : Color.White;

            // Determine the dash style of the selection rectangle based on whether the user is currently dragging the mouse to select a region.
            var dashStyle = isDragging
                ? System.Drawing.Drawing2D.DashStyle.Dash
                : System.Drawing.Drawing2D.DashStyle.Solid;

            // Draw the selection rectangle on the chart using a Pen with the determined color and dash style. The rectangle is drawn with a thickness of 3 pixels.
            using (Pen pen = new Pen(rectangleColor, 3))
            {
                pen.DashStyle = dashStyle;
                e.Graphics.DrawRectangle(pen, selectedRectangle);
            }

            // If the selected price movement is not valid, reset the form's title to "Stock Viewer" and return early, as there is no valid trend to analyze.
            if (!valid)
            {
                BoxTouches.Text = "Touches: 0";
                return;
            }

            // Draw the trend line connecting the starting point and the trend end point using a Pen with the determined color and dash style. The trend line is drawn with a thickness of 2 pixels.
            using (Pen trendPen = new Pen(rectangleColor, 2))
            {
                trendPen.DashStyle = dashStyle;
                e.Graphics.DrawLine(trendPen, startPoint, trendEnd);
            }

            // Starting price is determined based on whether the selection starts at a valley or a peak. If it starts at a valley, use the Low price; if it starts at a peak, use the High price.
            double startingPrice = startsAtValley
                ? (double)smartCandlesticks[startIndex].Low   // If the selection starts at a valley, use the Low price of the smart candlestick at the starting index.
                : (double)smartCandlesticks[startIndex].High; // If the selection starts at a peak, use the High price of the smart candlestick at the starting index.

            // Ratios for Fibonacci levels are defined as an array of doubles representing the common Fibonacci retracement levels used in technical analysis.
            double[] ratios = { 0.236, 0.382, 0.5, 0.618, 0.786 };

            // Create a list to store the calculated Fibonacci levels based on the starting and ending prices of the selected trend.
            List<double> fibLevels = new List<double>();

            // Use a Pen with the determined color and dash style to draw the Fibonacci levels on the chart. The Pen is disposed of automatically after use.
            using (Pen fibPen = new Pen(rectangleColor, 1.2f))
            using (Font fibFont = new Font("Segoe UI", 8))
            using (SolidBrush textBrush = new SolidBrush(Color.Blue))
            {
                fibPen.DashStyle = dashStyle;

                foreach (double ratio in ratios)
                {
                    // Fibonacci formula shown in class.
                    double levelPrice = startingPrice + (endingPrice - startingPrice) * ratio;

                    // Add the calculated Fibonacci level price to the list of Fibonacci levels for later use in drawing touch indicators.
                    fibLevels.Add(levelPrice);

                    // Convert the Fibonacci level price to pixel position on the Y axis of the chart to determine where to draw the horizontal line representing the level.
                    float y = (float)area.AxisY.ValueToPixelPosition(levelPrice);

                    // Draw a horizontal line representing the Fibonacci level across the selected rectangle on the chart using the calculated Y pixel position.
                    e.Graphics.DrawLine(fibPen, selectedRectangle.Left, y, selectedRectangle.Right, y);

                    // Display the percentage and price of the Fibonacci level on the right side of the selected rectangle, formatted to one decimal place for the percentage and two decimal places for the price.
                    string label = $"{ratio * 100:0.#}% - ${levelPrice:F2}";

                    // Measure the size of the label text to determine its width and height for proper positioning on the chart.
                    SizeF textSize = e.Graphics.MeasureString(label, fibFont);

                    // Calculate the position for the label to be drawn on the chart, ensuring that it is placed within the selected rectangle and does not overlap with the edges.
                    float labelX = selectedRectangle.Right - textSize.Width - 4;
                    float labelY = y - textSize.Height - 2;

                    // Only draw labels that fit inside the selection.
                    if (labelX >= selectedRectangle.Left + 4 && labelY >= selectedRectangle.Top && labelY + textSize.Height <= selectedRectangle.Bottom)
                    {
                        // Draw the label text on the chart at the calculated position using the specified font and brush for color.
                        e.Graphics.DrawString(label, fibFont, textBrush, labelX, labelY);
                    }
                }
            }

            // Count the number of candlesticks that touch any Fibonacci level and update 
            int touchCount = DrawTouchIndicators(e.Graphics, selectedRectangle, fibLevels, area);

            // Display the number of touches found within the selected area in the form's title bar, providing feedback to the user about the analysis results.
            BoxTouches.Text = $"Touches: {touchCount}";
        }
    }
}