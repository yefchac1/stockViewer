using System;
using System.Windows.Forms;

namespace yefersonProject
{
    /// <summary>
    /// Copilot generated several comments throughout the solution, while ChatGPT was used for debugging and implementation
    /// of a dynamic initial directory for the open file dialog in Project 1, as well as the implementation of a date range filter
    /// for the candlestick data. In Project 2, ChatGPT was also used to implement the functionality of closing the application when the user clicks 
    /// the "X" button on both the StockForm and MainForm, ensuring that all resources are properly released and the application exits gracefully.
    /// </summary>

    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Run the MainForm as our new starting point of the application, which allows users to select and open CSV files containing stock data.
            Application.Run(new MainForm());
        }
    }
}
