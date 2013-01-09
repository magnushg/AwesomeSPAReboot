using System.Web.Optimization;

namespace AwesomeSPAReboot.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                       "~/Scripts/jquery-{version}.js"));
            
            bundles.Add(new StyleBundle("~/Content/styles").Include("~/Content/styles.css", "~/Content/toastr.css"));
            
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap*"));
            bundles.Add(new StyleBundle("~/Content/bootstrap").Include("~/Content/bootstrap.css", "~/Content/bootstrap-responsive.css"));

            bundles.Add(new ScriptBundle("~/bundles/vendor").Include(
                "~/Scripts/knockout-{version}.js",
                "~/Scripts/toastr.js",
                "~/Scripts/underscore.js"));
        }
    }
}