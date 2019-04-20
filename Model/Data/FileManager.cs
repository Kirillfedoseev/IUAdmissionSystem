using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using Model.Support;
using Model.Users;

namespace Model.Data
{
    class FileManager:Singletone<FileManager>
    {
        public Dictionary<AbstractUser, List<string>> UsersFiles;
        public const string RootStorageDirectory = "Storage" ;

        private string GetFullFileName(int id, string name) => $"{RootStorageDirectory}/{id}/{name}";

        public void PostFile(AbstractUser user, string filename, StreamContent fileStream)
        {
            using (var file = File.Create(GetFullFileName(user.id,filename)))
            {
                var stream = fileStream.ReadAsStreamAsync().Result;
                stream.Seek(0, SeekOrigin.Begin);
                stream.CopyTo(file);
            }
            //todo handle errors
            if (UsersFiles.ContainsKey(user))
                UsersFiles.Add(user,new List<string>());

            UsersFiles[user].Add(filename);

        }

        public StreamContent GetFile(AbstractUser user, string filename)
        {
            if (UsersFiles.ContainsKey(user) && UsersFiles[user].Contains(filename))
                return new StreamContent(File.OpenRead(GetFullFileName(user.id, filename)));

            throw new Exception("No such file was found!");
        }

    }
}
