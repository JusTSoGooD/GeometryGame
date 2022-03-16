using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry_game
{
    public static class GameSpace
    {
        //Creating game space with params, introduced by user
        internal static int[,] CreateGameSpace()
        {
            Console.WriteLine("Enter the params of game field");
            Console.WriteLine("X length will be (min 30): ");
            int horizontalLength = CoordinatesEntryValidation(10);
            Console.WriteLine("Y length will be (min 20): ");
            int verticalLength = CoordinatesEntryValidation(10);
            int[,] gamespace = new int[verticalLength + 1, horizontalLength + 1];
            gamespace = FormingTheNumerationGrid(gamespace);
            return gamespace;
        }

        //Checking the correctness of the input of coordinates
        private static int CoordinatesEntryValidation(int limit)
        {
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out var coordinate) && coordinate >= limit)
                {
                    return coordinate;
                }
                else
                {
                    Console.WriteLine($"Check the input. It must be integer number greater or equal {limit}");
                }
            }
        }

        //Forming the numeration grid 
        private static int[,] FormingTheNumerationGrid(int[,] gameSpace)
        {
            for (int i = 0; i < gameSpace.GetLength(0); i++)
            {
                gameSpace[i, 0] = i;
            }

            for (int j = 0; j < gameSpace.GetLength(1); j++)
            {
                gameSpace[0, j] = j;
            }

            return gameSpace;
        }

        //Printing the game space matrix 
        internal static void PrintGameMatrix(int[,] gameSpace)
        {
            for (int i = 0; i < gameSpace.GetLength(1); i++)
            {
                Console.Write($"{gameSpace[0, i]} ");
            }

            Console.WriteLine();
            for (int i = 1; i < gameSpace.GetLength(0); i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.Write($"{gameSpace[i, j]} ");
                }

                for (int j = 9; j < gameSpace.GetLength(1); j++)
                {
                    Console.Write($"{gameSpace[i, j]}  ");
                }
                Console.WriteLine();
            }
        }
    }

}
