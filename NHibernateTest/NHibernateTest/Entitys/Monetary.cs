using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernateTest.Entitys
{
    public class Monetary
    {
        public virtual decimal Amount { get; set; }
        public virtual string Currency { get; set; }
    }
}
