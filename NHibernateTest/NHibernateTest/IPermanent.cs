using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernateTest
{
    public interface IPermanent
    {
        bool IsDeleted { get; set; }

    }
}
