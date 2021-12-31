using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataModel;
using DataModel.Entitys;

namespace Online_exam
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var ctx = new DBModel())
            {
                var stud = new Author() { Name = "Bill",Rank=DBModel.Ranks.Junior,Gender= DBModel.GenderType.Male };

               ctx.Authors.Add(stud);
                ctx.SaveChanges();
            }
        }
    }
}
