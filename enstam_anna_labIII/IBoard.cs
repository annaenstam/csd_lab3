using System;
using System.Collections.Generic;
using System.Text;

namespace enstam_anna_labIII
{
    public interface IBoard
    {
        bool Won { get; set; }
        string Winner { get; set; }
        string Id { get; set; }
        List<IBoard> Children { get; set; }
        string Parent { get; set; }
        int Level { get; set; }
        List<string> PlayedOrder { get; set; }
        Dictionary<string, string> Moves { get; set; }
        
        void RegisterMove(List<string> move, string player, List<string> address);
        
        List<List<string>> GetGeneralWinningStates();
        
        bool MatchesWonState(List<string> winningState, List<string> movesByPlayer);

        void DeclareWinnerIfWon();

        void DeclareBoardWon(string winner);

        List<string> GetMovesByO();

        List<string> GetMovesByX();

        List<string> GetWinningOrderLarge();

        List<string> GetAllWinningMovesSmall(List<string> allWinningMovesSmall, string winnerOfGame);

        List<Dictionary<int, int>> GetWinningCount(Dictionary<int, int> winningCountO, Dictionary<int, int> winningCountX);

        List<string> SortWinningOrder(List<string> winningState);
    }
}
