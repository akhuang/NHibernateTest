using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernateTest.Entitys
{
    public abstract class UploadFile : Entity
    {
        public virtual string FileName { get; set; }
    }

    public class UserAFile : UploadFile
    {
        public virtual UserA User { get; set; }
    }

    public class UserBFile : UploadFile
    {
        public virtual UserB User { get; set; }
    }

    public class UserA : Entity
    {
        public virtual string Name { get; set; }
        public virtual IList<UserAFile> Files { get; set; }
    }

    public class UserB : Entity
    {
        public virtual string Number { get; set; }
        public virtual IList<UserBFile> Files { get; set; }
    }
}
