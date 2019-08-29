using System.Collections.Generic;

namespace Sudoku
{
     public class SudokuSpace
     {
         public bool KnownValue {get;set;}
         public string SpaceCharter{get;set;}
         public int SpaceValue {get;set;}
         public List<int> PosibleValues {get;set;}

         override public string ToString()
         {
             return SpaceCharter;
         }

         public SudokuSpace Copy()
         {
             SudokuSpace copySpace = new SudokuSpace()
             {
                 KnownValue=this.KnownValue,
                 SpaceCharter=string.Copy(this.SpaceCharter),
                 SpaceValue=this.SpaceValue,
                 PosibleValues = this.PosibleValues==null?null:new List<int>(this.PosibleValues)
             };
            return copySpace;
         }

     }
}