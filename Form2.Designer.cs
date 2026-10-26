namespace yefersonProject
{
    partial class MainForm
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
            this.OpenFilesButton = new System.Windows.Forms.Button();
            this.EndDateOpenerLabel = new System.Windows.Forms.Label();
            this.StartDateOpenerLabel = new System.Windows.Forms.Label();
            this.EndDateOpenerPicker = new System.Windows.Forms.DateTimePicker();
            this.StartDateOpenerPicker = new System.Windows.Forms.DateTimePicker();
            this.openMultipleFilesDialog = new System.Windows.Forms.OpenFileDialog();
            this.SelectFilesButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // OpenFilesButton
            // 
            this.OpenFilesButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OpenFilesButton.Location = new System.Drawing.Point(183, 137);
            this.OpenFilesButton.Name = "OpenFilesButton";
            this.OpenFilesButton.Size = new System.Drawing.Size(164, 82);
            this.OpenFilesButton.TabIndex = 3;
            this.OpenFilesButton.Text = "Open";
            this.OpenFilesButton.UseVisualStyleBackColor = true;
            this.OpenFilesButton.Click += new System.EventHandler(this.OpenFilesButtonClick);
            // 
            // EndDateOpenerLabel
            // 
            this.EndDateOpenerLabel.AutoSize = true;
            this.EndDateOpenerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EndDateOpenerLabel.Location = new System.Drawing.Point(77, 93);
            this.EndDateOpenerLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.EndDateOpenerLabel.Name = "EndDateOpenerLabel";
            this.EndDateOpenerLabel.Size = new System.Drawing.Size(90, 22);
            this.EndDateOpenerLabel.TabIndex = 7;
            this.EndDateOpenerLabel.Text = "End Date:";
            // 
            // StartDateOpenerLabel
            // 
            this.StartDateOpenerLabel.AutoSize = true;
            this.StartDateOpenerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartDateOpenerLabel.Location = new System.Drawing.Point(70, 55);
            this.StartDateOpenerLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.StartDateOpenerLabel.Name = "StartDateOpenerLabel";
            this.StartDateOpenerLabel.Size = new System.Drawing.Size(96, 22);
            this.StartDateOpenerLabel.TabIndex = 6;
            this.StartDateOpenerLabel.Text = "Start Date:";
            // 
            // EndDateOpenerPicker
            // 
            this.EndDateOpenerPicker.CustomFormat = "MM/dd/yyyy";
            this.EndDateOpenerPicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EndDateOpenerPicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.EndDateOpenerPicker.Location = new System.Drawing.Point(195, 90);
            this.EndDateOpenerPicker.Margin = new System.Windows.Forms.Padding(4);
            this.EndDateOpenerPicker.MaxDate = new System.DateTime(2026, 12, 31, 0, 0, 0, 0);
            this.EndDateOpenerPicker.MinDate = new System.DateTime(2020, 1, 1, 0, 0, 0, 0);
            this.EndDateOpenerPicker.Name = "EndDateOpenerPicker";
            this.EndDateOpenerPicker.Size = new System.Drawing.Size(128, 27);
            this.EndDateOpenerPicker.TabIndex = 2;
            this.EndDateOpenerPicker.Value = new System.DateTime(2026, 9, 18, 0, 0, 0, 0);
            // 
            // StartDateOpenerPicker
            // 
            this.StartDateOpenerPicker.CustomFormat = "MM/dd/yyyy";
            this.StartDateOpenerPicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartDateOpenerPicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.StartDateOpenerPicker.Location = new System.Drawing.Point(195, 52);
            this.StartDateOpenerPicker.Margin = new System.Windows.Forms.Padding(4);
            this.StartDateOpenerPicker.MaxDate = new System.DateTime(2026, 12, 31, 0, 0, 0, 0);
            this.StartDateOpenerPicker.MinDate = new System.DateTime(2020, 1, 1, 0, 0, 0, 0);
            this.StartDateOpenerPicker.Name = "StartDateOpenerPicker";
            this.StartDateOpenerPicker.Size = new System.Drawing.Size(128, 27);
            this.StartDateOpenerPicker.TabIndex = 1;
            this.StartDateOpenerPicker.Value = new System.DateTime(2026, 6, 1, 0, 0, 0, 0);
            // 
            // openMultipleFilesDialog
            // 
            this.openMultipleFilesDialog.DefaultExt = "csv";
            this.openMultipleFilesDialog.FileName = "NVDA_daily";
            this.openMultipleFilesDialog.Filter = "All Stock Files (*.csv)|*.csv|Daily (*.csv)|*_daily.csv|Weekly (*.csv)|*_weekly.c" +
    "sv|Monthly (*.csv)|*_monthly.csv|Yearly (*.csv)|*_yearly.csv";
            this.openMultipleFilesDialog.Multiselect = true;
            // 
            // SelectFilesButton
            // 
            this.SelectFilesButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectFilesButton.Location = new System.Drawing.Point(12, 137);
            this.SelectFilesButton.Name = "SelectFilesButton";
            this.SelectFilesButton.Size = new System.Drawing.Size(164, 82);
            this.SelectFilesButton.TabIndex = 0;
            this.SelectFilesButton.Text = "Select Files";
            this.SelectFilesButton.UseVisualStyleBackColor = true;
            this.SelectFilesButton.Click += new System.EventHandler(this.MultipleFilesDialogOpener);
            // 
            // FormOpener
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 250);
            this.Controls.Add(this.SelectFilesButton);
            this.Controls.Add(this.EndDateOpenerPicker);
            this.Controls.Add(this.StartDateOpenerPicker);
            this.Controls.Add(this.EndDateOpenerLabel);
            this.Controls.Add(this.StartDateOpenerLabel);
            this.Controls.Add(this.OpenFilesButton);
            this.Name = "FormOpener";
            this.Text = "Open Files to Read";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OpenFilesButton;
        private System.Windows.Forms.Label EndDateOpenerLabel;
        private System.Windows.Forms.Label StartDateOpenerLabel;
        private System.Windows.Forms.DateTimePicker EndDateOpenerPicker;
        private System.Windows.Forms.DateTimePicker StartDateOpenerPicker;
        private System.Windows.Forms.OpenFileDialog openMultipleFilesDialog;
        private System.Windows.Forms.Button SelectFilesButton;
    }
}