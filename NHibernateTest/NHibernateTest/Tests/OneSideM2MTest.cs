using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Criterion;
using NHibernateTest.Entitys;
using NUnit.Framework;

namespace NHibernateTest.Tests
{
    [TestFixture]
    class OneSideM2MTest:BaseTest
    {
        private EmpUser u1;
        private EmpUser u2;
        private EmpRole r1;
        private EmpRole r2;



        [TestFixtureSetUp]
        public void InitData()
        {
            ReBuildDatabase();
            u1=new EmpUser(){Name = "User1"};
            u2 = new EmpUser() { Name = "User2" };
            r1=new EmpRole(){Name = "Select"};
            r2 = new EmpRole() { Name = "Edit" };
            u1.Roles=new List<EmpRole>(){r1,r2};
            u2.Roles=new List<EmpRole>(){r1};
            Session.Save(u1);
            Session.Save(u2);
            Session.Save(r1);
            Session.Save(r2);
            Session.Flush();
        }
        [Test]
        public void TestDeleteOneSide()
        {
            var session = NewSession;
            EmpUser u2 =
                session.CreateCriteria<EmpUser>().Add(Expression.Eq("Name", "User2")).UniqueResult<EmpUser>();
            session.Delete(u2);
            session.Flush();

        }
        [Test]
        public void TestAddOneSide()
        {


            var r3 = new EmpRole() {Name = "Delete"};
            u1.Roles.Add(r3);
            Session.SaveOrUpdate(u1);
            Session.Flush();
        }
    }
}
