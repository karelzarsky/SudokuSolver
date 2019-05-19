using SudokuLogic;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Media;
using System.Threading;
using System.Windows.Forms;

namespace SudokuSolver
{
    public partial class View : Form
    {
        private ViewModel vm = new ViewModel();
        AlertForm alert;

        public View()
        {
            InitializeComponent();
            vm.FillRandomCells(25);
            CreateBoard();
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
                NumberPressed(number);
            }
        }

        private void NumberPressed(byte number)
        {
            if (vm.TrySetNumber(vm.Selected.Column, vm.Selected.Row, number, Origin.Human))
            {
                StatusLabel.Text = number == 0
                    ? $"Cleared cell {vm.Selected.Column},{vm.Selected.Row}."
                    : $"Filled number {number} at {vm.Selected.Column},{vm.Selected.Row}.";
                RefreshBoard();
                SelectNext();
            }
            else
            {
                StatusLabel.Text = $"Cannot enter number {number} at {vm.Selected.Column},{vm.Selected.Row}.";
                vm.Cells[vm.Selected.Column, vm.Selected.Row].BackColor = Color.Red;
                vm.Cells[vm.Selected.Column, vm.Selected.Row].Refresh();
                SystemSounds.Asterisk.Play();
                Thread.Sleep(100);
                vm.Cells[vm.Selected.Column, vm.Selected.Row].BackColor = Color.White;
                vm.Cells[vm.Selected.Column, vm.Selected.Row].Refresh();
            }
        }

        private void SelectNext() =>
            SelectCell((vm.Selected.Column + 1) % 9, vm.Selected.Column != 8 ? vm.Selected.Row : (vm.Selected.Row + 1) % 9);

        private Color GetCellBgColor(int i, int j) =>
            (i / 3 + j / 3) % 2 == 0
                ? Color.DarkGray
                : Color.BurlyWood;

        private void CreateBoard()
        {
            for (int col = 0; col < 9; col++)
            {
                for (int row = 0; row < 9; row++)
                {
                    var cellPnl = vm.Cells[col, row] = new Panel
                    {
                        Dock = DockStyle.Fill,
                        BackColor = GetCellBgColor(col, row),
                        Margin = new Padding(5)
                    };
                    var bigNumberLbl = new Label
                    {
                        Dock = DockStyle.Fill,
                        Text = vm.GetNumberAsString(col, row),
                        TextAlign = ContentAlignment.MiddleCenter,
                        Visible = false
                    };
                    bigNumberLbl.Click += NumberClick;
                    cellPnl.Click += PnlClick;
                    mainMatrix.Controls.Add(cellPnl, col, row);
                    cellPnl.Controls.Add(bigNumberLbl);
                    cellPnl.Controls.Add(CreateHintPanel());
                }
            }
            RefreshBoard();
            SelectCell(0, 0);
        }

        private TableLayoutPanel CreateHintPanel()
        {
            var hintPanel = new TableLayoutPanel
            { Dock = DockStyle.Fill, Font = new Font("Arial Narrow", 8F), BorderStyle = BorderStyle.None, Visible = false };
            for (int j = 0; j < 3; j++)
            {
                hintPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
                hintPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
                for (int i = 0; i < 3; i++)
                {
                    var hintLbl = new Label { TextAlign = ContentAlignment.MiddleCenter };
                    hintLbl.Click += HintClick;
                    hintPanel.Controls.Add(hintLbl, i, j);
                }
            }
            return hintPanel;
        }

        private void HintClick(object sender, EventArgs e)
        {
            byte.TryParse(((Label)sender).Text, out byte number);
            var panel = (Panel)((Control)sender).Parent;
            var position = mainMatrix.GetCellPosition(panel.Parent);
            if (number != 0)
            {
                StatusLabel.Text = vm.TrySetNumber(position.Column, position.Row, number, Origin.Human)
                    ? $"Filled number {number} at {position.Column},{position.Row}."
                    : $"Cannot enter number {number} at {position.Column},{position.Row}.";
                RefreshBoard();
            }
            if (position.Column != -1)
            {
                SelectCell(position);
            }
        }

        private void PnlClick(object sender, EventArgs e)
        {
            var pos = mainMatrix.GetCellPosition((Panel)sender);
            if (pos.Column != -1)
            {
                SelectCell(pos);
            }
        }

        private void NumberClick(object sender, EventArgs e)
        {
            var pnl = (Panel)((Control)sender).Parent;
            var pos = mainMatrix.GetCellPosition(pnl);
            if (pos.Column != -1)
            {
                SelectCell(pos);
            }
        }

        private void SelectCell(TableLayoutPanelCellPosition pos)
        {
            vm.Cells[vm.Selected.Column, vm.Selected.Row].BackColor = GetCellBgColor(vm.Selected.Column, vm.Selected.Row);
            vm.Selected = pos;
            vm.Cells[vm.Selected.Column, vm.Selected.Row].BackColor = Color.White;
        }

        private void SelectCell(int col, int row) => SelectCell(new TableLayoutPanelCellPosition(col, row));

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            vm.Clear();
            RefreshBoard();
            StatusLabel.Text = "Board was cleared.";
        }

        private void FillRandomBtn_Click(object sender, EventArgs e)
        {
            RefreshBoard();
            StatusLabel.Text = $"{vm.FillRandomCells(5)} cells were filled randomly.";
        }

        private void RefreshBoard()
        {
            vm.BasicPossibilitiesReduction();
            for (int col = 0; col < 9; col++)
            {
                for (int row = 0; row < 9; row++)
                {
                    RefreshCell(col, row);
                }
            }
        }

        private void RefreshCell(int col, int row)
        {
            if (vm.CellEmpty(col, row))
            {
                vm.BigNumberLabel(col, row).Visible = false;
                if (hintsChk.Checked)
                {
                    vm.HintPanel(col, row).Visible = true;
                    for (int num = 1; num < 10; num++)
                    {
                        vm.HintPanel(col, row).Controls[num - 1].Text = vm.HintText(col, row, num);
                    }
                }
                else
                {
                    vm.HintPanel(col, row).Visible = false;
                }
            }
            else
            {
                vm.BigNumberLabel(col, row).ForeColor = vm.GetForeColor(col, row);
                vm.BigNumberLabel(col, row).Text = vm.GetNumberAsString(col, row);
                vm.BigNumberLabel(col, row).Visible = true;
                vm.HintPanel(col, row).Visible = false;
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
                    File.WriteAllText(fileDialog.FileName, vm.ToString());
                    StatusLabel.Text = $"Sudoku was saved to file {fileDialog.FileName}.";
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
                            vm.FillFromString(reader.ReadToEnd());
                            StatusLabel.Text = $"Sudoku loaded from file {openFileDialog.FileName}.";
                        }
                    }
                    RefreshBoard();
                }
            }
        }

        private void SinglesBtn_Click(object sender, EventArgs e)
        {
            vm.ColorChangeFreshToOld();
            vm.BasicPossibilitiesReduction();
            StatusLabel.Text = $"{vm.CheckForSolvedCells()} cells had one last possibility and was filled.";
            RefreshBoard();
        }

        private async void BruteForceBtn_Click(object sender, EventArgs e)
        {
            vm.ColorChangeFreshToOld();
            alert = new AlertForm();
            alert.Show();
            alert.Text = "Searching complete.";
            alert.OK_Btn.Visible = true;
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            StatusLabel.Text = alert.labelMessage.Text = await vm.BruteForce()
                ? $"Solution found using brute force method.\r\nTime elapsed : {stopwatch.ElapsedMilliseconds} ms."
                : $"This sudoku has no solution.\r\nTime elapsed : {stopwatch.ElapsedMilliseconds} ms.";
            RefreshBoard();
        }

        private void HintsChk_CheckedChanged(object sender, EventArgs e)
        {
            StatusLabel.Text = "Hints are now " + (hintsChk.Checked ? "shown." : "hidden.");
            RefreshBoard();
        }

        private void HiddenSinBtn_Click(object sender, EventArgs e)
        {
            vm.ColorChangeFreshToOld();
            StatusLabel.Text = $"Found {vm.FindHiddenSingles()} hidden singles.";
            RefreshBoard();
        }

        private void IntersectionsBtn_Click(object sender, EventArgs e)
        {
            vm.ColorChangeFreshToOld();
            StatusLabel.Text = $"Eliminated {vm.EliminateIntersections()} possibilities using intersections rules.";
            RefreshBoard();
        }
    }
}
