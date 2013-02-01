using System.Collections.Generic;
using FluentNHibernate.Mapping;

namespace NHibernateTest.Entitys
{
    public class Class : Entity//,IPermanent
    {
        public Class()
        {
            Students=new List<Student>();
        }
        //public virtual bool IsDeleted { get; set; }
        public virtual string ClassName { get; set; }
        public virtual IList<Student> Students { get; set; }
        public virtual Teacher Adviser { get; set; }

        public override string ToString()
        {
            return "Class Id:" + Id + " Name:" + ClassName;
        }
    }

   
}