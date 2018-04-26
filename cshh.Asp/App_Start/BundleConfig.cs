using System.Web;
using System.Web.Optimization;

namespace cshh.Asp
{
    public class BundleConfig
    {
        // Дополнительные сведения об объединении см. на странице https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region ScriptBundle

            bundles.Add(new ScriptBundle("~/bundles/common").Include(
            "~/Scripts/polyfill.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-unobtrusive-ajax").Include(
                        "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-ui").Include(
            "~/Scripts/jquery-ui.js"));



            bundles.Add(new ScriptBundle("~/bundles/JqGrid").Include(
            "~/Scripts/JqGrid/i18n/grid.locale-ru.js",
            "~/Scripts/JqGrid/jquery.jqGrid.js",
            "~/Scripts/JqGrid/JqGridSettings.js"));

            // Используйте версию Modernizr для разработчиков, чтобы учиться работать. Когда вы будете готовы перейти к работе,
            // готово к выпуску, используйте средство сборки по адресу https://modernizr.com, чтобы выбрать только необходимые тесты.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                     "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));
            #endregion


            #region Style

            bundles.Add(new StyleBundle("~/Content/ui.jqgrid-bootstrap.css").Include(
"~/Content/JqGrid/ui.jqgrid-bootstrap4.css"));

            bundles.Add(new StyleBundle("~/Content/ui.jqgrid.css").Include(
"~/Content/JqGrid/ui.jqgrid.css"));

            #region jquerty ui theme
            bundles.Add(new StyleBundle("~/bundles/jquery-ui-base").Include("~/Content/jquery-ui.css"));
            bundles.Add(new StyleBundle("~/bundles/jquery-ui-swanky").Include("~/Content/JqTheme/swanky/jquery-ui.css"));
            bundles.Add(new StyleBundle("~/bundles/jquery-ui-redmond").Include("~/Content/JqTheme/redmond/jquery-ui.css"));
            bundles.Add(new StyleBundle("~/bundles/jquery-ui-overcast").Include("~/Content/JqTheme/overcast/jquery-ui.css"));            
            #endregion



            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            #endregion
        }
    }
}
