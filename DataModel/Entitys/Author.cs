using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataModel.DBExam;



namespace DataModel.Entitys
{
    public class Author
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public GenderType Gender { get; set; }
        public Ranks Rank { get; set; }
        public string Nationality { get; set; }

    }
}
