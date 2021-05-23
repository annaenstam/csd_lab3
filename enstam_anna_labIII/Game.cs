using System;
using System.Collections.Generic;
using System.Text;

namespace enstam_anna_labIII
{
    public class Game
    {
        public string Turn { get; set; }
        public List<List<string>> ValidMoves { get; set; }
        public int Depth { get; set; }
        public IBoard GameTreeRoot { get; set; }

        public Game()
        {
            Turn = "O";
        }

        public void DisplayResult()
        {
            OutputLine1();
            OutputLine2();
            OutputLine3();
        }

        private void OutputLine1()
        {
            if (GameTreeRoot.Won == true)
            {
                Console.WriteLine("Output line 1:");
                Console.WriteLine(string.Join(", ", GameTreeRoot.GetWinningOrderLarge()));
            }
            else
            {
                Console.WriteLine("There was no winner");
            }
        }

        private void OutputLine2()
        {
            if (GameTreeRoot.Won == true)
            {
                Console.WriteLine("");
                Console.WriteLine("Output line 2:");
                Console.WriteLine(GetWinningOrderSmall());
            }
            else
            {
                Console.WriteLine("There was no winner");
            }
        }

        private void OutputLine3()
        {
            Console.WriteLine("");
            Console.WriteLine("Output line 3:");
            Console.WriteLine(GetWinningCount());
        }

        public string GetWinningOrderSmall()
        {
            List<string> emptyList = new List<string>();
            List<string> winningMovesSmall = GameTreeRoot.GetAllWinningMovesSmall(emptyList, GameTreeRoot.Winner);
            List<string> winningOrderSmall = new List<string>();

            foreach (string move in ValidMovesToSingleList(ValidMoves))
            {
                if (winningMovesSmall.Contains(move))
                {
                    winningOrderSmall.Add(move);
                }
            }
            return string.Join(", ", winningOrderSmall);
        }

        public string GetWinningCount()
        {
            Dictionary<int, int> winningCountO = new Dictionary<int, int>();
            Dictionary<int, int> winningCountX = new Dictionary<int, int>();

            for (int i = 1; i < Depth + 1; i++)
            {
                winningCountO.Add(i, 0);
                winningCountX.Add(i, 0);
            }

            List<Dictionary<int, int>> winningCount = GameTreeRoot.GetWinningCount(winningCountO, winningCountX);

            string stringOfWinningCountO = "";
            string stringOfWinningCountX = "";

            foreach (KeyValuePair<int, int> levelCount in winningCount[0])
            {
                stringOfWinningCountO = string.Concat(stringOfWinningCountO, string.Concat(".", levelCount.Value));
            }
            stringOfWinningCountO = stringOfWinningCountO.Substring(1);

            foreach (KeyValuePair<int, int> levelCount in winningCount[1])
            {
                stringOfWinningCountX = string.Concat(stringOfWinningCountX, string.Concat(".", levelCount.Value));
            }
            stringOfWinningCountX = stringOfWinningCountX.Substring(1);

            return string.Concat("Winning count O, X: ", string.Concat(stringOfWinningCountO, string.Concat(", ", stringOfWinningCountX)));
        }
        private List<string> ValidMovesToSingleList(List<List<string>> validMoves)
        {
            List<string> validMovesSingleList = new List<string>();
            foreach (List<string> move in validMoves)
            {
                validMovesSingleList.Add(string.Join(".", move));
            }
            return validMovesSingleList;
        }
    }

}
