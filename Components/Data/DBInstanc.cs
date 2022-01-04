using Components.Tools;
using DataModel;
using DataModel.Entitys;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Components.ExamGenerator;
using static DataModel.DBModel;

namespace Components
{
    public class DBInstanc
    {
        
        //use singleton pattern
        private static readonly DBInstanc instance = new DBInstanc();  
        
        static DBInstanc()
        {

        }
         private DBInstanc()
        {
        }
        public static DBInstanc Instance
        {

            get
            {
                return instance;
            }
        }
        /*return  Exam information and related author*/
        public ExamView GetExamInfos()
        {
            using (var ctx = new DBModel())
            {

                var ExamInfos = (from s in ctx.ExamInfos
                                 join e in ctx.Authors on s.Author.ID equals e.ID
                                 select new ExamView
                                 {
                                     Autor = e.Name + " " + e.LastName,
                                     CenterName = s.CenterName,
                                     ExamDate= s.ExamDate,
                                     ExamNumber = s.ID.ToString(),
                                     Phonenamber = s.PhoneNumber.ToString(),
                                     Title = s.Title

                                 }
                                 ).ToList();

                ctx.SaveChanges();
                Random rand = new Random();
                int r = rand.Next(0, ExamInfos.Count);
                return ExamInfos[r];
            }            
        }
        /*return result and related students*/
        public DataTable GetResult()
        {
            using (var ctx = new DBModel())
            {
                DataTable dt = new DataTable();
                var r = (from s in ctx.Results
                                 join e in ctx.Students on s.Student.ID equals e.ID
                                 select new
                                 {
                                     ExamTime = s.ResultDate,
                                     Name = e.Name + " " + e.LastName,
                                     Corect = (s.ResultNumber).ToString(),
                                     SpendTime= s.SpendTime,
                                     Wrong= (3 - s.ResultNumber).ToString()
                                 }).ToList();
                ctx.SaveChanges();
              
                ListtoDataTableConverter converter = new ListtoDataTableConverter();
                 dt = converter.ToDataTable(r);
                return dt;
            }
        }
        /*set  students*/
        public void SetStudent(Student student)
        {
            using (var ctx = new DBModel())
            {
                ctx.Students.Add(student);
                ctx.SaveChanges();
            }
        }
        /*set  Result*/
        public void SetResults(Result studentresults)
        {
            using (var ctx = new DBModel())
            {
                ctx.Results.Add(studentresults);
                ctx.SaveChanges();
            }
        }
        /*Get list of Qusetion by Type*/
        public  List<Question> GetQuestion(QuestionType type)
        {
         

                using (var ctx = new DBModel())
                {

                    var Questions = (from s in ctx.Questions
                                     where s.QuestionType == type
                                     select s
                                     ).ToList();

                    ctx.SaveChanges();

                    return Questions;
                }
        }
        /*Get list of  all Multiple-Choice Qusetion by Type*/
        public List<QuestionView> GetMultipleChoice()
        {

            using (var ctx = new DBModel())
            {

                var MultipleChoiceq = (from s in ctx.Questions
                                       join e in ctx.MultipleChoices on s.ID equals e.ID
                                       select new QuestionView
                                       {
                                           ID = e.ID,
                                           Hardness = s.Hardnes,
                                           Hints = s.Hint,
                                           Titel = s.Title,
                                           Answers = new List<string> {e.RightAnswer,e.WrongAnswer1,e.WrongAnswer2,e.WrongAnswer3}
                                       }
                                 ).ToList();

                ctx.SaveChanges();

                return MultipleChoiceq;
            }

        }
        public int GetMultipleChoiceby(int ID,string rightAnswer)
        {

            using (var ctx = new DBModel())
            {

                var MultipleChoiceq = (from e in ctx.MultipleChoices
                                       where e.ID == ID && e.RightAnswer.Equals(rightAnswer)
                                       select e
                                           ).ToList();
                int c = MultipleChoiceq.Count;
                ctx.SaveChanges();

                return c;
            }

        }
    }
}
