using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Iesi.Collections;
using NHibernate.Engine;
using NHibernate.Event;
using NHibernate.Event.Default;
using NHibernate.Persister.Entity;

namespace NHibernateTest
{
    public class MyDeleteEventListener : DefaultDeleteEventListener
    {

        protected override void DeleteEntity(IEventSource session, object entity,

            EntityEntry entityEntry, bool isCascadeDeleteEnabled,

            IEntityPersister persister, ISet transientEntities)
        {
        
            if (entity is IPermanent)
            {

                var e = (IPermanent)entity;

                e.IsDeleted = true;



                CascadeBeforeDelete(session, persister, entity, entityEntry, transientEntities);

                CascadeAfterDelete(session, persister, entity, transientEntities);

            }

            else
            {

                base.DeleteEntity(session, entity, entityEntry, isCascadeDeleteEnabled,

                                  persister, transientEntities);

            }

        }

    }

    public class  MyLoadEventListener:    NHibernate.Event.Default.DefaultLoadEventListener
    {
        protected override object DoLoad(LoadEvent @event, IEntityPersister persister, EntityKey keyToLoad, LoadType options)
        {
            return base.DoLoad(@event, persister, keyToLoad, options);
        }
        protected override IEntityPersister GetEntityPersister(ISessionFactoryImplementor factory, string entityName)
        {
            return base.GetEntityPersister(factory, entityName);
        }
    }
}
