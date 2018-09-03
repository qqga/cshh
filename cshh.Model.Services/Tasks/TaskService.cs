using cshh.Data.Services.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cshh.Data.Tasks;
using cshh.Data.User;

namespace cshh.Model.Services.Tasks
{
    public interface ITaskService
    {
        Task Add(Task task);
        void Delete(int id);
        IQueryable<Task> GetTasks();
        IQueryable<Task> GetUserTasks(string userAppId = null);
        Task Edit(Task task);
        ITaskRepository Repository { get; }
    }
    public class TaskService : ITaskService
    {
        public ITaskRepository Repository { get; }
        IWorkContext _WorkContext;
        public TaskService(ITaskRepository taskRepository, IWorkContext workContext)
        {
            Repository = taskRepository;
            _WorkContext = workContext;
        }
        public IQueryable<Task> GetTasks()
        {            
            return Repository.Tasks;
        }

        public IQueryable<Task> GetUserTasks(string userAppId = null)
        {
            userAppId = userAppId ?? _WorkContext.UserAppId;
            return Repository.Tasks.Where(t => t.UserProfile.ApplicationUserId == userAppId);
        }

        public Task Add(Task task)
        {
            UserProfile userProfile = Repository.GetUserProfile(_WorkContext.UserAppId);
            if(userProfile == null)
                throw new Exception("User not found.");

            var newTask = new Task()
            {
                CreatedDT = DateTime.Now,
                Description = task.Description,
                IsComplited = task.IsComplited,
                Name = task.Name,
                RemindPeriod = task.RemindPeriod,
                //SupTask = task.SupTask,
                SupTaskId = task.SupTaskId,
                TargetDT = task.TargetDT,
                UserProfile_Id = userProfile.Id
            };


            Repository.Add(newTask, true);
            return newTask;
        }

        public Task Edit(Task task)
        {
            UserProfile userProfile = Repository.GetUserProfile(_WorkContext.UserAppId);
            if(IsUserTaskOwning(task))
                throw new Exception($"User is not owner of the Task({task.Id})");

            var oldTask = Repository.GetByKey<Task>(task.Id);

            oldTask.Description = task.Description;
            oldTask.IsComplited = task.IsComplited;
            oldTask.Name = task.Name;
            oldTask.RemindPeriod = task.RemindPeriod;
            oldTask.SupTaskId = task.SupTaskId;
            oldTask.TargetDT = task.TargetDT;

            Repository.Update(oldTask, true);

            return oldTask;
        }

        public void Delete(int id)
        {
            //UserProfile userProfile = _TaskRepository.GetUserProfile(_WorkContext.UserAppId);
            if(!IsUserTaskOwning(id))
                throw new Exception($"User is not owner of the Task({id})");

            Repository.Delete<Task>(id, true);
        }

        bool IsUserTaskOwning(Task task, UserProfile userProfile = null)
        {
            userProfile = userProfile ?? Repository.GetUserProfile(_WorkContext.UserAppId);
            return task.UserProfile_Id == userProfile.Id;
        }
        bool IsUserTaskOwning(int taskId, UserProfile userProfile = null)
        {
            var task = Repository.GetByKey<Task>(taskId);
            return IsUserTaskOwning(task, userProfile);
        }
    }
}
