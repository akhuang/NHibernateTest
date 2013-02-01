using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace NHibernateTest.AutoMapping
{
    using System.Diagnostics;
    using Entitys;
    using FluentNHibernate.Automapping;
    using FluentNHibernate.Automapping.Alterations;

    public class CourseMapping : IAutoMappingOverride<Course>
    {
        public void Override(AutoMapping<Course> mapping)
        {
            //mapping.HasManyToMany(cr => cr.Students).Inverse();
            Debug.WriteLine("Inverse   Course");
        }
    }
    public class StudentMapping : IAutoMappingOverride<Student>
    {
        public void Override(AutoMapping<Student> mapping)
        {
            mapping.HasManyToMany(cr => cr.Courses).Not.Inverse();
            Debug.WriteLine("Inverse   Student");
            mapping.References(s => s.Class, "CLASS_ID").Not.Nullable();
        }
    }
    public class TeacherMapping : IAutoMappingOverride<Teacher>
    {
        public void Override(AutoMapping<Teacher> mapping)
        {
            mapping.HasOne(x => x.ManageClass);
        }
    }
    public class ClassMapping : IAutoMappingOverride<Class>
    {
        public void Override(AutoMapping<Class> mapping)
        {
            mapping.HasOne(x => x.Adviser);
            mapping.HasMany(c => c.Students).KeyColumn("CLASS_ID").Cascade.AllDeleteOrphan().Inverse();
        }
    }
    public class AMapping : IAutoMappingOverride<UserA>
    {
        public void Override(AutoMapping<UserA> mapping)
        {
            mapping.HasMany(a => a.Files).KeyColumn("USER_ID");
        }
      
    }

    public class UMapping : IAutoMappingOverride<UploadFile>
    {
        public void Override(AutoMapping<UploadFile> mapping)
        {
            mapping.DiscriminateSubClassesOnColumn("TYPE").AlwaysSelectWithValue();
        }

    }
    public class BMapping : IAutoMappingOverride<UserB>
    {
       
        public void Override(AutoMapping<UserB> mapping)
        {
            mapping.HasMany(a => a.Files).KeyColumn("USER_ID");//.Where("type= 'NHibernateTest.Entitys.UserB'");
        }
    }

    public class TypeFilter : FilterDefinition
    {
        public const string FilterName = "TypeFilter";
        public const string Parameter = "TypeFlag";

        public TypeFilter()
        {
            WithName(FilterName).WithCondition("Type == \"ProjectFile\"");
        }
    }

    //public class EmpUserMapping:IAutoMappingOverride<EmpUser>
    //{
    //    public void Override(AutoMapping<EmpUser> mapping)
    //    {
    //        mapping.HasManyToMany(u => u.Roles).Cascade.AllDeleteOrphan().Not.Inverse();
    //    }
    //}
    //public class EmpRoleMapping : IAutoMappingOverride<EmpRole>
    //{
    //    public void Override(AutoMapping<EmpRole> mapping)
    //    {
    //        mapping.HasManyToMany(u => u.Users).Cascade.None().Inverse();
    //    }
    //}
}
