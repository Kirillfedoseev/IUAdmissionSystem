using System;
using Model.Data;

namespace Model.Authentication
{
    public class FileException:Exception
    {
        public FileException() : base("Something went wrong with FileManager") { }

        protected FileException(string message) : base(message) { }




        public class UserDoesntHaveFileOfGivenTypeException : FileException
        {
            public UserDoesntHaveFileOfGivenTypeException(string type) : base($"The user doesn't have file of given type: {type}!"){}
        }

        public class UserDoesntHaveFilesException : FileException
        {
            public UserDoesntHaveFilesException() : base($"The user doesn't have any files!") { }
        }

    }

   
}