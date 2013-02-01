using System;
using System.Diagnostics;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace NHibernateTest.AutoMapping.Conventions
{
    using FluentNHibernate.Mapping;

    public class ReferenceConvention : IReferenceConvention
    {


        public void Apply(IManyToOneInstance instance)
        {
            string colName = null;
            Type referenceType = instance.Class.GetUnderlyingSystemType();
            Type entityType = instance.EntityType;
            string propertyName = instance.Property.Name;
            //instance.LazyLoad(Laziness.NoProxy);
            //Self association
            if (referenceType == entityType)
                colName = "PARENT_ID";
            else
                colName = propertyName.ToDatabaseName() + "_ID";

            instance.Column(colName);
            instance.Cascade.None();

            Debug.WriteLine("----ReferenceConvention----" + colName + instance.EntityType + " referenceType:" + referenceType);
        }


    }
}