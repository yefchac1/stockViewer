namespace yefersonProject
{
    partial class StockChart
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.StockViewerChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.StockViewerChart)).BeginInit();
            this.SuspendLayout();
            // 
            // StockViewerChart
            // 
            chartArea1.AxisY.IsStartedFromZero = false;
            chartArea1.Name = "StockArea";
            chartArea1.Position.Auto = false;
            chartArea1.Position.Height = 65F;
            chartArea1.Position.Width = 94F;
            chartArea1.Position.X = 3F;
            chartArea1.Position.Y = 3F;
            chartArea2.AlignWithChartArea = "StockArea";
            chartArea2.Name = "VolumeArea";
            chartArea2.Position.Auto = false;
            chartArea2.Position.Height = 30F;
            chartArea2.Position.Width = 94F;
            chartArea2.Position.X = 3F;
            chartArea2.Position.Y = 70F;
            this.StockViewerChart.ChartAreas.Add(chartArea1);
            this.StockViewerChart.ChartAreas.Add(chartArea2);
            this.StockViewerChart.Location = new System.Drawing.Point(12, 12);
            this.StockViewerChart.Name = "StockViewerChart";
            series1.ChartArea = "StockArea";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick;
            series1.CustomProperties = "PriceDownColor=Red, PriceUpColor=0\\, 192\\, 0";
            series1.Name = "PriceSeries";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            series1.YValuesPerPoint = 4;
            series2.ChartArea = "VolumeArea";
            series2.Name = "VolumeSeries";
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            this.StockViewerChart.Series.Add(series1);
            this.StockViewerChart.Series.Add(series2);
            this.StockViewerChart.Size = new System.Drawing.Size(1017, 551);
            this.StockViewerChart.TabIndex = 0;
            this.StockViewerChart.Text = "Stock Viewer";
            // 
            // StockChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1041, 575);
            this.Controls.Add(this.StockViewerChart);
            this.Name = "StockChart";
            this.Text = "Stock Viewer";
            ((System.ComponentModel.ISupportInitialize)(this.StockViewerChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart StockViewerChart;
    }
}