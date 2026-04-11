namespace Compiler.Models
{
    public class FileService
    {
        public string? CurrentFilePath { get; private set; }

        public string OpenFile(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Файл не найден.");

            CurrentFilePath = filePath;
            return File.ReadAllText(filePath);
        }

        public void SaveFile(string filePath, string content)
        {
            File.WriteAllText(filePath, content);
            CurrentFilePath = filePath;
        }

        public void ClearCurrentFile()
        {
            CurrentFilePath = null;
        }
    }
}
