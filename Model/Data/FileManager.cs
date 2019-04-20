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

        private static string GetFullFileName(int id, string name) => $"{RootStorageDirectory}/{id}/{name}";

#region Submit

        public static void SubmitCV(AbstractUser user, Stream fileStream) 
            => SaveFile(user, GetFullFileName(user.id, "CV"), fileStream);

        public static void SubmitPassport(AbstractUser user, Stream fileStream)
            => SaveFile(user, GetFullFileName(user.id, "Passport"), fileStream);

        public static void SubmitLetter(AbstractUser user, Stream fileStream)
            => SaveFile(user, GetFullFileName(user.id, "Letter"), fileStream);

        public static void SubmitTranscripts(AbstractUser user, Stream fileStream)
            => SaveFile(user, GetFullFileName(user.id, "Transcripts"), fileStream);
        #endregion

#region Get

        public static Stream GetCV(AbstractUser user)
            => GetFile(user, GetFullFileName(user.id, "CV"));

        public static Stream GetPassport(AbstractUser user)
            => GetFile(user, GetFullFileName(user.id, "Passport"));

        public static Stream GetLetter(AbstractUser user)
            => GetFile(user, GetFullFileName(user.id, "Letter"));

        public static Stream GetTranscripts(AbstractUser user)
            => GetFile(user, GetFullFileName(user.id, "Transcripts"));

        #endregion


        private static void SaveFile(AbstractUser user, string filename, Stream fileStream)
        {
            using (var file = File.Create(filename))
            {
                fileStream.Seek(0, SeekOrigin.Begin);
                fileStream.CopyTo(file);
            }

            //todo handle errors
            if (Instance.UsersFiles.ContainsKey(user))
                Instance.UsersFiles.Add(user,new List<string>());

            Instance.UsersFiles[user].Add(filename);

        }


        private static Stream GetFile(AbstractUser user, string filename)
        {
            if (Instance.UsersFiles.ContainsKey(user) && Instance.UsersFiles[user].Contains(filename))
                return File.OpenRead(GetFullFileName(user.id, filename));

            throw new Exception("No such file was found!");
        }

    }

}
