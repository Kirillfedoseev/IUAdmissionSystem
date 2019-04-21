using Model.Users;

namespace Model.Data
{
    public class RegistrationData : AuthData
    {
#pragma warning disable CS0108 // "RegistrationData.Type" скрывает наследуемый член "AuthData.Type". Если скрытие было намеренным, используйте ключевое слово new.
        public string Type { get; } = typeof(RegistrationData).ToString();
#pragma warning restore CS0108 // "RegistrationData.Type" скрывает наследуемый член "AuthData.Type". Если скрытие было намеренным, используйте ключевое слово new.

        public new string Data => "json";


        public string email;
        public long number;
        public string citezenship;
        public string acknowledgment;

        public RootEnum RootType;


#pragma warning disable CS0108 // "RegistrationData.SerializeToJSON()" скрывает наследуемый член "AuthData.SerializeToJSON()". Если скрытие было намеренным, используйте ключевое слово new.
        public string SerializeToJSON()
#pragma warning restore CS0108 // "RegistrationData.SerializeToJSON()" скрывает наследуемый член "AuthData.SerializeToJSON()". Если скрытие было намеренным, используйте ключевое слово new.
        {
            //            {
            //                type:"type",
            //                Data:
            //                {
            //                    data1:"data",
            //                    data2:"data"
            //                }
            //            }

            throw new System.NotImplementedException();
        }

        public IData DeserializeFromJSON()
        {
            throw new System.NotImplementedException();
        }

        public RegistrationData(string login, string password) : base(login, password)
        {
            RootType = RootEnum.Candidate;
        }
    }
}