using SudokuLogic;
using System;
using System.Drawing;
using System.IO;
using System.Media;
using System.Threading;
using System.Windows.Forms;

namespace SudokuSolver
{
    public partial class SudokuMainForm : Form
    {
        private Panel[,] Cells = new Panel[9, 9];
        private Board board = new Board();
        private TableLayoutPanelCellPosition selected;

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
            {
                keyChar = keyChar[6].ToString();
            }

            if (byte.TryParse(keyChar, out byte number))
            {
                if (board.TrySetNumber(selected.Column, selected.Row, number, Source.Keyboard))
                {
                    RefreshBoard();
                    SelectNext();
                }
                else
                {
                    Cells[selected.Column, selected.Row].BackColor = Color.Red;
                    Cells[selected.Column, selected.Row].Refresh();
                    SystemSounds.Asterisk.Play();
                    Thread.Sleep(100);
                    Cells[selected.Column, selected.Row].BackColor = Color.White;
                    Cells[selected.Column, selected.Row].Refresh();
                }
            }
        }

        private void SelectNext()
        {
            if (selected.Column == 8)
            {
                SelectCell(new TableLayoutPanelCellPosition(0, selected.Row == 8 ? 0 : selected.Row + 1));
            }
            else
            {
                SelectCell(new TableLayoutPanelCellPosition(selected.Column + 1, selected.Row));
            }
        }

        private Color GetCellColor(int i, int j) =>
            ((i / 3) + (j / 3)) % 2 == 0
                ? Color.DarkGray
                : Color.BurlyWood;

        private void PrepareBoard()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    var panel = Cells[i, j] = new Panel
                    {
                        Dock = DockStyle.Fill,
                        BackColor = GetCellColor(i, j),
                        Margin = new Padding(5),
                    };
                    var label = new Label
                    {
                        Dock = DockStyle.Fill,
                        Text = board.GetNumberAsString(i, j),
                        TextAlign = ContentAlignment.MiddleCenter,
                        Visible = false
                    };
                    var bulletPannel = CreateBulletPanel();
                    label.Click += LabelClick;
                    bulletPannel.Click += LabelClick;
                    tableLayoutPanel1.Controls.Add(panel, i, j);
                    panel.Controls.Add(label);
                    panel.Controls.Add(bulletPannel);
                }
            }
            RefreshBoard();
            SelectCell(new TableLayoutPanelCellPosition(0, 0));
        }

        private static TableLayoutPanel CreateBulletPanel()
        {
            var bulletPannel = new TableLayoutPanel
                { Dock = DockStyle.Fill, Font = new Font("Arial", 8F), Visible = false};
            for (int j = 0; j < 3; j++)
            {
                bulletPannel.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
                bulletPannel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
                for (int i = 0; i < 3; i++)
                {
                    bulletPannel.Controls.Add(new Label {TextAlign = ContentAlignment.MiddleCenter}, i, j);
                    // ○○◌●○●•
                }
            }
            return bulletPannel;
        }

        private void LabelClick(object sender, EventArgs e)
        {
            Panel p;
            if (sender is TableLayoutPanel)
            {
                p = (Panel)((TableLayoutPanel)sender).Parent;
            }
            else
            {
                p = (Panel)((Label)sender).Parent;
            }
            SelectCell(((TableLayoutPanel)p.Parent).GetCellPosition(p));
        }

        private void SelectCell(TableLayoutPanelCellPosition pos)
        {
            Cells[selected.Column, selected.Row].BackColor = GetCellColor(selected.Column, selected.Row);
            selected = pos;
            Cells[selected.Column, selected.Row].BackColor = Color.White;
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            board.Clear();
            RefreshBoard();
        }

        private void FillRandomBtn_Click(object sender, EventArgs e)
        {
            board.FillRandomCells(5);
            RefreshBoard();
        }

        private void RefreshBoard()
        {
            Strategies.BasicPossibilitiesReduction(board);
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    RefreshCell(i, j);
                }
            }
        }

        private void RefreshCell(int i, int j)
        {
            if (board.CellEmpty(i, j))
            {
                Cells[i, j].Controls[0].Visible = false;
                Cells[i, j].Controls[1].Visible = true;
                for (int k = 0; k < 9; k++)
                {
                    Cells[i, j].Controls[1].Controls[k].Text = board.possibilities[i, j, k + 1] ? (k+1).ToString() : "";
                }
            }
            else
            {
                Cells[i, j].Controls[0].Text = board.GetNumberAsString(i, j);
                Cells[i, j].Controls[1].Visible = false;
                Cells[i, j].Controls[0].Visible = true;
            }
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog fileDialog = new SaveFileDialog
            {
                Filter = "sudoku files |*.sud",
                InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath)
            })
            {
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(fileDialog.FileName, board.ToString());
                }
            }
        }

        private void LoadBtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "sudoku files |*.sud",
                InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath)
            })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (var fileStream = openFileDialog.OpenFile())
                    {
                        using (StreamReader reader = new StreamReader(fileStream))
                        {
                            board.FillFromString(reader.ReadToEnd());
                        }
                    }
                    RefreshBoard();
                }
            }
        }

        private void OneStepBtn_Click(object sender, EventArgs e)
        {
            Strategies.BasicPossibilitiesReduction(board);
            Strategies.CheckForSolvedCells(board);
            RefreshBoard();
        }

        private void BruteForceBtn_Click(object sender, EventArgs e)
        {
            var solutions = Strategies.BruteForce(board);
            RefreshBoard();
        }
    }
}
