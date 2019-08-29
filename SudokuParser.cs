using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Sudoku
{
    public class SudokuParser
    {
   
        public string UnknownTextValue { get; set; }
        public string NumberRangeRegex {get;set;}
        public string LineTermnation {get;}
        public Regex LineRegex {get; protected set;}

        public SudokuParser()
        {
            UnknownTextValue="X";
            NumberRangeRegex="[1-9]";
            LineTermnation="\r\n";
            BuildLineValidation();
        }
        public void BuildLineValidation(){
            LineRegex = new Regex(UnknownTextValue+"|"+NumberRangeRegex);
            
        }


        public void ParserText(string text, SudokuBoard sudokuBoard)
        {

            string[] lines =text.TrimEnd().Split(LineTermnation);
            if(lines.Length!=sudokuBoard.SquareSize)
            {
                throw new ParseException("Text row count does not match board size");
            }
           
           for (int i = 0; i < lines.Length; i++){
               
                // List<string> characterColumns = new List<string>();
                if( string.Join("", LineRegex.Split(lines[i]))!="")
                 {
                     throw new ParseException("Unknow characters");
                 }
                 MatchCollection matches= LineRegex.Matches(lines[i]);
                if(matches.Count!=sudokuBoard.SquareSize){
                   throw new ParseException("Text column count does not match board size");
                   //throw something here
               }
               foreach(Match match in matches){
                   sudokuBoard.AddSudokuSpace(
                       new SudokuSpace()
                        { 
                            SpaceCharter=match.Value,
                            KnownValue=match.Value!=UnknownTextValue,
                            SpaceValue= match.Value!=UnknownTextValue?int.Parse(match.Value):0
                        },
                        i,
                        match.Index
                        );
                       
               }
           }
           
        }

    }
}