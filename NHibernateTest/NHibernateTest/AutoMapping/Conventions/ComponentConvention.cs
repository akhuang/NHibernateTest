using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernateTest.AutoMapping.Conventions
{
    using System.Diagnostics;
    using FluentNHibernate.Conventions;
    using FluentNHibernate.Conventions.Instances;

    public class ComponentConvention : IComponentConvention
    {
        public void Apply(IComponentInstance instance)
        {
            var p = instance.Property;

            Debug.WriteLine("----ComponentConvention----"+p);
        }
    }
}
