using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;

namespace DataModel.Entitys.Config
{
    class QuestionC:EntityTypeConfiguration<Question>
    {
        public void QuestionConfig()
        {
            HasKey(a => a.ID);
            Property(a => a.Title).IsRequired();
            

        }
    }
}
