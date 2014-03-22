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
    public partial class ComboBoxUserName : UserControl
    {
        private ListBoxBase listBox = new ListBoxBase();
        public event EventHandler SelectedIndexChanged;
        public event EventHandler SelectedValueChanged;

        public ComboBoxUserName()
        {
            InitializeComponent();
            listBox.SelectedChanged += new EventHandler(listBox_SelectedChanged);
        }

        void listBox_SelectedChanged(object sender, EventArgs e)
        {
            if (listBox.SelectedItem == null)
                return;
            this.textBox.Text = listBox.Text;
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

        [Browsable(false)]
        [DefaultValue("")]
        public override string Text
        {
            get
            {
                return this.textBox.Text;
            }
            set
            {
                this.textBox.Text = value;
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
                listBox.DataSource = dataSource;
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
        private void pnlDropDown_Click(object sender, EventArgs e)
        {
            if (popup == null || popup.IsDisposed)
            {
                popup = new PopupControlHost(listBox);
                popup.Closed += popup_Closed;
            }
            this.BackgroundImage = ControlEx.Properties.Resources.inputid_down;
            Rectangle rect = this.Parent.RectangleToScreen(this.Bounds);
            Point pt = new Point(rect.Left, rect.Bottom);
            popup.Show(pt);
            pnlDropDown.Enabled = false;
        }

        void popup_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            if (this.ClientRectangle.Contains(this.PointToClient(MousePosition)))
                this.BackgroundImage = ControlEx.Properties.Resources.inputid_hot;
            else
                this.BackgroundImage = ControlEx.Properties.Resources.inputid_nor;
            pnlDropDown.Enabled = true;
        }

        private void OnMouseEnter(object sender, EventArgs e)
        {
            this.BackgroundImage = ControlEx.Properties.Resources.inputid_hot;
        }

        private void OnMouseLeave(object sender, EventArgs e)
        {
            if (popup != null && 
                popup.Visible)
                return;
            this.BackgroundImage = 
                    ControlEx.Properties.Resources.inputid_nor;
        }
    }
}
