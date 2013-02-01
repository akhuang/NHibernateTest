using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernateTest.Entitys
{
    public abstract class Employee:Person
    {
        public virtual string EmployeeNumber { get; set; }
        public virtual Monetary Salary { get; set; }
        public virtual DoorKey DoorKey { get; set; }
        public virtual IList<Education> Educations { get; set; }
        public Employee()
        {
            Educations=new List<Education>();
        }

        public override string ToString()
        {
            return "Employee[" + Id + "] " + EmployeeNumber + " " + Name;
        }
    }
}
