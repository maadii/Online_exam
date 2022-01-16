using DataModel.Entitys;
using DataModel.Entitys.Config;
using System;
using System.Data.Entity;
using DataModel.Seed;
using System.Linq;

namespace DataModel
{
    public class DBModel : DbContext
    {
        // Your context has been configured to use a 'DBModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'DataModel.DBModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'DBModel' 
        // connection string in the application configuration file.
        public DBModel()
            : base("name=DBModel")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DBModel, DataModel.Migrations.Configuration>());

            Database.SetInitializer(new ExamSeeds());
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        public enum GenderType { Male, Female, Other }
        public enum Ranks { Senior, Junior }
        public enum Hardness { Easy, Hard, Advanced }

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<ExamInfo> ExamInfos { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Result> Results { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AuthorC());
            modelBuilder.Configurations.Add(new ExamInfoC());
            base.OnModelCreating(modelBuilder);
        }

    }
}