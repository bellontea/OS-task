using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using System.IO.Compression;

namespace ConsoleApp1
{
  class Person
    {
      public string Name { get; set; }
      public int Age { get; set; }
    }
  class Program
  {
    public static void task_1()
    {
      DriveInfo[] drives = DriveInfo.GetDrives();

      foreach (DriveInfo drive in drives)
      {
        Console.WriteLine($"Название: {drive.Name}");
        Console.WriteLine($"Тип: {drive.DriveType}");
        if (drive.IsReady)
        {
          Console.WriteLine($"Объем диска: {drive.TotalSize}");
          Console.WriteLine($"Свободное пространство: {drive.TotalFreeSpace}");
          Console.WriteLine($"Метка: {drive.VolumeLabel}");
        }
        Console.WriteLine();
      }
    }
    public static void task_2()
    {
      Console.WriteLine("Введите путь, где будет создан файл:");
      string path = Console.ReadLine();
      path += @"\task_2.txt";
      // запись в файл
      using (FileStream fstream = File.Create(path))
      {
        Console.WriteLine("Введите строку для записи в файл:");
        string text = Console.ReadLine();
        Console.WriteLine();
        // преобразуем строку в байты
        byte[] array = System.Text.Encoding.Default.GetBytes(text);
        // запись массива байтов в файл
        fstream.Write(array, 0, array.Length);
        Console.WriteLine("Текст записан в файл");
      }
      using (FileStream fstream = File.OpenRead(path))
      {
        // преобразуем строку в байты
        byte[] array = new byte[fstream.Length];
        // считываем данные
        fstream.Read(array, 0, array.Length);
        // декодируем байты в строку
        string textFromFile = System.Text.Encoding.Default.GetString(array);
        Console.WriteLine($"Текст из файла: {textFromFile}\n");
      }
      Console.WriteLine("Нажмите, чтобы удалить файл");
      string awaiting = Console.ReadLine();
      // удаление файла
      FileInfo fileinfo = new FileInfo(path);
      if (fileinfo.Exists)
      {
        fileinfo.Delete();
      }
      Console.WriteLine("Файл удален\n");
    }
    public static async Task task_3()
    {
      Console.WriteLine("Введите путь, где будет создан файл:");
      string path = Console.ReadLine();
      path += @"\task_3.json";
      // сохранение данных
      using (FileStream fs = File.Create(path))
      {
        Person tom = new Person() { Name = "Tom", Age = 35 };
        await JsonSerializer.SerializeAsync<Person>(fs, tom);
        Console.WriteLine("\nData has been saved to file\n");
      }
      // чтение данных
      using (FileStream fs = File.OpenRead(path))
      {
        Person restoredPerson = await JsonSerializer.DeserializeAsync<Person>(fs);
        Console.WriteLine($"Name: {restoredPerson.Name}  \nAge: {restoredPerson.Age}\n");
      }
      // удаление файла
      Console.WriteLine("Нажмите, чтобы удалить файл");
      string awaiting = Console.ReadLine();
      FileInfo fileinfo = new FileInfo(path);
      if (fileinfo.Exists)
      {
        fileinfo.Delete();
      }
      Console.WriteLine("Файл удален\n");
    }
    public static void task_4()
    {
      Console.WriteLine("Введите путь, где будет создан файл:");
      string path = Console.ReadLine();
      Console.WriteLine();
      path += @"\task_4.xml";
      // запись в файл
      XmlDocument xmlDoc = new XmlDocument();
      XmlNode rootNode = xmlDoc.CreateElement("users");
      xmlDoc.AppendChild(rootNode);

      XmlNode userNode = xmlDoc.CreateElement("user");
      XmlAttribute attribute = xmlDoc.CreateAttribute("age");
      attribute.Value = "42";
      userNode.Attributes.Append(attribute);
      userNode.InnerText = "John Doe";
      rootNode.AppendChild(userNode);

      userNode = xmlDoc.CreateElement("user");
      attribute = xmlDoc.CreateAttribute("age");
      attribute.Value = "39";
      userNode.Attributes.Append(attribute);
      userNode.InnerText = "Jane Doe";
      rootNode.AppendChild(userNode);

      xmlDoc.Save(path);
      
      // чтение из файла
      xmlDoc.Load(path);
      // получим корневой элемент
      XmlElement xRoot = xmlDoc.DocumentElement;
      // обход всех узлов в корневом элементе
      foreach(XmlNode xnode in xRoot)
      {
        // вывод элемента user
        Console.WriteLine($"Пользователь: {xnode.InnerText}");
        // получаем атрибут age
        if(xnode.Attributes.Count>0)
        {
          XmlNode attr = xnode.Attributes.GetNamedItem("age");
          if (attr!=null)
            Console.WriteLine("Возраст: " + attr.Value);
        }
        Console.WriteLine();
      }
      
      Console.WriteLine("Нажмите, чтобы удалить файл");
      string awaiting = Console.ReadLine();
      // удаление файла
      FileInfo fileinfo = new FileInfo(path);
      if (fileinfo.Exists)
      {
        fileinfo.Delete();
      }
      Console.WriteLine("Файл удален\n");
    }
    public static void task_5()
    {
      Console.WriteLine("Введите путь, где будет создан zip архив:");
      string path = Console.ReadLine();
      string subpath = path + @"\task_5\";
      DirectoryInfo dirInfo = new DirectoryInfo(subpath);
      if (!dirInfo.Exists)
      {
        dirInfo.Create();
      }
      if (!File.Exists(subpath + "task_5.txt"))
      {
        File.Create(subpath + "task_5.txt").Close();
      }
      string sourceFolder = subpath; // исходная папка
      string zipFile = path + @"\task_5.zip"; // сжатый файл
      ZipFile.CreateFromDirectory(sourceFolder, zipFile);
      // удаление больше ненужной папки
      Directory.Delete(subpath, true);
      Console.WriteLine("\nСоздан архив task_5.zip, куда помещен файл task_5.txt\n");
      // определение размера архива
      var fileSize = new System.IO.FileInfo(zipFile).Length;
      Console.WriteLine($"Размер архива: {fileSize} байт\n");
      Console.WriteLine("Нажмите, чтобы удалить архив");
      string awaiting = Console.ReadLine();
      // удаление архива
      FileInfo fileinfo = new FileInfo(path + @"\task_5.zip");
      if (fileinfo.Exists)
      {
        fileinfo.Delete();
      }
      Console.WriteLine("Архив удален\n");
    }
    static async Task Main(string[] args)
    {
      int num = 1;
      while (num != 0)
      {
        Console.Write("Введите номер задания или 0 для выхода из программы: ");
        num = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine();
        switch (num)
        {
          case 1:
            task_1();
            break;
          case 2:
            task_2();
            break;
          case 3:
            await task_3();
            break;
          case 4:
            task_4();
            break;
          case 5:
            task_5();
            break;
        }
      }
    }
  }
}
