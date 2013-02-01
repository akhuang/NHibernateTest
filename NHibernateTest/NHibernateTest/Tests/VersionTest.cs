using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernateTest.Tests
{
    using System.Diagnostics;
    using NUnit.Framework;

    class VersionTest:BaseTest
    {
        [TestFixtureSetUp]
        public void Init()
        {
            ReBuildDatabase();
        }

        [Test]
        public void TestUpdateStudentVersion()
        {
            var c1 = InitClasses();
            var s1 = c1.Students[0];
            Session.SaveOrUpdate(c1);
            Session.Flush();
            Debug.WriteLine("Version:" +s1.VersionNumber);
            s1.Name = "测试2";
            Session.SaveOrUpdate(s1);
            Session.Flush();
            Debug.WriteLine("Version:" + s1.VersionNumber);
        }
    }
}
