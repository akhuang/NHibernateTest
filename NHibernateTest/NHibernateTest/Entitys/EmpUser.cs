using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernateTest.Entitys
{
    public class EmpUser : Entity, IPermanent
    {
        public virtual string Name { get; set; }
        public virtual IList<EmpRole> Roles { get; set; }
        public virtual bool IsDeleted { get; set; }
    }
}
