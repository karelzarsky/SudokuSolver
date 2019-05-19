using System;
using System.Windows.Forms;

namespace SudokuSolver
{
    public partial class AlertForm : Form
    {
        public string Message
        {
            set { labelMessage.Text = value; }
        }

        public AlertForm()
        {
            InitializeComponent();
        }

        private void OK_Btn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
