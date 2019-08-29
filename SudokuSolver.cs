using System.Collections.Generic;
using System.Linq;
using System;

namespace Sudoku{
public class SudokuSolver{



 private SudokuBoard Solved {get;set;}
 
 
 
 public void SolvePuzzle(SudokuBoard board) 
 {
     Solved=null;
     board= SolvePuzzleRecursion(ref board);
     if(board is null || board.GetSpaces().Any(i=>i.KnownValue==false))
     {
         throw new SolveException("This board cannot be solved");
     }
 }
 
 protected SudokuBoard SolvePuzzleRecursion(ref SudokuBoard  board){


    bool foundKnow=false;
    bool deadSpace=false;
    SudokuSpace spaceWithPossbile=null;
    int rowOfLeast=-1;
    int colOfLeast=-1;
   
    if(board is null)
    {
        return board;
    }
    if(!board.GetSpaces().All(i=>i.KnownValue))
    {
    
        for(int i =0; i<board.SquareSize; i++)
        {
            IEnumerable<int> rowSpaces=SpacesValuesKnown( board.GetRowValues(i));

            for(int j=0; j<board.SquareSize;j++)
            {
                SudokuSpace space = board.GetSudokuSpace(i,j);
                

                if(!space.KnownValue)

                {
                
                    IEnumerable<int> colSpaces=SpacesValuesKnown(board.GetColumnValues(j));
                    int uppperRow =i/3;
                    int upperColumn =j/3;
                    IEnumerable<int> block=SpacesValuesKnown(board.GetBlockValues(uppperRow*3,upperColumn*3,3));
                    space.PosibleValues =( from pos in board.NumberRange.AsEnumerable()
                    where !rowSpaces.Contains(pos) 
                        && !colSpaces.Contains(pos) 
                        && !block.Contains(pos)
                    select pos).ToList();
                    if(space.PosibleValues.Count==0)
                    {
                        deadSpace=true;
                    }
                    else if(space.PosibleValues.Count==1)
                    {
                        foundKnow=true;
                        space.KnownValue=true;
                        space.SpaceValue=space.PosibleValues.First();
                        space.SpaceCharter=space.PosibleValues.First().ToString();
                    }
                    else
                    {
                        if(spaceWithPossbile==null 
                        || spaceWithPossbile.PosibleValues.Count>space.PosibleValues.Count)
                        {
                            rowOfLeast=i;
                            colOfLeast=j;
                            spaceWithPossbile=space;
                        }
                    }

                }
            }

        }
        
        
        if(deadSpace)
        {
            board=null;
        }
        else if (foundKnow)
        {
            SolvePuzzleRecursion(ref board);
        } 
        else if(rowOfLeast!=-1)
        {
            SudokuSpace space = board.GetSudokuSpace(rowOfLeast,colOfLeast);
            foreach(int posibleValue in  space.PosibleValues)
            {
                if(!(Solved is null))
                {
                    break;
                }

                SudokuBoard clone = board.Copy();
                SudokuSpace cloneSpace= clone.GetSudokuSpace(rowOfLeast, colOfLeast);
                cloneSpace.PosibleValues=null;
                cloneSpace.KnownValue=true;
                cloneSpace.SpaceValue=posibleValue;
                cloneSpace.SpaceCharter=posibleValue.ToString();
                SolvePuzzleRecursion(ref clone);
                if(Solved is null 
                    && !(clone is null) 
                    && clone.GetSpaces().All(i=>i.KnownValue))
                {
                    Solved=clone;
                    break;
                }
            }
        }
    }
    return Solved is null? board:Solved;


}
private IEnumerable<int> SpacesValuesKnown(ICollection<SudokuSpace> spaces)
{
    return spaces.Where(i=>i.KnownValue).Select(i=>i.SpaceValue);
}



}
}
