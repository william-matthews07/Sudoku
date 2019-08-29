using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku
{


    public class SudokuBoard
    {


        private List<List<SudokuSpace>> SudokuSpaces {get;set;}
        //public SudokuParser Parser {get;set;}

        public List<int> NumberRange {get;}
        public int SquareSize {get;}

        public SudokuBoard(int squareSize)
        {
            NumberRange= new List<int>();
            SquareSize=squareSize;
            SudokuSpaces = new List<List<SudokuSpace>>();
            for (int i = 0; i < SquareSize; i++)
            {
                NumberRange.Add(i+1);
                List<SudokuSpace> spaces = new List<SudokuSpace>();
                for(int j=0;j<SquareSize; j++)
                {
                    spaces.Add(null);
                }
                SudokuSpaces.Add(spaces);
            }
            //SudokuSpaces.ForEach(i =>{i= new List<SudokuSpace>(SquareSize);});
        }
        public void AddSudokuSpace(SudokuSpace space, int row, int column)
        {
            SudokuSpaces[row][column]=space;
        }
        public SudokuSpace GetSudokuSpace(int row, int column)
        {
            return SudokuSpaces[row][column];
        }
        public ICollection<SudokuSpace> GetColumnValues(int column)
        {
            List<SudokuSpace> columnSpaces = new List<SudokuSpace>();
            SudokuSpaces.ForEach(row=>{columnSpaces.Add(row[column]);});
            return columnSpaces;
        }
        public ICollection<SudokuSpace> GetRowValues(int row)
        {
            return SudokuSpaces[row];
        }
        public ICollection<SudokuSpace> GetBlockValues(int startRow, int startCol, int size)
        {
            List<SudokuSpace> blockSpaces = new List<SudokuSpace>();
            for(int i =startRow; i<startRow+size; i++)
            {
                for (int j = startCol ; j < startCol+size; j++)
                {
                    blockSpaces.Add(SudokuSpaces[i][j]);
                }
            }
            return blockSpaces;
        }
       
      
        public SudokuBoard Copy()
        {

            SudokuBoard copy = new SudokuBoard(this.SquareSize);
            for(int i = 0; i<SquareSize; i++)
            {
                for(int j =0; j<SquareSize; j++)
                {
                    SudokuSpace space=GetSudokuSpace(i,j);
                    if(space!=null)
                    {
                        copy.AddSudokuSpace(space.Copy(),i,j);
                    }
                    
                }

            }
            return copy;
        }
        public IEnumerable<SudokuSpace> GetSpaces()
        {
          List<SudokuSpace> spaces = new List<SudokuSpace>();
          SudokuSpaces.ForEach(rowSpace => {spaces.AddRange(rowSpace);});
          return spaces;

        }
        override public string ToString()
        {
            StringBuilder boardString = new StringBuilder();
            
            for(int i =0 ; i<SquareSize; i++)
            {
                boardString.Append(string.Join("",GetRowValues(i)));
                boardString.Append("\r\n");
            }


            return boardString.ToString();
        }
    }
}