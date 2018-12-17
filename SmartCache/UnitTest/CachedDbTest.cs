using System;
using System.Collections.Generic;
using Xunit;

namespace UnitTest
{
    public class CachedDbTest
    {
        static Db db;
        const int companiesCount = 100000;

        static List<Guid> companies = new List<Guid>();
        static List<Guid> employes = new List<Guid>();
        static List<Guid> projects = new List<Guid>();

        [Fact, TestPriority(1)]
        void Insertion()
        {
            db = new Db();
            for(int i = 0; i < companiesCount; i++)
            {
                var company = new Company
                {
                     Id = Guid.NewGuid(),
                      Name="fuckyou",
                    employer = new Employer[]
                     {
                          new Employer()
                          {
                               Id = Guid.NewGuid(),
                               Name = "lolmdf",
                               project = new Project[]
                               {
                                   new Project()
                                   {
                                       Id = Guid.NewGuid(),
                                       Name = "sdafdafda"
                                   },
                                   new Project()
                                   {
                                       Id = Guid.NewGuid(),
                                       Name = "sdafdafda"
                                   },
                                   new Project()
                                   {
                                       Id = Guid.NewGuid(),
                                       Name = "sdafdafda"
                                   },
                                   new Project()
                                   {
                                       Id = Guid.NewGuid(),
                                       Name = "sdafdafda"
                                   },
                                   new Project()
                                   {
                                       Id = Guid.NewGuid(),
                                       Name = "sdafdafda"
                                   },
                                   new Project()
                                   {
                                       Id = Guid.NewGuid(),
                                       Name = "sdafdafda"
                                   }
                               }
                          },
                          new Employer()
                          {
                               Id = Guid.NewGuid(),
                               Name = "lolmdf",
                               project = new Project[]
                               {
                                   new Project()
                                   {
                                       Id = Guid.NewGuid(),
                                       Name = "sdafdafda"
                                   },
                                   new Project()
                                   {
                                       Id = Guid.NewGuid(),
                                       Name = "sdafdafda"
                                   },
                                   new Project()
                                   {
                                       Id = Guid.NewGuid(),
                                       Name = "sdafdafda"
                                   },
                                   new Project()
                                   {
                                       Id = Guid.NewGuid(),
                                       Name = "sdafdafda"
                                   },
                                   new Project()
                                   {
                                       Id = Guid.NewGuid(),
                                       Name = "sdafdafda"
                                   },
                                   new Project()
                                   {
                                       Id = Guid.NewGuid(),
                                       Name = "sdafdafda"
                                   }
                               }
                          },
                          new Employer()
                          {
                               Id = Guid.NewGuid(),
                               Name = "lolmdf",
                               project = new Project[]
                               {
                                   new Project()
                                   {
                                       Id = Guid.NewGuid(),
                                       Name = "sdafdafda"
                                   },
                                   new Project()
                                   {
                                       Id = Guid.NewGuid(),
                                       Name = "sdafdafda"
                                   },
                                   new Project()
                                   {
                                       Id = Guid.NewGuid(),
                                       Name = "sdafdafda"
                                   },
                                   new Project()
                                   {
                                       Id = Guid.NewGuid(),
                                       Name = "sdafdafda"
                                   },
                                   new Project()
                                   {
                                       Id = Guid.NewGuid(),
                                       Name = "sdafdafda"
                                   },
                                   new Project()
                                   {
                                       Id = Guid.NewGuid(),
                                       Name = "sdafdafda"
                                   }
                               }
                          },
                          new Employer()
                          {
                               Id = Guid.NewGuid(),
                               Name = "lolmdf",
                               project = new Project[]
                               {
                                   new Project()
                                   {
                                       Id = Guid.NewGuid(),
                                       Name = "sdafdafda"
                                   },
                                   new Project()
                                   {
                                       Id = Guid.NewGuid(),
                                       Name = "sdafdafda"
                                   },
                                   new Project()
                                   {
                                       Id = Guid.NewGuid(),
                                       Name = "sdafdafda"
                                   },
                                   new Project()
                                   {
                                       Id = Guid.NewGuid(),
                                       Name = "sdafdafda"
                                   },
                                   new Project()
                                   {
                                       Id = Guid.NewGuid(),
                                       Name = "sdafdafda"
                                   },
                                   new Project()
                                   {
                                       Id = Guid.NewGuid(),
                                       Name = "sdafdafda"
                                   }
                               }
                          },
                          new Employer()
                          {
                               Id = Guid.NewGuid(),
                               Name = "lolmdf",
                               project = new Project[]
                               {
                                   new Project()
                                   {
                                       Id = Guid.NewGuid(),
                                       Name = "sdafdafda"
                                   },
                                   new Project()
                                   {
                                       Id = Guid.NewGuid(),
                                       Name = "sdafdafda"
                                   },
                                   new Project()
                                   {
                                       Id = Guid.NewGuid(),
                                       Name = "sdafdafda"
                                   },
                                   new Project()
                                   {
                                       Id = Guid.NewGuid(),
                                       Name = "sdafdafda"
                                   },
                                   new Project()
                                   {
                                       Id = Guid.NewGuid(),
                                       Name = "sdafdafda"
                                   },
                                   new Project()
                                   {
                                       Id = Guid.NewGuid(),
                                       Name = "sdafdafda"
                                   }
                               }
                          },
                          new Employer()
                          {
                               Id = Guid.NewGuid(),
                               Name = "lolmdf",
                               project = new Project[]
                               {
                                   new Project()
                                   {
                                       Id = Guid.NewGuid(),
                                       Name = "sdafdafda"
                                   },
                                   new Project()
                                   {
                                       Id = Guid.NewGuid(),
                                       Name = "sdafdafda"
                                   },
                                   new Project()
                                   {
                                       Id = Guid.NewGuid(),
                                       Name = "sdafdafda"
                                   },
                                   new Project()
                                   {
                                       Id = Guid.NewGuid(),
                                       Name = "sdafdafda"
                                   },
                                   new Project()
                                   {
                                       Id = Guid.NewGuid(),
                                       Name = "sdafdafda"
                                   },
                                   new Project()
                                   {
                                       Id = Guid.NewGuid(),
                                       Name = "sdafdafda"
                                   }
                               }
                          }

                     }
                };

                companies.Add(company.Id);
                foreach (var e in company.employer)
                {
                    employes.Add(e.Id);
                    foreach (var p  in e.project)
                        projects.Add(p.Id);

                }
                db.Companies.Add(company);
            }
        }

        [Fact, TestPriority(2)]
        void Search()
        {
            foreach(var c in companies)
            {
               var a = db.Companies[c];
            }

            foreach (var c in employes)
            {
                var a = db.Employer[c];
            }

            foreach (var c in projects)
            {
                var a = db.Projects[c];
            }
        }

    }
}
