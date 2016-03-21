using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleActivities
{
    public partial class InputIntForm : Form
    {
        public InputIntForm()
        {
            InitializeComponent();
            NumericUpDown.Minimum = 0;
            NumericUpDown.Maximum = int.MaxValue;
        }

        public int Number
        {
            get 
            {
                return (int)NumericUpDown.Value;
            }
            set
            {
                if (NumericUpDown.Value != value)
                {
                    NumericUpDown.Value = value;
                }
            }
        }

        private string _message = string.Empty;
        public string Message 
        {
            get { return _message; }
            set
            {
                if (!string.IsNullOrEmpty(value) && !_message.Equals(value))
                {
                    _message = value;
                    Text = value;
                    InputNumberLabel.Text = value;
                }
            }
        }
    }
}
