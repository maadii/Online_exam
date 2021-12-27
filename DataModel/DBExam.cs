using DataModel.Entitys;
using DataModel.Entitys.Config;
using System;
using System.Data.Entity;
using System.Linq;

namespace DataModel
{
    public class DBExam : DbContext
    {
        public DBExam() : base("DBExam")
        {
        }
        public enum GenderType { Male, Female, Other }
            public enum Ranks { Senior, Junior }
            public enum Hardness { Easy, Hard, Advanced }

            public virtual DbSet<Author> Authors { get; set; }
            public virtual DbSet<Student> Students { get; set; }
           // public virtual DbSet<ExamInfo> ExamInfos { get; set; }


            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
            modelBuilder.Configurations.Add(new AuthorC());
            modelBuilder.Configurations.Add(new ExamInfoC());
            base.OnModelCreating(modelBuilder);
            }

    }
}