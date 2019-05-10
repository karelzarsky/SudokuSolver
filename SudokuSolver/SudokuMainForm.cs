using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuSolver
{
    public partial class SudokuMainForm : Form
    {
        public SudokuMainForm()
        {
            InitializeComponent();
            PrepareBoard();
        }

        private void PrepareBoard()
        {
            Random rnd = new Random();
            tableLayoutPanel1.Font = new Font("Courier New", 16, FontStyle.Bold, GraphicsUnit.Point, 0);
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                {
                    var color = (i / 3 + j / 3) % 2 == 0
                        ? Color.DarkGray
                        : Color.BurlyWood;
                    var panel = new Panel // for vertical allignment
                    {
                        BackColor = color,
                        Dock = DockStyle.Fill,
                        Margin = new Padding(10),
                    };
                    panel.Controls.Add(new Label
                        {
                            Dock = DockStyle.Fill,
                            BackColor = color,
                            Text = (1 + rnd.Next(9)).ToString(),
                            BorderStyle = BorderStyle.None,
                            TextAlign = ContentAlignment.MiddleCenter
                        }
                    );
                    tableLayoutPanel1.Controls.Add(panel, i, j );
                }
        }
    }
}
