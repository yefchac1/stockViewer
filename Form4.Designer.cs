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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
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
            chartArea3.AxisY.IsStartedFromZero = false;
            chartArea3.Name = "StockArea";
            chartArea3.Position.Auto = false;
            chartArea3.Position.Height = 65F;
            chartArea3.Position.Width = 94F;
            chartArea3.Position.X = 3F;
            chartArea3.Position.Y = 3F;
            chartArea4.AlignWithChartArea = "StockArea";
            chartArea4.Name = "VolumeArea";
            chartArea4.Position.Auto = false;
            chartArea4.Position.Height = 30F;
            chartArea4.Position.Width = 94F;
            chartArea4.Position.X = 3F;
            chartArea4.Position.Y = 70F;
            this.StockViewerChart.ChartAreas.Add(chartArea3);
            this.StockViewerChart.ChartAreas.Add(chartArea4);
            this.StockViewerChart.Location = new System.Drawing.Point(12, 12);
            this.StockViewerChart.Name = "StockViewerChart";
            series3.ChartArea = "StockArea";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick;
            series3.CustomProperties = "PriceDownColor=Red, PriceUpColor=0\\, 192\\, 0";
            series3.Name = "PriceSeries";
            series3.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            series3.YValuesPerPoint = 4;
            series4.ChartArea = "VolumeArea";
            series4.Name = "VolumeSeries";
            series4.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            this.StockViewerChart.Series.Add(series3);
            this.StockViewerChart.Series.Add(series4);
            this.StockViewerChart.Size = new System.Drawing.Size(1017, 551);
            this.StockViewerChart.TabIndex = 0;
            this.StockViewerChart.Text = "Stock Viewer";
            // 
            // UpdateDate
            // 
            this.UpdateDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold);
            this.UpdateDate.Location = new System.Drawing.Point(1088, 303);
            this.UpdateDate.Name = "UpdateDate";
            this.UpdateDate.Size = new System.Drawing.Size(147, 74);
            this.UpdateDate.TabIndex = 12;
            this.UpdateDate.Text = "Update Date";
            this.UpdateDate.UseVisualStyleBackColor = true;
            this.UpdateDate.Click += new System.EventHandler(this.UpdateDate_Click);
            // 
            // EndDateLabel
            // 
            this.EndDateLabel.AutoSize = true;
            this.EndDateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EndDateLabel.Location = new System.Drawing.Point(1047, 251);
            this.EndDateLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.EndDateLabel.Name = "EndDateLabel";
            this.EndDateLabel.Size = new System.Drawing.Size(90, 22);
            this.EndDateLabel.TabIndex = 11;
            this.EndDateLabel.Text = "End Date:";
            // 
            // StartDateLabel
            // 
            this.StartDateLabel.AutoSize = true;
            this.StartDateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartDateLabel.Location = new System.Drawing.Point(1040, 213);
            this.StartDateLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.StartDateLabel.Name = "StartDateLabel";
            this.StartDateLabel.Size = new System.Drawing.Size(96, 22);
            this.StartDateLabel.TabIndex = 10;
            this.StartDateLabel.Text = "Start Date:";
            // 
            // newEndDatePicker
            // 
            this.newEndDatePicker.CustomFormat = "MM/dd/yyyy";
            this.newEndDatePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newEndDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.newEndDatePicker.Location = new System.Drawing.Point(1156, 248);
            this.newEndDatePicker.Margin = new System.Windows.Forms.Padding(4);
            this.newEndDatePicker.MaxDate = new System.DateTime(2026, 12, 31, 0, 0, 0, 0);
            this.newEndDatePicker.MinDate = new System.DateTime(2020, 1, 1, 0, 0, 0, 0);
            this.newEndDatePicker.Name = "newEndDatePicker";
            this.newEndDatePicker.Size = new System.Drawing.Size(128, 27);
            this.newEndDatePicker.TabIndex = 9;
            this.newEndDatePicker.Value = new System.DateTime(2026, 9, 18, 0, 0, 0, 0);
            // 
            // newStartDatePicker
            // 
            this.newStartDatePicker.CustomFormat = "MM/dd/yyyy";
            this.newStartDatePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newStartDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.newStartDatePicker.Location = new System.Drawing.Point(1156, 210);
            this.newStartDatePicker.Margin = new System.Windows.Forms.Padding(4);
            this.newStartDatePicker.MaxDate = new System.DateTime(2026, 12, 31, 0, 0, 0, 0);
            this.newStartDatePicker.MinDate = new System.DateTime(2020, 1, 1, 0, 0, 0, 0);
            this.newStartDatePicker.Name = "newStartDatePicker";
            this.newStartDatePicker.Size = new System.Drawing.Size(128, 27);
            this.newStartDatePicker.TabIndex = 8;
            this.newStartDatePicker.Value = new System.DateTime(2026, 6, 1, 0, 0, 0, 0);
            // 
            // StockChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1297, 576);
            this.Controls.Add(this.UpdateDate);
            this.Controls.Add(this.EndDateLabel);
            this.Controls.Add(this.StartDateLabel);
            this.Controls.Add(this.newEndDatePicker);
            this.Controls.Add(this.newStartDatePicker);
            this.Controls.Add(this.StockViewerChart);
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