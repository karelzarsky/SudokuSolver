using System.Linq;

namespace SudokuLogic
{
    public class Board
    {
        public byte[,] submission = new byte[9, 9]; // Number 0 means empty
        public byte[,] solution = new byte[9, 9];
        public bool[,,] possibilities = new bool[9, 9, 9];

        public byte[] Col(int i)
        {
            return new byte[]{
                solution[0, i], solution[1, i], solution[2, i],
                solution[3, i], solution[4, i], solution[5, i],
                solution[6, i], solution[7, i], solution[8, i]};
        }

        public byte[] Row(int i)
        {
            return new byte[]{
                solution[i, 0], solution[i, 1], solution[i, 2],
                solution[i, 3], solution[i, 4], solution[i, 5],
                solution[i, 6], solution[i, 7], solution[i, 8]};
        }

        public byte[] Box(int i)
        {
            int OffsetX = (i % 3) * 3;
            int OffsetY = (i / 3) * 3;
            return new byte[]{
            solution[OffsetX + 0, OffsetY + 0], solution[OffsetX + 0, OffsetY + 1], solution[OffsetX + 0, OffsetY + 2],
            solution[OffsetX + 1, OffsetY + 0], solution[OffsetX + 1, OffsetY + 1], solution[OffsetX + 1, OffsetY + 2],
            solution[OffsetX + 2, OffsetY + 0], solution[OffsetX + 2, OffsetY + 1], solution[OffsetX + 2, OffsetY + 2]};
        }

        public bool IsGroupSolved(byte[] group)
        {
            if (group.Length != 9) return false;
            for (int i = 1; i < 10; i++)
            {
                if (group.Count(x => x == i) != 1) return false;
            }
            return true;
        }

        public bool IsGroupValid(byte[] group)
        {
            if (group.Length != 9) return false;
            if (group.Any(x => x > 9)) return false;
            for (int i = 1; i < 10; i++)
            {
                if (group.Count(x => x == i) > 1) return false;
            }
            return true;
        }

        public bool IsSolutionSolved()
        {
            for (int i = 0; i < 9; i++)
            {
                if (!IsGroupSolved(Row(i))) return false;
                if (!IsGroupSolved(Col(i))) return false;
                if (!IsGroupSolved(Box(i))) return false;
            }
            return true;
        }

        public bool IsSolutionValid()
        {
            for (int i = 0; i < 9; i++)
            {
                if (!IsGroupValid(Row(i))) return false;
                if (!IsGroupValid(Col(i))) return false;
                if (!IsGroupValid(Box(i))) return false;
            }
            return true;
        }
    }
}
