using DataModel;
using DataModel.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_exam
{
     public class DataLogic
    {
       public void CreateInstance()
        {

            using (var ctx = new DBModel())
            {
                var stud = new Result() { ResultDate = DateTime.Today, SpendTime = "3:30", Student = new Student() { Name = "moien", LastName = "vdfv", NationalCode = 1235535 } };

                ctx.Results.Add(stud);
                ctx.SaveChanges();
            }
        }
        public data
    }
}
