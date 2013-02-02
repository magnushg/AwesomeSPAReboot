using System.Web.Optimization;

namespace AwesomeSPAReboot.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                       "~/Scripts/jquery-1.9.0.min.js", "~/Scripts/jquery.signalR-1.0.0-rc2.js"));
            
            bundles.Add(new StyleBundle("~/Content/styles").Include("~/Content/styles.css", "~/Content/toastr.css"));
            
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap*"));
            bundles.Add(new StyleBundle("~/Content/bootstrap").Include("~/Content/bootstrap.css", "~/Content/bootstrap-responsive.css"));

            bundles.Add(new ScriptBundle("~/bundles/vendor").Include(
                "~/Scripts/knockout-{version}.js",
                "~/Scripts/toastr.js",
                "~/Scripts/underscore.js",
                "~/Scripts/highcharts.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").IncludeDirectory("~/Scripts/app/", "*.js"));
        }
    }
}