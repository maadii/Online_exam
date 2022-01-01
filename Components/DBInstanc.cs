using DataModel;
using DataModel.Entitys;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Components
{
    public class DBInstanc
    {
        //use singleton pattern
        private static readonly DBInstanc instance = new DBInstanc();
        
        
        static DBInstanc()
        {

        }
        private DBInstanc()
        {
            
        }
        public static DBInstanc Instance
        {

            get
            {
                return instance;
            }
        }
        public void Ini()
        {
            using (var ctx = new DBModel())
            {
                var stud = new Result() { ResultDate = DateTime.Today, SpendTime = "3:30", Student = new Student() { Name = "moien", LastName = "vdfv", NationalCode = 1235535 } };

                ctx.Results.Add(stud);
                ctx.SaveChanges();
            }
        }
        /*return  Exam information and related author*/
        public ExamView GetExamInfos()
        {

            using (var ctx = new DBModel())
            {

                var ExamInfos = (from s in ctx.ExamInfos
                                 join e in ctx.Authors on s.Author.ID equals e.ID
                                 select new ExamView
                                 {
                                     Autor = e.Name + " " + e.LastName,
                                     CenterName = s.CenterName,
                                     ExamDate = s.ExamDate.ToString(),
                                     ExamNumber = s.ID.ToString(),
                                     Phonenamber = s.PhoneNumber.ToString(),
                                     Title = s.Title

                                 }
                                 ).ToList();

                ctx.SaveChanges();

                Random rand = new Random();
                int r = rand.Next(0, ExamInfos.Count);
                return ExamInfos[r];
            }

        }
        public DataTable GetResult()
        {
            using (var ctx = new DBModel())
            {
                DataTable dt = new DataTable();
                var r = (from s in ctx.Results
                                 join e in ctx.Students on s.Student.ID equals e.ID
                                 select new
                                 {
                                     ExamTime = s.ResultDate,
                                     Name = e.Name + " " + e.LastName,
                                     Corect = (s.ResultNumber).ToString(),
                                     SpendTime= s.SpendTime,
                                     Wrong= (4 - s.ResultNumber).ToString()

                                 }
                                 ).ToList();

                ctx.SaveChanges();

                ListtoDataTableConverter converter = new ListtoDataTableConverter();
                 dt = converter.ToDataTable(r);
                return dt;
            }
        }
        public class ListtoDataTableConverter
        {
            public DataTable ToDataTable<T>(List<T> items)
            {
                DataTable dataTable = new DataTable(typeof(T).Name);
                //Get all the properties
                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in Props)
                {
                    //Setting column names as Property names
                    dataTable.Columns.Add(prop.Name);
                }
                foreach (T item in items)
                {
                    var values = new object[Props.Length];
                    for (int i = 0; i < Props.Length; i++)
                    {
                        //inserting property values to datatable rows
                        values[i] = Props[i].GetValue(item, null);
                    }
                    dataTable.Rows.Add(values);
                }
                //put a breakpoint here and check datatable
                return dataTable;
            }
        }
    }
}
