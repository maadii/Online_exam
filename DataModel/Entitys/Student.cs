using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataModel.DBExam;


namespace DataModel.Entitys
{
    public class Student
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public GenderType Genders { get; set; }
        public int NationalCode { get; set; }
        public DateTime Birthdate { get; set; }
    }
}
