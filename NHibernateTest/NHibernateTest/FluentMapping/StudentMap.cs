using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using NHibernateTest.Entitys;

namespace NHibernateTest.FluentMapping
{
    public class StudentMap : ClassMap<Student>
    {
        public StudentMap()
        {
            Table("Student");
            Id(x => x.Id, "StudentId").GeneratedBy.HiLo("1000000000");
            Map(x => x.Name, "Name").Not.Nullable();
            Map(x => x.Birthday, "Birthday");
            Map(x => x.Gender, "Gender");
            References(x => x.Class, "ClassId").Cascade.None().Not.Nullable();
            //HasManyToMany<Course>(x => x.Courses).Cascade.None();
            HasMany(x => x.Courses);
        }
    }
}
