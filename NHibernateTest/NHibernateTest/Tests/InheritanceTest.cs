using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernateTest.Tests
{
    using System.Diagnostics;
    using Entitys;
    using FluentNHibernate.Utils;
    using NHibernate.Criterion;
    using NUnit.Framework;

    [TestFixture]
    class InheritanceTest:BaseTest
    {
        [TestFixtureSetUp]
        public void SetUp()
        {
            ReBuildDatabase();
            OfficeUser u1=new OfficeUser(){Birthday = DateTime.Today,EmployeeNumber = "12046",Gender = Gender.Male,Name = "Devin"};
            Teacher t1=new Teacher(){Birthday = Convert.ToDateTime("1984-1-1"),EmployeeNumber = "12334",Gender = Gender.Female,Major = "Math",Name = "StevenMath"};
            Session.SaveOrUpdate(u1);
            Session.SaveOrUpdate(t1);
            Student s1=new Student(){Birthday = Convert.ToDateTime("1983-12-20"),Gender = Gender.Male,Name = "Jack"};
            Session.SaveOrUpdate(s1);
            Lab l1=new Lab(){Address = "Teaching Tower 1",Description = "For chemistry",Size = 70,LabSubject = "Primary Chemistry"};
            Classroom cr1=new Classroom(){Address = "Teaching Building 2",Description = "Common Room",RoomNumber = "402",Size = 150};
            Session.SaveOrUpdate(l1);
            Session.SaveOrUpdate(cr1);
            Session.Flush();



            DoorKey doorKey = new DoorKey() { Name = "401 Key" };
            doorKey.Employees = new List<Employee>();
            doorKey.Employees.Add(u1);
            doorKey.Employees.Add(t1);
            Session.SaveOrUpdate(doorKey);
            Session.Flush();
        }
        #region Single Table

        [Test]
        public void SingleTableTestSearchSubClass()
        {
            var ts = NewSession.QueryOver<Teacher>().List();
            foreach (var teacher in ts)
            {
                Debug.WriteLine(teacher);
            }
        }

        [Test]
        public void SingleTableTestSearchSubClassByAssociation()
        {
            Employee e = null;
            var ems = NewSession.QueryOver<DoorKey>()
                .JoinAlias(d=>d.Employees,()=>e)
                .Where(() => e.GetType() == typeof(Teacher))
               .Select(d=>e.Name)
                .List<string>();
            foreach (var employee in ems)
            {
                Debug.WriteLine(employee);
            }
        }


        [Test]
        public void SingleTableTestSearchSubClassByRootClassRepository()
        {
            var ems = NewSession.QueryOver<Employee>().Where(a =>a.GetType() == typeof (Teacher)).List();
            foreach (var employee in ems)
            {
                Debug.WriteLine(employee);
            }
        }

        [Test]
        public void SingleTableTestCritialSearchSubClassByRootClassRepository()
        {
            var cri = NewSession.CreateCriteria<Employee>();
          cri=  cri.Add(Expression.Eq("class","Teacher"));
            var ems = cri.List<Employee>();
            foreach (var employee in ems)
            {
                Debug.WriteLine(employee);
            }
        }
        [Test]
        public void SingleTableTestHqlSearchSubClassByRootClassRepository()
        {
            var cri = NewSession.CreateQuery("from Employee where Type='Teacher'");
            var ems = cri.List<Employee>();
            foreach (var employee in ems)
            {
                Debug.WriteLine(employee);
            }
        }
        #endregion

        #region Concrete Table
        [Test]
        public void ConcreteTableTestSearchSubClass()
        {
            var ts = NewSession.QueryOver<Student>().List();
            foreach (var s in ts)
            {
                Debug.WriteLine(s);
            }
        }


        [Test]
        public void ConcreteTableTestSearchSubClassByRootClassRepository()
        {
            var ems = NewSession.QueryOver<Person>().Where(a => a.GetType() == typeof(Student)).List();
            foreach (var employee in ems)
            {
                Debug.WriteLine(employee);
            }
        }

        [Test]
        public void ConcreteTableTestCritialSearchSubClassByRootClassRepository()
        {
            var cri = NewSession.CreateCriteria<Person>();
            cri = cri.Add(Expression.Eq("class", typeof(Student)));
            var ems = cri.List<Student>();
            foreach (var employee in ems)
            {
                Debug.WriteLine(employee);
            }
        }
        [Test]
        public void ConcreteTableTestHqlSearchSubClassByRootClassRepository()
        {
            var cri = NewSession.CreateQuery("from Person where type='Student'");
            var ems = cri.List<Student>();
            foreach (var employee in ems)
            {
                Debug.WriteLine(employee);
            }
        }
        #endregion

        #region Class Table
        [Test]
        public void ClassTableTestSearchSubClass()
        {
            var ts = NewSession.QueryOver<Classroom>().List();
            foreach (var s in ts)
            {
                Debug.WriteLine(s);
            }
        }


        [Test]
        public void ClassTableTestSearchSubClassByRootClassRepository()
        {
            var ems = NewSession.QueryOver<Place>().Where(a => a.GetType() == typeof(Classroom)).List();
            foreach (var employee in ems)
            {
                Debug.WriteLine(employee);
            }
        }

        [Test]
        public void ClassTableTestCritialSearchSubClassByRootClassRepository()
        {
            var cri = NewSession.CreateCriteria<Place>();
            cri = cri.Add(Expression.Eq("class", typeof(Classroom)));
            var ems = cri.List<Classroom>();
            foreach (var employee in ems)
            {
                Debug.WriteLine(employee);
            }
        }
        [Test]
        public void ClassTableTestHqlSearchSubClassByRootClassRepository()
        {
            var cri = NewSession.CreateQuery("from Classroom'");
            var ems = cri.List<Classroom>();
            foreach (var employee in ems)
            {
                Debug.WriteLine(employee);
            }
        }
        #endregion
    }
}
