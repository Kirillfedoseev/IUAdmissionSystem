using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Model.Data;
using Model.Support;
using Model.Users;

namespace Model.Files
{
    public class FileManager:Singletone<FileManager>
    {
        public const string RootStorageDirectory = "Storage" ;

        public Dictionary<AbstractUser, List<FileData>> UsersFiles { get; set; }

        private static string GetFullFileName(int id, string name) => $"{RootStorageDirectory}/{id}/{name}.bytes";

        public FileManager()
        {
            UsersFiles = new Dictionary<AbstractUser, List<FileData>>();
        }

        public static void SubmitFile(AbstractUser user, FileData info, string fileStream)
        {
            byte[] byteArray = Encoding.ASCII.GetBytes(fileStream);
            Stream stream = new MemoryStream(byteArray);

            SaveFile(GetFullFileName(user.id, info.Type), stream);

            //todo handle errors
            if (!Instance.UsersFiles.ContainsKey(user))
                Instance.UsersFiles.Add(user, new List<FileData>());

            Instance.UsersFiles[user].Add(info);
        }


        public static (FileData fileData, string fileStream) GetFileData(AbstractUser user, string type)
        {
            string filename = GetFullFileName(user.id, type);

            if (!Instance.UsersFiles.TryGetValue(user, out var list))
                throw new Exception("Incorrect User!");

            FileData fileData;
            return (fileData = list.SingleOrDefault(n => n.Type.Equals(type))) != null
                ? (fileData, LoadFileAsString(filename))
                : throw new Exception("File type doesn't exists!");

        }


        private static void SaveFile(string filename, Stream fileStream)
        {
            Directory.CreateDirectory(filename.Remove(filename.LastIndexOf('/')));
            using (var file = File.Create(filename))
            {
                fileStream.Seek(0, SeekOrigin.Begin);
                fileStream.CopyTo(file);
            }        
        }


        private static string LoadFileAsString(string filename)
        {
            StreamReader reader = new StreamReader(File.OpenRead(filename));
            string fileString = reader.ReadToEnd();
            return fileString;
        }

    }
}
