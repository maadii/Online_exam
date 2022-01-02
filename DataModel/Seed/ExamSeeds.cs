using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Entitys;

namespace DataModel.Seed
{
    class ExamSeeds: DropCreateDatabaseAlways<DBModel>
    {
        protected override void Seed(DBModel context)
        {
            IList<ExamInfo> defaultExamInfo = new List<ExamInfo>();
            IList<Question> defaultQuestion = new List<Question>();
            IList<MultipleChoice> defaultMultipleChoice = new List<MultipleChoice>();
            IList<Result> defaultResults = new List<Result>();
            /*
             * Add Exam inforamtion to  Database
             */
            defaultExamInfo.Add(new ExamInfo() { Title = "Ielts Center", CenterName = "C Center",
                Author = new Author() 
                {
                Name="Nader",
                LastName="Ghorbani",
                Nationality= " iranian", 
                Rank= DBModel.Ranks.Junior,
                Gender= DBModel.GenderType.Male }, 
                ExamDate = DateTime.Now, PhoneNumber = 22285173 });
            defaultExamInfo.Add(new ExamInfo() { Title = "toefl Center", CenterName = "First Center", 
                Author = new Author() 
            {
                Name = "Nasim",
                LastName = "Jafari",
                Nationality = " iranian",
                Rank = DBModel.Ranks.Senior,
                Gender = DBModel.GenderType.Female
            }
            , ExamDate = DateTime.UtcNow, PhoneNumber = 44237546 });

            /*
            * Add Question to  Database
            */
            defaultQuestion.Add(new Question()
            {
             QuestionType = DBModel.QuestionType.Descriptive,
             Title= "In some countries young people are encouraged to work or travel for a year between finishing high school and starting university studies." +
             "Discuss the advantages and disadvantages for young people who decide to do this. ",
             Hardnes = DBModel.Hardness.Advanced,
              Hint= "Give reasons for your answer and include any relevant examples",
               
            });
            defaultQuestion.Add(new Question()
            {
                QuestionType = DBModel.QuestionType.Speaking,
                Title = "Do any colours have a special meaning in your culture? ",
                Hardnes = DBModel.Hardness.Hard,
                Hint = "You should talk for more than 2 minutes. ",

            });
            defaultQuestion.Add(new Question()
            {
                QuestionType = DBModel.QuestionType.Descriptive,
                Title = "How has technology affected the kinds of music popular with young people? ",
                Hardnes = DBModel.Hardness.Easy,
                Hint = "Discuss the advantages and disadvantages",

            });
            defaultQuestion.Add(new Question()
            {
               QuestionType = DBModel.QuestionType.Speaking,
                Title = "How does the media in your country treat famous people?",
                Hardnes = DBModel.Hardness.Advanced,
                Hint = "You should talk for more than 3 minutes. ",

            });
            /*
            * Add Exam inforamtion to  MultipleChoice
            * 
            */
            defaultMultipleChoice.Add(new MultipleChoice()
            {
                
                    QuestionType = DBModel.QuestionType.MultipleChoice,
                  Title = "How many siblings ________?",
                    Hardnes = DBModel.Hardness.Easy,
                    Hint = "You have to choose one. ",
                
                RightAnswer= " both (A, B) ",
                 WrongAnswer1= "did you had",
                 WrongAnswer2= " have you gotten",
                  WrongAnswer3= "do you have",
                   
               

            });

            defaultMultipleChoice.Add(new MultipleChoice()
            {
              
                    QuestionType = DBModel.QuestionType.MultipleChoice,
                   Title = "The superlative form of far is_______.",
                   Hardnes = DBModel.Hardness.Easy,
                 Hint = "You have to choose one. ",
             
                RightAnswer = " farthest ",
                WrongAnswer1 = " farther",
                WrongAnswer2 = " more far",
                WrongAnswer3 = "most far",



            });
            defaultMultipleChoice.Add(new MultipleChoice()
            {
              
                  QuestionType = DBModel.QuestionType.MultipleChoice,
                   Title = "She______ the dishes yesterday.",
                   Hardnes = DBModel.Hardness.Easy,
                    Hint = "You have to choose one. ",
               
                RightAnswer = " washed ",
                WrongAnswer1 = " washes",
                WrongAnswer2 = " wash",
                WrongAnswer3 = "has have",



            });
            defaultMultipleChoice.Add(new MultipleChoice()
            {

                QuestionType = DBModel.QuestionType.MultipleChoice,
                Title = "Katie looked very tired because she __ the trombone for so long.",
                Hardnes = DBModel.Hardness.Easy,
                Hint = "You have to choose one. ",

                RightAnswer = "had been practising ",
                WrongAnswer1 = "has practised",
                WrongAnswer2 = "did practise",
                WrongAnswer3 = "has have",



            });

            defaultResults.Add (new Result(){
                ResultDate = DateTime.Today,
                SpendTime = "3:30",
                Student = new Student() { Name = "moien", LastName = "vdfv", NationalCode = 1235535 },
            });

            context.Questions.AddRange(defaultQuestion);
            context.ExamInfos.AddRange(defaultExamInfo);
            context.MultipleChoices.AddRange(defaultMultipleChoice);
            context.Results.AddRange(defaultResults);

            base.Seed(context);
        }
    }
}
