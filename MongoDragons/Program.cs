using System;
using System.Collections.Generic;
using MongoDragons.Managers;
using MongoDragons.Types;

namespace MongoDragons
{
    class Program
    {
        static void Main(string[] args)
        {
            string keyword = "";

            // Initialize our database provider.
            Setup.Initialize();

            while (true)
            {
                // Display the dragons.
                List<Dragon> dragons = DisplayDragons(keyword);

                // Get input from the user.
                Console.Write("Enter text to search by or Q to quit:>");
                keyword = Console.ReadLine();

                // Check the input.
                if (keyword.ToUpper() == "Q")
                    break;
            }
        }

        private static List<Dragon> DisplayDragons(string keyword = "")
        {
            Console.WriteLine(String.Format("{0,3} | {1,-24} | {2,3} | {3,4} | {4,3} | {5,10} | {6,8}", "Id", "Name", "Age", "Gold", "HP", "Breath", "Born"));
            Console.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-");

            int count = 0;
            List<Dragon> dragons = DragonManager.Find(keyword);
            foreach (Dragon dragon in dragons)
            {
                Console.WriteLine(String.Format("{0, 3} | {1}", ++count, dragon));
            }

            return dragons;
        }
    }
}
