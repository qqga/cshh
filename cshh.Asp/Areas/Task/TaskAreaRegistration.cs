using System.Web.Mvc;

namespace cshh.Asp.Areas.Task
{
    public class TaskAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Task";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Task_default",
                "Task/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional, controller = "Task" }
            );
        }
    }
}