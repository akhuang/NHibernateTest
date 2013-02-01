using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernateTest.Entitys
{
    public class DoorKey:Entity
    {
        public virtual IList<Employee> Employees { get; set; }
        public virtual string Name { get; set; }
    }
}
