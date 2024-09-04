using Object;


var objFile = new ObjFile(@"C:\Users\Acer\Desktop\tree\tree.obj"); // создается объект класса для работы с файлом
var model = objFile.GetModel(0); // так как файл уже прочтён (конструктор вызывается при создании
model.RotateX(Math.PI / 3);
model.RotateY(Math.PI / 6);
objFile.Clear();
objFile.AddModel(model);
