using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace NHibernateTest.AutoMapping.Conventions
{
    public class SubClassNameConvention : IJoinedSubclassConvention
    {
        #region IJoinedSubclassConvention Members

        public void Apply(IJoinedSubclassInstance instance)
        {
            instance.Table(instance.EntityType.Name.ToDatabaseName());
        }

        #endregion
    }
}