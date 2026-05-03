using System;
using System.Collections.Generic;
using System.Linq;

public class Poliz
{
    private readonly Dictionary<TokenType, int> _precedence = new Dictionary<TokenType, int>
    {
        { TokenType.Plus, 1 }, { TokenType.Minus, 1 },
        { TokenType.Mul, 2 }, { TokenType.Div, 2 }, { TokenType.Mod, 2 }
    };

    public string GenerateAndCalculate(List<Token> tokens)
    {
        if (tokens.Any(t => t.Type == TokenType.Id))
        {
            return "ПОЛИЗ не построен: присутствуют идентификаторы.";
        }

        var output = new List<string>();
        var stack = new Stack<Token>();

        foreach (var token in tokens)
        {
            if (token.Type == TokenType.EOF) break;

            if (token.Type == TokenType.Number)
            {
                output.Add(token.Value);
            }
            else if (token.Type == TokenType.LParen)
            {
                stack.Push(token);
            }
            else if (token.Type == TokenType.RParen)
            {
                while (stack.Count > 0 && stack.Peek().Type != TokenType.LParen)
                {
                    output.Add(stack.Pop().Value);
                }
                if (stack.Count > 0) stack.Pop();
            }
            else if (_precedence.ContainsKey(token.Type))
            {
                while (stack.Count > 0 && stack.Peek().Type != TokenType.LParen &&
                       _precedence[stack.Peek().Type] >= _precedence[token.Type])
                {
                    output.Add(stack.Pop().Value);
                }
                stack.Push(token);
            }
        }

        while (stack.Count > 0)
        {
            output.Add(stack.Pop().Value);
        }

        string polizString = string.Join(" ", output);

        var calcStack = new Stack<int>();
        try
        {
            foreach (var item in output)
            {
                if (int.TryParse(item, out int num))
                {
                    calcStack.Push(num);
                }
                else
                {
                    int b = calcStack.Pop();
                    int a = calcStack.Pop();
                    int res = 0;
                    switch (item)
                    {
                        case "+": res = a + b; break;
                        case "-": res = a - b; break;
                        case "*": res = a * b; break;
                        case "/": res = a / b; break;
                        case "%": res = a % b; break;
                    }
                    calcStack.Push(res);
                }
            }
            return $"ПОЛИЗ: {polizString}\r\nРезультат вычисления: {calcStack.Pop()}";
        }
        catch (Exception)
        {
            return $"ПОЛИЗ: {polizString}\r\nОшибка при вычислении (возможно, деление на ноль или неверный формат).";
        }
    }
}