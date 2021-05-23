using System;
using System.Collections.Generic;

namespace enstam_anna_labIII
{
    class Program
    {
        static void Main(string[] args)
        {
            ParseInput p = new ParseInput();

            if (!p.IsValidInputFormat(args[0]))
            {
                Console.WriteLine("The input is not of the correct format, please check your input.");
            }
            else
            {
                try
                {
                    Game game;
                    GameBuilder gameBuilder = new GameBuilder();
                    GameDirector gameDirector = new GameDirector(gameBuilder);
                    game = gameDirector.CreateGame(p.ParseGameInput(args[0]));
                    game.DisplayResult();
                }
                catch (Exception)
                {
                    Console.WriteLine("Unable to play game..");
                }
            }
        }
    }
}
