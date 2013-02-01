using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace NHibernateTest.AutoMapping.Conventions
{
    public class JoinConvention : IJoinConvention
    {
        #region IJoinConvention Members

        public void Apply(IJoinInstance instance)
        {
            string colName = PersistenceModelGenerator
                                 .GetColumnName(instance.EntityType)
                                 .ToDatabaseName()
                             + "_ID";
            instance.Key.Column(colName);
        }

        #endregion
    }
}