using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Auto_filler.MouseHook;

namespace Auto_filler
{
    class SnippingTool
    {
        private ScreenshotSave _screenSave;

        public void snippingTool(POINT startV, POINT endV)
        {
            MouseHook.stop();
            _screenSave = new ScreenshotSave();
            _screenSave.CaptureMyScreen(startV, endV);
            
        }
    }
}
