using System;
using System.Collections.Generic;
using System.Text;

namespace enstam_anna_labIII
{
    class CompositeBoard : IBoard
    {
        public bool Won { get; set; }
        public string Winner { get; set; }
        public string Id { get; set; }
        public List<IBoard> Children { get; set; }
        public string Parent { get; set; }
        public int Level { get; set; }
        public List<string> PlayedOrder { get; set; }
        public Dictionary<string, string> Moves { get; set; }

        public CompositeBoard(string id, List<IBoard> children, int level)
        {
            Won = false;
            Winner = "No winner";
            Children = children;
            Id = id;
            Level = level;
            PlayedOrder = new List<string>();

            Moves = new Dictionary<string, string>();
            Moves.Add("NW", ".");
            Moves.Add("NC", ".");
            Moves.Add("NE", ".");
            Moves.Add("CW", ".");
            Moves.Add("CC", ".");
            Moves.Add("CE", ".");
            Moves.Add("SW", ".");
            Moves.Add("SC", ".");
            Moves.Add("SE", ".");
        }

        public void DeclareWinnerIfWon()
        {
            foreach (IBoard child in Children)
            {
                if (child.Won == false)
                {
                    child.DeclareWinnerIfWon();
                }
            }

            if (Won == false)
            {
                foreach (List<string> winningState in GetGeneralWinningStates())
                {
                    if (MatchesWonState(winningState, GetMovesByO()))
                    {
                        DeclareBoardWon("O");
                    }
                    if (MatchesWonState(winningState, GetMovesByX()))
                    {
                        DeclareBoardWon("X");
                    }
                }
            }
        }

        public List<string> GetMovesByO()
        {
            List<string> movesByO = new List<string>();

            foreach (IBoard child in Children)
            {
                if (child.Won == true && child.Winner.Equals("O"))
                {
                    movesByO.Add(child.Id);
                }
            }
            return movesByO;
        }

        public List<string> GetMovesByX()
        {
            List<string> movesByX = new List<string>();

            foreach (IBoard child in Children)
            {
                if (child.Won == true && child.Winner.Equals("X"))
                {
                    movesByX.Add(child.Id);
                }
            }
            return movesByX;
        }

        public void DeclareBoardWon(string winner)
        {
            Won = true;
            Winner = winner;
        }

        public bool MatchesWonState(List<string> winningState, List<string> movesByPlayer)
        {
            int counter = 0;

            foreach (string move in movesByPlayer)
            {
                foreach (string coordinate in winningState)
                {
                    if (move.Equals(coordinate))
                    {
                        counter += 1;
                    }
                }
            }

            if (counter == 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void RegisterMove(List<string> move, string player, List<string> address)
        {
            foreach (IBoard child in Children)
            {
                if (move[0] == child.Id)
                {
                    child.RegisterMove(NextMove(move), player, address);
                    DeclareWinnerIfWon();
                    if (child.Won == true)
                    {
                        Moves[child.Id] = child.Winner;
                        if (!PlayedOrder.Contains(child.Id))
                        {
                            PlayedOrder.Add(child.Id);
                        }
                    }
                }
            }
        }

        private List<string> NextMove(List<string> move)
        {
            List<string> newMove = new List<string>();

            for (int i = 1; i < move.Count; i++)
            {
                newMove.Add(move[i]);
            }

            return newMove;
        }

        public List<string> GetAllWinningMovesSmall(List<string> allWinningMovesSmall, string winnerOfGame)
        {
            foreach (IBoard child in Children)
            {
                if (child.Won == true)
                {
                    child.GetAllWinningMovesSmall(allWinningMovesSmall, winnerOfGame);
                }
            }
            return allWinningMovesSmall; 
        }

        public List<string> GetWinningOrderLarge()
        {
            foreach (List<string> winningState in GetGeneralWinningStates())
            {
                if (MatchesWonState(winningState, GetMovesByO()))
                {
                    return SortWinningOrder(winningState);
                }
                if (MatchesWonState(winningState, GetMovesByX()))
                {
                    return SortWinningOrder(winningState);
                }
            }
            List<string> error = new List<string>();
            error.Add("No winning state found");
            return error;
        }

        public List<Dictionary<int, int>> GetWinningCount(Dictionary<int, int> winningCountO, Dictionary<int, int> winningCountX)
        {
            if (Winner.Equals("O"))
            {
                winningCountO[Level] = winningCountO[Level] + 1;
            }
            if (Winner.Equals("X"))
            {
                winningCountX[Level] = winningCountX[Level] + 1;
            }

            foreach (IBoard child in Children)
            {
                child.GetWinningCount(winningCountO, winningCountX);
            }

            List<Dictionary<int, int>> winningCounts = new List<Dictionary<int, int>>();
            winningCounts.Add(winningCountO);
            winningCounts.Add(winningCountX);
            return winningCounts;
        }

        public List<string> SortWinningOrder(List<string> winningState)
        {
            List<string> sortedWinningOrder = new List<string>();

            foreach (string playedMove in PlayedOrder)
            {
                foreach (string coordinate in winningState)
                {
                    if (playedMove.Equals(coordinate))
                    {
                        sortedWinningOrder.Add(playedMove);
                    }
                }
            }
            return sortedWinningOrder;
        }

        public List<List<string>> GetGeneralWinningStates()
        {
            List<List<string>> wonStates = new List<List<string>>();
            wonStates.Add(new List<string> { "NW", "NC", "NE" });
            wonStates.Add(new List<string> { "CW", "CC", "CE" });
            wonStates.Add(new List<string> { "SW", "SC", "SE" });
            wonStates.Add(new List<string> { "NW", "CW", "SW" });
            wonStates.Add(new List<string> { "NC", "CC", "SC" });
            wonStates.Add(new List<string> { "NE", "CE", "SE" });
            wonStates.Add(new List<string> { "NW", "CC", "SE" });
            wonStates.Add(new List<string> { "SW", "SC", "SE" });
            return wonStates;
        }
    }
}
