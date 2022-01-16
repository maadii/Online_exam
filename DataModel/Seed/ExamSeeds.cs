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

            context.ExamInfos.AddRange(defaultExamInfo);

            base.Seed(context);
        }
    }
}
