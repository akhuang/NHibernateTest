using System;
using System.Collections.Generic;
using FluentNHibernate.Mapping;

namespace NHibernateTest.Entitys
{
    public class Student : Person,ICloneable //,IPermanent
    {
        public Student()
        {
            Courses=new List<Course>();
        }

        public virtual int VersionNumber { get; set; }

        public virtual Class Class { get; set; }
        //public virtual bool IsDeleted { get; set; }
        public virtual IList<Course> Courses { get; set; }

        public override string ToString()
        {
            return "Student[" + Id + "] Name:" + Name + " Birthday:" + Birthday + " Gender:" + Gender;
        }

        public virtual object Clone()
        {
            return this.MemberwiseClone();
        }
    }


}