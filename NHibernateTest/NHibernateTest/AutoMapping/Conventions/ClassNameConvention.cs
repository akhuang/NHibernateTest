using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using NHibernateTest.Entitys;

namespace NHibernateTest.AutoMapping.Conventions
{
    public class ClassNameConvention : IClassConvention
    {
        #region IClassConvention Members

        public virtual void Apply(IClassInstance instance)
        {
            string tableName = instance.EntityType.Name.ToDatabaseName();

            if (typeof (IPermanent).IsAssignableFrom(instance.EntityType))
            {
                instance.ApplyFilter<IsDeletedFilter>("IS_DELETED = :DeleteFlag");
            }

            instance.Table(tableName);
        }

        #endregion
    }
}