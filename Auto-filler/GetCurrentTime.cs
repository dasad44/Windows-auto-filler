using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_filler
{
    class GetCurrentTime
    {
        public string GetTime()
        {
            DateTime localDate = DateTime.Now;
            String Month = localDate.Month.ToString();
            String Day = localDate.Day.ToString();
            String Hour = localDate.Hour.ToString();
            String Minute = localDate.Minute.ToString();
            String Second = localDate.Second.ToString();
            if (localDate.Month < 10)
            {
                Month = "0" + Month;
            }
            if (localDate.Day < 10)
            {
                Day = "0" + Day;
            }
            if (localDate.Hour < 10)
            {
                Hour = "0" + Hour;
            }
            if (localDate.Minute < 10)
            {
                Minute = "0" + Minute;
            }
            if (localDate.Second < 10)
            {
                Second = "0" + Second;
            }
            String Date = localDate.Year.ToString() + "." + Month +
                 "." + Day + "_" + Hour + "-" + Minute + "-" + Second;
            return Date;
        }
    }
}
