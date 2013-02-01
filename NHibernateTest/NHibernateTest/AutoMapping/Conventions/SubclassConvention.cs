using System.Diagnostics;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace NHibernateTest.AutoMapping.Conventions
{
    public class SubclassConvention : ISubclassConvention
    {
        #region ISubclassConvention Members

        public void Apply(ISubclassInstance instance)
        {
            Debug.WriteLine("Subclass:" + instance.EntityType.Name);
            instance.DiscriminatorValue(instance.EntityType.Name);
        }

        #endregion

        //#region IConventionAcceptance<ISubclassInspector> Members

        //public void Accept(IAcceptanceCriteria<ISubclassInspector> criteria)
        //{
        //    Debug.WriteLine("Subclass:Accept" + criteria);
        //    //criteria.Expect(subclass => Type.GetType(subclass.Name).BaseType == typeof(Invoice));
        //}

        //#endregion
    }
}