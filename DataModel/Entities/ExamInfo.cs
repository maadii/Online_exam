using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Entitys
{
    public class ExamInfo
        
    {
        
        public int ID { get; set; }
        public string Title { get; set; }
        public string CenterName { get; set; }
        public int PhoneNumber { get; set; }
        public DateTime? ExamDate { get; set; }
         public virtual Author Author { get; set; }

    }
}
