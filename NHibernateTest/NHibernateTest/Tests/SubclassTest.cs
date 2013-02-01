using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernateTest.Entitys;
using NUnit.Framework;

namespace NHibernateTest.Tests
{
    [TestFixture]
    class SubclassTest:BaseTest
    {
        [TestFixtureSetUp]
        public void Init()
        {
            base.ReBuildDatabase();
        }
        [Test]
        public void TestSelect()
        {
            UserA a=new UserA(){Name = "User A",Files = new List<UserAFile>{new UserAFile(){FileName = "About A.txt"},new UserAFile(){FileName = "Only for User A"}}};
            UserB b=new UserB(){Number = "NumberB"};
            Session.Save(a);
            Session.Save(b);
            Session.Flush();
            var userb = NewSession.Get<UserB>(b.Id);

            Assert.AreEqual(userb.Files.Count,0);

        }
    }
}
