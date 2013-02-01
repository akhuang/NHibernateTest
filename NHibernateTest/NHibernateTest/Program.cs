using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Tool.hbm2ddl;
using NHibernateTest.Entitys;

namespace NHibernateTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var helper = new NHibernateHelper();
            var export = new SchemaExport(helper.Configuration);
            export.Create(true, false);
            return;
            ISession session = helper.GetSession();

            //Class c = null;
            //var tran = session.BeginTransaction();
            //try
            //{

            //    TestInsertData2(session,ref c);
            //    session.Flush();
            //    tran.Commit();
            //}
            //catch (Exception ex)
            //{
            //    tran.Rollback();
            //    session.Clear();
            //    Console.WriteLine(ex.Message);
            //}


            ITransaction tran2 = session.BeginTransaction();
            try
            {
                TestInsertData(session);
                tran2.Commit();
            }
            catch (Exception ex)
            {
                tran2.Rollback();
                Console.WriteLine(ex.Message);
            }
            session.Close();
        }

        private void ShowCreateDbSql()
        {
            var helper = new NHibernateHelper();
            var export = new SchemaExport(helper.Configuration);
            export.Create(true,false);
        }

        /// <summary>
        /// 插入数据，会成功
        /// </summary>
        /// <param name="session"></param>
        public static void TestInsertData(ISession session)
        {
            //session.Save(c1);
            //session.Flush();
            //var testClass1 =(Class)( session.CreateCriteria(typeof (Class)).Add(Expression.Eq("Name", "测试班级1")).UniqueResult());
            //Console.WriteLine(testClass1.ToString());
            //foreach (var student in testClass1.Students)
            //{
            //    Console.WriteLine(student);
            //}
        }

        ///// <summary>
        ///// 插入数据，会失败
        ///// </summary>
        ///// <param name="session"></param>
        //public static void TestInsertData2(ISession session, ref Class c)
        //{
        //    var c1 = new Class {Students = new List<Student>()};
        //    c1.ClassName = "测试班级1";
        //    var s1 = new Student();
        //    s1.Name = "测试学生21";
        //    s1.Birthday = Convert.ToDateTime("1985-1-10");
        //    s1.Gender = Gender.Female;
        //    s1.Class = c1;
        //    c1.Students.Add(s1);

        //    var s2 = new Student();
        //    s2.Name = "测试学生22";
        //    s2.Birthday = Convert.ToDateTime("2012-1-10");
        //    s2.Gender = Gender.Female;
        //    s2.Class = c1;
        //    c = c1;
        //    c1.Students.Add(s2);
        //    session.Save(c1);
        //    session.Flush();
        //    var testClass1 =
        //        (Class) (session.CreateCriteria(typeof (Class)).Add(Restrictions.Eq("Name", "测试班级1")).UniqueResult());
        //    Console.WriteLine(testClass1.ToString());
        //    foreach (Student student in testClass1.Students)
        //    {
        //        Console.WriteLine(student);
        //    }
        //}
    }
}