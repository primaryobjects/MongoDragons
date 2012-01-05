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
            // Initialize our database provider.
            Setup.Initialize();

            // Create some dragons and save them in the database.
            GenerateDragons();

            while (true)
            {
                // Display the dragons.
                List<Dragon> dragons = DisplayDragons();

                // Get input from the user.
                Console.Write("Enter an Id to Delete, or Q to quit:>");
                string input = Console.ReadLine();

                // Execute the user's command.
                if (input.ToUpper() == "Q")
                {
                    break;
                }
                else
                {
                    // See if this is a delete request.
                    int index;
                    if (Int32.TryParse(input, out index))
                    {
                        // Delete the dragon.
                        DragonManager.Delete(dragons[index - 1]);
                    }
                }
            }
        }

        private static void GenerateDragons()
        {
            for (int i = 0; i < 3; i++)
            {
                Dragon dragon = DragonManager.CreateRandom();
                DragonManager.Save(dragon);
            }
        }

        private static List<Dragon> DisplayDragons()
        {
            Console.WriteLine(String.Format("{0,3} | {1,-24} | {2,3} | {3,4} | {4,3} | {5,10} | {6,8}", "Id", "Name", "Age", "Gold", "HP", "Breath", "Born"));
            Console.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-");

            int count = 0;
            List<Dragon> dragons = DragonManager.GetAll();
            foreach (Dragon dragon in dragons)
            {
                Console.WriteLine(String.Format("{0, 3} | {1}", ++count, dragon));
            }

            return dragons;
        }
    }
}
