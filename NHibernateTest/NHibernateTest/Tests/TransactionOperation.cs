using System;
using NHibernate;
using NHibernateTest.Entitys;
using NUnit.Framework;

namespace NHibernateTest.Tests
{
    [TestFixture]
    public class TransactionOperation : BaseTest
    {
        [Test]
        public void TestUpdateSubclass()
        {
            using (ISession s = Session)
            {
                Class c = InitClasses();
                s.Save(c);
                s.Flush();
            }

            ISession session = Session;
            using (ITransaction tran = session.BeginTransaction())
            {
                try
                {
                    Class c1 = session.CreateCriteria<Class>().List<Class>()[0];
                    session.Save(c1);
                    c1.ClassName = "Test1";
                    var student = new Student {Birthday = DateTime.Now, Class = c1, Gender = Gender.Male};
                    c1.Students.Add(student);

                    var c = new Course { CourseName = "Course1" };
                    session.Save(c);
                    session.Flush();
                    student.Name = "zzzz";

                    session.SaveOrUpdate(c1);
                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                }
            }
        }

        [Test]
        public void TransactionInsert()
        {
            Class c = InitClasses();
            using (ISession s = Session)
            {
                s.Save(c);
            }
            c = InitClasses();
            c.ClassName = "Test2";
            ISession session = Session;
            using (ITransaction tran = session.BeginTransaction())
            {
                try
                {
                    Class c1 = session.CreateCriteria<Class>().List<Class>()[0];
                    session.Save(c);
                    c1.ClassName = "Test1";
                    session.SaveOrUpdate(c1);
                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                }
            }
        }
    }
}