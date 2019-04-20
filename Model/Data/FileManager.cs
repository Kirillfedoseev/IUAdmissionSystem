using System;
using System.Collections.Generic;
using System.IO;
using Model.Support;
using Model.Users;

namespace Model.Data
{
    class FileManager:Singletone<FileManager>
    {
        public Dictionary<AbstractUser, List<string>> UsersFiles;

        public const string RootStorageDirectory = "Storage" ;

        private static string GetFullFileName(int id, string name) => $"{RootStorageDirectory}/{id}/{name}.bytes";

        public FileManager()
        {
            UsersFiles = new Dictionary<AbstractUser, List<string>>();
        }

        public static void SubmitFile(AbstractUser user, FileTypes type, Stream fileStream)
            => SaveFile(user, GetFullFileName(user.id, type.ToString()), fileStream);
        

        public static Stream GetFile(AbstractUser user, FileTypes type)
            => LoadFile(user, GetFullFileName(user.id, type.ToString()));
        


        private static void SaveFile(AbstractUser user, string filename, Stream fileStream)
        {
            Directory.CreateDirectory(filename.Remove(filename.LastIndexOf('/')));
            using (var file = File.Create(filename))
            {
                fileStream.Seek(0, SeekOrigin.Begin);
                fileStream.CopyTo(file);
            }

            //todo handle errors
            if (!Instance.UsersFiles.ContainsKey(user))
                Instance.UsersFiles.Add(user,new List<string>());

            Instance.UsersFiles[user].Add(filename);

        }


        private static Stream LoadFile(AbstractUser user, string filename)
        {
            if (Instance.UsersFiles.ContainsKey(user) && Instance.UsersFiles[user].Contains(filename))
                return File.OpenRead(filename);

            throw new Exception("No such file was found!");
        }

    }

    public enum FileTypes
    {
        CV,
        Photo,
        Passport,
        Letter,
        Transcripts

    }
}
