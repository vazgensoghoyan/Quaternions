using Object;


var objFile = new ObjFile(@"C:\Users\Acer\Desktop\tree\tree.obj");
var model = objFile.GetModel(0);
model.RotateX(Math.PI / 2.1163);
objFile.Clear();
objFile.AddModel(model);