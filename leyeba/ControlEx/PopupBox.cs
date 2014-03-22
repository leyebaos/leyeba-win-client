using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlEx
{
    public static class PopupBox
    {
        public static void Alert(string text, string caption)
        {
            PromptBox prompt = new PromptBox(PromptBoxIcon.Information, text, caption);
            prompt.TopMost = true;
            prompt.ShowDialog();
        }

        public static DialogResult Question(string text, string caption)
        {
            PromptBox prompt = new PromptBox(PromptBoxIcon.Question, text, caption);
            return prompt.ShowDialog();
        }

        public static DialogResult Question(string text, string caption, FormStartPosition pos)
        {
            PromptBox prompt = new PromptBox(PromptBoxIcon.Question, text, caption);
            prompt.StartPosition = pos;
            return prompt.ShowDialog();
        }
    }
}
