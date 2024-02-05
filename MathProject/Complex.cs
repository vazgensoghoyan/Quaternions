using static System.Math;

namespace MyMath
{
    public enum FormOfWriting
    {
        Algebraic,
        Trigonometric,
    }

    public class Complex
    {
        /// <summary>
        /// Действительная часть комплексного числа.
        /// </summary>
        public double Re { get; }
        /// <summary>
        /// Мнимая часть комплексного числа.
        /// </summary>
        public double Im { get; }

        /// <summary>
        /// Модуль комплексного числа.
        /// </summary>
        public double Modulus { get; }
        /// <summary>
        /// Главный аргумент комплексного числа на [-pi/2; pi/2].
        /// </summary>
        public double Arg { get; }

        /// <summary>
        /// Создание объекта класса Complex.
        /// </summary>
        /// <param name="a">Либо действ. часть, либо модуль (в зависимости от форма записи)</param>
        /// <param name="b">Либо мнимая часть, либо аргумент (в зависимости от форма записи)</param>
        /// <param name="form">Форма записи комплексного числа. 
        /// Алгебраическая (что стоит по умолчанию) или тригонометрическая</param>
        public Complex(double a, double b, FormOfWriting form = FormOfWriting.Algebraic)
        {
            if (form == FormOfWriting.Algebraic)
            {
                Re = a;
                Im = b;

                Modulus = Sqrt(Re * Re + Im * Im);

                var atan = Atan(Im / Re);
                if (Re >= 0) Arg = atan;
                else if (Im <= 0) Arg = -PI + atan;
                else Arg = PI + atan;
            }
            else
            {
                Modulus = a;

                // ////////////////////////////////
                Arg = b;
                // ////////////////////////////////

                Re = Modulus * Cos(Arg);
                Im = Modulus * Sin(Arg);
            }
        }

        // 4 блока кода ниже - реализация арифмитических операторов для работы с компл числами
        public static Complex operator *(Complex n1, Complex n2)
        {
            return new Complex(n1.Re * n2.Re - n1.Im * n2.Im, n1.Im * n2.Re + n1.Re * n2.Im);
        }
        public static Complex operator /(Complex n1, Complex n2)
        {
            double a = n2.Modulus * n2.Modulus;

            return new Complex((n1.Re * n2.Re + n1.Im * n2.Im) / a,
                                     (n1.Im * n2.Re - n1.Re * n2.Im) / a);
        }
        public static Complex operator +(Complex n1, Complex n2)
        {
            return new Complex(n1.Re + n2.Re, n1.Im + n2.Im);
        }
        public static Complex operator -(Complex n1, Complex n2)
        {
            return new Complex(n1.Re - n2.Re, n1.Im - n2.Im);
        }
        public static Complex operator -(Complex n)
        {
            return 0 - n;
        }
        /// <summary>
        /// унарный оператор, возвращающий комплексно-сопряженное аргументу число
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static Complex operator ~(Complex n)
        {
            return new Complex(n.Re, -n.Im);
        }
        // 4 блока кода ниже - реализация некоторых операторов сравнения для объектов этого класса
        public static bool operator ==(Complex n1, Complex n2)
        {
            byte accuracy = 5;

            return (Round(n1.Re, accuracy) == Round(n2.Re, accuracy) &&
                    Round(n1.Im, accuracy) == Round(n2.Im, accuracy));
        }
        public static bool operator !=(Complex n1, Complex n2)
        {
            return !(n1 == n2);
        }
        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
                return false;

            return this == (Complex)obj;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Неявное преобразование из double в Complex.
        /// </summary>
        /// <param name="number"></param>
        public static implicit operator Complex(double number)
        {
            return new Complex(number, 0);
        }

        /// <summary>
        /// корень n-ной степени из комплексного числа. Возвращает массивом
        /// n комплексных чисел, которые соответствуют решениям требуемой задачи.
        /// </summary>
        /// <param name="number"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static Complex[] TakeRoot(Complex number, ushort n)
        {
            var answers = new Complex[n];
            // Ищем модуль итоговых ответов корня (все лежат на одной окружостив компл плоскости)
            double module;
            if (n == 3)
                module = Cbrt(number.Modulus);
            else
                module = Math.Pow(number.Modulus, 1f / n);
            // ищем первый аргумент
            var initialArg = number.Arg / n;
            if (number.Im == 0 && number.Re == 0)
                initialArg = 0;
            // Крутимся по окружности радиуса module, записываем все корни
            for (int i = 0; i < n; i++)
            {
                answers[i] = new Complex
                (
                    module,
                    initialArg + 2 * PI * i / n,
                    FormOfWriting.Trigonometric
                );
            }
            // Возвращаем результат
            return answers;
        }

        /// <summary>
        /// возводит данное комплексное число в n-ную степень.
        /// Формула Муавра
        /// </summary>
        /// <param name="number"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static Complex Pow(Complex number, int n)
        {
            return new Complex
            (
                Math.Pow(number.Modulus, n),
                n * number.Arg,
                FormOfWriting.Trigonometric
            );
        }

        public override string ToString()
        {
            //return string.Format("{0} + i * {1}", Re, Im);
            var re = Round(Re, 10);
            var im = Round(Im, 10);

            if (re == 0 && im == 0)
                return "0";

            string result = (re != 0) ? re.ToString() : string.Empty;
            if (im != 0)
            {
                if (im == 1 || im == -1)
                    result += (im < 0) ? "-i" : "+i";
                else
                    result += (im < 0) ? $"-{-im}i" : $"+{im}i";
            }

            return result[0] == '+' ? result.Substring(1, result.Length - 1) : result;
        }
    }
}