using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace NHibernateTest.AutoMapping.Conventions
{
    public class PrimaryKeyConvention : IIdConvention
    {
        public const string NextHiValueColumnName = "VALUE";
        public const string NHibernateHiLoIdentityTableName = "HIBERNATE_UNIQUE_KEY";
        public const string TableColumnName = "TABLE_NAME";

        #region IIdConvention Members

        public virtual void Apply(IIdentityInstance instance)
        {
            string tableName = instance.EntityType.Name.ToDatabaseName();
            instance.Column(tableName + "_ID");
            if (instance.Type == typeof (long))
            {
                instance.GeneratedBy.HiLo(
                    NHibernateHiLoIdentityTableName,
                    NextHiValueColumnName,
                    "1000000000",
                    builder => builder.AddParam("where", string.Format("{0} = '{1}'", TableColumnName, tableName)));
            }
        }

        #endregion
    }
}