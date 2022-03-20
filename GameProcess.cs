using System.Drawing;

namespace Geometry_game
{
    internal static class GameProcess
    {
        private const int MAX_RETHROWS_COUNT = 2;

        //Game process main part
        public static void PlayingGame(GameSpace space)
        {
            int firstPlayerId = 1;
            int secondPlayerId = 2;
            int numberOfTurns = CalculatingTheNumberOfTurns(space);

            for (int i = 1; i <= numberOfTurns; i++)
            {
                PlayerTurn(space, i, firstPlayerId);                
                
                PlayerTurn(space, i, secondPlayerId);                
            }

            DrawManager.DrawTable(space);
            WithdrawingGameWinner(space.GetScoreForPlayer(firstPlayerId), space.GetScoreForPlayer(secondPlayerId));
        }

        //Making the player's turn accordng to player's number 
        private static void PlayerTurn(GameSpace space, int turnNumber, int currentPlayer)
        {
            DrawManager.DrawTable(space);
            Console.WriteLine($"It's {turnNumber} turn player {currentPlayer}");

            Size rectangleSize = TryGetSuitableRectangleSizeWithRethrows(space);
            if (rectangleSize.IsEmpty)
            {
                return;
            }

            while (true)
            {
                Point rectangleRoot = GetRectangleRoot(rectangleSize, space);

                if (space.IsPossibleToSetRectangle(rectangleRoot, rectangleSize))
                {
                    space.SetRectangle(rectangleRoot, rectangleSize, currentPlayer);
                    return;
                }

            }
        }

        private static Size TryGetSuitableRectangleSizeWithRethrows(GameSpace space)
        {
            int rethrowsCount = 0;
            while (true)
            {
                Size rectangleSize = GetRectangleSize();
                if (space.IsHasSpaceForRectangleAnywhere(rectangleSize))
                {
                    return rectangleSize;
                }

                if (rethrowsCount == MAX_RETHROWS_COUNT)
                {
                    Console.WriteLine("You don't have enough space to draw your rectangle. " +
                        "You are run out of rethrows. \n(press any button to continue)");
                    Console.ReadKey();
                    return Size.Empty;
                }

                Console.WriteLine($"You don't have enough space to draw your rectangle. " +
                        $"You can rethrow your dices {MAX_RETHROWS_COUNT - rethrowsCount} more time. " +
                        $"\n(press any button to continue)");
                Console.ReadKey();
                rethrowsCount++;
            }
        }

        //Calculating the number of turns bazed on the size of playing field
        private static int CalculatingTheNumberOfTurns(GameSpace space)
        { 
            int numberOfTurns = space.Area / 30;
            Console.WriteLine($"Number of turns for each player is {numberOfTurns}.");
            return numberOfTurns;
        }

        //Getting the length of rectangle by dice throw
        private static Size GetRectangleSize()
        {
            Random random = new();
            Size rectangleSize = new Size();
            int maxDiceValue = 6;
            rectangleSize.Width = random.Next(1, maxDiceValue + 1);
            rectangleSize.Height = random.Next(1, maxDiceValue + 1);
            Console.WriteLine($"Your X length (width) is {rectangleSize.Width} and Y length (height) is {rectangleSize.Height}");
            return rectangleSize;
        }

        //Getting the coordinates of dot, from wich the rectangle will be drawed
        private static Point GetRectangleRoot(Size rectangleSize, GameSpace space)
        {
            string widthRequestMessage = "Enter the x (width) coodinate of rectangle vertex (numbering starts from 1):";
            int xRectangleRootUserInput = UserInput.GetIntInRange(1, space.Width - rectangleSize.Width + 1, widthRequestMessage);

            string heightRequestMessage = "Enter the y (height) coodinate of rectangle vertex (numbering starts from 1):";
            int yRectangleRootUserInput = UserInput.GetIntInRange(1, space.Height - rectangleSize.Height + 1, heightRequestMessage);

            // For user numbering starts from 1, for matrix numbering starts from 0 
            return new Point(xRectangleRootUserInput - 1, yRectangleRootUserInput - 1);
        }

        //Withdrawing game winners 
        private static void WithdrawingGameWinner(int firstPlayerScore, int secondPlayerScore)
        {
            if (firstPlayerScore > secondPlayerScore)
            {
                Console.WriteLine("First player wins!");
                return;
            }

            if (firstPlayerScore < secondPlayerScore)
            {
                Console.WriteLine("Second player wins!");
                return;
            }

            Console.WriteLine("DRAW");
        }
    }
}
