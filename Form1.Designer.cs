namespace yefersonProject
{
    partial class stockForm
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
            this.dataStockViewer = new System.Windows.Forms.DataGridView();
            this.openStockFile = new System.Windows.Forms.Button();
            this.startDatePicker = new System.Windows.Forms.DateTimePicker();
            this.endDatePicker = new System.Windows.Forms.DateTimePicker();
            this.startDateLabel = new System.Windows.Forms.Label();
            this.endDateLabel = new System.Windows.Forms.Label();
            this.stockFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.updateDate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataStockViewer)).BeginInit();
            this.SuspendLayout();
            // 
            // dataStockViewer
            // 
            this.dataStockViewer.AllowUserToOrderColumns = true;
            this.dataStockViewer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataStockViewer.Location = new System.Drawing.Point(17, 16);
            this.dataStockViewer.Margin = new System.Windows.Forms.Padding(4);
            this.dataStockViewer.Name = "dataStockViewer";
            this.dataStockViewer.RowHeadersWidth = 51;
            this.dataStockViewer.Size = new System.Drawing.Size(855, 311);
            this.dataStockViewer.TabIndex = 0;
            // 
            // openStockFile
            // 
            this.openStockFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openStockFile.Location = new System.Drawing.Point(324, 353);
            this.openStockFile.Margin = new System.Windows.Forms.Padding(4);
            this.openStockFile.Name = "openStockFile";
            this.openStockFile.Size = new System.Drawing.Size(217, 99);
            this.openStockFile.TabIndex = 0;
            this.openStockFile.Text = "Open File to Read";
            this.openStockFile.UseVisualStyleBackColor = true;
            this.openStockFile.Click += new System.EventHandler(this.openFileClick);
            // 
            // startDatePicker
            // 
            this.startDatePicker.CustomFormat = "MM/dd/yyyy";
            this.startDatePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.startDatePicker.Location = new System.Drawing.Point(135, 348);
            this.startDatePicker.Margin = new System.Windows.Forms.Padding(4);
            this.startDatePicker.MaxDate = new System.DateTime(2026, 12, 31, 0, 0, 0, 0);
            this.startDatePicker.MinDate = new System.DateTime(2020, 1, 1, 0, 0, 0, 0);
            this.startDatePicker.Name = "startDatePicker";
            this.startDatePicker.Size = new System.Drawing.Size(128, 27);
            this.startDatePicker.TabIndex = 1;
            this.startDatePicker.Value = new System.DateTime(2026, 6, 1, 0, 0, 0, 0);
            // 
            // endDatePicker
            // 
            this.endDatePicker.CustomFormat = "MM/dd/yyyy";
            this.endDatePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.endDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.endDatePicker.Location = new System.Drawing.Point(135, 386);
            this.endDatePicker.Margin = new System.Windows.Forms.Padding(4);
            this.endDatePicker.MaxDate = new System.DateTime(2026, 12, 31, 0, 0, 0, 0);
            this.endDatePicker.MinDate = new System.DateTime(2020, 1, 1, 0, 0, 0, 0);
            this.endDatePicker.Name = "endDatePicker";
            this.endDatePicker.Size = new System.Drawing.Size(128, 27);
            this.endDatePicker.TabIndex = 2;
            this.endDatePicker.Value = new System.DateTime(2026, 9, 18, 0, 0, 0, 0);
            // 
            // startDateLabel
            // 
            this.startDateLabel.AutoSize = true;
            this.startDateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startDateLabel.Location = new System.Drawing.Point(27, 353);
            this.startDateLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.startDateLabel.Name = "startDateLabel";
            this.startDateLabel.Size = new System.Drawing.Size(91, 20);
            this.startDateLabel.TabIndex = 4;
            this.startDateLabel.Text = "Start Date:";
            // 
            // endDateLabel
            // 
            this.endDateLabel.AutoSize = true;
            this.endDateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.endDateLabel.Location = new System.Drawing.Point(34, 391);
            this.endDateLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.endDateLabel.Name = "endDateLabel";
            this.endDateLabel.Size = new System.Drawing.Size(84, 20);
            this.endDateLabel.TabIndex = 5;
            this.endDateLabel.Text = "End Date:";
            // 
            // stockFileDialog
            // 
            this.stockFileDialog.DefaultExt = "csv";
            this.stockFileDialog.FileName = "NVDA_daily";
            this.stockFileDialog.Filter = "All Stock Files (*.csv)|*.csv|Daily (*.csv)|*_daily.csv|Weekly (*.csv)|*_weekly.c" +
    "sv|Monthly (*.csv)|*_monthly.csv|Yearly (*.csv)|*_yearly.csv";
            // 
            // updateDate
            // 
            this.updateDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updateDate.Location = new System.Drawing.Point(135, 420);
            this.updateDate.Name = "updateDate";
            this.updateDate.Size = new System.Drawing.Size(128, 38);
            this.updateDate.TabIndex = 6;
            this.updateDate.Text = "Update Date";
            this.updateDate.UseVisualStyleBackColor = true;
            this.updateDate.Click += new System.EventHandler(this.updateDate_Click);
            // 
            // stockForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(888, 478);
            this.Controls.Add(this.updateDate);
            this.Controls.Add(this.endDateLabel);
            this.Controls.Add(this.startDateLabel);
            this.Controls.Add(this.endDatePicker);
            this.Controls.Add(this.startDatePicker);
            this.Controls.Add(this.openStockFile);
            this.Controls.Add(this.dataStockViewer);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "stockForm";
            this.Text = "Stock Data Viewer";
            ((System.ComponentModel.ISupportInitialize)(this.dataStockViewer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataStockViewer;
        private System.Windows.Forms.Button openStockFile;
        private System.Windows.Forms.DateTimePicker startDatePicker;
        private System.Windows.Forms.DateTimePicker endDatePicker;
        private System.Windows.Forms.Label startDateLabel;
        private System.Windows.Forms.Label endDateLabel;
        private System.Windows.Forms.OpenFileDialog stockFileDialog;
        private System.Windows.Forms.Button updateDate;
    }
}

