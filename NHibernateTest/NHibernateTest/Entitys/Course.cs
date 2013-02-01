using System.Collections.Generic;
using FluentNHibernate.Mapping;

namespace NHibernateTest.Entitys
{
    public class Course:Entity
    {
        public Course()
        {
            //Students=new List<Student>();
        }
        public virtual string CourseName { get; set; }
        //public virtual IList<Student> Students { get; set; }

        public override string ToString()
        {
            return "Id:" + Id + " Name:" + CourseName;
        }
    }


}