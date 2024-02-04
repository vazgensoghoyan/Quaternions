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
            var indexes = new List<int[]>();
            
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
                }
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

        public void AddModel(Model model)
        {
            var s = $"\no {model.Name}\n";

            foreach (var v in model.Vertices) 
            {
                s += $"v {v.X} {v.Y} {v.Z}\n";
            }

            foreach (var f in model.Indexes)
            {
                s += $"f {string.Join(' ', f)}\n";
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