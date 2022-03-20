using System.Drawing;

namespace Geometry_game
{
    public class GameSpace
    {
        private const int WIDTH_MIN = 30;
        private const int WIDTH_MAX = 99;
        private const int HEIGHT_MIN = 20;
        private const int HEIGHT_MAX = 99;

        private readonly int[,] valuesMatrix;

        public GameSpace(int width, int height)
        {
            valuesMatrix = new int[height, width];
        }

        public int Width { get => valuesMatrix.GetLength(1); }
        public int Height { get => valuesMatrix.GetLength(0); }
        public int Area { get => Width * Height; }
        public int[,] Values { get => valuesMatrix; }


        //Creating game space with params, inputted by user
        public static GameSpace CreateGameSpace()
        {
            Console.WriteLine("Enter the params of game field");

            string widthRequestMessage = $"X length (width) will be (from {WIDTH_MIN} to {WIDTH_MAX}):";
            int width = UserInput.GetIntInRange(WIDTH_MIN, WIDTH_MAX, widthRequestMessage);

            string heightRequestMessage = $"Y length (height) will be (from {HEIGHT_MIN} to {HEIGHT_MAX}):";
            int height = UserInput.GetIntInRange(HEIGHT_MIN, HEIGHT_MAX, heightRequestMessage);

            return new GameSpace(width, height);
        }

        //Checking is there are enough space in whole gamespace to print a rectangle 
        public bool IsHasSpaceForRectangleAnywhere(Size rectangleSize)
        {
            for (int y = 0; y < this.Height - rectangleSize.Height; y++)
            {
                for (int x = 0; x < this.Width - rectangleSize.Width; x++)
                {
                    if (IsPossibleToSetRectangle(new Point(x, y), rectangleSize))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        //Checking the ability to print rectangle with current params on gamespace
        public bool IsPossibleToSetRectangle(Point rectangleRoot, Size rectangleSize)
        {
            for (int y = rectangleRoot.Y; y < rectangleRoot.Y + rectangleSize.Height; y++)
            {
                for (int x = rectangleRoot.X; x < rectangleRoot.X + rectangleSize.Width; x++)
                {
                    if (valuesMatrix[y, x] != 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        //Painting the rectangles (1 for first player and 2 for second player) 
        public void SetRectangle(Point rectangleRoot, Size rectangleSize, int playerMarker)
        {
            for (int y = rectangleRoot.Y; y < rectangleRoot.Y + rectangleSize.Height; y++)
            {
                for (int x = rectangleRoot.X; x < rectangleRoot.X + rectangleSize.Width; x++)
                {
                    valuesMatrix[y, x] = playerMarker;
                }
            }
        }

        public int GetScoreForPlayer(int playerId)
        {
            int score = 0;
            for (int y = 0; y < this.Height; y++)
            {
                for (int x = 0; x < this.Width; x++)
                {
                    if (valuesMatrix[y, x] == playerId)
                    {
                        score++;
                    }
                }
            }
            return score;
        }
    }
}
