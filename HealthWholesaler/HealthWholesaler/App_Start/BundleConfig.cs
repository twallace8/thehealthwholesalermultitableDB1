using System.Web;
using System.Web.Optimization;

namespace HealthWholesaler
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/bootbox.js",
                      "~/scripts/DataTables/jquery.dataTables.js",
                      "~/scripts/DataTables/dataTables.bootstrap.js",
                      "~/Scripts/typeahead.bundle.js",
                      "~/Scripts/toastr.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/Sketch.css",
                      //"~/Content/lumen.css",
                      "~/content/DataTables/css/dataTables.bootstrap.css",
                     "~/Content/Typeahead.css",
                     "~/Content/toastr.css",
                      "~/Content/site.css"));
        }
    }
}
