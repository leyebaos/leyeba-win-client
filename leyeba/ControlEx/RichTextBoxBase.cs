using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ControlEx
{
    public partial class RichTextBoxBase : UserControl
    {
        public RichTextBoxBase()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics gh = e.Graphics;
            gh.SmoothingMode = SmoothingMode.HighQuality;
            gh.DrawRectangle(
                new Pen(Color.FromArgb(174, 198, 214)),
                new Rectangle(0, 0, Width - 1, Height - 1));
        }

        public override string Text
        {
            get
            {
                return richTextBox.Text;
            }
            set
            {
                richTextBox.Text = value;
            }
        }
    }
}
