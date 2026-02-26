    using Compiler.Controllers;
using Compiler.Models;

namespace Compiler
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            FileService model = new FileService();
            MainForm view = new MainForm();
            MainController controller = new MainController(view, model);

            Application.Run(view);
        }
    }
}