using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace enstam_anna_labIII
{
    public class ParseInput
    {
        public bool IsValidInputFormat(string input)
        {
            if (!IsOnlyLettersAndAllowedSymbols(input))
            {
                return false;
            }
            if (!EveryMoveIsSameDepth(input))
            {
                return false;
            }
            if (!IsOnlyValidCoordinates(input))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        
        public List<List<string>> ParseGameInput(string input)
        {
            return AllMovesToListOfLists(RemoveDuplicateMoves(AllMovesToList(input)));
        }

        private List<string> AllMovesToList(string input)
        {
            return input.ToUpper().Replace(" ", string.Empty).Split(',').ToList();
        }
        
        private List<string> RemoveDuplicateMoves(List<string> allMoves)
        {
            return allMoves.Distinct().ToList();
        }

        private List<List<string>> AllMovesToListOfLists(List<string> allMoves)
        {
            List<List<string>> listOfListsOfAllMoves = new List<List<string>>();
            foreach (string move in allMoves)
            {
                List<string> listOfOneMove = move.Split('.').ToList();
                listOfListsOfAllMoves.Add(listOfOneMove);
            }
            return listOfListsOfAllMoves;
        }

        private bool EveryMoveIsSameDepth(string input)
        {
            List<string> movesToCheck = AllMovesToList(input);

            int depthOfFirstMove = movesToCheck[0].Split('.').Length;

            foreach (string move in movesToCheck)
            {
                if (move.Split('.').Length != depthOfFirstMove)
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsOnlyLettersAndAllowedSymbols(string input)
        {
            string trimmedInput = input.ToUpper().Replace(" ", string.Empty);
            string allowedLettersAndSymbols = "NWCES.,";

            foreach (char c in trimmedInput)
            {
                if (!allowedLettersAndSymbols.Contains(c.ToString()))
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsOnlyValidCoordinates(string input)
        {
            List<string> allowedCoordinates = new List<string>();
            allowedCoordinates.Add("NW");
            allowedCoordinates.Add("NC");
            allowedCoordinates.Add("NE");
            allowedCoordinates.Add("CW");
            allowedCoordinates.Add("CC");
            allowedCoordinates.Add("CE");
            allowedCoordinates.Add("SW");
            allowedCoordinates.Add("SC");
            allowedCoordinates.Add("SE");

            foreach (List<string> move in AllMovesToListOfLists(RemoveDuplicateMoves(AllMovesToList(input))))
            {
                foreach (string coordinate in move)
                {
                    if (!allowedCoordinates.Contains(coordinate))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
