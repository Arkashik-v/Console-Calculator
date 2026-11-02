using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator;

/// Методы для выполнения математических операций, статические, чтобы их можно было вызывать без создания экземпляра

public static class MathOperations
{

    public static double Add(double a, double b) => a + b;

    public static double Subtract(double a, double b) => a - b;

    public static double Multiply(double a, double b) => a * b;

    public static double Divide(double a, double b)
    {
        if (Math.Abs(b) < double.Epsilon)
            throw new DivideByZeroException("Деление на ноль недопустимо.");
        return a / b;
    }

    public static double Modulo(double a, double b)
    {
        if (Math.Abs(b) < double.Epsilon)
            throw new DivideByZeroException("Делитель не может быть равен нулю.");
        return a % b;
    }

    public static double Min(params double[] numbers)
    {
        if (numbers == null || numbers.Length == 0)
            throw new ArgumentException("Необходимо передать хотя бы одно число.");

        double min = numbers[0];
        for (int i = 1; i < numbers.Length; i++)
        {
            if (numbers[i] < min)
                min = numbers[i];
        }
        return min;
    }

    public static double Max(params double[] numbers)
    {
        if (numbers == null || numbers.Length == 0)
            throw new ArgumentException("Необходимо передать хотя бы одно число.");

        double max = numbers[0];
        for (int i = 1; i < numbers.Length; i++)
        {
            if (numbers[i] > max)
                max = numbers[i];
        }
        return max;
    }

    public static double ToPercent(double value, double total)
    {
        if (Math.Abs(total) < double.Epsilon)
            throw new DivideByZeroException("Общее значение не может быть равно нулю.");
        return (value / total) * 100.0;
    }

    public static double FromPercent(double percent, double total)
    {
        return (percent / 100.0) * total;
    }

    public static double Square(double x) => x * x;

    public static double Cube(double x) => x * x * x;

    public static double Sqrt(double x)
    {
        if (x < 0)
            throw new ArgumentException("Нельзя извлечь квадратный корень из отрицательного числа.");
        return Math.Sqrt(x);
    }

    public static double Cbrt(double x) => Math.Cbrt(x);
}