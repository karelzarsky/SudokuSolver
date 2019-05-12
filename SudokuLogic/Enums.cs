using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuLogic
{
    public enum Source : byte
    {
        Empty,
        File,
        Keyboard,
        Random,
        Solver,
        BruteForce
    }
}
