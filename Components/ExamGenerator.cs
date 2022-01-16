using DataModel.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataModel.DBModel;

namespace Components
{
     public class ExamGenerator
    {
        private static readonly ExamGenerator instance = new ExamGenerator();
        static ExamGenerator()
        {

        }
        private ExamGenerator()
        {
        }
        public static ExamGenerator Instance
        {

            get
            {
                return instance;
            }
        }
        public struct Qustions
        {
            public string Titel { get; set; }
            public Hardness Hardness;
            public string Hints { get; set; }
        }
    
        public Qustions GetQustion(QuestionType qu)
        {
            Qustions Q = new Qustions();
            Random rand = new Random();;
            List<Question> result = DBInstanc.Instance.GetQuestion(qu);
            int r = rand.Next(0, result.Count);
            Q.Hardness = result[r].Hardnes;
            Q.Hints = result[r].Hint;
            Q.Titel = result[r].Title;
            return Q;
        }
        public List<QuestionView> GetQustion()
        {
           List<QuestionView> Q = new List<QuestionView>();
           List<QuestionView> Qt = new List<QuestionView>();
           Q = DBInstanc.Instance.GetMultipleChoice();
           var sequence = Enumerable.Range(0, Q.Count).OrderBy(n => n * n * (new Random()).Next());
           var results = sequence.Distinct().Take(3).ToList();
            foreach (var item in results)
            {
                Qt.Add(Q[item]);
            }       
            return Q;
        }
    }
}
