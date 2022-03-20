using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry_game
{
    internal static class UserInput
    {
        public static int GetInt(string requestMessage, string errorMessage = "")
        {
            while (true)
            {
                Console.WriteLine(requestMessage);
                if (int.TryParse(Console.ReadLine(), out int input))
                {
                    return input;
                }

                Console.WriteLine(errorMessage); 
            }
        }

        public static int GetIntInRange(int minValue, int maxValue, string requestMessage, string errorMessage = "")
        {
            string defaultErrorMessage = $"Check the input. It must be integer number between {minValue} and {maxValue}.";
            errorMessage = errorMessage.Equals(string.Empty) ? defaultErrorMessage : errorMessage;

            while (true)
            {
                int input = GetInt(requestMessage, errorMessage);
                if (input >= minValue && input <= maxValue)
                {
                    return input;
                }
                Console.WriteLine(errorMessage);
            }
        }
    }
}
