using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernateTest.Entitys
{
    public class Teacher:Employee
    {
        /// <summary>
        /// 专业方向
        /// </summary>
        public virtual string Major { get; set; }

        public virtual Class ManageClass { get; set; }
        public override string ToString()
        {
            return "Teacher[" + Id + "] " + EmployeeNumber + " " + Name+" "+Major;
        }
    }
}
