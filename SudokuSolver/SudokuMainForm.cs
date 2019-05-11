using SudokuLogic;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SudokuSolver
{
    public partial class SudokuMainForm : Form
    {
        Panel[,] Cells = new Panel[9, 9];
        Board board = new Board();
        TableLayoutPanelCellPosition selected;

        public SudokuMainForm()
        {
            InitializeComponent();
            board.FillRandomCells(30);
            PrepareBoard();
            KeyPreview = true;
            KeyDown += OnKeyDown;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            string keyChar = new KeysConverter().ConvertToString(e.KeyData);
            if (keyChar.StartsWith("NumPad"))
                keyChar = keyChar[6].ToString();
            if (byte.TryParse(keyChar, out byte number))
            {
                board.solution[selected.Column, selected.Row] = number;
                Cells[selected.Column, selected.Row].Controls[0].Text = number == 0 ? "" : number.ToString();
                SelectNext();
            }
        }

        private void SelectNext()
        {
            if (selected.Column == 8)
                SelectCell(new TableLayoutPanelCellPosition(0, selected.Row == 8 ? 0 : selected.Row + 1));
            else
                SelectCell(new TableLayoutPanelCellPosition(selected.Column + 1, selected.Row));
        }

        Color GetCellColor(int i, int j) =>
            ((i / 3) + (j / 3)) % 2 == 0
                ? Color.DarkGray
                : Color.BurlyWood;

        private void PrepareBoard()
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                {
                    var panel = Cells[i, j] = new Panel
                    {
                        Dock = DockStyle.Fill,
                        Margin = new Padding(5),
                    };
                    var label = new Label
                    {
                        Dock = DockStyle.Fill,
                        BackColor = GetCellColor(i, j),
                        Text = board.solution[i, j] == 0 ? "" : board.solution[i, j].ToString(),
                        BorderStyle = BorderStyle.None,
                        TextAlign = ContentAlignment.MiddleCenter
                    };
                    label.Click += LabelClick;
                    panel.Controls.Add(label);
                    tableLayoutPanel1.Controls.Add(panel, i, j );
                }
            SelectCell(new TableLayoutPanelCellPosition(0,0));
        }

        private void LabelClick(object sender, EventArgs e)
        {
            Panel p = (Panel)((Label)sender).Parent;
            SelectCell(((TableLayoutPanel)p.Parent).GetCellPosition(p));
        }

        private void SelectCell(TableLayoutPanelCellPosition pos)
        {
            Cells[selected.Column, selected.Row].Controls[0].BackColor = GetCellColor(selected.Column, selected.Row);
            selected = pos;
            Cells[selected.Column, selected.Row].Controls[0].BackColor = Color.White;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
