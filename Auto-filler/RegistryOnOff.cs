using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_filler
{
    public class RegistryOnOff
    {
        public static string BaseDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        RegistryKey reg;
        public void AutoStartOn()
        {

            reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            reg.SetValue("Auto filler", System.IO.Path.GetFullPath(BaseDir));            
        }

        public void AutoStartOff()
        {
            try
            {
                reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                reg.DeleteValue("Auto filler");
            }
            catch (ArgumentException e)
            {

            }
        }
    }
}
