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
    class ItemsMergeTest:BaseTest
    {
        [TestFixtureSetUp]
        public void InitData()
        {
            ReBuildDatabase();
            var class1 = InitClasses();
            Session.Save(class1);
            Session.Flush();
            class1Id = class1.Id;
            stuId1 = class1.Students[0].Id;
        }

        private long class1Id, stuId1;

        [Test]
        [ExpectedException]
        public void TestReplaceCollections()
        {
            var nclass = NewClass();
            var class1 = Session.Get<Class>(nclass.Id);
            class1.ClassName = nclass.ClassName;
            class1.Students = nclass.Students;
            Session.SaveOrUpdate(class1);
            Session.Flush();
        }

        [Test(Description = "这种做法是错误的，但是运行会正确")]
        public void TestRemoveAndAddCollections()
        {
            var nclass = NewClass();
            var class1 = Session.Get<Class>(nclass.Id);
            class1.ClassName = nclass.ClassName;
            class1.Students.Clear();
            Session.SaveOrUpdate(class1);
            Debug .WriteLine("-----------------------");
            foreach (var student in nclass.Students)
            {
                student.Id = 0;
            }
            class1.Students = nclass.Students;
            Session.Flush();
        }

        [Test]
        public void TestMergeCollections()
        {
            var nclass = NewClass();
            var class1 = Session.Get<Class>(nclass.Id);
            class1.ClassName = nclass.ClassName;

            Debug.WriteLine("------先去掉源对象还有，但新对象没有的-----------------");
            var needRemove = new List<Student>();
            var needUpdate = new List<Student>();
            foreach (var student in class1.Students)
            {
                var newStudent = nclass.Students.SingleOrDefault(s => s.Id == student.Id);
                if ( newStudent== null)
                {
                    needRemove.Add(student);
                }
                else//更新源对象还保留的
                {
                    needUpdate.Add(newStudent);
                    student.Name = newStudent.Name;
                    student.Birthday = newStudent.Birthday;
                    student.Gender = newStudent.Gender;
                }
            }
            foreach (var student in needRemove)
            {
                class1.Students.Remove(student);
            }
            foreach (var student in nclass.Students)
            {
                if (!needUpdate.Contains(student))
                {
                    class1.Students.Add(student);
                }
            }
            Debug.WriteLine("------Merge完成-----------------");
            Session.SaveOrUpdate(class1);
           
            
            Session.Flush();
        }
        [Test]
        public void TestMergeCollectionsByCommonFunction()
        {
            var nclass = NewClass();
            var class1 = Session.Get<Class>(nclass.Id);
            class1.ClassName = nclass.ClassName;

            Debug.WriteLine("------先去掉源对象还有，但新对象没有的-----------------");
            //AbstractMerge<Class, Student>(class1, nclass, c => c.Students, (student, newStudent) =>
            //    {
            //        student.Name = newStudent.Name;
            //        student.Birthday = newStudent.Birthday;
            //        student.Gender = newStudent.Gender;
            //    });

            AbstractMergeItems<Class, Student, Student>(class1.Students, nclass.Students, c => c, (student, newStudent) =>
            {
                student.Name = newStudent.Name;
                student.Birthday = newStudent.Birthday;
                student.Gender = newStudent.Gender;
            });

            Debug.WriteLine("------Merge完成-----------------");
            Session.SaveOrUpdate(class1);


            Session.Flush();
        }
        private void AbstractMerge<T,S>(T class1,T nclass,Func<T,IList<S>> func,Action<S,S> updateAction  ) where T:Entity where S:Entity
        {
            var needRemove = new List<S>();
            var needUpdate = new List<S>();
            var class1Students = func(class1);
            var nclassStudents = func(nclass);
            foreach (var student in class1Students)
            {
                var newStudent = nclassStudents.SingleOrDefault(s => s.Id == student.Id);
                if (newStudent == null)
                {
                    needRemove.Add(student);
                }
                else//更新源对象还保留的
                {
                    needUpdate.Add(newStudent);
                    updateAction(student, newStudent);
                 
                }
            }
            foreach (var student in needRemove)
            {
                class1Students.Remove(student);
            }
            foreach (var student in nclassStudents)
            {
                if (!needUpdate.Contains(student))
                {
                    class1Students.Add(student);
                }
            }
        }


        private void AbstractMergeItems<T, S,VS>(IList<S> class1Students, IList<VS> nclassStudents, Func<VS, S> converter, Action<S, VS> updateAction)
            where T : Entity
            where S : Entity
            where VS:Entity
        {
            var needRemove = new List<S>();
            var needUpdate = new List<VS>();
            foreach (var student in class1Students)
            {
                var newStudent = nclassStudents.SingleOrDefault(s => s.Id == student.Id);
                if (newStudent == null)
                {
                    needRemove.Add(student);
                }
                else//更新源对象还保留的
                {
                    needUpdate.Add(newStudent);
                    updateAction(student, newStudent);
                }
            }
            foreach (var student in needRemove)
            {
                class1Students.Remove(student);
            }
            foreach (var student in nclassStudents)
            {
                if (!needUpdate.Contains(student))
                {
                    class1Students.Add(converter(student));
                }
            }
        }
        /// <summary>
        /// 班级的Id相同，只是班级名字进行更改
        /// 学生1,2变成了学生1,3
        /// 1是进行了Update，2需要删除，3需要添加
        /// </summary>
        /// <returns></returns>
        private Class NewClass()
        {
            var c1 = new Class { Students = new List<Student>(),Id = class1Id};
            c1.ClassName = "New测试班级1";
            var s1 = new Student(){Id = stuId1};
            s1.Name = "测试学生1";
            s1.Birthday = Convert.ToDateTime("1985-11-11");
            s1.Gender = Gender.Female;
            s1.Class = c1;
            c1.Students.Add(s1);

            var s2 = new Student();
            s2.Name = "测试学生3";
            s2.Birthday = Convert.ToDateTime("1988-2-10");
            s2.Gender = Gender.Male;
            s2.Class = c1;
            c1.Students.Add(s2);
            return c1;
        }
    }
}
