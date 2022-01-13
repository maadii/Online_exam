using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Entitys
{
    public class Result
    {
        public int ResultId { get; set; }
        public DateTime? ResultDate { get; set; }
        public string SpendTime { get; set; }
        public int ResultNumber { get; set; }
        public int ExamNumber { get; set; }

        public Student Student { get; set; }
    }
}
