using System;
using System.Collections.Generic;
using System.Text;

namespace enstam_anna_labIII
{
    public class GameDirector
    {
        private IGameBuilder GameBuilder;

        public GameDirector(IGameBuilder gameBuilder)
        {
            GameBuilder = gameBuilder;
        }

        public Game CreateGame(List<List<string>> validMoves)
        {
            GameBuilder.SetValidMoves(validMoves);
            GameBuilder.SetGameDepth();
            GameBuilder.BuildGameBoard();
            GameBuilder.RegisterMoves();
            return GameBuilder.GetGame();
        }
    }
}
