using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ControlEx
{
    public partial class ComboBoxProject : UserControl
    {
        private ListBoxBase listBox = new ListBoxBase();
        public event EventHandler SelectedIndexChanged;
        public event EventHandler SelectedValueChanged;

        public ComboBoxProject()
        {
            InitializeComponent();
            listBox.SelectedChanged += new EventHandler(listBox_SelectedChanged);
        }

        void listBox_SelectedChanged(object sender, EventArgs e)
        {
            if (listBox.SelectedItem == null)
                return;
            this.Text = listBox.Text;
            this.Select();
            if (SelectedIndexChanged != null)
            {
                SelectedIndexChanged(this, e);
            }
            if (SelectedValueChanged != null)
            {
                SelectedValueChanged(this, e);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            listBox.Width = this.Width;
        }

        public ListBox.ObjectCollection Items
        {
            get {
                return listBox.Items;
            }
        }

        private int maxItemHeight = 80;

        public int MaxItemHeight
        {
            get { return maxItemHeight; }
            set
            {
                maxItemHeight = value;
                listBox.Height = maxItemHeight;
            }
        }

        private int popupHeight = 80;

        public int PopupHeight
        {
            get { return popupHeight; }
            set
            {
                if (value > maxItemHeight)
                    popupHeight = maxItemHeight;
                else
                    popupHeight = value;
                listBox.Height = popupHeight;
            }
        }

        private Size popupSize = Size.Empty;

        public Size PopupSize
        {
            get
            {
                return popupSize;
            }
            set
            {
                popupSize = value;
                if (value.IsEmpty)
                    return;
                if (value.Width > 0 &&
                    value.Width > this.Width)
                    listBox.Width = value.Width;
                if (maxItemHeight > 0 &&
                    value.Height > maxItemHeight)
                    popupHeight = maxItemHeight;
                else
                    popupHeight = value.Height;
                listBox.Height = popupHeight;
            }
        }

        private string text = string.Empty;
        [Browsable(false)]
        [DefaultValue("")]
        public override string Text
        {
            get {
                return text;
            }
            set {
                this.text = value;
                pnlText.Refresh();
                this.toolTip1.SetToolTip(this.pnlText, listBox.Text);
            }
        }
        [Browsable(false)]
        [DefaultValue(null)]
        public object SelectedItem
        {
            get {
                return listBox.SelectedItem;
            }
            set {
                listBox.SelectedItem = value;
            }
        }
        [Browsable(false)]
        [DefaultValue(null)]
        public object SelectedValue
        {
            get
            {
                return listBox.SelectedValue;
            }
            set
            {
                listBox.SelectedValue = value;
                listBox_SelectedChanged(listBox, EventArgs.Empty);
            }
        }

        private object dataSource;
        [Browsable(false)]
        [DefaultValue(null)]
        public object DataSource
        {
            get { return dataSource; }
            set {
                dataSource = value;
                if (dataSource == null)
                    return;
                listBox.DataSource = dataSource;
                popup = new PopupControlHost(listBox);
                popup.Closed -= popup_Closed;
                popup.Closed += popup_Closed;
                listBox.SelectedIndex = 0;
                this.Text = listBox.Text;
            }
        }

        private string displayMember = string.Empty;
        [Browsable(false)]
        [DefaultValue("")]
        public string DisplayMember
        {
            get { return displayMember; }
            set
            {
                displayMember = value;
                listBox.DisplayMember = displayMember;
            }
        }

        private string valueMember = string.Empty;
        [Browsable(false)]
        [DefaultValue("")]
        public string ValueMember
        {
            get { return valueMember; }
            set
            {
                valueMember = value;
                listBox.ValueMember = valueMember;
            }
        }
        private PopupControlHost popup = null;
        private void OnDropDownClick(object sender, EventArgs e)
        {
            if (popup == null || popup.IsDisposed)
            {
                popup = new PopupControlHost(listBox);
                popup.Closed += popup_Closed;
            }
            Rectangle rect = this.Parent.RectangleToScreen(this.Bounds);
            Point pt = new Point(rect.Left, rect.Bottom);
            popup.Show(pt);
            this.Enabled = false;
        }

        void popup_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            this.Enabled = true;
        }

        private void pnlText_Paint(object sender, PaintEventArgs e)
        {
            if (string.IsNullOrEmpty(this.text))
                return;
            Panel pnl = sender as Panel;            
            if (pnl == null)
                return;
            TextRenderer.DrawText(
                e.Graphics,
                this.text,
                pnl.Font,
                pnl.Bounds,
                pnl.ForeColor,
                TextFormatFlags.VerticalCenter |
                TextFormatFlags.WordEllipsis);
        }
    }
}
