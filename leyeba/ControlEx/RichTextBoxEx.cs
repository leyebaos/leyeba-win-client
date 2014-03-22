using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Util;

namespace ControlEx
{
    public class RichTextBoxEx
    {
        private const int WM_USER = 0x400;
        private const int EM_SETCHARFORMAT = WM_USER + 68;

        public static void SetSelectTextAsLink(RichTextBox rtb, bool link)
        {
            if (rtb == null)
            {
                throw new ArgumentNullException("rtb");
            }

            Win32API.CHARFORMAT2 charformat;
            charformat = new Win32API.CHARFORMAT2();
            charformat.dwMask = 0x00000020;//CFM_LINK
            charformat.dwEffects = link ? 0x20/*CFE_LINK*/ : 0;
            Win32API.SendMessage(rtb.Handle, EM_SETCHARFORMAT, 1/*SCF_SELECTION*/, charformat);
        }
    }
}
