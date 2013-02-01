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
    class DeleteTest:BaseTest
    {
        [TestFixtureSetUp]
        public void Setup()
        {
            ReBuildDatabase();
        }
        [Test]
        public void SoftDeleteTest()
        {
            var c = InitClasses();
            Session.Save(c);
            Session.Flush();
            c.Students.Clear();
            //Session.Delete(c);
            Session.Flush();
        }
        [Test]
        public void TestDeleteMany2Many()
        {
            Student s=new Student(){Birthday = DateTime.Today,Gender = Gender.Male,Name = "测试3"};
            Course c=new Course(){CourseName = "英语"};
            s.Courses.Add(c);
            //c.Students.Add(s);
            Session.SaveOrUpdate(s);
            Session.Flush();
            s.Courses.Clear();
            Session.SaveOrUpdate(s);
            Session.Flush();
        }

        [Test]
        public void TestDelete()
        {
            var cust = new Customer() {Name = "Customer1",Contacters = new List<Contacter>()};
            var ct1 = new Contacter() {Email = "ct1@cicc.com.cn",Customer = cust};
            var ct2 = new Contacter() { Email = "ct2@cicc.com.cn", Customer = cust };
            cust.Contacters.Add(ct1);
            cust.Contacters.Add(ct2);
            Session.SaveOrUpdate(cust);
            Session.Flush();
            Debug.WriteLine("--------------------");
            var ns = NewSession;
            var ncust= ns.Get<Customer>(cust.Id);
            ncust.Contacters.RemoveAt(1);
            ns.SaveOrUpdate(ncust);
            ns.Flush();
        }
    }
}
