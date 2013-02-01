using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernateTest.AutoMapping.Conventions
{
    using System.Diagnostics;
    using FluentNHibernate.Conventions;
    using FluentNHibernate.Conventions.Inspections;
    using FluentNHibernate.Conventions.Instances;
    using FluentNHibernate.Mapping;

     class HasOneToOneConvention : IHasOneConvention
    {
        public void Apply(IOneToOneInstance instance)
        {
            Type inferredType = ((IOneToOneInspector)instance).Class.GetUnderlyingSystemType();
         
            instance.LazyLoad();
         
            instance.Cascade.None();


            Debug.WriteLine("----OneToOneInstance ----" + inferredType);
        }
    }
}
