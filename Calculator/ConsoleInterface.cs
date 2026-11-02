using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace Calculator;

/// Консольный интерфейс калькулятора, обрабатывающий ввод пользователя и вызывающий методы MathOperations

public static class ConsoleInterface
{
    private static readonly CultureInfo Culture = CultureInfo.InvariantCulture;

    public static void Run()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("Консольный калькулятор");
        PrintHelp();

        while (true)
        {
            Console.Write("\n> ");
            string? input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input)) continue;

            string[] parts = input.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string command = parts[0].ToLowerInvariant();

            try
            {
                switch (command)
                {
                    case "exit":
                    case "quit":
                    case "q":
                        Console.WriteLine("Завершение работы.");
                        return;

                    case "help":
                    case "h":
                    case "?":
                        PrintHelp();
                        break;

                    case "min":
                        RequireArgs(parts, 1);
                        PrintResult("Минимум",
                            MathOperations.Min(ParseAll(parts, 1)));
                        break;

                    case "max":
                        RequireArgs(parts, 1);
                        PrintResult("Максимум",
                            MathOperations.Max(ParseAll(parts, 1)));
                        break;

                    case "add":
                    case "+":
                        RequireArgs(parts, 2);
                        PrintResult("Сложение",
                            MathOperations.Add(Parse(parts[1]), Parse(parts[2])));
                        break;

                    case "sub":
                    case "-":
                        RequireArgs(parts, 2);
                        PrintResult("Вычитание",
                            MathOperations.Subtract(Parse(parts[1]), Parse(parts[2])));
                        break;

                    case "mul":
                    case "*":
                        RequireArgs(parts, 2);
                        PrintResult("Умножение",
                            MathOperations.Multiply(Parse(parts[1]), Parse(parts[2])));
                        break;

                    case "div":
                    case "/":
                        RequireArgs(parts, 2);
                        PrintResult("Деление",
                            MathOperations.Divide(Parse(parts[1]), Parse(parts[2])));
                        break;

                    case "mod":
                    case "%":
                        RequireArgs(parts, 2);
                        PrintResult("Остаток от деления",
                            MathOperations.Modulo(Parse(parts[1]), Parse(parts[2])));
                        break;

                    case "topercent":
                    case "toperc":
                        RequireArgs(parts, 2);
                        PrintResult("В проценты",
                            MathOperations.ToPercent(Parse(parts[1]), Parse(parts[2])) + " %");
                        break;

                    case "frompercent":
                    case "fromperc":
                        RequireArgs(parts, 2);
                        PrintResult("Из процентов",
                            MathOperations.FromPercent(Parse(parts[1]), Parse(parts[2])));
                        break;

                    case "sqr":
                    case "square":
                    case "^2":
                        RequireArgs(parts, 1);
                        PrintResult("В квадрат",
                            MathOperations.Square(Parse(parts[1])));
                        break;

                    case "cube":
                    case "^3":
                        RequireArgs(parts, 1);
                        PrintResult("В куб",
                            MathOperations.Cube(Parse(parts[1])));
                        break;

                    case "sqrt":
                        RequireArgs(parts, 1);
                        PrintResult("Квадратный корень",
                            MathOperations.Sqrt(Parse(parts[1])));
                        break;

                    case "cbrt":
                        RequireArgs(parts, 1);
                        PrintResult("Кубический корень",
                            MathOperations.Cbrt(Parse(parts[1])));
                        break;

                    case "clear":
                    case "cls":
                        Console.Clear();
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Неизвестная команда: '{command}'. Введите 'help' для списка команд.");
                        Console.ResetColor();
                        break;
                }
            }
            catch (FormatException)
            {
                PrintError("Ошибка формата числа. Проверьте введённые значения.");
            }
            catch (ArgumentException ex)
            {
                PrintError(ex.Message);
            }
            catch (DivideByZeroException ex)
            {
                PrintError(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                PrintError(ex.Message);
            }
        }
    }

    private static double Parse(string s)
    {
        if (!double.TryParse(s, NumberStyles.Float | NumberStyles.AllowThousands, Culture, out double value))
            throw new FormatException();
        return value;
    }

    private static double[] ParseAll(string[] parts, int startIndex)
    {
        double[] result = new double[parts.Length - startIndex];
        for (int i = startIndex; i < parts.Length; i++)
        {
            if (!double.TryParse(parts[i], NumberStyles.Float | NumberStyles.AllowThousands, Culture, out double value))
                throw new FormatException($"Не удалось распознать число: '{parts[i]}'");
            result[i - startIndex] = value;
        }
        return result;
    }

    private static void RequireArgs(string[] parts, int count)
    {
        if (parts.Length - 1 < count)
            throw new InvalidOperationException(
                $"Команда '{parts[0]}' требует {count} аргумент(а/ов), а передано {parts.Length - 1}.");
    }

    private static void PrintResult(string operation, object value)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write($"{operation}: ");
        Console.ResetColor();
        Console.WriteLine(value);
    }

    private static void PrintError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"[Ошибка] {message}");
        Console.ResetColor();
    }

    private static void PrintHelp()
    {
        Console.WriteLine("\nДоступные команды:");
        Console.WriteLine("  add  a b      или  + a b     — сложение");
        Console.WriteLine("  sub  a b      или  - a b     — вычитание");
        Console.WriteLine("  mul  a b      или  * a b     — умножение");
        Console.WriteLine("  div  a b      или  / a b     — деление");
        Console.WriteLine("  mod  a b      или  % a b     — остаток от деления");
        Console.WriteLine("  min  a b c ...               — минимум из любого количества чисел");
        Console.WriteLine("  max  a b c ...               — максимум из любого количества чисел");
        Console.WriteLine("  topercent  v t               — сколько % составляет v от t");
        Console.WriteLine("  frompercent p t              — вычислить p% от t");
        Console.WriteLine("  sqr  a        или  ^2 a      — возвести в квадрат");
        Console.WriteLine("  cube a        или  ^3 a      — возвести в куб");
        Console.WriteLine("  sqrt a                       — квадратный корень");
        Console.WriteLine("  cbrt a                       — кубический корень");
        Console.WriteLine("  clear / cls                  — очистить экран");
        Console.WriteLine("  help / h / ?                 — показать эту справку");
        Console.WriteLine("  exit / q                     — выйти из программы");
    }
}