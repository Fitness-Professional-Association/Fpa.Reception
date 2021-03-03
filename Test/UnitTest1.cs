using Domain;
using lc.fitnesspro.library;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using Service.lC;
using Service.lC.Manager;
using Service.lC.Provider;
using Service.MongoDB;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Test
{

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod2()
        {
            var lcManager = new Manager("kloder", "Kaligula2");

            var http = new HttpClient();
            http.BaseAddress = new Uri("https://api.fitness-pro.ru/");
            var baseHttp = new BaseHttpClient(http);

            var providerDepository = new ProviderDepository(baseHttp);
            var eduManager = new EducationManager(providerDepository, lcManager);

            var res = eduManager.GetTeacherPrograms(new Guid("f9d94670-5fb3-11eb-8138-0cc47a4b75cc")).Result;

            var res1 = eduManager.GetDisciplinePrograms(new Guid("51170581-907b-11e6-80e4-0cc47a4b75cc")).Result;
        }




        [TestMethod]
        public void TestMethod1()
        {
            var settings = new MongoDbSettings()
            {
                ConnectionString = "http://localhost:7010",
                DatabaseName = "Reception"
            };

            var mongo = new MongoRepository<ReceptionDto>(settings);


            var item = new ReceptionDto
            {
                Date = DateTime.Now,
                IsActive = true,
                Key = Guid.NewGuid(),
                Histories = new List<History> { new History { Object = Guid.NewGuid(), Action = " ������ ", Subject = Guid.NewGuid(), DateTime = DateTime.UtcNow } },
                Events = new List<Event> {
                         new Event {
                          Teachers = new List<BaseInfo>{ new BaseInfo { Key = Guid.NewGuid(), Title = "���������" }, new BaseInfo { Key = Guid.NewGuid(), Title = "����������" } },
                           Discipline =  new BaseInfo { Key = Guid.NewGuid(), Title = "Anatomy" },
                             Restrictions = new List<PayloadRestriction>{ new PayloadRestriction { Program = Guid.NewGuid(), Group = Guid.NewGuid(), SubGroup = Guid.NewGuid(),
                                 Option = new PayloadOption{
                                     CheckAttemps = true,
                                      CheckContractExpired = true,
                                       CheckDependings = false
                                 }
                             } },
                              Requirement = new PayloadRequirement{
                               AllowedAttemptCount = 10,
                                SubscribeBefore = DateTime.UtcNow,
                                 UnsubscribeBefore = DateTime.Now,
                                  DependsOnOtherDisciplines = new List<Guid>{
                                   Guid.NewGuid()
                                  }
                              }
                         }
                     },
                PositionManager = new PositionManager()
                {
                    Positions = new List<Position> {
                   new Position{
                     Key = Guid.NewGuid(),
                      IsActive = true,
                       Time = DateTime.Now,
                        Record = new Record{
                             DisciplineKey = Guid.NewGuid(),
                              ProgramKey = Guid.NewGuid(),
                               StudentKey = Guid.NewGuid(),
                                 Result = new Result{
                                     TeacherKey = Guid.NewGuid(),
                                      Comment = "Rate comment",
                                       Score = new Hundred(45)
                                 }
                        }
                   }
                  }
                }
            };


            mongo.InsertOneAsync(item).ConfigureAwait(false).GetAwaiter().GetResult();

        }

        public class ReceptionDto : Reception, IDocument
        {
            ObjectId IDocument.Id { get; set; }
            DateTime IDocument.CreatedAt { get; }
        }
    }
}