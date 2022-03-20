using System.Runtime.InteropServices;

namespace Geometry_game
{
    internal class DrawManager
    {
        #region Import Win32 API calls
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        private static IntPtr ThisConsole = GetConsoleWindow();
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int MAXIMIZE = 3;
        #endregion

        private static string player1Symbol = "O";
        private static string player2Symbol = "X";

        private static int cellSize = 3;

        public static void SetUpConsole()
        {
            Console.SetBufferSize(1000, 1000);
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            ShowWindow(ThisConsole, MAXIMIZE);
        }

        public static void DrawTable(GameSpace space)
        {
            Console.Clear();

            DrawHeader(space);
            DrawHorizontalLine(space.Width);

            for (int y = 0; y < space.Height; y++)
            {
                DrawRow(space, y);
                DrawHorizontalLine(space.Width);
            }
        }

        private static void DrawHeader(GameSpace space)
        {
            Console.Write(new string(' ', cellSize));

            for (int x = 1; x <= space.Width; x++)
            {
                Console.Write(string.Format($"|{{0,{cellSize}}}", x));
            }
            Console.WriteLine("|");
        }

        private static void DrawRow(GameSpace space, int rowId)
        {
            // Drow left part of row with row number. For user numbering starts from 1, for matrix numbering starts from 0.
            Console.Write(string.Format($"{{0,{cellSize}}}", rowId + 1));

            for (int x = 0; x < space.Width; x++)
            {
                string symbol = GetSymbolForCellByValue(space.Values[rowId, x]);
                Console.Write($"| {symbol} ");
            }
            Console.WriteLine("|");
        }

        private static string GetSymbolForCellByValue(int value)
        {
            if (value == 0)
            {
                return " ";
            }

            if (value == 1)
            {
                return player1Symbol;
            }

            if (value == 2)
            {
                return player2Symbol;
            }

            return String.Empty;
        }

        private static void DrawHorizontalLine(int spaceWidth)
        {
            // Size of matrix with values + column with rows id's
            for (int i = 0; i < spaceWidth + 1; i++)
            {
                Console.Write("---+");
            }            
            Console.WriteLine();
        }
    }
}
