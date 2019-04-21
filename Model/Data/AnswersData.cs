using System;
using System.Collections.Generic;
using System.Text;
using Model.Data;

namespace Model.Data
{
    class AnswersData: IData
    {

        public int TestId { get; set; }
        //TODO add question answer pair

        
        #region IData
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
        #endregion
    }
}
