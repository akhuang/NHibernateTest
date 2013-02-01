using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernateTest.Entitys
{
    public class Education:Entity
    {
        public virtual Employee Employee { get; set; }
        public virtual string University { get; set; }
        public virtual int GraduateYear { get; set; }
    }
}
