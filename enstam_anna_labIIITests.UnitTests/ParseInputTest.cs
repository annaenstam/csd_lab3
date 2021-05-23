using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace enstam_anna_labIIITests.UnitTests
{
    [TestClass]
    public class ParseInputTest
    {
        [TestMethod]
        public void ParseGameInput_DuplicateMoveInInput_ReturnsMovesWithoutDuplicateMoves()
        {
            string movesWithDuplicateMove = "NW.CC, NC .cc , NW. CC, SW.CC, CC.CC, NW.NW";

            List<List<string>> movesWithoutDuplicateMove = new List<List<string>>();
            List<string> move1 = new List<string>();
            List<string> move2 = new List<string>();
            List<string> move3 = new List<string>();
            List<string> move4 = new List<string>();
            List<string> move5 = new List<string>();

            move1.Add("NW");
            move1.Add("CC");
            move2.Add("NC");
            move2.Add("CC");
            move3.Add("SW");
            move3.Add("CC");
            move4.Add("CC");
            move4.Add("CC");
            move5.Add("NW");
            move5.Add("NW");

            movesWithoutDuplicateMove.Add(move1);
            movesWithoutDuplicateMove.Add(move2);
            movesWithoutDuplicateMove.Add(move3);
            movesWithoutDuplicateMove.Add(move4);
            movesWithoutDuplicateMove.Add(move5);

            //Arrange
            var parser = new ParseInput();

            //Act
            List<List<string>> result = parser.ParseGameInput(movesWithDuplicateMove);
            //string resultString = "";

            //foreach (List<string> move in result)
            //{
            //    resultString = string.Concat(resultString, move)
            //}

            //List<string> test = result.ToString();
            //string test2 = test.ToString();
            //string referens = "NW.CC, NC.CC, NW.CC, SW.CC, CC.CC, NW.NW";

            //Assert
            CollectionAssert.AreEqual(movesWithoutDuplicateMove, result);
            
        }
    }
}
