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
                if (protocol == "https:/")
                {
                    Process.Start(link);
                }
                else if (protocol == "http:/")
                {
                    Process.Start(link);
                }
                else
                {
                    Process.Start("https://" + link);
                }
                           
            }
            catch (Exception e)
            {
                Process.Start("https://" + link);

            }

        }
        public void LinkOpen2(string link2)
        {
            string protocol2;
            try
            {
                protocol2 = link2.Substring(0, 7);
                if (protocol2 == "https:/")
                {
                    Process.Start(link2);
                }
                else if (protocol2 == "http:/")
                {
                    Process.Start(link2);
                }
                else
                {
                    Process.Start("https://" + link2);
                }

            }
            catch (Exception e)
            {
                Process.Start("https://" + link2);

            }


        }
        public void LinkOpen3(string link3)
        {
            string protocol3;
            try
            {
                protocol3 = link3.Substring(0, 7);
                if (protocol3 == "https:/")
                {
                    Process.Start(link3);
                }
                else if (protocol3 == "http:/")
                {
                    Process.Start(link3);
                }
                else
                {
                    Process.Start("https://" + link3);
                }

            }
            catch (Exception e)
            {
                Process.Start("https://" + link3);

            }


        }
        public void LinkOpen4(string link4)
        {
            string protocol4;
            try
            {
                protocol4 = link4.Substring(0, 7);
                if (protocol4 == "https:/")
                {
                    Process.Start(link4);
                }
                else if (protocol4 == "http:/")
                {
                    Process.Start(link4);
                }
                else
                {
                    Process.Start("https://" + link4);
                }

            }
            catch (Exception e)
            {
                Process.Start("https://" + link4);

            }


        }
        public void LinkOpen5(string link5)
        {
            string protocol5;
            try
            {
                protocol5 = link5.Substring(0, 7);
                if (protocol5 == "https:/")
                {
                    Process.Start(link5);
                }
                else if (protocol5 == "http:/")
                {
                    Process.Start(link5);
                }
                else
                {
                    Process.Start("https://" + link5);
                }

            }
            catch (Exception e)
            {
                Process.Start("https://" + link5);

            }


        }

    }
}