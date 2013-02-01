using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernateTest.Entitys
{
    public class User:Entity
    {
        public virtual string Name { get; set; }
        public virtual Department Department { get; set; }
    }
    public class Department:Entity
    {
        public virtual IList<User> Users { get; set; }
        public virtual string Name { get; set; }
    }
    public class Award : Entity
    {
        public virtual string Name { get; set; }
        public virtual IList<Emp> Emps { get; set; }
    }
    public class Emp : Entity
    {
        public virtual string Name { get; set; }
        public virtual IList<Award> Awards { get; set; }
    }

#region one 2 one

    public class Dept : Entity
    {
        public virtual string Name { get; set; }
        public virtual Mgr Mgr { get; set; }
    }

    public class Mgr : Entity
    {
        public virtual string Name { get; set; }
        public virtual Dept Dept { get; set; }
    }

    #endregion

}
