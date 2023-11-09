using MathProject;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = new Matrix(new Complex[,] { 
                { -11, 5, 3, 99, 11 },
                { 81, 744, 6, 40, -8 },
                { -308, 18, -9, 53, 0 },
                { 7, 8, 1, 101, 11 },
                { 6, 6, 8, 0, 0 }
            });
            Console.WriteLine(a);
            Console.WriteLine(a.GetRow(1));
            Console.WriteLine(a.GetColumn(1));

            Console.WriteLine(a.Determinant());
        }
    }
}