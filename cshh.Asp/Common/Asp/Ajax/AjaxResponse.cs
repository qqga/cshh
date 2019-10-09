using cshh.Asp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cshh.Asp
{
    public class AjaxResponse : ActionResult
    {
        public AjaxResponse(string message, bool error = false)
        {
            Message = message;
            IsError = error;
            if(error)
                StatusCode = 400;

            DateTime = DateTime.Now;
        }
        public int StatusCode { get; set; } = 200;

        public AjaxResponse(Exception ex) : this(ex.CollectMessages(), true) { }

        public bool IsError { get; set; }
        public string Message { get; set; }
        public DateTime DateTime { get; set; }
        public string DateTimeStr => DateTime.ToString("dd.MM.yyyy HH:mm:ss");

        public override void ExecuteResult(ControllerContext context)
        {
            JsonResult jsonResult = new JsonResult()
            {
                Data = this, 
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            };
            context.HttpContext.Response.StatusCode = StatusCode;
            jsonResult.ExecuteResult(context);
        }
    }
}