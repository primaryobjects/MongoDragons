using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Norm;

namespace MongoDragons.Types
{
    public class Realm
    {
        public enum RegionType
        {
            Mist,
            Love,
            Abyss,
            Hatred,
            Hell
        };

        public ObjectId Id { get; private set; }
        public string Name { get; set; }
        public RegionType Region { get; set; }

        public Realm()
        {
        }

        public Realm(RegionType region)
        {
            Name = region.ToString();
            Region = region;
        }
    }
}
