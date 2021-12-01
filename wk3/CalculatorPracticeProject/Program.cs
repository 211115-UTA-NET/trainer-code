// evaluates expressions in RPN (reverse Polish notation).
// e.g. '3 4 +' evaluates to '7'.
// pass multiple expressions separated by newlines to the standard input stream.
// e.g. 'printf "2 3 + \n 4.5 2 / 1 -" | CalculatorPracticeProject',
// or 'echo "2 3 +" | dotnet run',
// or 'dotnet run < input.txt > output.txt',
// or use interactively and quit with EOF (Ctrl+Z on Windows, Ctrl+D on Linux).
// numerical values are manipulated in double-precision floating point format.
// supports +, -, *, and /.

Dictionary<string, Func<double, double, double>> binaryOperators = new()
{
    ["+"] = (x, y) => x + y,
    ["-"] = (x, y) => x - y,
    ["*"] = (x, y) => x * y,
    ["/"] = (x, y) => x / y,
};

Stack<double> memory = new();

// each line of input until EOF
string? line;
while ((line = Console.ReadLine()) != null)
{
    // each token, e.g. "-1.2" or "+"
    foreach (string token in line.Split())
    {
        if (binaryOperators.TryGetValue(token, out var func))
        {
            if (memory.Count < 2)
            {
                Console.Error.WriteLine($"Not enough operands for {token}.");
                return 1;
            }
            // "3 2 -" is "1", not "-1", but the stack is LIFO,
            // so the second value we pop must be the first operand
            var (first, second) = (memory.Pop(), memory.Pop());
            memory.Push(func(second, first));
        }
        else if (double.TryParse(token, out double value))
        {
            memory.Push(value);
        }
        else
        {
            Console.Error.WriteLine($"Invalid token {token} in expression.");
            return 1;
        }
    }

    if (memory.Count != 1)
    {
        Console.Error.WriteLine($"Malformed expression \"${line}\".");
        return 1;
    }

    // the result of the evaluation
    Console.WriteLine(memory.Pop());
    // the stack is empty, ready for the next expression
}

return 0;
