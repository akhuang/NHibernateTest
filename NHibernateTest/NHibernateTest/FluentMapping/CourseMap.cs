using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using NHibernateTest.Entitys;

namespace NHibernateTest.FluentMapping
{
    public class CourseMap : ClassMap<Course>
    {
        public CourseMap()
        {
            Table("Course");
            Id(x => x.Id, "CourseId").GeneratedBy.HiLo("1000000000");
            Map(x => x.CourseName, "CourseName");
            //HasMany(x => x.Students).Cascade.All();
        }
    }
}
