using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace cshh.Asp
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString TextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<string> values, object htmlAttributes = null)
        {
            MemberExpression memberexpression = expression.Body as MemberExpression;
            string eprMemerName = memberexpression.Member.Name;

            TProperty modelValue = expression.Compile()(htmlHelper.ViewData.Model);

            string dataOptionsStr = values.Aggregate("", (acc, val) => acc += $"<option>{val}</option>{Environment.NewLine}");

            PropertyInfo[] atrProperties = htmlAttributes?.GetType()?.GetProperties();
            string attributesStr = atrProperties?.Aggregate("", (acc, val) => acc += $"{val.Name}='{val.GetValue(htmlAttributes)}'") ?? string.Empty;

            string result =
                $@"<input type='text' name='{eprMemerName}' value='{modelValue}' list='{eprMemerName}-datalist' {attributesStr}/>
                <datalist id = '{eprMemerName}-datalist' >  
                {dataOptionsStr}
                </datalist>";
            return MvcHtmlString.Create(result);
        }

        public static MvcHtmlString TextBox<TModel>(this HtmlHelper<TModel> htmlHelper, string name, IEnumerable<string> values, string value = null, object htmlAttributes = null)
        {
            //MemberExpression memberexpression = expression.Body as MemberExpression;
            //string eprMemerName = memberexpression.Member.Name;

            //TProperty modelValue = expression.Compile()(htmlHelper.ViewData.Model);

            string dataOptionsStr = values.Aggregate("", (acc, val) => acc += $"<option>{val}</option>{Environment.NewLine}");

            PropertyInfo[] atrProperties = htmlAttributes?.GetType()?.GetProperties();
            string attributesStr = atrProperties?.Aggregate("", (acc, val) => acc += $"{val.Name}='{val.GetValue(htmlAttributes)}'") ?? string.Empty;

            string result =
                $@"<input type='text' name='{name}' value='{value}' list='{name}-datalist' {attributesStr}/>
                <datalist id = '{name}-datalist' >  
                {dataOptionsStr}
                </datalist>";
            return MvcHtmlString.Create(result);
        }
    }
}