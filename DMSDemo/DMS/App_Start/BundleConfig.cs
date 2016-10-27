using System.Web;
using System.Web.Optimization;

namespace DMS
{
    /// <summary>
    /// BundleConfig
    /// </summary>
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/toastr.css",
                 "~/Content/bootstrap.css",
                 "~/Content/bootstrap-multiselect.css",
                 "~/Content/bootstrap-select.css", 
                 "~/Content/DataTables/css/jquery.dataTables.css",
                 "~/Content/jquery-ui.min.css",
                // "~/Content/ui-grid.css",
                "~/Content/site.css",
                "~/Content/custom.css",
                "~/Content/responsive-tabs.css"
                ));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));


            bundles.Add(new ScriptBundle("~/bundles/appConfig")
                 .IncludeDirectory("~/JS", "*.js"));

            bundles.Add(new ScriptBundle("~/bundles/login")
                .IncludeDirectory("~/JS/Login", "*.js"));

            bundles.Add(new ScriptBundle("~/bundles/Controller")
                 .IncludeDirectory("~/JS/Controller", "*.js"));

            bundles.Add(new ScriptBundle("~/bundles/Services")
                 .IncludeDirectory("~/JS/Services", "*.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                 "~/Scripts/angular.js",
                  "~/Scripts/angular-sanitize.js",
                  "~/Scripts/angular-animate.js",
                 "~/Scripts/angular-ui-router.js",
                 //"~/Scripts/ui-grid.js",
                 "~/Scripts/angular-local-storage.js",
                 "~/Scripts/toastr.js",
                 "~/Scripts/DataTables/jquery.dataTables.js"
                 ));

            bundles.Add(new ScriptBundle("~/Scripts/Accordian").Include(
               "~/Scripts/ddaccordion.js",
                "~/Scripts/jquery.responsiveTabs.js"
               ));

            bundles.Add(new ScriptBundle("~/Scripts/Scroll").Include(
             "~/Scripts/jquery.slimscroll.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
               "~/Scripts/bootstrap.min.js"));

            //bundles.Add(new ScriptBundle("~/bundles/UIGrid").Include(
            //    "~/Scripts/ui-grid.min.js"
            //    ));

            bundles.Add(new ScriptBundle("~/bundles/SignUp").Include(
                 "~/JS/Services/RegistrationService.js",
                "~/JS/Controller/RegistrationController.js"
                ));
        }
    }
}