using SmartCache;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTest
{

    public class Db : CacheDB
    {
        DictionaryCachedType<Guid, Company> companies = new DictionaryCachedType<Guid, Company>("Id");
        DictionaryCachedType<Guid, Employer> employer = new DictionaryCachedType<Guid, Employer>("Id");
        DictionaryCachedType<Guid, Project> projects = new DictionaryCachedType<Guid, Project>("Id");

        public DictionaryCachedType<Guid, Company> Companies { get => companies; }

        public DictionaryCachedType<Guid, Employer> Employer { get => employer; }

        public DictionaryCachedType<Guid, Project> Projects { get => projects; }
    }

    public class Company
    {
        Guid Id;
        public string Name;
        public Employer employer;
    }

    public class Employer
    {
        Guid Id;
        public string Name;
        public Project project;
    }

    public class Project
    {
        Guid Id;
        public string Name;
    }

}
