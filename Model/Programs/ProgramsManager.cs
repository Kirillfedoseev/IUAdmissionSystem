using System.Collections.Generic;
using Model.Support;

namespace Model.Programs
{
    public class ProgramsManager : Singletone<ProgramsManager> 
    {
        private List<AbstractProgram> _programs { get; set; }



        public ProgramsManager()
        {
            _programs = new List<AbstractProgram>();
        }


         


    }
}