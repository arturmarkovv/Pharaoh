using System;
using System.Collections.Generic;
using System.Linq;
using Pharaoh.Helpers;

namespace Pharaoh
{
    public static class Input
    {
        public static void WaitInput()
        {
            Console.ReadKey();
        }
        public static int GetInput(InputType inputType, List<int> availableMoves = null)
        {
            SceneBuilder.SetCursorToInputPos();
            switch (inputType)
            {

                case InputType.PlSelectCard:
                    if (availableMoves == null) 
                        throw new ArgumentNullException(nameof(availableMoves), "Missing argument");
                    return PlSelectCard();

                case InputType.PlSelectSuit:
                    return PlSelectSuit();

                case InputType.AiSelectCard:
                    if (availableMoves == null) throw new ArgumentNullException(nameof(availableMoves), "Missing argument");
                    return AiSelect(availableMoves);

                case InputType.AiSelectSuit:
                    return AiSelect(new List<int> {0,1,2,3});

                default:
                    throw new ArgumentOutOfRangeException(nameof(inputType), inputType, null);
            }
        }

        public static string GetPlayerName()
        {
            SceneBuilder.SetCursorToInputPos();
            var playerName = Console.ReadLine();
            if (string.IsNullOrEmpty(playerName))
            {
                playerName = "Player";
            }

            return playerName;
        }

        private static int PlSelectCard()
        {
            int input = -1;
            SceneBuilder.SetCursorToInputPos();
            do
            {
                try
                {
                    input = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    // ignored
                }
            } while (input == -1);
             
            //if (input > 0) input -= 1;
            //while (!availableMoves.Contains(input))
            //{
            //    input = Convert.ToInt32(Console.ReadLine());
            //    if (input > 0) input -= 1;
            //}


            return input;
        }

        public static int GetValidPlayerInput(int input, List<int> availableMoves)
        {
            if (input > 0) input -= 1;
            while (!availableMoves.Contains(input))
            {
                input = Input.GetInput(InputType.PlSelectCard, availableMoves);
                if (input > 0) input -= 1;
            }

            return input;
        }

        private static int PlSelectSuit()
        {
            SceneBuilder.SetCursorToInputPos();
            var availableMoves = new[]{1, 2, 3, 4};
            int input = -1;
            SceneBuilder.SetCursorToInputPos();
            do
            {
                try
                {
                    input = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    // ignored
                }
            } while (input == -1);
            while (!availableMoves.Contains(input))
            {
                input = Convert.ToInt32(Console.ReadLine());
            }

            return input-1;
        }

        private static int AiSelect(List<int> aiAvailableMoves)
        {
            var rnd = new Random();
            return aiAvailableMoves[rnd.Next(aiAvailableMoves.Count)];
        }
    }

    public enum InputType
    {
        PlSelectCard,
        PlSelectSuit,
        AiSelectCard,
        AiSelectSuit
    }
}
