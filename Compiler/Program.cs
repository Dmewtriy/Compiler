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
            InfoService infoModel = new InfoService();
            Scanner scanner = new Scanner();
            MainForm view = new MainForm();
            MainController controller = new MainController(view, model, infoModel, scanner);

            Application.Run(view);
        }
    }
}