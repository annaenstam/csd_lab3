using System;
using System.Collections.Generic;
using System.Text;

namespace enstam_anna_labIII
{
    class NullBoard : IBoard
    {
        public bool Won { get; set; }
        public string Winner { get; set; }
        public string Id { get; set; }
        public List<IBoard> Children { get; set; }
        public string Parent { get; set; }
        public int Level { get; set; }
        public List<string> PlayedOrder { get; set; }
        public Dictionary<string, string> Moves { get; set; }

        public void DeclareBoardWon(string winner)
        {
        }

        public void DeclareWinnerIfWon()
        {
        }

        public List<string> GetAllWinningMovesSmall(List<string> allWinningMovesSmall, string winnerOfGame)
        {
            return new List<string>();
        }

        public List<List<string>> GetGeneralWinningStates()
        {
            return new List<List<string>>();
        }

        public List<string> GetMovesByO()
        {
            return new List<string>();
        }

        public List<string> GetMovesByX()
        {
            return new List<string>();
        }

        public List<Dictionary<int, int>> GetWinningCount(Dictionary<int, int> winningCountO, Dictionary<int, int> winningCountX)
        {
            return new List<Dictionary<int, int>>();
        }

        public List<string> GetWinningOrderLarge()
        {
            return new List<string>();
        }

        public bool MatchesWonState(List<string> winningState, List<string> movesByPlayer)
        {
            return false;
        }

        public void RegisterMove(List<string> move, string player, List<string> address)
        {
        }

        public List<string> SortWinningOrder(List<string> winningState)
        {
            return new List<string>();
        }
    }
}