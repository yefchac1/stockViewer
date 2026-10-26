using System;
using System.Windows.Forms;

namespace yefersonProject
{
    /// <summary>
    /// Copilot generated comments throughout the solution, while ChatGPT was used for debugging and implementation
    /// of a dynamic initial directory for the open file dialog, as well as the implementation of a date range filter
    /// for the candlestick data.
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
            Application.Run(new MainForm());
        }
    }
}
