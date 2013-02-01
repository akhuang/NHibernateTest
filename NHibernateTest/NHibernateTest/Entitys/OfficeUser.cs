using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernateTest.Entitys
{
    public class OfficeUser:Employee
    {
        public override string ToString()
        {
            return "OfficeUser[" + Id + "] " + EmployeeNumber + " " + Name;
        }
    }
}
