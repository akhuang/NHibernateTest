using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernateTest.Entitys
{
    public class Classroom:Place
    {
        public virtual string RoomNumber { get; set; }
        public override string ToString()
        {
            return "Classroom[" + Id + "] " + Address + " " + RoomNumber;
        }
    }
}
