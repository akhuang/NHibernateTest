using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using NHibernateTest.Entitys;
using NUnit.Framework;

namespace NHibernateTest.Tests
{
    [TestFixture]
    public class NullableTest:BaseTest
    {
        [TestFixtureSetUp]
        public void Init()
        {
            ReBuildDatabase();
            //A a1=new A(){Age = 20};
            //A a2= new A() { Age = 22 };
            //CollA collA=new CollA(){AList = new List<A>(){a1,a2}};
            //Session.SaveOrUpdate(collA);
            //Session.Flush();
           

        }
        [Test]
        public void TestInsertA()
        {
            Session.CreateSQLQuery("INSERT INTO COLL_A (COLL_A_ID) VALUES (1);").ExecuteUpdate();
            Session.CreateSQLQuery("INSERT INTO A (COLL_A_ID ,IS_RIGHT, AGE, A_ID) VALUES (1,null,20,1);").ExecuteUpdate();
            Session.CreateSQLQuery("INSERT INTO A (COLL_A_ID ,IS_RIGHT, AGE, A_ID) VALUES (1,1,null,2);").ExecuteUpdate();
            Session.Flush();

            var session = NewSession;
            A a1 = new A() { Age = 20 };
            var ca = session.Get<CollA>((long)1);
            foreach (var a in ca.AList)
            {
                Debug.WriteLine(a);
            }
            a1.CollA = ca;
            session.SaveOrUpdate(a1);
            session.Flush();

        }
    }
}
