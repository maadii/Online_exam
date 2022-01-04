using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.Tools
{
    public  class CESTime
    {
        private static readonly CESTime instance = new CESTime();

        static CESTime()
        {

        }
        private CESTime()
        {
        }
        public static CESTime Instance
        {

            get
            {
                return instance;
            }
        }
        public DateTime ChangeZone(DateTime? date = null)
        {
            
            TimeSpan ts = new TimeSpan(1, 30, 0);
            DateTime t = (DateTime)date - ts;
            return t;
        }
        
    }
}
