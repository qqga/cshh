using cshh.Asp.Areas.Task.Models;
using cshh.Data.Services.DbContexts;
using cshh.Data.Tasks;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace cshh.Tests.Automapper
{
    [TestClass]
    public class AutoMappTest
    {
        public AutoMappTest()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<cshh.Data.Tasks.Task, cshh.Asp.Areas.Task.Models.TaskVM>().ReverseMap();
            });
        }
        [TestMethod]
        public void TestMethod1()
        {
            cshh.Data.Tasks.Task task = new Data.Tasks.Task()
            {
                CreatedDT = DateTime.Now,
                Name = "test,",
                Description = "testd",
                Id = 11,
                RemindPeriod = TimeSpan.FromMinutes(10),
                TargetDT = DateTime.Now,
                UserProfile_Id = 11,
                SupTaskId = 11,
                SubTasks = new List<Data.Tasks.Task>() { new Data.Tasks.Task() { Id = 2, Name = "sub1" } }
            };


            TaskVM map = AutoMapper.Mapper.Map<cshh.Asp.Areas.Task.Models.TaskVM>(task);


        }

        [TestMethod]
        public void MethodName()
        {
            TaskDbContext taskDbContext = new cshh.Data.Services.DbContexts.TaskDbContext();

            taskDbContext.Configuration.ProxyCreationEnabled = false;

            IQueryable<Task> tasks = taskDbContext.Tasks.Where(t => t.SupTaskId == null);//.Include(t => t.SubTasks);//.Include(t=>t.SupTask).

            List<Task> list = tasks.ToList();

            IEnumerable<TaskVM> map = AutoMapper.Mapper.Map<IEnumerable<TaskVM>>(tasks);

        }
    }
}
