using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace ControlEx
{
    internal class ListBoxBase : ListBox
    {
        private int maxWidth = 0;

        public event EventHandler SelectedChanged;

        public ListBoxBase()
            : base()
        {
            this.BorderStyle = BorderStyle.None;
            this.DrawMode = DrawMode.OwnerDrawFixed;
            this.FormattingEnabled = true;
            this.ItemHeight = 20;            
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (this.Items.Count == 0)
                return;
            Graphics gh = e.Graphics;
            e.DrawBackground();
            if ((e.State & DrawItemState.Selected) ==
                DrawItemState.Selected)
            {
                RectangleF selectedRec = new RectangleF(e.Bounds.Left - 2, e.Bounds.Top - 2, e.Bounds.Width + 1, e.Bounds.Height + 1);
                gh.FillRectangle(new SolidBrush(Color.FromArgb(153, 180, 209)), e.Bounds);
            }

            string text = string.Empty;
            Type type = this.Items[e.Index].GetType();
            if (type == null) return;
            System.Reflection.PropertyInfo[] propertys = type.GetProperties();
            System.Reflection.PropertyInfo property =
                propertys.FirstOrDefault(p => 
                    p.Name.Equals(this.DisplayMember, StringComparison.OrdinalIgnoreCase));
            if (property == null)
                text = this.Items[e.Index].ToString();
            else
                text = property.GetValue(this.Items[e.Index], null).ToString();
            maxWidth = Math.Max(TextRenderer.MeasureText(text, e.Font).Width, maxWidth);            
            TextRenderer.DrawText(
                gh, 
                text, 
                e.Font, 
                e.Bounds, 
                e.ForeColor, 
                TextFormatFlags.VerticalCenter);
            base.OnDrawItem(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            this.SelectedIndex = this.IndexFromPoint(e.Location);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button != MouseButtons.Left)
                return;
            int index = this.IndexFromPoint(e.Location);
            if (index == ListBox.NoMatches) return;
            if (SelectedChanged != null)
            {
                SelectedChanged(this, EventArgs.Empty);
            }
        }        
    }
}
