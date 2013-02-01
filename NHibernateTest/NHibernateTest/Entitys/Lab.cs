using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernateTest.Entitys
{
    public class Lab:Place
    {
        public virtual string LabSubject { get; set; }
        public override string ToString()
        {
            return "Lab[" + Id + "] " + Address + " " + LabSubject;
        }
    }
}
