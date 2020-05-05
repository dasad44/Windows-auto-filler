using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Auto_filler
{
    class CatchLink
    {
        public void LinkOpen(string link)
        {
            try
            {
                Process.Start(link);
            }
            catch(InvalidOperationException e)
            {
                MessageBox.Show("Wpisałeś/aś zły link!");
            }
        }
    }
}
