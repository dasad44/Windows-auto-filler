using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Auto_filler
{
    class CatchLink
    {
        public void LinkOpen(string link)
        {
            string protocol;
            try
            {
                protocol = link.Substring(0, 7);
                if (protocol == "https:/" || protocol == "http:/")
                {

                    Process.Start(link);

                }
                else
                {                   
                    Process.Start("https://" + link);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Coś poszło nie tak :O. Sprawdź poprawność linku lub czy strona istnieje!");
                
            }


        }
    }
}