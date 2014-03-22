using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;

namespace ControlEx
{
    public partial class TextBoxTime : UserControl
    {
        private string hourPattern = @"^(([0-1]?\d{0,1})|(2[0-3]{0,1})){0,1}$";
        private string minutePattern = @"^[0-5]{0,1}\d{0,1}$";

        public TextBoxTime()
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

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            txtHour.Focus();
            txtHour.SelectAll();
        }

        public void Reset()
        {
            txtHour.Text = "00";
            txtMinute.Text = "00";
        }

        public decimal Time
        {
            get
            {
                decimal time = 0m;
                decimal.TryParse(
                    string.Format(
                    "{0}.{1}",
                    txtHour.Text, 
                    txtMinute.Text), 
                    out time);
                return time;
            }
        }

        private string txtHourValue = "";
        private void txtHour_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (txt == null)
                return;
            if (txt.TextLength == 0)
            {
                txt.TextChanged -= txtHour_TextChanged;
                txt.Text = "00";
                txt.TextChanged += txtHour_TextChanged;
                txt.SelectAll();
                return;
            }
            if (!Regex.Match(txt.Text, hourPattern).Success)
            {
                //输入的不是数字
                txt.Text = txtHourValue;  //textBox内容不变
                txt.SelectionStart = txt.Text.Length;  //将光标定位到文本框的最后
            }
            else
            {
                //输入的是数字
                txtHourValue = txt.Text;  //将现在textBox的值保存下来
                if (txt.TextLength == 2)
                {
                    txtMinute.Select();
                }
            }
        }
        private string txtMinuteValue = "";
        private void txtMinute_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (txt == null)
                return;
            if (txt.TextLength == 0)
            {
                txt.TextChanged -= txtMinute_TextChanged;
                txt.Text = "00";
                txt.TextChanged += txtMinute_TextChanged;
                txt.SelectAll();
                return;
            }
            if (!Regex.Match(txt.Text, minutePattern).Success)
            {
                //输入的不是数字
                txt.Text = txtMinuteValue;  //textBox内容不变
                txt.SelectionStart = txt.Text.Length;  //将光标定位到文本框的最后
            }
            else
            {
                //输入的是数字
                txtMinuteValue = txt.Text;  //将现在textBox的值保存下来
            }
        }

        private void txtHour_Enter(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (txt == null)
                return;
            if (string.IsNullOrEmpty(txt.Text))
                return;
            txt.Focus();
            txt.SelectAll();
        }

        private void txtHour_Leave(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (txt == null)
                return;
            txt.Tag = 0;
            if (txt.TextLength == 1)
            {
                txt.Text = string.Format("0{0}", txt.Text); 
            }
        }

        private void txtHour_MouseClick(object sender, MouseEventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (txt == null)
                return;
            txt.Tag = 1;
            txt.SelectAll();
        }

        private void txtMinute_Enter(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (txt == null)
                return;
            if (string.IsNullOrEmpty(txt.Text))
                return;
            txt.Focus();
            txt.SelectAll();
        }

        private void txtMinute_Leave(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (txt == null)
                return;
            txt.Tag = 0;
            if (txt.TextLength == 1)
            {
                txt.Text = string.Format("0{0}", txt.Text);
            }
        }

        private void txtMinute_MouseClick(object sender, MouseEventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (txt == null)
                return;
            txt.Tag = 1;
            txt.SelectAll();
        }
    }
}
