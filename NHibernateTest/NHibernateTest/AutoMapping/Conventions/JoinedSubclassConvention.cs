using System.Diagnostics;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace NHibernateTest.AutoMapping.Conventions
{
    public class JoinedSubclassConvention : IJoinedSubclassConvention
    {
        #region IJoinedSubclassConvention Members

        public void Apply(IJoinedSubclassInstance instance)
        {
            string colName = PersistenceModelGenerator
                                 .GetColumnName(instance.Type.BaseType)
                                 .ToDatabaseName()
                             + "_ID";
            instance.Key.Column(colName);
            Debug.WriteLine("----IJoinedSubclassConvention----"+instance.Type);
        }

        #endregion
    }
}