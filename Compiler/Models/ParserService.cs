using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Compiler.Models
{
    public class ParserService
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void ErrorCallbackDelegate(int line, [MarshalAs(UnmanagedType.LPUTF8Str)] string message);

        [DllImport("parser.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern int ParseSourceCode(
            [MarshalAs(UnmanagedType.LPUTF8Str)] string sourceCode,
            ErrorCallbackDelegate errorCb);

        public static (bool IsSuccess, string Message) Parse(string sourceCode)
        {
            var errorBuilder = new StringBuilder();

            void Callback(int line, string msg)
            {
                errorBuilder.AppendLine($"Строка {line}: {msg}");
            }

            var result = ParseSourceCode(sourceCode, Callback);
            var errors = errorBuilder.ToString().Trim();

            if (result == 0 && string.IsNullOrEmpty(errors))
            {
                return (true, "Синтаксический анализ пройден успешно!");
            }
            else
            {
                return (false, errors);
            }
        }
    }
}