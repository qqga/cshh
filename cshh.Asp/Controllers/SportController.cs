using cshh.Asp.Models;
using cshh.Model.Services.Sport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cshh.Asp.Controllers
{
    public class SportController : Controller
    {
        readonly IWorkoutService _WorkoutService;
        public SportController(IWorkoutService workoutService)
        {
            _WorkoutService = workoutService;
        }
        // GET: Sport
        public ActionResult Set(int userId)
        {
            if(userId <= 0)
                throw new Exception("userId not set");

            var vm = GetSetViewModel(userId);
            return View(vm);
        }

        [HttpPost]
        public ActionResult Set(WorkoutSetViewModel vmIn)
        {
            if(vmIn.UserId <= 0)
                throw new Exception("userId not set");

            _WorkoutService.AddSet(vmIn.UserId, vmIn.Reps);

            //var vmOut = GetSetViewModel(vmIn.UserId);
            return RedirectToAction(nameof(Set), new { UserId = vmIn.UserId });
        }

        WorkoutSetViewModel GetSetViewModel(int userId)
        {
            return new WorkoutSetViewModel(_WorkoutService, userId);
        }

        public ActionResult Clear(int userId)
        {
            var identityName = User?.Identity?.Name;
            if(identityName != "qqga@mail.ru")
                throw new Exception($"Not authorized for user {identityName}");

            _WorkoutService.ClearSets(userId);
            return RedirectToAction(nameof(Set), new { UserId = userId });
        }

        public ActionResult History(int userId)
        {
            var sets = _WorkoutService.GetSets(userId).ToList();
            return View(sets);
        }
    }
}

namespace cshh.Asp.Models
{
    public class WorkoutSetViewModel
    {
        public int UserId { get; set; }
        public int Reps { get; set; }
        public UserTotalResult UserTotalResult { get; }
        public WorkoutSetViewModel()
        {
        }
        public WorkoutSetViewModel(IWorkoutService workoutService, int userId)
        {
            UserId = userId;
            UserTotalResult = workoutService.GetUserTotalResult(userId);
            var lastSet = workoutService.GetSets(userId).OrderByDescending(_ => _.Id).FirstOrDefault();
            var lastReps = lastSet?.Reps ?? 50;
            Reps = lastReps;
        }
    }
}


