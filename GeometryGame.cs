using Geometry_game;

namespace GeometryGame
{
    internal static class GeometryGame
    {
        static void Main()
        {
            DrawManager.SetUpConsole();
            GameSpace space = GameSpace.CreateGameSpace();
            GameProcess.PlayingGame(space);
        }
    }
}
