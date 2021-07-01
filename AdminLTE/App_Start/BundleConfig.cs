using System.Web.Optimization;

namespace AdminLTE
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-1.10.2.min.js",
                "~/Scripts/app.min.js"
                ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"
                ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/respond.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/utilitarios").Include(
                "~/Scripts/moment.js",
                "~/Scripts/sweetalert.min.js",
                "~/Scripts/bootstrap-datepicker.min.js",
                "~/Scripts/bootstrap-datepicker.pt-BR.min.js",
                "~/Scripts/Chart.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
               "~/Scripts/angular.min.js",
               "~/Scripts/app.js",
               "~/Scripts/angular-locale_pt-br.js",
               "~/Scripts/dirPagination.js",
               "~/Scripts/mask.js",
               "~/Scripts/ng-file-upload.js",
               "~/Scripts/angular-input-masks-standalone.min.js",
               "~/Scripts/Controllers/HomeController.js",
               "~/Scripts/Controllers/UsuarioController.js",
               "~/Scripts/Controllers/CargoController.js",
               "~/Scripts/Controllers/BoletoController.js"
              
               ));

            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                "~/Content/bootstrap.min.css",
                "~/Content/bootstrap-datepicker.min.css"
                ));

            bundles.Add(new StyleBundle("~/Content/AdminLTE").Include(
                "~/Content/AdminLTE.min.css",
                "~/Content/skin-blue.min.css",
                "~/Content/sweetalert.min.css"
                ));

        }
    }
}