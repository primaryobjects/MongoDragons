using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MongoDragons.Managers
{
    public class HelperManager
    {
        public static Random RandomGenerator = new Random((int)DateTime.Now.Ticks);

        public static string CreateRandomName(string[] firstNameList, string[] lastNameList)
        {

            // Generate a random Id from 0-99.
            int randomId = RandomGenerator.Next(100);

            // Pad the Id to be 2 digits (01, 10, 99, etc).
            string randomString = randomId.ToString().PadLeft(2, '0');

            // Take the left digit to use as an index for the first name and the right digit to use as an index for the last name.
            int leftIndex = Convert.ToInt32(randomString[0].ToString());
            int rightIndex = Convert.ToInt32(randomString[1].ToString());

            string name = firstNameList[leftIndex] + " " + lastNameList[rightIndex];

            return name;
        }
    }
}
