using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernateTest.Entitys
{
    public abstract class Person:Entity
    {
        public virtual string Name { get; set; }
        public virtual DateTime Birthday { get; set; }
        public virtual int Age { get { return DateTime.Today.Year - Birthday.Year; } }
        public virtual Gender Gender { get; set; }
        public override string ToString()
        {
            return "Person[" + Id + "] " + Name + " " + Birthday;
        }
    }
}
