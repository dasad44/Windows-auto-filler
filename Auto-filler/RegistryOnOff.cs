﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_filler
{
    class RegistryOnOff
    {
        RegistryKey reg;
        public void AutoStartOn()
        {

            reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            reg.SetValue("Auto filler", System.IO.Path.GetFullPath("Auto-filler.exe"));            
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
