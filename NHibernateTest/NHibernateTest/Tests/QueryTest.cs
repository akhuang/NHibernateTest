using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernateTest.Tests
{
    using System.Diagnostics;
    using Entitys;
    using NUnit.Framework;
    [TestFixture]
    class QueryTest:BaseTest
    {
        [TestFixtureSetUp]
        public void Init()
        {
            ReBuildDatabase();
            InitDoor();
        }

        private OfficeUser u1;
        private Teacher t1;
        private void InitDoor()
        {
             u1 = new OfficeUser() { Birthday = DateTime.Today, EmployeeNumber = "12046", Gender = Gender.Male, Name = "Devin" };
             t1 = new Teacher() { Birthday = Convert.ToDateTime("1984-1-1"), EmployeeNumber = "12334", Gender = Gender.Female, Major = "Math", Name = "StevenMath" };
            Session.SaveOrUpdate(u1);
            Session.SaveOrUpdate(t1);
            Student s1 = new Student() { Birthday = Convert.ToDateTime("1983-12-20"), Gender = Gender.Male, Name = "Jack" };
            Session.SaveOrUpdate(s1);
            Lab l1 = new Lab() { Address = "Teaching Tower 1", Description = "For chemistry", Size = 70, LabSubject = "Primary Chemistry" };
            Classroom cr1 = new Classroom() { Address = "Teaching Building 2", Description = "Common Room", RoomNumber = "402", Size = 150 };
            Session.SaveOrUpdate(l1);
            Session.SaveOrUpdate(cr1);
            Session.Flush();

            Education e1=new Education(){Employee = t1,GraduateYear = 2003,University = "UESTC"};
            Education e11 = new Education() { Employee = t1, GraduateYear = 2007, University = "BJU" };
            Education e2 = new Education() { Employee = u1, GraduateYear = 2001, University = "USTC" };
            t1.Educations.Add(e1);
            t1.Educations.Add(e11);
            u1.Educations.Add(e2);
            DoorKey doorKey = new DoorKey() { Name = "401 Key" };
            doorKey.Employees = new List<Employee>();
            doorKey.Employees.Add(u1);
            doorKey.Employees.Add(t1);
            Session.SaveOrUpdate(doorKey);
            Session.Flush();
        }


        [Test]
        public void TestSearchEmployee()
        {
            var doors = NewSession.QueryOver<Education>()
                .Fetch(a=>a.Employee).Eager
                .List<Education>();
            foreach (var employee in doors)
            {
               // foreach (var employee in doorKey.Employees)
                {
                    Debug.WriteLine( employee.Employee.GetType()+" ----"+employee.ToString());
                }
            }
           
        }
        [Test]
        public void TestSearchTeacher()
        {
            var em = NewSession.QueryOver<Employee>().Where(e => e.Id == t1.Id).SingleOrDefault();
            Debug.WriteLine(em.GetType()+"--"+em.ToString());
        }
    }
}
