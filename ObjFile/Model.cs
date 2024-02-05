using MathProject;
using System.Numerics;

namespace Object
{
    public class Model
    {
        public string Name { get; }
        public Vector3[] Vertices { get; }
        //public Vector2[] TextureVertices { get; }
        public Vector3[] Normals { get; }
        public int[][] Indexes { get; }
        //public int[] TextureIndexes { get; }
        public int[][] NormalIndexes { get; }

        public Model(string name, Vector3[] v, Vector3[] ns, int[][] i, int[][] ni)
        {
            Name = name;
            Vertices = v;
            Normals = ns;
            Indexes = i;
            NormalIndexes = ni;
        }

        public void Rotate(Vector3 u, double phi)
        {
            for (int i = 0; i < Vertices.Length; i++)
            {
                Vertices[i] = MathProject.Quaternion.RotatePoint(Vertices[i], u, phi);
            }
            for (int i = 0; i < Normals.Length; i++)
            {
                Normals[i] = MathProject.Quaternion.RotatePoint(Normals[i], u, phi);
            }
        }

        public void RotateX(double phi) => Rotate(new Vector3(1, 0, 0), phi);
        public void RotateY(double phi) => Rotate(new Vector3(0, 1, 0), phi);
        public void RotateZ(double phi) => Rotate(new Vector3(0, 0, 1), phi);

        public override string ToString()
        {
            return "this is object named " + Name;
        }
    }
}
