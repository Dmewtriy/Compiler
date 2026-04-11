using System.Reflection;
using System.Text;

namespace Compiler.Models
{
    public class InfoService
    {
        private string LoadResource(string fileName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            string resourcePath = $"Compiler.resources.{fileName}";

            using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
            {
                if (stream == null) return $"<h1>Ошибка: Ресурс {fileName} не найден</h1>";

                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public string GetGrammarContent() => LoadResource("Grammar.html");
        public string GetTaskDescriptionContent() => LoadResource("Task.html");
        public string GetGrammarClassificationContent() => LoadResource("Classification.html");
        public string GetAnalysisMethodContent() => LoadResource("Method.html");
        public string GetTestExampleContent() => LoadResource("Tests.html");
        public string GetReferencesContent() => LoadResource("References.html");
        public string GetSourceCodeContent() => "https://github.com/Dmewtriy/Compiler";

        public string GetAboutText() => LoadResource("About.html");
        public string GetHelpText() => LoadResource("Help.html");
    }
}
