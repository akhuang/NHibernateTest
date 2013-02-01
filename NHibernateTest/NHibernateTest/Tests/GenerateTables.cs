using System.IO;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace NHibernateTest.Tests
{
    [TestFixture]
    internal class GenerateTables : BaseTest
    {
        [TestFixtureSetUp]
        public void Setup()
        {
        }

        [Test]
        [Ignore]
        public void TestGenerateScriptFile()
        {
            string filePath = "D:\\NHibernateTest.sql";

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            var export = new SchemaExport(helper.Configuration);
            export.SetOutputFile(filePath).Create(true, false);
            Assert.True(File.Exists(filePath));
        }

        [Test]
        public void TestGenerateTables()
        {
            var export = new SchemaExport(helper.Configuration);
            export.Execute(true, true, false);
        }
        [Test]
        public void TestGenerateMappingFiles()
        {
            var export = new SchemaExport(helper.Configuration);
            export.SetOutputFile("D:\\Temp");
        }
    }
}