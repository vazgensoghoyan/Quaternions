using System.Numerics;

namespace Object
{
    public class Model
    {
        public string Name { get; }
        public Vector3[] Vertices { get; }
        public int[][] Indexes { get; }

        public Model(string name, Vector3[] v, int[][] i)
        {
            Name = name;
            Vertices = v;
            Indexes = i;
        }

        public void Rotate(Vector3 u, double phi)
        {
            for (int i = 0; i < Vertices.Length; i++)
            {
                Vertices[i] = MyMath.Quaternion.RotatePoint(Vertices[i], u, phi);
            }
        }

        public void RotateX(double phi) => Rotate(new Vector3(1, 0, 0), phi);
        public void RotateY(double phi) => Rotate(new Vector3(0, 1, 0), phi);
        public void RotateZ(double phi) => Rotate(new Vector3(0, 0, 1), phi);

        public override string ToString()
        {
            return "object " + Name;
        }
    }
}
