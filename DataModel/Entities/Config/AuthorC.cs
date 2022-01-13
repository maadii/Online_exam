using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;


namespace DataModel.Entitys.Config
{
    class AuthorC:EntityTypeConfiguration<Author>
    {
        public  void AuthorConfig()
        {
            HasKey(a => a.ID);
            Property(a => a.Name).HasMaxLength(150).IsRequired();
            Property(a => a.LastName).HasMaxLength(150).IsRequired();
           // Property(a => a.Rank).IsRequired();

        }
    }
}
