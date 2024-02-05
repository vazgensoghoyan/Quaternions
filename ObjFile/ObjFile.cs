using System.Globalization;
using System.IO;
using System.Numerics;
using System.Xml.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace Object
{
    public class ObjFile
    {
        public string Path { get; }
        // as i think its not good to read every time (if its what this does). I can deal with it later
        public Model[] Models { get => Read(); }

        public ObjFile(string path)
        {
            Path = path;
        }

        public Model[] Read()
        {
            string modelName = string.Empty;
            var models = new List<Model>();
            var vertices = new List<Vector3>();
            var normals = new List<Vector3>();
            var indexes = new List<int[]>();
            var normalIndexes = new List<int[]>();

            using (StreamReader reader = new StreamReader(Path))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line == null || line == string.Empty) continue;

                    switch (line[0])
                    {
                        case 'o':
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
                            break;
                        case 'v':
                            ReadVertex(line, vertices, normals);
                            break;
                        case 'f':
                            ReadPolygon(line, indexes, normalIndexes);
                            break;
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

            if (line[1] == ' ')
                vertices.Add(new Vector3(nums[0], nums[1], nums[2]));
            else if (line[1] == 'n')
                normals.Add(new Vector3(nums[0], nums[1], nums[2]));
        }

        static void ReadPolygon(string line, List<int[]> indexes, List<int[]> normalIndexes)
        {
            var ind = line.Split(' ').Skip(1)
                .Where(l => l != "")
                .Select(l => l.Split('/').Select(n => n == "" ? 0 : int.Parse(n)).ToArray())
                .ToArray();

            var forVerts = new List<int>();
            var forNormals = new List<int>();

            for (int i = 0; i < ind.Length; i++)
            {
                forVerts.Add(ind[i][0]);
                forNormals.Add(ind[i][2]);
            }

            indexes.Add(forVerts.ToArray());
            normalIndexes.Add(forNormals.ToArray());
        }
    
        public Model? GetModel(int index)
        {
            if (0 <= index && index <= Models.Length)
            {
                return Models[index];
            }
            return null;
        }

        public async void Clear()
        {
            using (StreamWriter writer = new StreamWriter(Path, false))
            {
                await writer.WriteLineAsync("");
            }
        }

        public async void AddText(string text)
        {
            using (StreamWriter writer = new StreamWriter(Path, true))
            {
                await writer.WriteAsync(text);
            }
        }

        private string FlToStr(float x, int digitsAfterDot)
        {
            return x.ToString($"N{digitsAfterDot}", System.Globalization.CultureInfo.InvariantCulture);
        }
        
        public void AddModel(Model model)
        {
            var s = $"\no {model.Name}\n";

            foreach (var v in model.Vertices) 
            {
                s += $"v {FlToStr(v.X, 6)} {FlToStr(v.Y, 6)} {FlToStr(v.Z, 6)}\n";
            }

            foreach (var v in model.Normals)
            {
                s += $"vn {FlToStr(v.X, 4)} {FlToStr(v.Y, 4)} {FlToStr(v.Z, 4)}\n";
            }

            for (int i = 0; i < model.Indexes.Length; i++)
            {
                s += "f";
                for (int j = 0; j < model.Indexes[i].Length; j++)
                {
                    s += $" {model.Indexes[i][j]}//{model.NormalIndexes[i][j]}";
                }
                s += "\n";
            }

            AddText(s);
        }

        public void AddFewModels(Model[] models)
        {
            foreach (var model in models)
            {
                AddModel(model);
            }
        }
    }
}