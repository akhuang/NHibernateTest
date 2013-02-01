using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernateTest.Entitys
{
    public class A:Entity
    {
        public virtual bool IsRight { get; set; }
        public virtual int Age { get; set; }
        public virtual CollA CollA { get; set; }
    }
    public class CollA:Entity
    {
        public virtual IList<A> AList { get; set; } 
    }
}
