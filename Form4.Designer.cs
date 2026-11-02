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
            this.UpdateDate = new System.Windows.Forms.Button();
            this.EndDateLabel = new System.Windows.Forms.Label();
            this.StartDateLabel = new System.Windows.Forms.Label();
            this.newEndDatePicker = new System.Windows.Forms.DateTimePicker();
            this.newStartDatePicker = new System.Windows.Forms.DateTimePicker();
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
            this.StockViewerChart.Location = new System.Drawing.Point(11, 9);
            this.StockViewerChart.Margin = new System.Windows.Forms.Padding(2);
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
            this.StockViewerChart.Size = new System.Drawing.Size(1177, 627);
            this.StockViewerChart.TabIndex = 0;
            this.StockViewerChart.Text = "Stock Viewer";
            this.StockViewerChart.Click += new System.EventHandler(this.StockViewerChart_Click);
            // 
            // UpdateDate
            // 
            this.UpdateDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold);
            this.UpdateDate.Location = new System.Drawing.Point(1229, 333);
            this.UpdateDate.Margin = new System.Windows.Forms.Padding(2);
            this.UpdateDate.Name = "UpdateDate";
            this.UpdateDate.Size = new System.Drawing.Size(110, 60);
            this.UpdateDate.TabIndex = 12;
            this.UpdateDate.Text = "Update Date";
            this.UpdateDate.UseVisualStyleBackColor = true;
            this.UpdateDate.Click += new System.EventHandler(this.UpdateDate_Click);
            // 
            // EndDateLabel
            // 
            this.EndDateLabel.AutoSize = true;
            this.EndDateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EndDateLabel.Location = new System.Drawing.Point(1198, 291);
            this.EndDateLabel.Name = "EndDateLabel";
            this.EndDateLabel.Size = new System.Drawing.Size(73, 18);
            this.EndDateLabel.TabIndex = 11;
            this.EndDateLabel.Text = "End Date:";
            // 
            // StartDateLabel
            // 
            this.StartDateLabel.AutoSize = true;
            this.StartDateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartDateLabel.Location = new System.Drawing.Point(1193, 260);
            this.StartDateLabel.Name = "StartDateLabel";
            this.StartDateLabel.Size = new System.Drawing.Size(78, 18);
            this.StartDateLabel.TabIndex = 10;
            this.StartDateLabel.Text = "Start Date:";
            // 
            // newEndDatePicker
            // 
            this.newEndDatePicker.CustomFormat = "MM/dd/yyyy";
            this.newEndDatePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newEndDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.newEndDatePicker.Location = new System.Drawing.Point(1280, 289);
            this.newEndDatePicker.MaxDate = new System.DateTime(2026, 12, 31, 0, 0, 0, 0);
            this.newEndDatePicker.MinDate = new System.DateTime(2020, 1, 1, 0, 0, 0, 0);
            this.newEndDatePicker.Name = "newEndDatePicker";
            this.newEndDatePicker.Size = new System.Drawing.Size(97, 23);
            this.newEndDatePicker.TabIndex = 9;
            this.newEndDatePicker.Value = new System.DateTime(2026, 9, 18, 0, 0, 0, 0);
            // 
            // newStartDatePicker
            // 
            this.newStartDatePicker.CustomFormat = "MM/dd/yyyy";
            this.newStartDatePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newStartDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.newStartDatePicker.Location = new System.Drawing.Point(1280, 258);
            this.newStartDatePicker.MaxDate = new System.DateTime(2026, 12, 31, 0, 0, 0, 0);
            this.newStartDatePicker.MinDate = new System.DateTime(2020, 1, 1, 0, 0, 0, 0);
            this.newStartDatePicker.Name = "newStartDatePicker";
            this.newStartDatePicker.Size = new System.Drawing.Size(97, 23);
            this.newStartDatePicker.TabIndex = 8;
            this.newStartDatePicker.Value = new System.DateTime(2026, 6, 1, 0, 0, 0, 0);
            // 
            // StockChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1390, 647);
            this.Controls.Add(this.UpdateDate);
            this.Controls.Add(this.EndDateLabel);
            this.Controls.Add(this.StartDateLabel);
            this.Controls.Add(this.newEndDatePicker);
            this.Controls.Add(this.newStartDatePicker);
            this.Controls.Add(this.StockViewerChart);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "StockChart";
            this.Text = "Stock Viewer";
            ((System.ComponentModel.ISupportInitialize)(this.StockViewerChart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart StockViewerChart;
        private System.Windows.Forms.Button UpdateDate;
        private System.Windows.Forms.Label EndDateLabel;
        private System.Windows.Forms.Label StartDateLabel;
        private System.Windows.Forms.DateTimePicker newEndDatePicker;
        private System.Windows.Forms.DateTimePicker newStartDatePicker;
    }
}