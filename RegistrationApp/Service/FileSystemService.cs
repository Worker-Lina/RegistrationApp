using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RegistrationApp.Service
{
    public class FileSystemService
    {
        public string PucturePlace()
        {
            var drives = DriveInfo.GetDrives();
            string path = "";
            int count = 1;
            foreach (var drive in drives)
            {
                Console.WriteLine($"{drive.Name}");
                count++;
            }

            Console.WriteLine("Выберите номер диска: ");

            if (int.TryParse(Console.ReadLine(), out int userDrivePosition))
            {
                if (userDrivePosition > drives.Length)
                {
                    Console.WriteLine("Неверный выбор!");
                }
                var drive = drives[userDrivePosition - 1];
                if (drive.IsReady && drive.DriveType == DriveType.Fixed)
                {
                    path = drive.Name;

                    foreach (var entity in Directory.GetFileSystemEntries(path))
                    {
                        Console.WriteLine(entity);
                    }
                    Console.WriteLine("Введите имя папки(папок): ");

                    var directoryName = Console.ReadLine();

                    if (string.IsNullOrEmpty(directoryName)) return null;

                    path = $"{path}{directoryName}/";
                    Directory.CreateDirectory(path);

                    Console.WriteLine("Введите имя файла с расширением:");
                    var fileName = Console.ReadLine();

                    if (string.IsNullOrEmpty(fileName)) return null;

                    path = $"{path}{fileName}";
                }
            }
            return path;
        }
    }
}
