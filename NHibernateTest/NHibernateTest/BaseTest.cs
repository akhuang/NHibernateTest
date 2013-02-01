using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Tool.hbm2ddl;
using NHibernateTest.Entitys;

namespace NHibernateTest
{
    public class BaseTest
    {
        protected NHibernateHelper helper;
        private ISession session;

        public ISession Session
        {
            get
            {
                if (session == null)
                {
                    session = helper.GetSession();
                }
                return session;
            }
        }
        public ISession NewSession
        {
            get { return helper.GetSession(); }
        }

        private string hilo =
            "drop table if exists hibernate_unique_key;CREATE TABLE `hibernate_unique_key` (`next_hi` BIGINT(20) NOT NULL,PRIMARY KEY (`next_hi`));insert into hibernate_unique_key values(100000);";

        public BaseTest()
        {
            helper = new NHibernateHelper();
        }
        protected T GetById<T>(long id)where  T:Entity
        {
            return Session.CreateCriteria<T>().Add(Expression.Eq("Id", id)).UniqueResult<T>();
        }
        protected IList<T> GetAll<T>(ISession session) where T : Entity
        {
            return session.CreateCriteria<T>().List<T>();
        }
        protected void ReBuildDatabase()
        {
            var export = new SchemaExport(helper.Configuration);
            export.Execute(true, true, false);
            //var query = Session.CreateSQLQuery(hilo);
            //query.ExecuteUpdate();
        }

        /// <summary>
        /// 初始化一个班级和下面的2个学生
        /// </summary>
        /// <returns></returns>
        protected Class InitClasses()
        {
            var c1 = new Class { Students = new List<Student>() };
            c1.ClassName = "测试班级1";
            var s1 = new Student();
            s1.Name = "测试学生1";
            s1.Birthday = Convert.ToDateTime("1985-1-10");
            s1.Gender = Gender.Female;
            s1.Class = c1;
            c1.Students.Add(s1);

            var s2 = new Student();
            s2.Name = "测试学生2";
            s2.Birthday = Convert.ToDateTime("1986-1-10");
            s2.Gender = Gender.Female;
            s2.Class = c1;
            c1.Students.Add(s2);
            return c1;
        }
        /// <summary>
        /// 初始化数理化三个课程
        /// </summary>
        /// <returns></returns>
        protected IList<Course> InitCourses()
        {
            List<Course> cc = new List<Course>();
            cc.Add(new Course() { CourseName = "物理" });
            cc.Add(new Course() { CourseName = "化学" });
            cc.Add(new Course() { CourseName = "数学" });
            return cc;
        }
    }
}