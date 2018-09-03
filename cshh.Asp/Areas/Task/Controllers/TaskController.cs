using AutoMapper;
using Common.Asp.JqGrid;
using cshh.Asp.Areas.Task.Models;
using cshh.Asp.Controllers.Base;
using cshh.Asp.Extensions;
using cshh.Data.Services.Repo;
using cshh.Model.Services.Tasks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace cshh.Asp.Areas.Task.Controllers
{
    [Authorize]
    public class TaskController : BaseCshhController
    {
        ITaskService _taskService;
        IMapper _mapper;

        public TaskController(ITaskService taskService, IMapper mapper)
        {
            _taskService = taskService;
            _mapper = mapper;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult JqGridEditList()
        {
            return PartialView("_JqGrid");
        }


        public ActionResult JqGridList(JqGridRequest jqGridRequest)
        {
            try
            {
                _taskService.Repository.SetProxyCreationEnabled(false);
                IQueryable<Data.Tasks.Task> tasks = _taskService.GetTasks()/*.Include(t => t.SubTasks)*/.Where(t => t.SupTaskId == null);

                var resp = new JqGridResponse<Data.Tasks.Task, TaskVM>
                    (jqGridRequest, tasks, w => w.Id, JsonConvert.DeserializeObject, t => _mapper.Map<TaskVM>(t));

                resp.rows.Count();//invoke query to detect errrors

                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.CollectMessages());
            }
        }

        public ActionResult JqGridListSubTasks(JqGridRequest jqGridRequest, int SupTaskId)
        {
            try
            {
                _taskService.Repository.SetProxyCreationEnabled(false);

                IQueryable<Data.Tasks.Task> tasks = _taskService.GetTasks().Where(t => t.SupTaskId == SupTaskId);//.Include(t => t.SubTasks);

                var resp = new JqGridResponse<Data.Tasks.Task, TaskVM>
                    (jqGridRequest, tasks, w => w.Id, JsonConvert.DeserializeObject, t => _mapper.Map<TaskVM>(t));

                resp.rows.Count();//invoke query to detect errrors

                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.CollectMessages());
            }
        }

        public string JqGridAdd(TaskVM taskVM)
        {
            try
            {
                //_taskService.Repository.SetProxyCreationEnabled(false);
                Data.Tasks.Task task = _mapper.Map<Data.Tasks.Task>(taskVM);
                Data.Tasks.Task newTask = _taskService.Add(task);

                //newTask.UserProfile = null;
                //string serializedTask = JsonConvert.SerializeObject(newTask, typeof(TaskLib.ITask), new JsonSerializerSettings() { });
                //string serializedTask = JsonConvert.SerializeObject(newTask, new JsonSerializerSettings() { ContractResolver = new JsonTypeContractResolver<TaskLib.ITask>() });
                string serializedTask = newTask.ToJson<TaskLib.ITask>();
                Handlers.TaskHandler.InformClients(this.GetAppUserId(), $"{{Operation:\"add\",Tasks:[{serializedTask}]}}");//todo 
            }
            catch(Exception ex)
            {
                return this.BadRequestAndCollectEx(ex);
            }

            return null;
        }
        public string JqGridEdit(TaskVM taskVM)
        {
            try
            {
                //_taskService.Repository.SetProxyCreationEnabled(false);

                Data.Tasks.Task task = _mapper.Map<Data.Tasks.Task>(taskVM);
                Data.Tasks.Task editedTask = _taskService.Edit(task);

                //editedTask.UserProfile = null;
                //string serializedTask = JsonConvert.SerializeObject(editedTask, typeof(TaskLib.ITask), new JsonSerializerSettings() { });
                string serializedTask = editedTask.ToJson<TaskLib.ITask>();
                Handlers.TaskHandler.InformClients(this.GetAppUserId(), $"{{Operation:\"update\",Tasks:[{serializedTask}]}}");//todo 
            }
            catch(Exception ex)
            {
                return this.BadRequestAndCollectEx(ex);
            }

            return null;
        }

        public string JqGridDelete(int id)
        {
            try
            {
                _taskService.Delete(id);
                Handlers.TaskHandler.InformClients(this.GetAppUserId(), $"{{Operation:\"delete\",Tasks:[{{TaskId:{id}}}]}}");//todo
            }
            catch(Exception ex)
            {
                return this.BadRequestAndCollectEx(ex);
            }

            return null;
        }
    }
}