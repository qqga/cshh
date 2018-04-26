using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace Common.Asp.ModelBinder
{
    public class DIModelBinder : System.Web.Mvc.DefaultModelBinder
    {
        protected override object CreateModel(ControllerContext controllerContext, System.Web.Mvc.ModelBindingContext bindingContext, Type modelType)
        {
            object model = null;

            try
            {
                model = base.CreateModel(controllerContext, bindingContext, modelType);
            }
            catch(MissingMethodException ex)
            {
                model = DependencyResolver.Current.GetService(modelType);
                if(model == null)
                    throw ex;
            }

            return model;
        }
    }
}