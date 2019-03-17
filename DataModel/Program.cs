using System;
using DataModel.Authentication;
using DataModel.Data;
using DataModel.Users;

namespace DataModel
{
    class Program
    {
        static void Main(string[] args)
        {
            var token = AuthManager.RegisterUser(new AuthData("login", "password"), new[] {RootEnum.CreateUser});
            AuthManager.LogOutUser(token);
            token = AuthManager.AuthUser(new AuthData("login1", "password"));
            Console.WriteLine(AuthManager.ValidateAuthToken(token));
            try
            {
                AuthManager.AuthUser(new AuthData("login", "password"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }
    }
}
