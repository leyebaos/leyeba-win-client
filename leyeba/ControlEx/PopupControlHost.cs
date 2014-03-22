using System;
using System.Windows.Forms;
using System.Drawing;

namespace ControlEx
{
    internal class PopupControlHost : ToolStripDropDown
    {
        private ListBoxBase content;
        public PopupControlHost(ListBoxBase content)
            : base()
        {
            if (content == null)
                return;
            DoubleBuffered = true;
            ResizeRedraw = true;
            AutoSize = false;
            Padding = Padding.Empty;
            Margin = Padding.Empty;
            this.content = content;
            this.content.SelectedChanged += new EventHandler(content_SelectedChanged);
            initControlHost();            
        }

        void content_SelectedChanged(object sender, EventArgs e)
        {
            this.Close();
        }

        private void initControlHost()
        {
            content.BorderStyle = BorderStyle.FixedSingle;
            Form form = new Form();
            form.FormBorderStyle = FormBorderStyle.None;
            form.Size = content.Size;
            form.Controls.Add(content);
            form.TopLevel = false;

            ToolStripControlHost controlHost =
                new ToolStripControlHost(form);            
            controlHost.AutoSize = false;
            controlHost.Size = content.Size;
            controlHost.Padding = Padding.Empty;
            controlHost.Margin = Padding.Empty;
            this.Size = content.Size;
            Items.Add(controlHost);
            if (content.DataSource != null)
                content.SelectedIndex = 0;
        }
    }
}
