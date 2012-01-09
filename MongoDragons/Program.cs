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
                // Search for dragons.
                List<Dragon> dragons = DragonManager.Find(keyword);

                // Display the dragons.
                DisplayDragons(dragons);

                // Get input from the user.
                Console.Write("Enter text to search by or Q to quit:>");
                keyword = Console.ReadLine();

                // Check the input.
                if (keyword.ToUpper() == "Q")
                    break;
            }

            Setup.Close();
        }

        #region Helpers

        private static List<Dragon> DisplayDragons(List<Dragon> dragons)
        {
            Console.WriteLine(String.Format("{0,3} | {1,-17} | {2,3} | {3,4} | {4,3} | {5,10} | {6,8} | {7,5}", "Id", "Name", "Age", "Gold", "HP", "Breath", "Born", "Realm"));
            Console.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-");

            int count = 0;
            foreach (Dragon dragon in dragons)
            {
                Console.WriteLine(String.Format("{0, 3} | {1}", ++count, dragon));
            }

            return dragons;
        }

        #endregion
    }
}
