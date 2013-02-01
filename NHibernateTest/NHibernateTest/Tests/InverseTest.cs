using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using NHibernateTest.Entitys;
using NUnit.Framework;

namespace NHibernateTest.Tests
{
    [TestFixture]
    public class InverseTest:BaseTest
    {
        [TestFixtureSetUp]
        public void Init()
        {
            ReBuildDatabase();
        }

        [Test]
        public void TestInverseSql()
        {
            var class1 = InitClasses();
            var s1 = class1.Students[0];
            var s2 = class1.Students[1];
            var class2 = new Class() {ClassName = "Class2"};
            
            s1.Class = class2;
            s1.Name = "班级2的学生";
            s1.Class = class2;
            s2.Class = class2;
            Debug.WriteLine("--Begin Save--");
            Session.SaveOrUpdate(class1);
            Session.SaveOrUpdate(class2);
            Debug.WriteLine("--End Save--");
            Session.Flush();
            Debug.WriteLine("--End Flush--");
            var c1 = NewSession.Get<Class>(class1.Id);
            Debug.WriteLine("Class 1 Student count:"+ c1.Students.Count);
            var c2 = NewSession.Get<Class>(class2.Id);
            Debug.WriteLine("Class 2 Student count:" + c2.Students.Count);
        }

        [Test]
        public void TestInverseEasy()
        {
            Department d1=new Department(){Name = "D1"};
            Department d2=new Department(){Name = "D2"};
            User u1=new User(){Name = "U1",Department = d1};
            User u2=new User(){Name = "U2",Department = d2};
            d1.Users=new List<User>(){u1,u2};
            Debug.WriteLine("---Begin Save---");
         
            Session.SaveOrUpdate(d2);
            Session.SaveOrUpdate(d1);
            Debug.WriteLine("---Begin Flush---");
            Session.Flush();
        }

        public class AwardMapping : IAutoMappingOverride<Award>
        {
            public void Override(AutoMapping<Award> mapping)
            {
                mapping.HasManyToMany(a => a.Emps).Inverse();
            }
        }
         public class EmpMapping : IAutoMappingOverride<Emp>
        {
            public void Override(AutoMapping<Emp> mapping)
            {
                mapping.HasManyToMany(a => a.Awards).Not.Inverse();
            }
        }

        [Test]
        public void TestM2MInverse()
        {
            Emp e1=new Emp(){Name = "E1"};
            Emp e2 = new Emp() { Name = "E2" };
            Award a1=new Award(){Name = "A1"};
            Award a2 = new Award() { Name = "A2" };
            //e1.Awards=new List<Award>(){a1,a2};
            a1.Emps = new List<Emp>() { e1 };
            a2.Emps = new List<Emp>() { e1 };
            Debug.WriteLine("---Begin Save---");
         
       
            Session.SaveOrUpdate(e1);
            Session.SaveOrUpdate(e2);
            Session.SaveOrUpdate(a1);
            Session.SaveOrUpdate(a2);
            Debug.WriteLine("---Begin Flush---");
            Session.Flush();
            Debug.WriteLine("---End Flush---");
            var award1 = NewSession.Get<Award>(a1.Id);
            foreach (var emp in award1.Emps)
            {
                Debug.WriteLine(emp);
            }

        }



        public class DeptMapping : IAutoMappingOverride<Dept>
        {
            public void Override(AutoMapping<Dept> mapping)
            {
                mapping.HasOne(d => d.Mgr).PropertyRef(m => m.Dept).Constrained().Cascade.All();
            }
        }
        public class MgrMapping : IAutoMappingOverride<Mgr>
        {
            public void Override(AutoMapping<Mgr> mapping)
            {
              
            }
        }
        [Test]
        public void TestO2OInverse()
        {
            Dept d1=new Dept(){Name = "D1"};
            Dept d2 = new Dept() { Name = "D2" };
            Mgr m1=new Mgr(){Name = "M1"};
            Mgr m2 = new Mgr() { Name = "M2" };
            d1.Mgr = m1;
            m1.Dept = d2;
            d2.Mgr = m2;
            m2.Dept = d1;
            Debug.WriteLine("---Begin Save---");
            Session.SaveOrUpdate(d1);
            Session.SaveOrUpdate(d2);
            Session.SaveOrUpdate(m1);
            Session.SaveOrUpdate(m2);
            Debug.WriteLine("---Begin Flush---");
            Session.Flush();

        }
    }
}
