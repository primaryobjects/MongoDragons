using System;
using MongoDB.Bson;

namespace MongoDragons.Types
{
    public class Dragon
    {
        public ObjectId Id { get; private set; }

        public string Name { get; set; }
        public int Age { get; set; }
        public string Description { get; set; }
        public int Gold { get; set; }
        public int MaxHP { get; set; }
        public int HP { get; set; }
        public Breath Weapon { get; set; }

        public bool IsPlayer { get; set; }
        public DateTime DateBorn { get; set; }
        public DateTime? DateDied { get; set; }
        public int Kills { get; set; }

        public Dragon()
        {
            DateBorn = DateTime.Now;
        }

        public override string ToString()
        {
            return string.Format("{0,-24} | {1,3} | {2,4} | {3,3} | {4,10} | {5,8}", Name, Age, Gold, HP, Weapon.Type.ToString(), DateBorn.ToShortDateString());
        }
    }
}