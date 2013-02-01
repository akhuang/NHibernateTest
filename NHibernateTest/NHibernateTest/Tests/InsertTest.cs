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
    class InsertTest:BaseTest
    {
        [TestFixtureSetUp]
        public void Init()
        {
            ReBuildDatabase();
        }
        [Test]
        public void TestInsert3ClassData()
        {
            Place p=new Place(){Address = "place",Description = "this is a place",Size = 100};
            Session.SaveOrUpdate(p);
            Classroom c = new Classroom() { Address = "place", Description = "this is a place", Size = 100,RoomNumber = "301"};
            Session.SaveOrUpdate(c);
            Lab l = new Lab() { Address = "place", Description = "this is a place", Size = 100,LabSubject = "Physical"};
            Session.SaveOrUpdate(l);
            Session.Flush();

            var list = NewSession.QueryOver<Place>().List();
            Assert.Greater(list.Count,0);
            foreach (var place in list)
            {
                Debug.WriteLine(place.GetType().ToString());
            }
        }
    }
}
