using SudokuLogic;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Media;
using System.Text;
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
                board.solution[selected.Column, selected.Row] = number;
                if (board.IsSolutionValid())
                {
                    Cells[selected.Column, selected.Row].Controls[0].Text = number == 0 ? "" : number.ToString();
                    SelectNext();
                }
                else
                {
                    Cells[selected.Column, selected.Row].Controls[0].BackColor = Color.Red;
                    Cells[selected.Column, selected.Row].Controls[0].Refresh();
                    SystemSounds.Asterisk.Play();
                    board.solution[selected.Column, selected.Row] = 0;
                    Thread.Sleep(100);
                    Cells[selected.Column, selected.Row].Controls[0].BackColor = Color.White;
                    Cells[selected.Column, selected.Row].Controls[0].Refresh();
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
                        Margin = new Padding(5),
                    };
                    var label = new Label
                    {
                        Dock = DockStyle.Fill,
                        BackColor = GetCellColor(i, j),
                        Text = board.solution[i, j] == 0 ? "" : board.solution[i, j].ToString(),
                        TextAlign = ContentAlignment.MiddleCenter
                    };
                    label.Click += LabelClick;
                    panel.Controls.Add(label);
                    tableLayoutPanel1.Controls.Add(panel, i, j);
                }
            }

            SelectCell(new TableLayoutPanelCellPosition(0, 0));
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

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Cells[i, j].Controls[0].Text = "";
                    board.solution[i, j] = 0;
                }
            }
        }

        private void FillRandomBtn_Click(object sender, EventArgs e)
        {
            board.FillRandomCells(5);
            RefreshBoard();
        }

        private void RefreshBoard()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Cells[i, j].Controls[0].Text = board.solution[i, j] == 0 ? "" : board.solution[i, j].ToString();
                }
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
                    var sb = new StringBuilder();
                    for (int i = 0; i < 9; i++)
                    {
                        for (int j = 0; j < 9; j++)
                        {
                            sb.Append(board.solution[i, j].ToString());
                        }
                    }
                    File.WriteAllText(fileDialog.FileName, sb.ToString());
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
                    string fileContent;
                    using (var fileStream = openFileDialog.OpenFile())
                    {
                        using (StreamReader reader = new StreamReader(fileStream))
                        {
                            fileContent = reader.ReadToEnd();
                        }
                    }
                    if (fileContent.Length < 81) return;
                    int characterCounter = 0;
                    for (int i = 0; i < 9; i++)
                    {
                        for (int j = 0; j < 9; j++)
                        {
                            byte.TryParse(fileContent[characterCounter++].ToString(), out byte number);
                            board.solution[i, j] = number;
                        }
                    }
                    RefreshBoard();
                }
            }
        }
    }
}
