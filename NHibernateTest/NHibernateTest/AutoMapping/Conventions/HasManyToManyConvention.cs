using System;
using System.Diagnostics;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace NHibernateTest.AutoMapping.Conventions
{
    public class HasManyToManyConvention : IHasManyToManyConvention
    {


        public void Apply(IManyToManyCollectionInstance instance)
        {
            string entityDatabaseName = PersistenceModelGenerator.GetColumnName(instance.EntityType).ToDatabaseName();
            string childDatabaseName = PersistenceModelGenerator.GetColumnName(instance.ChildType).ToDatabaseName();
            string name = GetTableName(entityDatabaseName, childDatabaseName);

            instance.Table(name);
            
            instance.Key.Column(entityDatabaseName + "_ID");
            instance.Relationship.Column(childDatabaseName + "_ID");
            instance.Cascade.AllDeleteOrphan();
            Debug.WriteLine("----HasManyToManyConvention----"+instance.EntityType+" "+instance.ChildType); 
        }


        private string GetTableName(string a, string b)
        {
            int r = String.CompareOrdinal(a, b);
            if (r > 0)
            {
                return string.Format("{0}_{1}",b, a);
            }
            else
            {
                return string.Format("{0}_{1}", a, b);
            }
        }
    }
}