using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace yefersonProject
{
    public partial class FormOpener : Form
    {
        public FormOpener()
        {
            InitializeComponent();
        }

        private void OpenMultipleFilesDialog(object sender, EventArgs e)
        {
            // Set the initial directory to the "Stock Data" folder relative to the application startup path
            openMultipleFilesDialog.InitialDirectory = System.IO.Path.GetFullPath(
            System.IO.Path.Combine(Application.StartupPath, @"..\..\..\Stock Data")
            );

            // Show the file dialog and check if the user selected a file
            if (openMultipleFilesDialog.ShowDialog() == DialogResult.OK)
            {
                // Lets put a start date and end date based on the startDateOpener and end

                DateTime startDateOpener = StartDateOpenerPicker.Value.Date;
                DateTime endDateOpener = EndDateOpenerPicker.Value.Date;
                if (endDateOpener < startDateOpener)
                {
                    MessageBox.Show("End date cannot be earlier than start date.");
                    return;
                }

                // Now we will open each instance of new form using each CSV selected
                foreach (var file in openMultipleFilesDialog.FileNames)
                {
                    
                    // We will use show dialog for this
                    StockForm stockForm = new StockForm(file, startDateOpener, endDateOpener);
                    stockForm.Show();

                }
                // Now we will ONLY close FormOpener after all the forms have been opened while StockForm is still open
                this.Hide();






            }
        }

    }

}
