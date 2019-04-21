namespace Model.Data
{
    public interface IData
    {
        
        /// <summary>
        /// Type of the data, restircted to be the same name as C# class
        /// e.g. AuthData(login, password), PassportData(series, number etc)
        /// </summary>
        string Type { get; }
        
        
        /// <summary>
        /// Data of type specified in <see cref="Type"/>
        /// e.g. if AuthData then (login, password), if PassportData then (series, number etc)
        /// all data stores in JSON foramte, aka
        /// <code>
        /// {
        ///     login: "Kirill",
        ///     password: "123123"
        /// }
        /// </code>
        /// </summary>
        string Data { get; }

        
        /// <summary>
        /// the function, which serialize current instance of IData succesor to JSON
        /// </summary>
        /// <returns>JSON formatted instance</returns>
        string SerializeToJSON();


        /// <summary>
        /// Desirialize JSON object to IData instance
        /// </summary>
        /// <param name="json">passsed json object as string </param>
        /// <returns>IData instance</returns>
        IData DeserializeFromJSON(string json);
        
        

    }

    class TestData : IData
    {
        public string Type { get; }
        public string Data { get; }



        public string SerializeToJSON()
        {
            throw new System.NotImplementedException();
        }

        public IData DeserializeFromJSON(string json)
        {
            throw new System.NotImplementedException();
        }
    }

    public class ProgramData : IData
    {
        

        public int ID { get; }

        public string Type => typeof(ProgramData).Name;

        public string Data { get; }

        public string ProgramName { get; set; }

        public string Description { get; set; }

        public ProgramData(int id, string programName, string description)
        {
            ID = id;
            ProgramName = programName;
            Description = description;
        }

        public string SerializeToJSON()
        {
            throw new System.NotImplementedException();
        }

        public IData DeserializeFromJSON(string json)
        {
            throw new System.NotImplementedException();
        }
    }
}