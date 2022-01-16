using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Components.ExamGenerator;
using static DataModel.DBModel;

namespace Components
{
    public class QuestionView
    {
        public int ID;
        public string Titel;
        public Hardness Hardness;
        public string Hints;
        public List<string> Answers { get; set;}
    }   
}
