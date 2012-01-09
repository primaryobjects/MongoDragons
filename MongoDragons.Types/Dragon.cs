using System;
using Norm;
using Norm.Attributes;
using MongoDragons.Repository.Context;

namespace MongoDragons.Types
{
    public class Dragon
    {
        public ObjectId Id { get; private set; }
        public ObjectId RealmId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Description { get; set; }
        public int Gold { get; set; }
        public int MaxHP { get; set; }
        public int HP { get; set; }
        public Breath Weapon { get; set; }
        public DateTime DateBorn { get; set; }
        public DateTime? DateDied { get; set; }

        private Realm _realm;
        [MongoIgnore]
        public Realm Realm
        {
            get
            {
                // Lazy-load.
                if (_realm == null)
                {
                    _realm = DbContext.Current.Single<Realm>(r => r.Id == RealmId);
                }

                return _realm;
            }
            set
            {
                RealmId = value.Id;
                _realm = value;
            }
        }

        public Dragon()
        {
            DateBorn = DateTime.Now;
        }

        public override string ToString()
        {
            return string.Format("{0,-17} | {1,3} | {2,4} | {3,3} | {4,-10} | {5,8} | {6,-6}", Name, Age, Gold, HP, Weapon.Type.ToString(), DateBorn.ToShortDateString(), Realm.Name);
        }
    }
}