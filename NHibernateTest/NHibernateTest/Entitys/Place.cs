using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernateTest.Entitys
{
    public class Place:Entity
    {
        public virtual string Address { get; set; }
        public virtual int Size { get; set; }
        public virtual string Description { get; set; }
        public override string ToString()
        {
            return "Place[" + Id + "] " + Address + " " + Size;
        }
    }
}
