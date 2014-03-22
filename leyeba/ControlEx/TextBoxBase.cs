using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ControlEx
{
    public partial class TextBoxBase : UserControl
    {
        private string intPattern = @"^\d{0,3}$";
        private string decimalPattern = @"^\d{0,3}(?:\.\d{0,1})?$|^0\.[0-9][1-9]$|^0\.[1-9]0?$";
        private Regex regex;
        private string txtValue = string.Empty;

        public TextBoxBase()
        {
            InitializeComponent();
        }

        public override string Text
        {
            get
            {
                return textBox.Text;
            }
            set
            {
                textBox.Text = value;
            }
        }

        public int MaxLength
        {
            get
            {
                return textBox.MaxLength;
            }
            set
            {
                textBox.MaxLength = value;
            }
        }

        private TextType type = TextType.Text;

        public TextType Type
        {
            get { return type; }
            set
            {
                type = value;
                switch (type)
                {
                    case TextType.Text:
                        this.textBox.TextChanged -= textBox_TextChanged;
                        textBox.ImeMode = ImeMode.NoControl;
                        break;
                    case TextType.Interger:
                        regex = new Regex(intPattern, RegexOptions.Compiled);
                        this.textBox.TextChanged -= textBox_TextChanged;
                        this.textBox.TextChanged += textBox_TextChanged;
                        textBox.ImeMode = ImeMode.Disable;
                        break;
                    case TextType.Decimal:
                        regex = new Regex(decimalPattern, RegexOptions.Compiled);
                        this.textBox.TextChanged -= textBox_TextChanged;
                        this.textBox.TextChanged += textBox_TextChanged;
                        textBox.ImeMode = ImeMode.Disable;
                        break;
                }
            }
        }

        public new void Select()
        {
            this.textBox.Select();
        }

        public void SelectAll()
        {
            this.textBox.SelectAll();
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

        void textBox_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (txt == null)
                return;
            if (!regex.IsMatch(txt.Text))
            {
                //输入的不是数字
                txt.Text = txtValue;  //textBox内容不变
                txt.SelectionStart = txt.Text.Length;  //将光标定位到文本框的最后
            }
            else
            {
                //输入的是数字
                txtValue = txt.Text;  //将现在textBox的值保存下来
            }
        }
    }

    public enum TextType
    {
        /// <summary>
        /// 文本
        /// </summary>
        Text = 0,
        /// <summary>
        /// 整数
        /// </summary>
        Interger = 1,
        /// <summary>
        /// 小数
        /// </summary>
        Decimal = 2
    }
}
