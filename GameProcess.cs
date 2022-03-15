using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry_game
{
    internal static class GameProcess
    {
        //Game process main part
        public static void PlayingGame(int[,] gameSpace)
        {
            int firstPlayersScore = 0;
            int secondPlayersScore = 0;
            int numberOfTurns = CalculatingTheNumberOfTurns(gameSpace);
            for (int i = 1; i <= numberOfTurns * 2; i++)
            {
                Console.WriteLine($"It's {(i + 1) / 2} turn");
                if (i % 2 == 1)
                {
                    gameSpace = PlayersTurn(gameSpace, 1, out int score);
                    firstPlayersScore += score;
                }
                else
                {
                    gameSpace = PlayersTurn(gameSpace, 2, out int score);
                    secondPlayersScore += score;
                }

                GameSpace.PrintingTheGameMatrix(gameSpace);
            }

            WithdrawingGameWinners(firstPlayersScore, secondPlayersScore);
        }

        //Making the player's turn accordng to player's number 
        private static int[,] PlayersTurn(int[,] gameSpace, int currentPlayer, out int score)
        {
            Console.WriteLine($"Now it's the {currentPlayer} player's turn");
            GettingRectangleLengths(out int xLength, out int yLength);
            GettingRectangleVertexCoordinates(out int xVertexCoordinate, out int yVertexCoorinate, xLength, yLength, gameSpace);
            gameSpace = PaintingRectangleWithcurrentParamsOrRetrowDices(gameSpace, xVertexCoordinate, yVertexCoorinate, xLength, yLength, currentPlayer, out score);
            return gameSpace;
        }

        //Module that can paint rectangle or rethrow dices if there is not enough space to paint it
        private static int[,] PaintingRectangleWithcurrentParamsOrRetrowDices (int[,] gameSpace, int xVertexCoordinate, int yVertexCoorinate, int xLength, int yLength, int currentPlayer, out int score)
        {
            int turnsCount = 1;
            score = 0;
            while (turnsCount <= 2)
            {
                if (CheckingTheAbilityToPrintRectangle(xVertexCoordinate, yVertexCoorinate, xLength, yLength, gameSpace))
                {
                    score += xLength * yLength;
                    Console.Clear();
                    return PaintingTheRectangle(xVertexCoordinate, yVertexCoorinate, xLength, yLength, gameSpace, currentPlayer);
                }
                else
                {
                    if (CheckingTheSpaceToPrintARectangle(gameSpace, xLength, yLength))
                    {
                        Console.WriteLine("You have enough space to draw your rectangle. Choose other vertex coordinate");
                        GettingRectangleVertexCoordinates(out xVertexCoordinate, out yVertexCoorinate, xLength, yLength, gameSpace);
                    }
                    else if (!CheckingTheSpaceToPrintARectangle(gameSpace, xLength, yLength) && turnsCount < 2)
                    {
                        Console.WriteLine($"You don't have enough space to draw your rectangle. You can rethrow your dices {2 - turnsCount++} more time (press any button)");
                        Console.ReadKey();
                        GettingRectangleLengths(out xLength, out yLength);
                        GettingRectangleVertexCoordinates(out xVertexCoordinate, out yVertexCoorinate, xLength, yLength, gameSpace);
                    }
                    else
                    {
                        Console.WriteLine("You don't have any space to draw or rethrows (press any button to continue)");
                        Console.ReadKey();
                        turnsCount++;
                    }
                }
            }

            Console.Clear();
            return gameSpace;
        }

        //Calculating the number of turns bazed on the size of playing field
        private static int CalculatingTheNumberOfTurns(int[,] gamespace)
        {
            int numberOfTurns = gamespace.GetLength(0) * gamespace.GetLength(1) / 30 - 1;
            Console.WriteLine($"Number of turns for each player is {numberOfTurns}");
            return numberOfTurns;
        }

        //Getting the length of rectangle by dice throw
        private static void GettingRectangleLengths(out int xLength, out int yLength)
        {
            Random random = new();
            xLength = random.Next(1, 6);
            yLength = random.Next(1, 6);
            Console.WriteLine($"Your X length is {xLength} and Y length is {yLength}");

        }

        //Getting the coordinates of dot, from wich the rectangle will be drawed
        private static void GettingRectangleVertexCoordinates(out int xVertexCoordinate, out int yVertexCoordinate, int xLength, int yLength, int[,] gameSpace)
        {
            Console.WriteLine("Enter the x coodinate of rectangle vertex (numbering starts from 1):");
            xVertexCoordinate = CoordinatesEntryValidation(gameSpace.GetLength(1), xLength);
            Console.WriteLine("Enter the y coodinate of rectangle vertex (numbering starts from 1):");
            yVertexCoordinate = CoordinatesEntryValidation(gameSpace.GetLength(0), yLength);
        }

        //Checking the correctness of the input of coordinates
        private static int CoordinatesEntryValidation(int limit, int rectangleSideLength)
        {
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out var coordinate) && coordinate >= 1 && coordinate <= limit - rectangleSideLength)
                {
                    return coordinate;
                }
                else
                {
                    Console.WriteLine($"Check the input. It must be integer number between 1 and {limit - rectangleSideLength} to fit the rectangle into the game space");
                }
            }
        }

        //Painting the rectangles (1 for first player and 2 for second player) 
        private static int[,] PaintingTheRectangle(int xVertexCoordinate, int yVertexCoordinate, int xLength, int yLength, int[,] gameSpace, int playersMarker)
        {
            for (int i = yVertexCoordinate; i < yVertexCoordinate + yLength; i++)
            {
                for (int j = xVertexCoordinate; j < xVertexCoordinate + xLength; j++)
                {
                    gameSpace[i, j] = playersMarker;
                }
            }

            return gameSpace;
        }

        //Checking the ability to print rectangle with current params on gamespace
        private static bool CheckingTheAbilityToPrintRectangle(int xVertexCoordinate, int yVertexCoordinate, int xLength, int yLength, int[,] gameSpace)
        {
            for (int i = yVertexCoordinate; i < yVertexCoordinate + yLength; i++)
            {
                for (int j = xVertexCoordinate; j < xVertexCoordinate + xLength; j++)
                {
                    if (gameSpace[i, j] != 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        //Withdrawing game winners 
        private static void WithdrawingGameWinners(int firstPlayerScore, int secondPlayerScore)
        {
            if (firstPlayerScore > secondPlayerScore)
            {
                Console.WriteLine("First player wins!");
            }
            else if (firstPlayerScore < secondPlayerScore)
            {
                Console.WriteLine("Second player wins!");
            }
            else
            {
                Console.WriteLine("DRAW");
            }
        }

        //Checking is there are enough space in whole gamespace to print a rectangle 
        private static bool CheckingTheSpaceToPrintARectangle(int[,] gameSpace, int xLength, int yLength)
        {
            for (int i = 1; i < gameSpace.GetLength(0) - yLength; i++)
            {
                for (int j = 1; j < gameSpace.GetLength(1) - xLength; j++)
                {
                    if (CheckingTheAbilityToPrintRectangle(j, i, xLength, yLength, gameSpace))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
