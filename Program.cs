using System;
using System.IO;
using System.Linq;
using System.Globalization;
namespace Sudoku
{
    class Program

    {
        static void Main(string[] args)
        {



 
            string dir = args.First();
            var files= Directory.EnumerateFiles(dir).Where(file =>file.EndsWith(".txt", true,CultureInfo.InvariantCulture));
            foreach(string file in files)
            {
                string outputString ="";
                try
                {
                    SudokuBoard  board = new SudokuBoard(9);
                    SudokuParser parser = new SudokuParser();
                    parser.ParserText(File.ReadAllText(file),board);
                    SudokuSolver solver = new SudokuSolver();
                    solver.SolvePuzzle(board);
                    file.Substring(0,file.Length-4);
                    outputString=board.ToString();
                    
                } 
                catch( Exception e) when (( e is ParseException) || (e is SolveException))
                {
               
                    outputString=e.Message;
                }
                File.WriteAllText($"{file.Substring(0,file.Length-4)}.sln.txt",outputString);
            }


           
        }
    }
}
