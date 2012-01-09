using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Norm;
using MongoDragons.Repository.Context;
using MongoDragons.Types;
using MongoDragons.Managers.Helpers;

namespace MongoDragons.Managers
{
    public static class RealmManager
    {
        public static Realm GetByRegion(Realm.RegionType region)
        {
            return DbContext.Current.Single<Realm>(r => r.Region == region);
        }

        public static void Save(Realm realm)
        {
            DbContext.Current.Add(realm);
        }

        #region Helpers

        public static Realm CreateRandom()
        {            
            Realm.RegionType region = (Realm.RegionType)HelperManager.RandomGenerator.Next(1, 5);

            // Load the realm.
            Realm realm = GetByRegion(region);
            if (realm == null)
            {
                // Create the realm if it doesn't exist.
                realm = new Realm(region);
                Save(realm);
            }

            return realm;
        }

        #endregion
    }
}
