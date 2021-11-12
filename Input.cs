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
        public static int GetInput(InputType inputType, List<ICard>playerHand = null, List<int> availableMoves = null)
        {
            SceneBuilder.SetCursorToInputPos();
            switch (inputType)
            {
                case InputType.Pass:
                    Console.ReadKey();
                    return 0;

                case InputType.PlSelectCard:
                    if (playerHand == null||availableMoves == null) 
                        throw new ArgumentNullException(nameof(playerHand) + " or " + nameof(availableMoves), "Missing argument");
                    return PlSelectCard(playerHand, availableMoves);

                case InputType.PlSelectSuit:
                    return PlSelectSuit();

                case InputType.AiSelectCard:
                    if (availableMoves == null) throw new ArgumentNullException(nameof(availableMoves), "Missing argument");
                    return AiSelect(availableMoves);

                case InputType.AiSelectSuit:
                    return AiSelect(new List<int>{0,1,2,3});

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

        private static int PlSelectCard(List<ICard> playerHand, List<int> availableMoves)
        {
            SceneBuilder.SetCursorToInputPos();
            var input = Convert.ToInt32(Console.ReadLine());
            if (input > 0) input -= 1;
            while (!availableMoves.Contains(input))
            {
                input = Convert.ToInt32(Console.ReadLine());
                if (input > 0) input -= 1;
            }

            return input;
        }
        private static int PlSelectSuit()
        {
            SceneBuilder.SetCursorToInputPos();
            var availableMoves = new[]{1, 2, 3, 4};
            var input = Convert.ToInt32(Console.ReadLine());
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
        Pass,
        PlSelectCard,
        PlSelectSuit,
        AiSelectCard,
        AiSelectSuit
    }
}
