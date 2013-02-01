using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using NHibernateTest.Entitys;

namespace NHibernateTest.FluentMapping
{
    public class ClassesMap : ClassMap<Class>
    {
        public ClassesMap()
        {
            Table("Class");
            Id(x => x.Id, "ClassId").GeneratedBy.HiLo("1000000000");
            Map(x => x.ClassName, "ClassName").Not.Nullable();
            HasMany(x => x.Students).KeyColumn("ClassId").Cascade.All().Inverse();
        }
    }
}
