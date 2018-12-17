using SmartCache;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTest
{

    public class Db : CacheDB
    {        
        public CachedModel<Company, Guid> Companies { get; private set; }

        public CachedModel<Employer, Guid> Employer { get; private set; }

        public CachedModel<Project, Guid> Projects { get; private set; }

        public Db()
        {
            Companies =
                CachedModel.Create<Company>()
                .SetPrimaryKey(x => x.Id)
                .Build<Guid>();

            Employer =
                CachedModel.Create<Employer>()
                .SetPrimaryKey(x => x.Id)
                .Build<Guid>();

            Projects =
                CachedModel.Create<Project>()
                .SetPrimaryKey(x => x.Id)
                .Build<Guid>();

            Initialize();
        }

    }

    public class Company
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Employer[] employer { get; set; }
    }

    public class Employer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Project[] project { get; set; }
    }

    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

}
