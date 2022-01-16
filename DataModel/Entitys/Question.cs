using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using static DataModel.DBModel;

namespace DataModel.Entitys
{
    public class Question
    {
        public int ID { get; set; }
        public Hardness Hardnes { get; set; }
        public string Title { get; set; }
        public string Hint { get; set; }
    }
}
