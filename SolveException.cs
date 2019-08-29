using System;
namespace Sudoku{
    public class SolveException : Exception
    {
         public SolveException()
        {
        }

        public SolveException(string message)
            : base(message)
        {
        }

        public SolveException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}