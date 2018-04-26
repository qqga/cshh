using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cshh.Asp.Extensions
{
    public static class SelectListItemExtnesions
    {
        public static IEnumerable<SelectListItem> ToSeletListItems<T>(this IEnumerable<T> list
            , Func<T, object> textSelector
            , Func<T, object> valueSelector
            , Func<T, bool> selectedPredicate = null
            , Func<T, bool> disabledPredicate = null)
        {
            IEnumerable<SelectListItem> select = list.Select(li => new SelectListItem()
            {
                Text = textSelector(li).ToString(),
                Value = valueSelector(li).ToString(),
                Selected = selectedPredicate?.Invoke(li) ?? false,
                Disabled = disabledPredicate?.Invoke(li) ?? false,
            }).ToList();

            return select;
        }

        public static IEnumerable<SelectListItem> ToSeletListItems<T>(this IEnumerable<T> list
         , Func<T, object> textSelector
         , Func<T, object> valueSelector
         , object selectedId = null
         , Func<T, bool> disabledPredicate = null)

        => ToSeletListItems(
            list,
            textSelector,
            valueSelector,
            (li) => selectedId != null && valueSelector(li).Equals(selectedId),
            disabledPredicate);
    }
}