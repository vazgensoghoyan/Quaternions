using Object;
using MyMath;


/*var objFile = new ObjFile(@"C:\Users\Acer\Desktop\1.obj"); // создается объект класса для работы с файлом
var model = objFile.GetModel(0); // так как файл уже прочтён (конструктор вызывается при создании
model.RotateX(Math.PI / 3);
objFile.Clear();
objFile.AddModel(model);*/

var q = new Quaternion(1, 2, 4, 5);
var w = new Quaternion(3, 4, -1, 0);

Console.WriteLine(q + w);
Console.WriteLine(q * w);