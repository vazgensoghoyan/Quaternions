using MathProject;
using System.Numerics;
using static System.Math;

namespace Program
{
    class Program
    {
        static string F(double a1, double b1, double c1, double d1, double a2, double b2, double c2, double d2)
        {
            double x1 = a1 * a2 - b1 * b2 - c1 * c2 - d1 * d2;
            double x2 = a1 * b2 + a2 * b1 + c1 * d2 - c2 * d1;
            double x3 = a1 * c2 + a2 * c1 + b2 * d1 - b1 * d2;
            double x4 = a1 * d2 + a2 * d1 + b1 * c2 - b2 * c1;

            return string.Format("{0}+{1}*i+{2}*j+{3}*k", x1, x2, x3, x4);
        }

        static string Inverse(double a, double b, double c, double d)
        {
            double Modulus = a * a + b * b + c * c + d * d;

            double x1 = a / Modulus;
            double x2 = -b / Modulus;
            double x3 = -c / Modulus;
            double x4 = -d / Modulus;

            return string.Format("{0}+{1}*i+{2}*j+{3}*k", x1, x2, x3, x4);
        }

        static void Main(string[] args)
        {
            /*var a = new Complex[,] 
            {
                { 1, new Complex(-1, 1), 8, 2 },
                { 9, 24, -4, 23 },
                { 8, 2, 89, 1 },
                { -3, -1, 1, 37 }
            };

            var b = new Complex[,] { { -3, -1, 1, 37 } };

            Console.WriteLine(new SystemOfLinearEqs(new Matrix(a), new Matrix(b).Transposed()).GetSolutions());

            double m1 = 2;
            double n1 = -4;
            double l1 = 15;
            double p1 = 1;

            double m2 = 3;
            double n2 = 4;
            double l2 = -1;
            double p2 = 0;

            Console.WriteLine(new Quaternion(m1, n1, l1, p1) * new Quaternion(m2, n2, l2, p2));
            Console.WriteLine();
            Console.WriteLine(F(m1, n1, l1, p1, m2, n2, l2, p2));

            Console.WriteLine();

            Console.WriteLine(Inverse(m1, n1, l1, p1));
            var p = new Quaternion(m1, n1, l1, p1);
            Console.WriteLine(p.Inverse());
            Console.WriteLine(p * p.Inverse() == 1);

            Console.WriteLine(p / 2);*/

            var p = new Vector3(0, 0, 1);
            var u = new Vector3(3, 0, 0);
            var phi = Math.PI / 4;

            var answer = MathProject.Quaternion.RotatePoint(p, u, phi);

            Console.WriteLine(answer);
        }
    }
}