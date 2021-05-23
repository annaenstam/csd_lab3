using System;
using System.Collections.Generic;
using System.Text;

namespace enstam_anna_labIII
{
    public class GameBuilder : IGameBuilder
    {
        private Game Game;
        private List<string> BoardCoordinates = new List<string> { "NW", "NC", "NE", "CW", "CC", "CE", "SW", "SC", "SE" };
        private List<List<IBoard>> ListsOfLeafBoards { get; set; }

        public GameBuilder()
        {
            Reset();
        }

        public void Reset()
        {
            Game = new Game();
        }
       
        public void SetValidMoves(List<List<string>> validMoves)
        {
            Game.ValidMoves = validMoves;
        }

        public void SetGameDepth()
        {
            Game.Depth = CalculateDepth(Game.ValidMoves[0]);
        }

        public void BuildGameBoard()
        {
            ListsOfLeafBoards = new List<List<IBoard>>();
            
            CreateLeafBoards();

            if (Game.Depth > 1)
            {
                CreateCompositeBoards(ListsOfLeafBoards);
            }
        }

        public void RegisterMoves()
        {
            foreach (List<string> move in Game.ValidMoves)
            {
                List<string> address = new List<string>();
                address.AddRange(move);

                Game.GameTreeRoot.RegisterMove(move, Game.Turn, address);

                if (Game.Turn.Equals("O"))
                {
                    Game.Turn = "X";
                }   
                else
                {   
                    Game.Turn = "O";
                }
            }
        }

        public Game GetGame()
        {
            return Game;
        }

        private void CreateCompositeBoards(List<List<IBoard>> children)
        {
            bool rootCreated = false;
            List<IBoard> compositeBoards = new List<IBoard>();
            int coordinateCounter = 0;

            foreach (List<IBoard> collectionOfChildren in children)
            {
                if (children.Count == 1)
                {
                    CompositeBoard root = new CompositeBoard("root", collectionOfChildren, collectionOfChildren[0].Level - 1);
                    foreach (IBoard child in collectionOfChildren)
                    {
                        child.Parent = root.Id;
                    }
                    root.Level = 1;
                    rootCreated = true;
                    Game.GameTreeRoot = root;
                }

                if (children.Count != 1)
                {
                    CompositeBoard parent = new CompositeBoard(BoardCoordinates[coordinateCounter], collectionOfChildren, collectionOfChildren[0].Level - 1);
                    compositeBoards.Add(parent);

                    foreach (IBoard child in collectionOfChildren)
                    {
                        child.Parent = parent.Id;
                    }

                    coordinateCounter++;

                    if (coordinateCounter == 9)
                    {
                        coordinateCounter = 0;
                    }
                }
            }
            if (!rootCreated)
            {
                List<List<IBoard>> newChildren = SplitToSublists(compositeBoards);

                CreateCompositeBoards(newChildren);
            }
        }

        public void CreateLeafBoards()
        {
            int pow = Game.Depth - 2;

            if (pow == -1)
            {
                LeafBoard root = new LeafBoard("root", Game.Depth);
                Game.GameTreeRoot = root;
            }
            else if (pow > -1)
            {
                for (int i = 1; i <= Math.Pow(9, pow); i++)
                {
                    List<IBoard> leafboards = new List<IBoard>();

                    foreach (string coordinate in BoardCoordinates)
                    {
                        LeafBoard LeafBoard = new LeafBoard(coordinate, Game.Depth);
                        leafboards.Add(LeafBoard);
                    }

                    ListsOfLeafBoards.Add(leafboards);
                }
            }
        }

        private List<List<IBoard>> SplitToSublists(List<IBoard> input, int size = 9)
        {
            List<List<IBoard>> listOfSublists = new List<List<IBoard>>();

            for (int i = 0; i < input.Count; i += size)
            {
                listOfSublists.Add(input.GetRange(i, Math.Min(size, input.Count - i)));
            }

            return listOfSublists;
        }

        /// <summary>
        /// A method calculating the depth of a game. One board = depth 1. Zero '.' = depth 1.
        /// </summary>
        /// <param name="input"> A string of all the moves. </param>
        private int CalculateDepth(List<string> firstMove)
        {
            return firstMove.Count;
        }
    }
}
