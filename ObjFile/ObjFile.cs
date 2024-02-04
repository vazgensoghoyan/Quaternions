using System.Globalization;
using System.Numerics;
using System.Xml.Schema;

namespace Object
{
    public class ObjFile
    {
        public string Path { get; }
        public Model[] Models { get; }
        
        public ObjFile(string path)
        {
            Path = path;
            Models = Read();
        }

        public Model[] Read()
        {
            var models = new List<Model>();
            var vertices = new List<Vector3>();
            var normals = new List<Vector3>();
            var indexes = new List<int>();
            var normalIndexes = new List<int>();
            string modelName = string.Empty;
            
            using (StreamReader reader = new StreamReader(Path))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line == null) continue;

                    if (line[0] == 'o')
                    {
                        if (modelName != "")
                        {
                            models.Add(new Model(
                                modelName,
                                vertices.ToArray(),
                                normals.ToArray(),
                                indexes.ToArray(),
                                normalIndexes.ToArray()
                            ));
                        }
                        modelName = line.Substring(2);
                    }
                    else if (line[0] == 'v')
                    {
                        ReadVertex(line, vertices, normals);
                    }
                    else if (line[0] == 'f')
                    {
                        ReadPolygon(line, indexes, normalIndexes);
                    }
                }
            }

            if (modelName != "")
            {
                models.Add(new Model(
                    modelName,
                    vertices.ToArray(),
                    normals.ToArray(),
                    indexes.ToArray(),
                    normalIndexes.ToArray()
                ));
            }

            return models.ToArray();
        }

        static void ReadVertex(string line, List<Vector3> vertices, List<Vector3> normals)
        {
            var nums = line.Split(' ').Skip(1)
                .Where(n => n != "")
                .Select(n => float.Parse(n, CultureInfo.InvariantCulture))
                .ToArray();

            if (line[1] == 'n') {
                normals.Add(Vector3.Normalize(new Vector3(nums[0], nums[1], nums[2])));
            }
            else {
                vertices.Add(new Vector3(nums[0], nums[1], nums[2]));
            }
        }

        static void ReadPolygon(string line, List<int> indexes, List<int> normalIndexes)
        {
            var ind = line.Split(' ').Skip(1)
                .Where(l => l != "")
                .Select(l => l.Split('/').Select(n => n == "" ? 0 : int.Parse(n)).ToArray())
                .ToArray();

            for (int i = 1; i < ind.Length - 1; i++)
            {
                indexes.Add(ind[0][0] - 1);
                indexes.Add(ind[i][0] - 1);
                indexes.Add(ind[i + 1][0] - 1);

                if (ind[0].Length > 2)
                {

                    normalIndexes.Add(ind[0][2] - 1);
                    normalIndexes.Add(ind[i][2] - 1);
                    normalIndexes.Add(ind[i + 1][2] - 1);
                }
            }
        }
    }
}