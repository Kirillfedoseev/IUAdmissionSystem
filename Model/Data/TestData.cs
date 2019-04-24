namespace Model.Data
{
    public class TestData : IData
    {
        public int TestId { get; set; }


        public int ProgramId {get; set;}
        public string Name { get; set; }

        public Question[] Questions { get; set; }

        public struct Question
        {
            public int QuestionId { get; set; }

            public string QuestionText;

            public Answer[] Answers;

            public struct Answer 
            {
                public int AnswerId { get; set; }

                public string AnswerText { get; set; }

                public bool IsCorrect { get; set; }             
            }

        }

        public void SetIds(int id)
        {
            TestId = id;

            for (var questionId = 0; questionId < Questions.Length; questionId++)
            {
                Questions[questionId].QuestionId = questionId;

                for (int answerId = 0; answerId < Questions[questionId].Answers.Length; answerId++)
                {
                    Questions[questionId].Answers[answerId].AnswerId = answerId;
                }
            }
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