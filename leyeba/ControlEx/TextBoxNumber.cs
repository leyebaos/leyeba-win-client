using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ControlEx
{
    public class TextBoxNumber : TextBox
    {
        private string intPattern = @"^\d{0,3}$";
        private string decimalPattern = @"^\d{0,3}(?:\.\d{0,1})?$|^0\.[0-9][1-9]$|^0\.[1-9]0?$";
        private Regex regex;
        private string txtValue = string.Empty;

        private NumberType type = NumberType.Interger;

        public NumberType Type
        {
            get { return type; }
            set { 
                type = value;
                switch (type)
                {
                    case NumberType.Interger:
                        regex = new Regex(intPattern, RegexOptions.Compiled);
                        break;
                    case NumberType.Decimal:
                        regex = new Regex(decimalPattern, RegexOptions.Compiled);
                        break;
                }
            }
        }        

        public TextBoxNumber()
            : base()
        {
            Type = NumberType.Interger;
        }
        
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            if (!regex.IsMatch(this.Text))
            {
                //输入的不是数字
                this.Text = txtValue;  //textBox内容不变
                this.SelectionStart = this.Text.Length;  //将光标定位到文本框的最后
            }
            else
            {
                //输入的是数字
                txtValue = this.Text;  //将现在textBox的值保存下来
            }
        }
    }

    public enum NumberType
    {
        /// <summary>
        /// 整数
        /// </summary>
        Interger=0,
        /// <summary>
        /// 小数
        /// </summary>
        Decimal=1
    }
}
