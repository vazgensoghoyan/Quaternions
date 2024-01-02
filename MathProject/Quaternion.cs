using static System.Math;

namespace MathProject
{
    public class Quaternion
    {
        public Complex Z1 { get; }
        public Complex Z2 { get; }

        public double Modulus { get; }

        public Quaternion(Complex z1, Complex z2)
        {
            Z1 = z1;
            Z2 = z2;

            Modulus = Sqrt(z1.Modulus * z1.Modulus + z2.Modulus * z2.Modulus);
        }

        public Quaternion(double a, double b, double c, double d)
        {
            Z1 = new Complex(a, b);
            Z2 = new Complex(c, d);

            Modulus = Sqrt(a * a + b * b + c * c + d * d);
        }

        public static Quaternion operator +(Quaternion p1, Quaternion p2)
        {
            return new Quaternion(p1.Z1 + p2.Z2, p1.Z2 + p2.Z2);
        }
        public static Quaternion operator -(Quaternion p1, Quaternion p2)
        {
            return p1 + (-p2);
        }
        public static Quaternion operator -(Quaternion p)
        {
            return new Quaternion(-p.Z1, -p.Z2);
        }
        public static Quaternion operator *(Quaternion p1, Quaternion p2)
        {
            return new Quaternion
            (
                p1.Z1 * p2.Z1 - p1.Z2 * ~p2.Z2,
                p1.Z1 * p2.Z2 + p1.Z2 * ~p2.Z1
            );
        }
        public static Quaternion operator /(Quaternion p1, Quaternion p2)
        {
            return p1 * p2.Inverse();
        }
        public static Quaternion operator ~(Quaternion p)
        {
            return new Quaternion(~p.Z1, -p.Z2);
        }

        public static bool operator ==(Quaternion n1, Quaternion n2)
        {
            return n1.Z1 == n2.Z1 && n1.Z2 == n1.Z2;
        }
        public static bool operator !=(Quaternion n1, Quaternion n2)
        {
            return !(n1 == n2);
        }
        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
                return false;

            return this == (Quaternion)obj;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static implicit operator Quaternion(double number)
        {
            return new Quaternion(number, 0);
        }

        public Quaternion Inverse()
        {
            return new Quaternion(~this.Z1 / (Modulus * Modulus), -this.Z2 / (Modulus * Modulus));
        }

        public override string ToString()
        {
            return string.Format("{0}+{1}*i+{2}*j+{3}*k", Z1.Re, Z1.Im, Z2.Re, Z2.Im);
        }
    }
}
