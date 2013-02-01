using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernateTest.Entitys
{
  public class Customer:Entity
  {
      public virtual string Name { get; set; }
      public virtual IList<Contacter> Contacters { get; set; } 
  }
    public class Contacter:Entity,IPermanent
    {
        public virtual string Email { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual bool IsDeleted { get; set; }
    }
}
