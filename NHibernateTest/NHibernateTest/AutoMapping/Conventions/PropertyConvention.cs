using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace NHibernateTest.AutoMapping.Conventions
{
    public class PropertyConvention : IPropertyConvention
    {
        #region IPropertyConvention Members

        public void Apply(IPropertyInstance instance)
        {
            instance.Column(instance.Name.ToDatabaseName());
        }

        #endregion
    }
}