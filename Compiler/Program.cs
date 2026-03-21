using Compiler.Controllers;
using Compiler.Models;
using ParserANTLR;

namespace Compiler
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var model = new FileService();
            var infoModel = new InfoService();
            var scanner = new Scanner();
            var view = new MainForm();
            _ = new MainController(view, model, infoModel, scanner);

            Application.Run(view);
        }
    }
}