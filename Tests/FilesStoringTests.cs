using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Data;
using Model.Files;
using Model.Users;

namespace Tests
{
    [TestClass]
    public class FilesStoringTests
    {
        [TestMethod]
        public void SubmitFileCompleted()
        {
            string file =
                "jhgadflnbijsdfgnbmoisdfgbnmojsdfgbnmosfgnmodgfosfnmosfgmnosfmnoiksfgmbnoisgnhoigsdfxnmoisdrftmnoisfrmnoisfgnm[isofgnmoiksfrnmosifgtnmoiskfgnmsfr";
          
            AbstractUser user = new TestUser(new RootEnum[0]);
            FileData data = new FileData{Type = "CV", FileName = "test.txt"};
            FileManager.SubmitFile(user, data, file);
            var wrapper= FileManager.GetFileData(user, "CV");

            Assert.IsTrue(data.Equals(wrapper.Data) && file.Equals(wrapper.Bytes));         
          
            File.Delete("tests.bytes");
        }


        //[TestMethod]
        //public void GetFileWithoutRoots()
        //{          
        //    TokenData tokenData = new TokenData("123");

        //    try
        //    {
        //        var b = DataModelFacade.GetFile(tokenData, FileTypes.CV);
        //        Assert.Fail("Was get without token!");
        //    }
        //    catch (TokenExceptions.TokenDoesNotExists e)
        //    {
        //    }           
        //}


        //[TestMethod]
        //public void SubmitFileWithoutRoots()
        //{
        //    TokenData tokenData = new TokenData("123");

        //    try
        //    {
        //        DataModelFacade.SubmitFile(tokenData, FileTypes.CV, new MemoryStream());
        //        Assert.Fail("Was submited without token!");
        //    }
        //    catch (TokenExceptions.TokenDoesNotExists e)
        //    {
        //    }
        //}


        //[TestMethod]
        //public void ResendFileCompleted()
        //{

        //    AuthData authData = new AuthData("test", "test");
        //    TokenData tokenData = AuthManager.RegisterUser(authData, new RootEnum[0]);

        //    File.WriteAllBytes("tests.bytes", new byte[] { 234, 4, 3, 2, 4, 5, 2, 3, 5, 6, 23, 5, 5, 32, 3, 5, 4, 3, 6, 7, 5, 7, 65, 7, 3, 7, 37, 5, 67, 7, 58, 9 });
        //    bool equal = true;

        //    using (Stream a = File.OpenRead("tests.bytes"))
        //    {
        //        DataModelFacade.SubmitFile(tokenData, FileTypes.CV, a);

        //        DataModelFacade.SubmitFile(tokenData, FileTypes.CV, a);

        //        var b = DataModelFacade.GetFile(tokenData, FileTypes.CV);

        //        Assert.IsTrue(a.Length == b.Length);
        //        a.Seek(0, SeekOrigin.Begin);
        //        b.Seek(0, SeekOrigin.Begin);
        //        for (int i = 0; i < a.Length; i++)
        //        {
        //            if (a.ReadByte() != b.ReadByte())
        //            {
        //                equal = false;
        //                break;
        //            }
        //        }

        //    }
        //    File.Delete("tests.bytes");
        //    Assert.IsTrue(equal);

        //}

    }
}
