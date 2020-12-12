using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Auto_filler
{
    class NumberOperation
    {

        public static int ToPositive(int number)
        {
            if(number < 0)
            {
                return number * -1;
            }
            else
            {
                return 0;
            }
        }
    }
}
