using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDragons.Repository.Context;
using MongoDragons.Types;
using MongoDB;

namespace MongoDragons.Managers
{
    public static class DragonManager
    {
        #region Names

        private static string[] _firstNames = new string[]
        {
            "White",
            "Black",
            "Light",
            "Dark",
            "Evil",
            "Cunning",
            "Magic",
            "Silver",
            "Golden",
            "Slimy"
        };

        private static string[] _lastNames = new string[]
        {
            "Legendary",
            "Sneaky",
            "Cheating",
            "Stealth",
            "Serpent",
            "Ghost",
            "Chimaera",
            "Hippogryph",
            "Spirit",
            "Skeleton"
        };

        #endregion

        public static List<Dragon> GetAll()
        {
            return DbContext.Current.All<Dragon>().OrderBy(d => d.Name).ToList();
        }

        public static void Save(Dragon dragon)
        {
            DbContext.Current.Add(dragon);
        }

        public static void Delete(Dragon dragon)
        {
            DbContext.Current.Delete<Dragon>(d => d.Id == dragon.Id);
        }

        public static Dragon CreateRandom()
        {
            Random r = new Random((int)DateTime.Now.Ticks);
            Dragon dragon = new Dragon();

            dragon.Name = HelperManager.CreateRandomName(_firstNames, _lastNames);
            dragon.Age = r.Next(1, 101);
            dragon.Description = "A big dragon.";
            dragon.Gold = r.Next(1, 1001);
            dragon.Weapon = new Breath() { Name = "Breath", Description = "A breath attack.", Type = (Breath.BreathType)r.Next(0, 6) };
            dragon.MaxHP = r.Next(10, 21);
            dragon.HP = dragon.MaxHP;

            return dragon;
        }
    }
}
