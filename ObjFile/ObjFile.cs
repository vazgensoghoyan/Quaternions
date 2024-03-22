using System.Globalization;
using System.Numerics;

namespace Object
{
    public class ObjFile
    {
        public string Path { get; }
        public Model[] Models { get; private set; }

        public ObjFile(string path)
        {
            Path = path;
            Models = Read();
        }

        public Model[] Read()
        {
            string modelName = string.Empty;
            var models = new List<Model>();
            var vertices = new List<Vector3>();
            var indexes = new List<int[]>();

            using (StreamReader reader = new StreamReader(Path))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line == null || line == string.Empty) continue;

                    modelName = ReadLine(line, modelName, models, vertices, indexes);
                }
            }

            if (modelName == "" && vertices.Any())
            {
                modelName = "SomeObject";
            }

            if (modelName != "")
            {
                models.Add(new Model(
                    modelName,
                    vertices.ToArray(),
                    indexes.ToArray()
                ));
            }

            return models.ToArray();
        }

        static string ReadLine(string line, string modelName, List<Model> models, List<Vector3> vertices, List<int[]> indexes)
        {
            switch (line[0])
            {
                case 'o':
                    if (modelName != "")
                    {
                        models.Add(new Model(
                            modelName,
                            vertices.ToArray(),
                            indexes.ToArray()
                        ));
                    }
                    modelName = line.Substring(2);
                    break;
                case 'v':
                    if (line[1] == ' ')
                        ReadVertex(line, vertices);
                    break;
                case 'f':
                    ReadPolygon(line, indexes);
                    break;
            }

            return modelName;
        }

        static void ReadVertex(string line, List<Vector3> vertices)
        {
            var nums = line.Split(' ').Skip(1)
                .Where(n => n != "")
                .Select(n => float.Parse(n, CultureInfo.InvariantCulture))
                .ToArray();

            vertices.Add(new Vector3(nums[0], nums[1], nums[2]));
        }

        static void ReadPolygon(string line, List<int[]> indexes)
        {
            var ind = line.Split(' ').Skip(1)
                .Where(l => l != "")
                .Select(l => int.Parse(l.Split('/')[0]))
                .ToArray();

            indexes.Add(ind);
        }
    
        public Model? GetModel(int index)
        {
            if (0 <= index && index < Models.Length)
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

            Models = new Model[0];
        }

        public async void AddText(string text)
        {
            foreach (var line in text.Split('\n'))
            {
                using (StreamWriter writer = new StreamWriter(Path, true))
                {
                    await writer.WriteLineAsync(line);
                }
            }

            Models = Read();
        }

        private string FlToStr(float x, int digitsAfterDot)
        {
            return x.ToString($"N{digitsAfterDot}", CultureInfo.InvariantCulture);
        }
        
        public void AddModel(Model model)
        {
            var text = $"\no {model.Name}\n";

            foreach (var v in model.Vertices)
            {
                text += $"v {FlToStr(v.X, 6)} {FlToStr(v.Y, 6)} {FlToStr(v.Z, 6)}\n";
            }

            foreach (var i in model.Indexes)
            {
                text += $"f {string.Join(' ', i)}\n";
            }

            AddText(text);
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