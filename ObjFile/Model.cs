using System.Numerics;

namespace Object
{
    public class Model
    {
        private string Name;
        private Vector3[] Vertices;
        //public Vector2[] TextureVertices { get; }
        private Vector3[] Normals;
        private int[] Indexes;
        //public int[] TextureIndexes { get; }
        private int[] NormalIndexes;

        public Model(string name, Vector3[] v, Vector3[] n, int[] i, int[] ni)
        {
            Name = name;
            Vertices = v;
            Normals = n;
            Indexes = i; 
            NormalIndexes = ni;
        }

        public void Rotate()
        {

        }
    }
}
