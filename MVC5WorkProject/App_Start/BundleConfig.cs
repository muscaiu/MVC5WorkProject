using System.Web.Optimization;

namespace MVC5WorkProject
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
    // SCRIPT BUNDLES:
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(         //jQuery.js
                        "~/Scripts/jQuery/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(      //jQueryUI.js
                        "~/Scripts/jQuery/jquery-ui-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(      //jQueryVal.js
                        "~/Scripts/jQuery/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(      //Modernizer.js
                        "~/Scripts/modernizr-*")); 
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(     //Bootstrap.js
                      "~/Scripts/Bootstrap/bootstrap.js",                    //Respond.js
                      "~/Scripts/Bootstrap/respond.js"));
            bundles.Add(new ScriptBundle("~/bundles/angular").Include(       //Angular.js
                  "~/Scripts/Angularjs/angular.js"));
            //Angular Bundle for all the js inside the "app" folder for Servers project
            bundles.Add(new ScriptBundle("~/bundles/AngularCustom")          //AngularCustom.js
                .IncludeDirectory(
                  "~/Scripts/app", "*.js", true));

   //STYLE BUNDLES:
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/site.css",                                        //Site.css
                "~/Content/Bootstrap/bootstrap.css",                         //Bootstrap.css
                "~/Content/themes/base/jquery.ui.core.css",                  //jQuery.ui.core.css
                "~/Content/themes/base/jquery.ui.draggable.css",             //jQuery.ui.draggable.css
                "~/Content/themes/base/jquery.ui.theme.css",                 //jquery.ui.theme.css
                "~/fonts/FontAwesome/CSS/font-awesome.css"));                  //font-awesome.css
        }
    }
}
