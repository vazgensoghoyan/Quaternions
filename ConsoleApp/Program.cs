using Object;


var objFile = new ObjFile(@"C:\Users\Acer\Desktop\1.obj");
var model = objFile.GetModel(0);
model.RotateZ(Math.PI / 4);
model.RotateY(Math.PI / 9);
objFile.Clear();
objFile.AddModel(model);