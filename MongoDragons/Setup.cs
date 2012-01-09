using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StructureMap;
using MongoDragons.Repository.Interface;
using MongoDragons.Repository.Concrete;
using MongoDragons.Repository.Context;

namespace MongoDragons
{
    internal static class Setup
    {
        /// <summary>
        /// Initializes StructureMap (dependency injector) to setup our concrete database provider.
        /// </summary>
        public static void Initialize()
        {
            // Initialize our concrete database provider type.
            ObjectFactory.Initialize(x => { x.For<IRepository>().Use<MongoRepository>(); });
        }

        /// <summary>
        /// Disposes the database provider context.
        /// </summary>
        public static void Close()
        {
            if (DbContext.IsOpen)
            {
                DbContext.Current.Dispose();
            }
        }
    }
}
