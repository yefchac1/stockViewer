namespace yefersonProject
{
    partial class StockForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.DataStockViewer = new System.Windows.Forms.DataGridView();
            this.OpenStockFile = new System.Windows.Forms.Button();
            this.StartDatePicker = new System.Windows.Forms.DateTimePicker();
            this.EndDatePicker = new System.Windows.Forms.DateTimePicker();
            this.StartDateLabel = new System.Windows.Forms.Label();
            this.EndDateLabel = new System.Windows.Forms.Label();
            this.StockFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.UpdateDate = new System.Windows.Forms.Button();
            this.StockDataChartViewer = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.stockFormBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.DataStockViewer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StockDataChartViewer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stockFormBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // DataStockViewer
            // 
            this.DataStockViewer.AllowUserToOrderColumns = true;
            this.DataStockViewer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataStockViewer.Location = new System.Drawing.Point(326, 465);
            this.DataStockViewer.Margin = new System.Windows.Forms.Padding(4);
            this.DataStockViewer.Name = "DataStockViewer";
            this.DataStockViewer.RowHeadersWidth = 51;
            this.DataStockViewer.Size = new System.Drawing.Size(896, 191);
            this.DataStockViewer.TabIndex = 0;
            // 
            // OpenStockFile
            // 
            this.OpenStockFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OpenStockFile.Location = new System.Drawing.Point(13, 571);
            this.OpenStockFile.Margin = new System.Windows.Forms.Padding(4);
            this.OpenStockFile.Name = "OpenStockFile";
            this.OpenStockFile.Size = new System.Drawing.Size(147, 74);
            this.OpenStockFile.TabIndex = 0;
            this.OpenStockFile.Text = "Open File to Read";
            this.OpenStockFile.UseVisualStyleBackColor = true;
            this.OpenStockFile.Click += new System.EventHandler(this.OpenFileClick);
            // 
            // StartDatePicker
            // 
            this.StartDatePicker.CustomFormat = "MM/dd/yyyy";
            this.StartDatePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.StartDatePicker.Location = new System.Drawing.Point(167, 480);
            this.StartDatePicker.Margin = new System.Windows.Forms.Padding(4);
            this.StartDatePicker.MaxDate = new System.DateTime(2026, 12, 31, 0, 0, 0, 0);
            this.StartDatePicker.MinDate = new System.DateTime(2020, 1, 1, 0, 0, 0, 0);
            this.StartDatePicker.Name = "StartDatePicker";
            this.StartDatePicker.Size = new System.Drawing.Size(128, 27);
            this.StartDatePicker.TabIndex = 1;
            this.StartDatePicker.Value = new System.DateTime(2026, 6, 1, 0, 0, 0, 0);
            // 
            // EndDatePicker
            // 
            this.EndDatePicker.CustomFormat = "MM/dd/yyyy";
            this.EndDatePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EndDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.EndDatePicker.Location = new System.Drawing.Point(167, 518);
            this.EndDatePicker.Margin = new System.Windows.Forms.Padding(4);
            this.EndDatePicker.MaxDate = new System.DateTime(2026, 12, 31, 0, 0, 0, 0);
            this.EndDatePicker.MinDate = new System.DateTime(2020, 1, 1, 0, 0, 0, 0);
            this.EndDatePicker.Name = "EndDatePicker";
            this.EndDatePicker.Size = new System.Drawing.Size(128, 27);
            this.EndDatePicker.TabIndex = 2;
            this.EndDatePicker.Value = new System.DateTime(2026, 9, 18, 0, 0, 0, 0);
            // 
            // StartDateLabel
            // 
            this.StartDateLabel.AutoSize = true;
            this.StartDateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartDateLabel.Location = new System.Drawing.Point(51, 483);
            this.StartDateLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.StartDateLabel.Name = "StartDateLabel";
            this.StartDateLabel.Size = new System.Drawing.Size(96, 22);
            this.StartDateLabel.TabIndex = 4;
            this.StartDateLabel.Text = "Start Date:";
            // 
            // EndDateLabel
            // 
            this.EndDateLabel.AutoSize = true;
            this.EndDateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EndDateLabel.Location = new System.Drawing.Point(58, 521);
            this.EndDateLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.EndDateLabel.Name = "EndDateLabel";
            this.EndDateLabel.Size = new System.Drawing.Size(90, 22);
            this.EndDateLabel.TabIndex = 5;
            this.EndDateLabel.Text = "End Date:";
            // 
            // StockFileDialog
            // 
            this.StockFileDialog.DefaultExt = "csv";
            this.StockFileDialog.FileName = "NVDA_daily";
            this.StockFileDialog.Filter = "All Stock Files (*.csv)|*.csv|Daily (*.csv)|*_daily.csv|Weekly (*.csv)|*_weekly.c" +
    "sv|Monthly (*.csv)|*_monthly.csv|Yearly (*.csv)|*_yearly.csv";
            // 
            // UpdateDate
            // 
            this.UpdateDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold);
            this.UpdateDate.Location = new System.Drawing.Point(167, 571);
            this.UpdateDate.Name = "UpdateDate";
            this.UpdateDate.Size = new System.Drawing.Size(147, 74);
            this.UpdateDate.TabIndex = 6;
            this.UpdateDate.Text = "Update Date";
            this.UpdateDate.UseVisualStyleBackColor = true;
            this.UpdateDate.Click += new System.EventHandler(this.UpdateDate_Click);
            // 
            // StockDataChartViewer
            // 
            chartArea1.AxisY.IsStartedFromZero = false;
            chartArea1.AxisY2.IsStartedFromZero = false;
            chartArea1.Name = "PriceArea";
            chartArea1.Position.Auto = false;
            chartArea1.Position.Height = 64F;
            chartArea1.Position.Width = 94F;
            chartArea1.Position.X = 3F;
            chartArea1.Position.Y = 3F;
            chartArea2.AlignWithChartArea = "PriceArea";
            chartArea2.Name = "VolumeArea";
            chartArea2.Position.Auto = false;
            chartArea2.Position.Height = 30F;
            chartArea2.Position.Width = 94F;
            chartArea2.Position.X = 3F;
            chartArea2.Position.Y = 68F;
            this.StockDataChartViewer.ChartAreas.Add(chartArea1);
            this.StockDataChartViewer.ChartAreas.Add(chartArea2);
            this.StockDataChartViewer.DataSource = this.stockFormBindingSource;
            this.StockDataChartViewer.Location = new System.Drawing.Point(13, 12);
            this.StockDataChartViewer.Name = "StockDataChartViewer";
            series1.ChartArea = "PriceArea";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick;
            series1.CustomProperties = "PriceDownColor=Red, PriceUpColor=0\\, 192\\, 0";
            series1.Name = "PriceSeries";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            series1.YValuesPerPoint = 4;
            series2.ChartArea = "VolumeArea";
            series2.CustomProperties = "PointWidth=0.8";
            series2.Name = "VolumeSeries";
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            this.StockDataChartViewer.Series.Add(series1);
            this.StockDataChartViewer.Series.Add(series2);
            this.StockDataChartViewer.Size = new System.Drawing.Size(1209, 446);
            this.StockDataChartViewer.TabIndex = 7;
            this.StockDataChartViewer.Text = "Stock Chart Viewer";
            title1.DockedToChartArea = "PriceArea";
            title1.Name = "Price:";
            title2.DockedToChartArea = "VolumeArea";
            title2.Name = "Volume:";
            this.StockDataChartViewer.Titles.Add(title1);
            this.StockDataChartViewer.Titles.Add(title2);
            // 
            // StockForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1238, 669);
            this.Controls.Add(this.StockDataChartViewer);
            this.Controls.Add(this.UpdateDate);
            this.Controls.Add(this.EndDateLabel);
            this.Controls.Add(this.StartDateLabel);
            this.Controls.Add(this.EndDatePicker);
            this.Controls.Add(this.StartDatePicker);
            this.Controls.Add(this.OpenStockFile);
            this.Controls.Add(this.DataStockViewer);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "StockForm";
            this.Text = "Stock Data Viewer";
            ((System.ComponentModel.ISupportInitialize)(this.DataStockViewer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StockDataChartViewer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stockFormBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DataStockViewer;
        private System.Windows.Forms.Button OpenStockFile;
        private System.Windows.Forms.DateTimePicker StartDatePicker;
        private System.Windows.Forms.DateTimePicker EndDatePicker;
        private System.Windows.Forms.Label StartDateLabel;
        private System.Windows.Forms.Label EndDateLabel;
        private System.Windows.Forms.OpenFileDialog StockFileDialog;
        private System.Windows.Forms.Button UpdateDate;
        private System.Windows.Forms.DataVisualization.Charting.Chart StockDataChartViewer;
        private System.Windows.Forms.BindingSource stockFormBindingSource;
    }
}

