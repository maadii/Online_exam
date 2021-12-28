using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;

namespace DataModel.Entitys.Config
{
    class ExamInfoC: EntityTypeConfiguration<ExamInfo>
    {

        public void ExamInfoConfig()
        {
            HasKey(a => a.ID);
            Property(a => a.ExamDate).IsRequired();
          
        }
    }
}
