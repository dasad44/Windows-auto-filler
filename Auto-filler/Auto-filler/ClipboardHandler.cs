using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Auto_filler
{
    class ClipboardHandler
    {

        string text = "";
        public string SaveText()
        {
            text = Clipboard.GetText();
            //Clipboard.SetDataObject(text);
            return text;
        }


    }
}
