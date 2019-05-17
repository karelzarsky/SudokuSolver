using SudokuLogic;
using System;
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

        public View()
        {
            InitializeComponent();
            vm.FillRandomCells(30);
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
                if (vm.TrySetNumber(vm.Selected.Column, vm.Selected.Row, number, Source.Keyboard))
                {
                    RefreshBoard();
                    SelectNext();
                }
                else
                {
                    vm.Cells[vm.Selected.Column, vm.Selected.Row].BackColor = Color.Red;
                    vm.Cells[vm.Selected.Column, vm.Selected.Row].Refresh();
                    SystemSounds.Asterisk.Play();
                    Thread.Sleep(100);
                    vm.Cells[vm.Selected.Column, vm.Selected.Row].BackColor = Color.White;
                    vm.Cells[vm.Selected.Column, vm.Selected.Row].Refresh();
                }
            }
        }

        private void SelectNext()
        {
            if (vm.Selected.Column == 8)
            {
                SelectCell(new TableLayoutPanelCellPosition(0, vm.Selected.Row == 8 ? 0 : vm.Selected.Row + 1));
            }
            else
            {
                SelectCell(new TableLayoutPanelCellPosition(vm.Selected.Column + 1, vm.Selected.Row));
            }
        }

        private Color GetCellColor(int i, int j) =>
            ((i / 3) + (j / 3)) % 2 == 0
                ? Color.DarkGray
                : Color.BurlyWood;

        private void CreateBoard()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    var cellPnl = vm.Cells[i, j] = new Panel
                    {
                        Dock = DockStyle.Fill,
                        BackColor = GetCellColor(i, j),
                        Margin = new Padding(5),
                    };
                    var bigNumberLbl = new Label
                    {
                        Dock = DockStyle.Fill,
                        Text = vm.GetNumberAsString(i, j),
                        TextAlign = ContentAlignment.MiddleCenter,
                        Visible = false,
                    };
                    var hintPanel = CreateHintPanel();
                    bigNumberLbl.Click += NumberClick;
                    cellPnl.Click += PnlClick;
                    mainMatrix.Controls.Add(cellPnl, i, j);
                    cellPnl.Controls.Add(bigNumberLbl);
                    cellPnl.Controls.Add(hintPanel);
                }
            }
            RefreshBoard();
            SelectCell(new TableLayoutPanelCellPosition(0, 0));
        }

        private TableLayoutPanel CreateHintPanel()
        {
            var hintPanel = new TableLayoutPanel
                { Dock = DockStyle.Fill, Font = new Font("Arial Narrow", 8F), BorderStyle = BorderStyle.None, Visible = false};
            for (int j = 0; j < 3; j++)
            {
                hintPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
                hintPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
                for (int i = 0; i < 3; i++)
                {
                    var hintLbl = new Label {TextAlign = ContentAlignment.MiddleCenter};
                    hintLbl.Click += HintClick;
                    hintPanel.Controls.Add(hintLbl, i, j);
                }
            }
            return hintPanel;
        }

        private void HintClick(object sender, EventArgs e)
        {
            var lbl = (Label) sender;
            byte.TryParse(lbl.Text, out byte num);
            var pnl = (Panel)((Control)sender).Parent;
            var pos = mainMatrix.GetCellPosition(pnl.Parent);
            if (num != 0)
            {
                vm.TrySetNumber(pos.Column, pos.Row, num, Source.Keyboard);
                RefreshBoard();
            }
            if (pos.Column != -1)
                SelectCell(pos);
        }

        private void PnlClick(object sender, EventArgs e)
        {
            var pos = mainMatrix.GetCellPosition((Panel)sender);
            if (pos.Column != -1)
                SelectCell(pos);
        }

        private void NumberClick(object sender, EventArgs e)
        {
            var pnl = (Panel)((Control)sender).Parent;
            var pos = mainMatrix.GetCellPosition(pnl);
            if (pos.Column != -1)
                SelectCell(pos);
        }

        private void SelectCell(TableLayoutPanelCellPosition pos)
        {
            vm.Cells[vm.Selected.Column, vm.Selected.Row].BackColor = GetCellColor(vm.Selected.Column, vm.Selected.Row);
            vm.Selected = pos;
            vm.Cells[vm.Selected.Column, vm.Selected.Row].BackColor = Color.White;
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            vm.Clear();
            RefreshBoard();
        }

        private void FillRandomBtn_Click(object sender, EventArgs e)
        {
            vm.FillRandomCells(5);
            RefreshBoard();
        }

        private void RefreshBoard()
        {
            vm.BasicPossibilitiesReduction();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    RefreshCell(i, j);
                }
            }
        }

        private void RefreshCell(int col, int row)
        {
            if (vm.CellEmpty(col, row))
            {
                vm.Cells[col, row].Controls[0].Visible = false;
                if (hintsChk.Checked)
                {
                    vm.Cells[col, row].Controls[1].Visible = true;
                    for (int num = 1; num < 10; num++)
                    {
                        vm.Cells[col, row].Controls[1].Controls[num - 1].Text = vm.HintText(col, row, num);
                    }
                }
                else
                {
                    vm.Cells[col, row].Controls[1].Visible = false;
                }
            }
            else
            {
                vm.Cells[col, row].Controls[0].Text = vm.GetNumberAsString(col, row);
                vm.Cells[col, row].Controls[1].Visible = false;
                vm.Cells[col, row].Controls[0].Visible = true;
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
                        }
                    }
                    RefreshBoard();
                }
            }
        }

        private void OneStepBtn_Click(object sender, EventArgs e)
        {
            vm.BasicPossibilitiesReduction();
            vm.CheckForSolvedCells();
            RefreshBoard();
        }

        private void BruteForceBtn_Click(object sender, EventArgs e)
        {
            vm.BruteForce();
            RefreshBoard();
        }

        private void hintsChk_CheckedChanged(object sender, EventArgs e)
        {
            RefreshBoard();
        }

        private void HiddenSinBtn_Click(object sender, EventArgs e)
        {
            vm.FindHiddenSingles();
            RefreshBoard();
        }

        private void IntersectionsBtn_Click(object sender, EventArgs e)
        {
            vm.EliminateIntersections();
            RefreshBoard();
        }
    }
}
