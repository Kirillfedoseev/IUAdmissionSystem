using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Model.Authentication;
using Model.Data;
using Model.Support;
using Model.Users;

namespace Model.Files
{
    /// <summary>
    /// Class with manage all actions, which relate to files in system
    /// </summary>
    public class FileManager:Singletone<FileManager>
    {

        /// <summary>
        /// Related path to the file storage
        /// </summary>
        public const string RootStorageDirectory = "Storage" ;

        /// <summary>
        /// Collection with all files of user, mapped as User to list of FileData
        /// Each user can have only one unique file for each Type
        /// </summary>
        public Dictionary<AbstractUser, List<FileData>> UsersFiles { get; set; }

        /// <summary>
        /// Help method for getting filename
        /// </summary>
        /// <param name="id">id of user, which submit file</param>
        /// <param name="type">type of the file</param>
        /// <returns>filename with path to it</returns>
        private static string GetFullFileName(int id, string type) => $"{RootStorageDirectory}/{id}/{type}.bytes";

        /// <summary>
        /// Special object for preventing racecondion for writing and reading the same files  
        /// </summary>
        private readonly object _lock;

        /// <summary>
        /// Constructor, which create Static Instance of the FileManager
        /// </summary>
        public FileManager()
        {
            UsersFiles = new Dictionary<AbstractUser, List<FileData>>();
            _lock = new object();
        }


        /// <summary>
        /// Submit file to server storage
        /// </summary>
        /// <param name="user">the user, which submit file to the storage</param>
        /// <param name="info">Information about file</param>
        /// <param name="fileStream">string, which contain representation of file in bytes</param>
        /// <exception cref="ArgumentNullException">throw exception if arguments is null</exception>
        public static void SubmitFile(AbstractUser user, FileData info, string fileStream)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user), "user was null");
            if (info == null)
                throw new ArgumentNullException(nameof(info), "FileData was null");
            if (fileStream == null)
                throw new ArgumentNullException(nameof(fileStream), "FileStream was null");

            try
            { 
                byte[] byteArray = Encoding.UTF8.GetBytes(fileStream);
                SaveFile(GetFullFileName(user.Id, info.Type), byteArray);

            }
            catch (Exception)
            {
                throw new FileException();
            }

            if (!Instance.UsersFiles.ContainsKey(user))
                    Instance.UsersFiles.Add(user, new List<FileData>());
            FileData a;

            if ((a = Instance.UsersFiles[user].SingleOrDefault(n=>n.Type == info.Type)) != null)
                a.FileName = info.FileName;
            else Instance.UsersFiles[user].Add(info);         
        }


        /// <summary>
        /// Get file from server file storage
        /// </summary>
        /// <param name="user">the user, which request file from the storage</param>
        /// <param name="type">Type of the file, which was requested</param>
        /// <exception cref="FileException.UserDoesntHaveFilesException"> If user doesn't have any file</exception>
        /// <exception cref="FileException.UserDoesntHaveFileOfGivenTypeException">If file of given type doesn't exist in storage</exception>
        /// <returns>Wrapper, which contain FileData and it's representation in string</returns>
        public static FileDataWrapper GetFileData(AbstractUser user, string type)
        {
            string filename = GetFullFileName(user.Id, type);

            if (!Instance.UsersFiles.TryGetValue(user, out var list))
                throw new FileException.UserDoesntHaveFilesException();

            var fileData = list.SingleOrDefault(n => n.Type.Equals(type));

            return fileData != null
                ? new FileDataWrapper(fileData, LoadFileAsString(filename))
                : throw new FileException.UserDoesntHaveFileOfGivenTypeException(type);
        }


        /// <summary>
        /// Internal function for saving file
        /// </summary>
        /// <param name="filename">filename and also the path</param>
        /// <param name="fileStream">stream, from which bytes are reading to be written</param>
        private static void SaveFile(string filename, byte[] fileStream)
        {
            lock (Instance._lock)
            {
                Directory.CreateDirectory(filename.Remove(filename.LastIndexOf('/')));
                File.WriteAllBytes(filename, fileStream);
            }                 
        }


        /// <summary>
        /// Internal function for loading file as string
        /// </summary>
        /// <param name="filename">filename and also the path</param>
        /// <returns>string of byte, which represents file</returns>
        private static string LoadFileAsString(string filename)
        {
            lock (Instance._lock)
            {
                byte[] bytes = File.ReadAllBytes(filename);
                string fileString = Encoding.UTF8.GetString(bytes);
                return fileString;
            }
        }

    }
}
