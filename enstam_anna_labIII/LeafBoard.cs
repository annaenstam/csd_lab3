using System;
using System.Collections.Generic;
using System.Text;

namespace enstam_anna_labIII
{
    class LeafBoard : IBoard
    {
        public bool Won { get; set; }
        public string Winner { get; set; }
        public string Id { get; set; }
        public List<IBoard> Children { get; set; }
        public string Parent { get; set; }
        public string Address { get; set; }
        public int Level { get; set; }
        public List<string> PlayedOrder { get; set; }
        public Dictionary<string, string> Moves { get; set; }

        public LeafBoard(string id, int level)
        {
            Won = false;
            Winner = "No winner";
            Id = id;
            Level = level;
            PlayedOrder = new List<string>();
            Children = new List<IBoard>();
            Children.Add(new NullBoard());

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

        public void RegisterMove(List<string> move, string player, List<string> address)
        {
            Moves[move[0]] = player;
            DeclareWinnerIfWon();
            if (!PlayedOrder.Contains(move[0]))
            {
                PlayedOrder.Add(move[0]);
            }
            address.RemoveAt(address.Count - 1);
            Address = string.Join(".", address);
        }

        public List<string> GetMovesByO()
        {
            List<string> movesByO = new List<string>();

            foreach (KeyValuePair<string, string> move in Moves)
            {
                if (move.Value.Equals("O"))
                {
                    movesByO.Add(move.Key);
                }
            }
            return movesByO;
        }

        public List<string> GetMovesByX()
        {
            List<string> movesByX = new List<string>();

            foreach (KeyValuePair<string, string> move in Moves)
            {
                if (move.Value.Equals("X"))
                {
                    movesByX.Add(move.Key);
                }
            }
            return movesByX;
        }

        public void DeclareWinnerIfWon()
        {
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

        public void DeclareBoardWon(string winner)
        {
            Won = true;
            Winner = winner;
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

        public List<string> GetAllWinningMovesSmall(List<string> allWinningMovesSmall, string winnerOfGame)
        {
            foreach (KeyValuePair<string, string> move in Moves)
            {
                if (move.Value.Equals(winnerOfGame))
                {
                    allWinningMovesSmall.Add(string.Concat(string.Concat(Address, "."), move.Key));
                }
            }
            return allWinningMovesSmall;
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

            List<Dictionary<int, int>> winningCounts = new List<Dictionary<int, int>>();
            winningCounts.Add(winningCountO);
            winningCounts.Add(winningCountX);
            return winningCounts;
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
