using System;
using System.Diagnostics;
using FluentNHibernate;
using FluentNHibernate.Automapping;
using FluentNHibernate.Utils;
using NHibernateTest.Entitys;

namespace NHibernateTest.AutoMapping
{
    using Conventions;

    public class AutoMapConfiguration : DefaultAutomappingConfiguration
    {
        public override bool ShouldMap(Type type)
        {
          
            return (type.IsClass && type.Namespace.StartsWith("NHibernateTest.Entitys"));
            //return type.In(typeof (IbEmployee), typeof (Employee), typeof (EmployeeBackup));
        }
        public override bool ShouldMap(Member member)
        {
            return member.IsProperty && member.IsPublic && member.CanWrite;
        }
        public override bool IsComponent(System.Type type)
        {
            return type.In(typeof(Monetary));
        }
        public override string GetComponentColumnPrefix(Member member)
        {
            return member.Name.ToDatabaseName() + "_";
        }
        public override bool IsVersion(Member member)
        {
            return false;
            return member.Name == "Version";
        }

        //public override bool IsId(Member member)
        //{
        //    return member.Name == "Id";
        //}

        //public override bool IsConcreteBaseType(Type type)
        //{
        //    return type.In(typeof(Entity), typeof(Person)
        //        );
        //}
        public override bool AbstractClassIsLayerSupertype(Type type)
        {
            return !type.In(
                typeof(Employee),
                typeof(UploadFile)
                );
        }
        
        public override bool IsDiscriminated(Type type)
        {
            return type.In(
                typeof(Employee), 
                typeof(UploadFile)
                );
        }

        public override string GetDiscriminatorColumn(Type type)
        {
            return "TYPE";
        }
    }
}