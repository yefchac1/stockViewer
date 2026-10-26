using System;
using System.Windows.Forms;

namespace yefersonProject
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// A string array to hold the paths of the selected CSV files. This array is populated when the user selects files using the OpenFileDialog. 
        /// It is used to open multiple instances of StockForm, each displaying data from a different CSV file.
        /// </summary>
        private string[] selectedFiles = new string[0];

        /// <summary>
        /// Initializes a new instance of the FormOpener class and sets up the form's components. 
        /// This form allows users to select multiple CSV files and specify a date range for filtering stock data.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the click event of the "Open Multiple Files" button. Opens a file dialog to select multiple CSV files containing stock data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MultipleFilesDialogOpener(object sender, EventArgs e)
        {
            // Set the initial directory to the "Stock Data" folder relative to the application startup path
            openMultipleFilesDialog.InitialDirectory = System.IO.Path.GetFullPath(
            System.IO.Path.Combine(Application.StartupPath, @"..\..\..\Stock Data")
            );

            // Show the file dialog and check if the user selected a file. If this is correct, we will append the selected files to the selectedFiles array
            if (openMultipleFilesDialog.ShowDialog() == DialogResult.OK)
            {
                selectedFiles = openMultipleFilesDialog.FileNames;

            }
        }

        /// <summary>
        /// Handles the click event of the "Open Files" button. This method checks if any files were selected, validates the date range, 
        /// and opens a new instance of StockForm for each selected CSV file, passing the specified date range to each instance. 
        /// If no files are selected or if the date range is invalid, appropriate messages are displayed to the user.
        /// </summary>
        /// <param name="sender"></param> 
        /// <param name="e"></param>
        private void OpenFilesButtonClick(object sender, EventArgs e)
        {
            // Check if any files were selected, if there are no strings in selected files array, then we will show a message box to the user to select at least one CSV file
            if (selectedFiles.Length == 0)
            {
                MessageBox.Show("Please select at least one CSV file.");
                return;
            }

            // Get the start and end dates from the date pickers
            DateTime startDateOpener = StartDateOpenerPicker.Value.Date;
            DateTime endDateOpener = EndDateOpenerPicker.Value.Date;
            if (endDateOpener < startDateOpener)
            {
                MessageBox.Show("End date cannot be earlier than start date.");
                return;
            }

            // Now we will open each instance of new form using each CSV selected from selectedFiles array and pass the start and end dates to each instance of StockForm
            foreach (var file in selectedFiles)
            {

                // We will use show dialog for this
                FormChart dataViewerForm = new FormChart(file, startDateOpener, endDateOpener);
                StockChart stockViewerForm = new StockChart(file, startDateOpener, endDateOpener);
                dataViewerForm.Show();
                stockViewerForm.Show();


            }
            // Now we will ONLY close FormOpener after all the forms have been opened while StockForm is still open

            this.Hide();

        }
    }

}
