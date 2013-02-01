using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using NHibernate;
using NHibernate.Type;

namespace NHibernateTest
{
    public class IsDeletedFilter : FilterDefinition
    {
        public const string FilterName = "IsDeletedFilter";
        public const string Parameter = "DeleteFlag";

        public IsDeletedFilter()
        {
            this.WithName("IsDeletedFilter").WithCondition("IsDeleted = :DeleteFlag").AddParameter("DeleteFlag", (IType)NHibernateUtil.Boolean);
        }
    }
}
