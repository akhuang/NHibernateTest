using System.Diagnostics;
using System.Threading;
using NHibernate;
using NUnit.Framework;

namespace NHibernateTest.Tests
{
    [TestFixture]
    internal class LockTest : BaseTest
    {
        [TestFixtureSetUp]
        public void Init()
        {
            //base.ReBuildDatabase();
            //Class c1=new Class(){Name = "zengyi"};
            //var session = Session;
            //session.Save(c1);
            //session.Flush();
            c1Id = 1000000001;
            ReBuildDatabase();
        }

        private long c1Id;

        [Test]
        public void QuickUpdateData()
        {
            ISession session = Session;
            ITransaction tran = session.BeginTransaction();
            string hql = "update NHibernateTest.Entitys.Class set ClassName=ClassName+1 where Id=" + c1Id;
            Debug.WriteLine("Will Update ");
            IQuery uq = session.CreateQuery(hql);
            if (uq.ExecuteUpdate() == 1)
            {
                Debug.WriteLine("ExecuteUpdate ");
                IQuery sq = session.CreateQuery("select ClassName from NHibernateTest.Entitys.Class where Id=" + c1Id);
                var result = sq.UniqueResult<string>();
                Debug.WriteLine("UniqueResult ");
                tran.Commit();
                Debug.WriteLine("Commit ");
                Debug.WriteLine("Result:" + result);
            }
            else
            {
                Debug.WriteLine("uq.ExecuteUpdate() != 1");
            }
        }

        [Test]
        public void UpdateData()
        {
            ISession session = Session;
            ITransaction tran = session.BeginTransaction();

            string hql = "update Class set ClassName=ClassName+1 where Id=" + c1Id;
            //string sql = "update Class set ClassName='lulu' where ClassId=" + c1Id;
            Debug.WriteLine("Will Update 5 second later");
            Thread.Sleep(5000);
            IQuery uq = session.CreateQuery(hql);
            if (uq.ExecuteUpdate() == 1)
            {
                Thread.Sleep(10000);
                IQuery sq = session.CreateQuery("select ClassName from Class where Id=" + c1Id);
                var result = sq.UniqueResult<string>();
                tran.Commit();
                Debug.WriteLine("Result:" + result);
            }
            else
            {
                Debug.WriteLine("uq.ExecuteUpdate() != 1");
            }
        }
    }
}