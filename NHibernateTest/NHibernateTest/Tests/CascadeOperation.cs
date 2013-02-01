using System;
using System.Diagnostics;
using NHibernate;
using NHibernate.Criterion;
using NHibernateTest.Entitys;
using NUnit.Framework;

namespace NHibernateTest.Tests
{
    [TestFixture]
    public class CascadeOperation : BaseTest
    {
        [TestFixtureSetUp]
        public void Setup()
        {
            ReBuildDatabase();
        }


        [Test]
        public void CascadeInsert()
        {
            Class c = InitClasses();
            ISession session = Session;
            session.Save(c);
            session.Flush();
            Assert.Greater(c.Id, 0);
            Assert.Greater(c.Students[0].Id, 0);
        }
        [Test]
        public void CascadeDelete()
        {
            Class c = InitClasses();
            var s1 = c.Students[0];
            ISession session = Session;
            session.Save(c);
            session.Flush();
            Debug.WriteLine("----Save----");
            Assert.Greater(c.Id, 0);
            session.Delete(c);
            s1.Gender = Gender.Unknown;
            session.SaveOrUpdate(s1);
            session.Flush();
            Debug.WriteLine("----Delete----");
            var result = GetById<Class>(c.Id);
            Assert.IsNull(result);
            var sList = GetAll<Student>(NewSession);
            Assert.Greater(sList.Count,0);
            foreach (var student in sList)
            {
                Assert.IsNull(student.Class);
            }
        }
        [Test]
        public void CascadeUpdate()
        {
            var c = InitClasses();
            var s = c.Students[0];
            var news = s.Clone() as Student;
            Session.SaveOrUpdate(c);
            Session.Flush();
            news.Name = "Clone Student";
            news.Id = 0;
            //Session.SaveOrUpdate(news);
            Session.Flush();
            var ss = GetAll<Student>(NewSession);
            foreach (var student in ss)
            {
                Debug.WriteLine(student);
            }
        }


        private Class InitClassAndStudentAndCourse()
        {
            Class c = InitClasses();
            foreach (var course in InitCourses())
            {
                foreach (var student in c.Students)
                {
                    student.Courses.Add(course);
                    //course.Students.Add(student);
                }
            }
            return c;
        }
        [Test]
        public void CasCadeMany2Many()
        {
            Class c = InitClassAndStudentAndCourse();
            ISession session = Session;
            session.Save(c);
            session.Flush();
            var cId = c.Students[0].Courses[0].Id;
            
            var course1 = GetById<Course>(cId);
            //c.Students[0].Courses.Clear();
            //foreach (var student in course1.Students)
            //{
            //    student.Courses.Remove(course1);
            //}
            //course1.Students.Clear();
            session.Delete(course1);
            session.Flush();
        }
        

        [Test]
        public void CascadeInsertAndUpdate()
        {
            Class c = InitClasses();
            ISession session = Session;
            session.Save(c);
            session.Flush();
            Assert.Greater(c.Id, 0);
            Assert.Greater(c.Students[0].Id, 0);
            c.Students.Add(new Student {Birthday = DateTime.Now, Gender = Gender.Male, Name = "Test3"});
            c.ClassName = "测试班级1修改";
            session.Update(c);
            c.Students[1].Name = "update name";
            session.Flush();
        }
    }
}