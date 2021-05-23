using System;
using System.Collections.Generic;
using System.Text;

namespace enstam_anna_labIII
{
    public interface IGameBuilder
    {
        void Reset();
        void SetValidMoves(List<List<string>> validMoves);
        void SetGameDepth();
        void BuildGameBoard();
        void RegisterMoves();
        Game GetGame();
    }
}
