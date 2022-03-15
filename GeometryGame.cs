using Geometry_game;
using System;
using System.Linq;


namespace GeometryGame 
{
    internal static class GeometryGame
    {
        static void Main()
        {         
            int[,] gameSpace = GameSpace.CreateGameSpace();
            GameSpace.PrintingTheGameMatrix(gameSpace);
            GameProcess.PlayingGame(gameSpace);
        }        
    }
}
