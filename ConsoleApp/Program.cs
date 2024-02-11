using Object;


var objFile = new ObjFile(@"C:\Users\Acer\Desktop\1.obj");
var model = objFile.GetModel(0);
model.RotateX(Math.PI / 3);
objFile.Clear();
objFile.AddModel(model);