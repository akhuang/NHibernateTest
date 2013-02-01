using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Cfg;
using Iesi.Collections.Generic;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Event;
using NHibernate.Mapping;
using NHibernateTest.AutoMapping;
using NHibernateTest.AutoMapping.Conventions;
using NHibernateTest.Entitys;
using Environment = System.Environment;

namespace NHibernateTest
{
    public class NHibernateHelper
    {
        private readonly ISessionFactory _sessionFactory;

        public NHibernateHelper()
        {
            _sessionFactory = GetSessionFactory();
        }

        public Configuration Configuration { get; set; }

        private ISessionFactory GetSessionFactory()
        {
            Configuration = new Configuration().Configure("NHibernate.config");
            string path = Environment.CurrentDirectory + "\\TestDB.db3";
            Configuration.Properties["connection.connection_string"] = "Data Source=" + path + ";Version=3;";

            FluentConfiguration fluentConfiguration = Fluently.Configure(Configuration);
            Configuration.SetListener(ListenerType.Delete, new MyDeleteEventListener());
            Configuration.SetListener(ListenerType.Load, new MyLoadEventListener());
            InitMapping(fluentConfiguration);
            
            var sf= fluentConfiguration.BuildSessionFactory();
            //fluentConfiguration.Mappings(x => x.AutoMappings.ExportTo(@"D:\Temp"));
            CreateHighLowTable(Configuration, new HiloTable() { TableNameColumn = PrimaryKeyConvention.TableColumnName, Name = PrimaryKeyConvention.NHibernateHiLoIdentityTableName, NextHiColumn = PrimaryKeyConvention.NextHiValueColumnName });
            
            return sf;
        }

        private void InitMapping(FluentConfiguration fluentConfiguration)
        {
            fluentConfiguration.Mappings(
                x =>
                    {
                        x.AutoMappings.Add(PersistenceModelGenerator.Generate(new string[] {"NHibernateTest.exe"},
                                                                              new string[] { "NHibernateTest.exe" }));
                        x.FluentMappings.Add<IsDeletedFilter>();

                        x.AutoMappings.ExportTo(@"D:\Temp");
                    });

            //fluentConfiguration.Mappings(m => m.FluentMappings.Add<ClassesMap>());
            //fluentConfiguration.Mappings(m => m.FluentMappings.Add<StudentMap>());
            //fluentConfiguration.Mappings(m => m.FluentMappings.Add<CourseMap>());
        }

        public ISession GetSession()
        {
            return _sessionFactory.OpenSession();
        }


        public static void CreateHighLowTable(NHibernate.Cfg.Configuration config, HiloTable hiloTable)
        {
            StringBuilder stringBuilder1 = new StringBuilder();
            stringBuilder1.AppendFormat("DELETE FROM {0};", (object)hiloTable.Name);
            stringBuilder1.AppendLine();
            stringBuilder1.AppendFormat("ALTER TABLE {0} ADD {1} VARCHAR(128);", (object)hiloTable.Name, (object)hiloTable.TableNameColumn);
            stringBuilder1.AppendLine();
            StringBuilder stringBuilder2 = new StringBuilder();
            foreach (string str in Enumerable.Distinct<string>(Enumerable.Select<PersistentClass, string>(Enumerable.Where<PersistentClass>((IEnumerable<PersistentClass>)config.ClassMappings, (Func<PersistentClass, bool>)(m => m.IdentifierProperty.Type.ReturnedClass == typeof(long))), (Func<PersistentClass, string>)(m => m.Table.Name))))
            {
                stringBuilder2.AppendFormat(string.Format("INSERT INTO [{0}] ({1}, {2}) VALUES ('{3}', 1);", (object)hiloTable.Name, (object)hiloTable.TableNameColumn, (object)hiloTable.NextHiColumn, (object)str), new object[0]);
                stringBuilder2.AppendLine();
            }
            NHibernate.Cfg.Configuration configuration1 = config;
            string sqlCreateString1 = ((object)stringBuilder1).ToString();
            // ISSUE: variable of the null type
 
            HashedSet<string> hashedSet1 = new HashedSet<string>();
            hashedSet1.Add(typeof(MsSql2005Dialect).FullName);
            hashedSet1.Add(typeof(MsSql2008Dialect).FullName);
            hashedSet1.Add(typeof(MySQL5Dialect).FullName);
            hashedSet1.Add(typeof(SQLiteDialect).FullName);
            HashedSet<string> dialectScopes1 = hashedSet1;
            SimpleAuxiliaryDatabaseObject auxiliaryDatabaseObject1 = new SimpleAuxiliaryDatabaseObject(sqlCreateString1, (string)"", dialectScopes1);
            configuration1.AddAuxiliaryDatabaseObject((IAuxiliaryDatabaseObject)auxiliaryDatabaseObject1);
            NHibernate.Cfg.Configuration configuration2 = config;
            string sqlCreateString2 = ((object)stringBuilder2).ToString();
            // ISSUE: variable of the null type

            HashedSet<string> hashedSet2 = new HashedSet<string>();
            hashedSet2.Add(typeof(MsSql2005Dialect).FullName);
            hashedSet2.Add(typeof(MsSql2008Dialect).FullName);
            hashedSet2.Add(typeof(MySQL5Dialect).FullName);
            hashedSet2.Add(typeof(SQLiteDialect).FullName);
            HashedSet<string> dialectScopes2 = hashedSet2;
            SimpleAuxiliaryDatabaseObject auxiliaryDatabaseObject2 = new SimpleAuxiliaryDatabaseObject(sqlCreateString2, (string)"", dialectScopes2);
            configuration2.AddAuxiliaryDatabaseObject((IAuxiliaryDatabaseObject)auxiliaryDatabaseObject2);
        }
        public class HiloTable
        {
            public string Name { get; set; }

            public string TableNameColumn { get; set; }

            public string NextHiColumn { get; set; }
            public HiloTable()
            {
            }

            public HiloTable(string name, string tableNameColumn, string nextHiColumn)
            {
                this.Name = name;
                this.TableNameColumn = tableNameColumn;
                this.NextHiColumn = nextHiColumn;
            }
        }
    }
}