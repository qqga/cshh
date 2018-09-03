using cshh.Asp;
using cshh.Data.Tasks;
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Serialization;


namespace cshh.Tests.Extensions
{
    [TestClass]
    public class TestJsonConvert
    {


        [TestMethod]
        public void TestArray()
{
            Task[] tasks = GetTasks();

            string json = tasks.ToJson<TaskLib.ITask>();

        }

        private static Data.Tasks.Task[] GetTasks()
        {
            return new Data.Tasks.Task[] {
                new Data.Tasks.Task() {TaskId = 1, Name = "t1", RemindPeriod = TimeSpan.FromMinutes(10) },
                new Data.Tasks.Task() {TaskId = 2, Name = "t2", RemindPeriod = TimeSpan.FromMinutes(11) },
                new Data.Tasks.Task() {TaskId = 3, Name = "t3", RemindPeriod = TimeSpan.FromMinutes(12) },
                new Data.Tasks.Task() {TaskId = 4,  Name = "t4", RemindPeriod = TimeSpan.FromMinutes(13) }
            };
        }
    }
}
