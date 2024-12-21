using System;
using System.IO;
using System.IO.Compression;

class Program
{
    static string FindFile(string directoryPath, string fileName)
    {
        foreach (string file in Directory.GetFiles(directoryPath, fileName, SearchOption.AllDirectories))
        {
            return file;
        }
        return null;
    }

    public static void FileContent(string foundFilePath)
    {
        using (FileStream fileStream = new FileStream(foundFilePath, FileMode.Open, FileAccess.Read))
        using (StreamReader reader = new StreamReader(fileStream))
        {
            string content = reader.ReadToEnd();
            Console.WriteLine(content);
        }
    }

    static void CompressFile(string filePath)
    {
        string compressedFilePath = filePath + ".gz";
        using (FileStream originalFileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        using (FileStream compressedFileStream = new FileStream(compressedFilePath, FileMode.Create))
        using (GZipStream compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress))
        {
            originalFileStream.CopyTo(compressionStream);
        }
        Console.WriteLine($"Файл успешно сжат: {compressedFilePath}");
    }

    public static void Main()
    {
        Console.WriteLine("Введите путь к директории, в которой находится файл: ");
        string directoryPath = Console.ReadLine();

        Console.WriteLine("Введите имя файла: ");
        string fileName = Console.ReadLine();

        string foundFilePath = FindFile(directoryPath, fileName);

        if (foundFilePath == null)
        {
            Console.WriteLine("Файл не найден");
        }
        Console.WriteLine("Файл найден: {0}", foundFilePath);

        Console.WriteLine("\nВывод содержимого файла");
        FileContent(foundFilePath);

        Console.WriteLine("\nХотите сжать файл? (y/n)");
        string compressResponse = Console.ReadLine();
        if (compressResponse?.ToLower() == "y")
        {
            CompressFile(foundFilePath);
        }
    }
}