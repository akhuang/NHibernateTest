using System.Diagnostics;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using NHibernateTest.Entitys;

namespace NHibernateTest.AutoMapping.Conventions
{
    using FluentNHibernate.Mapping;

    public class HasManyConvention : IHasManyConvention
    {
        #region IHasManyConvention Members

        public void Apply(IOneToManyCollectionInstance instance)
        {
            instance.Key.Column(instance.EntityType.Name.ToDatabaseName() + "_ID");
            instance.Cascade.AllDeleteOrphan();
            //instance.Inverse();

            if (typeof (IPermanent).IsAssignableFrom(instance.ChildType))
            {
                instance.ApplyFilter<IsDeletedFilter>("IS_DELETED = :DeleteFlag");
            }
            Debug.WriteLine("----HasManyConvention----"+instance.EntityType.Name.ToDatabaseName() + "_ID" + instance.EntityType.ToString()+" "+instance.ChildType.ToString()); 
        }

        #endregion

     
    }
}