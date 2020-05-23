using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Auto_filler
{
    class ClipboardHandler
    {
        //Clipboard formats
        private static readonly string[] clipboardMetaFormats = { "application/x-moz-nativeimage", "FileContents", "EnhancedMetafile", "System.Drawing.Imaging.Metafile", "MetaFilePict", "Object Descriptor", "ObjectLink", "Link Source Descriptor", "Link Source", "Embed Source", "Hyperlink" };
        string text = "";
        public string SaveText()
        {
            text = Clipboard.GetText();
            //Clipboard.SetDataObject(text);
            return text;
        }

        public void UpdateClipboard(DataObject data)
        {
            if (data == null) return;
            try
            {
                Clipboard.SetDataObject(data);
            }
            catch (ExternalException ex)
            {
                Debug.WriteLine($"Error {ex.ErrorCode}: {ex.Message}");
            }
        }

        //format IdataObject to DataObject
        public DataObject ReadClipboard(IDataObject d)
        {
            DataObject result = new DataObject();
            d = Clipboard.GetDataObject();
            string[] formats = d.GetFormats()?.Except(clipboardMetaFormats).ToArray() ?? Array.Empty<string>();
            foreach (string format in formats)
            {
                try
                {
                    object data = d.GetData(format);
                    if (data != null) result.SetData(format, data);
                }
                catch (ExternalException ex)
                {
                    Debug.WriteLine($"Error {ex.ErrorCode}: {ex.Message}");
                }
            }
            return result;
        }


    }
}
