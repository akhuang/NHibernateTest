using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernateTest
{
    public abstract class Entity
    {
        public virtual long Id { get; set; }
        public override string ToString()
        {
            return this.GetType() + "[" + Id + "]";
        }
    }
    
}
