using System;
using System.Diagnostics;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace NHibernateTest.AutoMapping.Conventions
{
    internal class CollectionConvention : ICollectionConvention
    {


        public void Apply(ICollectionInstance instance)
        {
            string colName;
            Type entityType = instance.EntityType;
            Type childType = instance.ChildType;
            if (entityType == childType)
                colName = "PARENT_ID";
            else
            {
                colName = PersistenceModelGenerator.GetColumnName(entityType).ToDatabaseName() + "_ID";
            }
            instance.Key.Column(colName);
            instance.Cascade.AllDeleteOrphan();
            Debug.WriteLine("----CollectionConvention----"+entityType.ToString()+" "+childType.ToString()); 
        }



        //private static bool HasMoreThanOneProperty(Type p, Type c)
        //{
        //    var count = 0;
        //    var ps = p.GetProperties();
        //    foreach (var propertyInfo in ps)
        //    {
        //        if (!propertyInfo.PropertyType.IsGenericType) continue;
        //        var argType = propertyInfo.PropertyType.GetGenericArguments()[0];
        //        if (argType.FullName == c.FullName)
        //            count++;
        //        if (count > 1)
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}
    }
}