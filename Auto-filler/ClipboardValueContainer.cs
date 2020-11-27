using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Auto_filler
{
    public static class ClipboardValueContainer
    {
        public static DataObject clipboard_1 { get; set; }
        public static DataObject clipboard_2 { get; set; }
        public static DataObject clipboard_3 { get; set; }

        public static string text_1 { get; set; }
        public static string text_2 { get; set; }
        public static string text_3 { get; set; }
    }
}
