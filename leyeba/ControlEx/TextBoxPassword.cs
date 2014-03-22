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
    public partial class TextBoxPassword : UserControl
    {
        public TextBoxPassword()
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

        public TextBox TextBox
        {
            get
            {
                return textBox;
            }
        }

        private void textBox_Enter(object sender, EventArgs e)
        {
            TextBox txtBox = (TextBox)sender;
            if (txtBox.ForeColor == Color.Gray &&
                txtBox.Text.Trim().Equals("密码"))
            {
                txtBox.Text = "";
                txtBox.ForeColor = Color.Black;
                txtBox.PasswordChar = '●';
            }
        }

        private void textBox_Leave(object sender, EventArgs e)
        {
            TextBox txtBox = (TextBox)sender;
            if (txtBox.ForeColor == Color.Black &&
                txtBox.Text == string.Empty)
            {
                textBox.TextChanged -= textBox_TextChanged;
                txtBox.Text = "密码";
                textBox.TextChanged += textBox_TextChanged;
                txtBox.ForeColor = Color.Gray;
                txtBox.PasswordChar = '\0';
            }
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            TextBox txtBox = (TextBox)sender;
            txtBox.ForeColor = Color.Black;
            txtBox.PasswordChar = '●';
        }

        private void OnMouseEnter(object sender, EventArgs e)
        {
            this.BackgroundImage = ControlEx.Properties.Resources.inputpassword_hot;
        }

        private void OnMouseLeave(object sender, EventArgs e)
        {
            this.BackgroundImage = ControlEx.Properties.Resources.inputpassword_nor;
        }
    }
}
