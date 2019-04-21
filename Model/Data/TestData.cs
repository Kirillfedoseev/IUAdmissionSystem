namespace Model.Data
{
public class TestData : IData
    {

        public int TestId { get; set; }
        public string Name { get; set; }
        public string Program { get; set; }
        public Question[] Questions { get; set; }

        public class Question : IData
        {
            public int QuestionID;
            public string QuestionText;
            public Answer[] Anwers;
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

        public class Answer : IData
        {

            public int AnswerID;
            public string AnswerText { get; set; }
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
}