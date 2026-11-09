using System;
using System.Linq;
using System.Windows.Forms;

namespace yefersonProject
{
    /// <summary>
    /// This class represents the main entry point of the application and contains the Main method, which is responsible for initializing and running the application. 
    /// It also defines the MainForm class, which serves as the primary user interface for selecting and opening CSV files containing stock data.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// A string array to hold the paths of the selected CSV files. 
        /// This array is initialized as empty and will be populated when the user selects files through the OpenFileDialog.
        /// </summary>
        private string[] selectedFiles = new string[0];

        /// <summary>
        /// Initializes a new instance of the MainForm class and sets up the form's components. This constructor is called when the form is created, \
        /// and it initializes the user interface elements defined in the form's designer file.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Compared to our previous FileDialogOpener, MultipleFilesDialogOpener represents a more advanced 
        /// implementation that allows users to select multiple CSV files at once.
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

                // Set focus to the OpenFilesButton after files are selected to allow TabIndex navigation to work correctly.
                OpenFilesButton.Focus(); 
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
            // Error handling for the case where the end date is earlier than the start date.
            if (endDateOpener < startDateOpener)
            {
                MessageBox.Show("End date cannot be earlier than start date.");
                return;
            }

            // Now we will open each instance of new form using each CSV selected from selectedFiles array and pass the start and end dates to each instance of StockForm
            foreach (var file in selectedFiles)
            {
                // Create a new instance of StockForm for each selected file, passing the file path and date range to the constructor
                FormChart dataViewerForm = new FormChart(file, startDateOpener, endDateOpener);

                // Create a new instance of StockChart for each selected file, passing the file path, date range, and the data viewer form to the constructor
                StockChart stockViewerForm = new StockChart(file, startDateOpener, endDateOpener, dataViewerForm);

                // Show both forms to the user
                dataViewerForm.Show();
                stockViewerForm.Show();
            }
            // Now we will ONLY close FormOpener after all the forms have been opened while StockForm and DataViewerForm
            // remain open for the user to interact with. This is done to ensure that the user can continue to work with
            // the opened forms without losing access to them when the main form is closed.

            this.Hide();

            // This code snippet ensures that when the MainForm is closed, it checks if there are any other open forms in the application.
            // If all other forms are closed, it will also close the MainForm. This is important for managing the application's lifecycle and
            // ensuring that the application exits cleanly when all forms are closed.
            Application.OpenForms.Cast<Form>()
                .Where(f => f != this)
                .ToList()
                .ForEach(f => f.FormClosed += (formSender, args) =>
                {
                    if (Application.OpenForms.Cast<Form>().All(form => form == this))
                    {
                        this.Close();
                    }
                });

        }
    }

}