using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernateTest.Entitys
{
    public class EmpRole:Entity,IPermanent
    {
        public virtual string Name { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual IList<EmpUser> Users { get; set; }
    }
}
