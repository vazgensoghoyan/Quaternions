using System;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using static System.Math;

namespace Program
{
    class Program
    {
        static double Integrate1(Func<double, double> func, double a, double b, int pc)
        {
            double part = (b - a) / pc;
            double sum = 0;

            for (var i = 0; i < pc; i++)
            {
                sum += func.Invoke(a + (2 * i + 1) * part / 2);
            }

            return sum * part;
        }

        static double Integrate2(Func<double, double> func, double a, double b, int pc)
        {
            double part = (b - a) / pc;
            double sum = (func.Invoke(a) + func.Invoke(b)) / 2;

            for (var i = a + part; i + part < b; i += part)
            {
                sum += func.Invoke(i);
            }

            return sum * part;
        }

        static double Integrate3(Func<double, double> func, double a, double b, int pc)
        {
            double part = (b - a) / pc;
            double sum = func.Invoke(a) + func.Invoke(b);

            for (var i = a + part; i + part < b; i += part)
            {
                sum += 2 * func.Invoke(i);
            }

            for (var i = a; i + part <= b; i += part)
            {
                sum += 4 * func.Invoke(i + part / 2);
            }

            return sum * part / 6;
        }

        static void Try(Func<double, double> func, double a, double b, int pc)
        {
            Console.WriteLine(Integrate1(func, a, b, pc));
            Console.WriteLine(Integrate2(func, a, b, pc));
            Console.WriteLine(Integrate3(func, a, b, pc));

            Console.WriteLine();
        }

        static double Factorial(double x) 
        {
            double result = 1;

            while (x >= 1)
            {
                result *= x;
                x--;
            }
            while (x < 0)
            {
                x++;
                result /= x;
            }

            result *= Integrate1((double z) => Math.Pow(-1 * Math.Log(z), x), 0, 1, 100000000);

            return result;
        }

        static void Main()
        {
            //double F1(double x) => Math.Sin(x) / x;
            //double F2(double x) => 2 * x + 1 / Math.Sqrt(x + 1f / 16);
            //double F3(double x) => Math.Pow(x, 4) - 918;

            //Try(F1, 1, 14, 1000000);
            //Try(F2, 0, 1.5, 1000000);
            //Try(F3, 0, 1.5, 1000000);

            Console.WriteLine(Factorial(15.1243));
        }
    }
}