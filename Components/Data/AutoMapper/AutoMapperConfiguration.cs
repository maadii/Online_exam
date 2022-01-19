using AutoMapper;
using DataModel.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.Data.AutoMapper
{
    public  class AutoMapperConfiguration
    {
        //use singleton pattern
        private static readonly AutoMapperConfiguration instance = new AutoMapperConfiguration();
        static AutoMapperConfiguration()
        {

        }
        private AutoMapperConfiguration()
        {
        }
        public static AutoMapperConfiguration Instance
        {

            get
            {
                return instance;
            }
        }

        public static void Configure()
        {
            ConfigureUserMapping();
        }

        private static void ConfigureUserMapping()
        {
            var config = new MapperConfiguration(cfg =>
             {
                 cfg.CreateMap<ExamInfo, ExamView>();
             });
            var mapper = config.CreateMapper();
        }
    }
}
