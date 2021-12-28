﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Entitys
{
    public class Result
    {
        public int ResultId { get; set; }
        public DateTime? ResultDate { get; set; }
        public string Section { get; set; }
        public ExamInfo ExamInfo { get; set; }

        public Student Student { get; set; }
    }
}