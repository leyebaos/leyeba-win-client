using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ControlEx
{
    public partial class ComboBoxBase : UserControl
    {
        private ListBoxBase listBox = new ListBoxBase();
        public event EventHandler SelectedIndexChanged;
        public event EventHandler SelectedValueChanged;
        public event EventHandler PopupColsed;

        public ComboBoxBase()
        {
            InitializeComponent();            
            initEvent();
        }

        private void initEvent()
        {
            pnlDropDown.Click += OnDropDownClick;
            listBox.SelectedChanged += listBox_SelectedChanged;
            listBox.SizeChanged += listBox_SizeChanged;
        }

        void listBox_SizeChanged(object sender, EventArgs e)
        {
            if (popup != null)
                popup.Width = listBox.Width;
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

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            listBox.Width = this.Width;
        }

        public ListBox.ObjectCollection Items
        {
            get {
                return listBox.Items;
            }
        }

        private int maxItemHeight = 0;

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
            set {
                if (maxItemHeight > 0 &&
                    value > maxItemHeight)
                    popupHeight = maxItemHeight;
                else
                    popupHeight = value;
                listBox.Height = popupHeight;
            }
        }

        private Size popupSize = Size.Empty;

        public Size PopupSize 
        {
            get {
                return popupSize;
            }
            set {
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

        private bool canEdit = true;

        public bool CanEdit
        {
            get { return canEdit; }
            set { 
                canEdit = value;
                textBox.Visible = canEdit;
                if (!canEdit)
                {
                    pnlText.Paint += pnlText_Paint;
                    pnlText.Click += OnDropDownClick;
                }
                else
                {
                    pnlText.Paint -= pnlText_Paint;
                    pnlText.Click -= OnDropDownClick;
                }
            }
        }

        void pnlText_Paint(object sender, PaintEventArgs e)
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
        
        private string text = string.Empty;
        [Browsable(false)]
        [DefaultValue("")]
        public override string Text
        {
            get
            {
                return this.text;
            }
            set {
                this.text = value;
                if (canEdit)
                {
                    this.textBox.Text = value;
                }
                else
                {
                    pnlText.Refresh();
                    this.toolTip1.SetToolTip(this.pnlText, listBox.Text);
                }
            }
        }

        [Browsable(false)]
        [DefaultValue(null)]
        public object SelectedItem
        {
            get
            {
                return listBox.SelectedItem;
            }
            set
            {
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

        [Browsable(false)]
        [DefaultValue(0)]
        public int SelectedIndex 
        {
            get {
                return listBox.SelectedIndex;
            }
        }

        private object dataSource;
        [Browsable(false)]
        [DefaultValue(null)]
        public object DataSource
        {
            get { return dataSource; }
            set
            {
                dataSource = value;
                if (dataSource == null)
                    return;
                listBox.DataSource = dataSource;
                popup = new PopupControlHost(listBox);
                popup.Closed -= popup_Closed;
                popup.Closed += popup_Closed;
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
            if (canEdit)
                pnlDropDown.Enabled = false;
            else
                this.Enabled = false;
        }

        void popup_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            if (canEdit)
                pnlDropDown.Enabled = true;
            else
                this.Enabled = true;
            if (this.PopupColsed != null)
                PopupColsed(this, EventArgs.Empty);
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
    }
}
