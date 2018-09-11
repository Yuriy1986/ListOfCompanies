using System.Web;
using System.Web.Optimization;

namespace ListOfCompanies.WEB
{
    public class BundleConfig
    {
        //Дополнительные сведения об объединении см. по адресу: http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Используйте версию Modernizr для разработчиков, чтобы учиться работать. Когда вы будете готовы перейти к работе,
            // используйте средство сборки на сайте http://modernizr.com, чтобы выбрать только нужные тесты.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            // jquery-ui.
            bundles.Add(new ScriptBundle("~/bundles/jquery-ui").Include(
            "~/Scripts/jquery-ui.js",
            "~/Scripts/jquery-ui-i18n.min.js"));
            bundles.Add(new StyleBundle("~/Content/jquicss").Include(
          "~/Content/jquery-ui.css",
          "~/Content/jquery-ui.theme.css"));

            // jqGrid.
            bundles.Add(new StyleBundle("~/Content/jqgridcss").Include(
            "~/Content/jquery.jqGrid/ui.jqgrid.css"));
            bundles.Add(new ScriptBundle("~/bundles/jqgrid").Include(
            "~/Scripts/jquery.jqGrid.min.js",
            "~/Scripts/i18n/grid.locale-ru.js"));
        }
    }
}
