﻿namespace UniversityStudentSystem.Web
{
    using System.Web.Optimization;

    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/css-SBAdmin").Include(
                            "~/SBAdminTheme/bower_components/bootstrap/dist/css/bootstrap.min.css",
                            "~/SBAdminTheme/bower_components/metisMenu/dist/metisMenu.min.css",
                            "~/SBAdminTheme/dist/css/timeline.css",
                            //"~/SBAdminTheme/bower_components/datatables-plugins/integration/bootstrap/3/dataTables.bootstrap.css",
                            //"~/SBAdminTheme/bower_components/datatables-responsive/css/dataTables.responsive.css",
                            "~/SBAdminTheme/dist/css/sb-admin-2.css",
                            "~/SBAdminTheme/bower_components/morrisjs/morris.css",
                            "~/SBAdminTheme/bower_components/font-awesome/css/font-awesome.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/js-SBAdmin").Include(
                            //"~/SBAdminTheme/bower_components/jquery/dist/jquery.min.js",
                            "~/SBAdminTheme/bower_components/bootstrap/dist/js/bootstrap.min.js",
                            "~/SBAdminTheme/bower_components/metisMenu/dist/metisMenu.min.js",
                            "~/SBAdminTheme/bower_components/raphael/raphael-min.js",
                            "~/SBAdminTheme/bower_components/morrisjs/morris.min.js",
                            "~/SBAdminTheme/dist/js/sb-admin-2.js"
                           // "~/SBAdminTheme/bower_components/datatables/media/js/jquery.dataTables.min.js",
                            //"~/SBAdminTheme/bower_components/datatables-plugins/integration/bootstrap/3/dataTables.bootstrap.min.js"
                            ));

            bundles.Add(new StyleBundle("~/bundles/css-social").Include(
                            "~/SBAdminTheme/bower_components/bootstrap-social/bootstrap-social.css"));

            //// ----- Kendo --------
            bundles.Add(new ScriptBundle("~/bundles/js-kendo-jquery").Include(
                            "~/Scripts/Kendo/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/js-kendo").Include(
                "~/Scripts/Kendo/kendo.web.min.js",
                "~/Scripts/Kendo/kendo.aspnetmvc.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/js-kendo-listview").Include(
                        "~/Scripts/Kendo/kendo.listview.min.js"));

            //// ------ Default -----
            bundles.Add(new StyleBundle("~/bundles/css-kendo").Include(
                            "~/Content/Kendo/kendo.common.min.css",
                            "~/Content/Kendo/kendo.metro.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/css-site").Include(
                            "~/Content/Site.css"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
