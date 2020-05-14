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
                else //dla http nie działa, nie wiemy jak zrobić żeby działało z http, ale nie jest to aż tak istotne, 99% internetu to https
                {                   
                    Process.Start("https://" + link);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Coś poszło nie tak :O. Sprawdź poprawność linku lub czy strona istnieje!");
                
            }


        }
    }
}