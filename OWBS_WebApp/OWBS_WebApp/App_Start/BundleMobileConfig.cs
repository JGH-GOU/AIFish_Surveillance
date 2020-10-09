using System.Web;
using System.Web.Optimization;

namespace OWBS_WebApp {
    public class BundleMobileConfig {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                                         "~/Scripts/jquery-{version}.js"            
                                         #region 加入 jQuery DataTable
                                         , "~/Scripts/DataTables/jquery.dataTables.js"
                                         //
                                         , "~/Scripts/DataTables/extensions/Buttons/js/dataTables.buttons.js"
                                         , "~/Scripts/DataTables/extensions/Buttons/js/buttons.flash.js"
                                         , "~/Scripts/DataTables/extensions/JSZip/jszip.js"
                                         , "~/Scripts/DataTables/extensions/pdfmake/pdfmake.js"
                                         , "~/Scripts/DataTables/extensions/pdfmake/vfs_fonts.js"
                                         , "~/Scripts/DataTables/extensions/Buttons/js/buttons.html5.js"
                                         , "~/Scripts/DataTables/extensions/Buttons/js/buttons.print.js"
                                         #endregion
                                         #region 加入 jQuery DateTimePicker
                                         , "~/Scripts/jquery.datetimepicker.js"
                                         #endregion
                                         ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                                         "~/Scripts/jquery.validate*"
                                         ));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                                         "~/Scripts/modernizr-*"
                                         ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                                         "~/Scripts/bootstrap.js"
                                         , "~/Scripts/respond.js"
                                         ));

            #region VideoJs
            bundles.Add(new ScriptBundle("~/bundles/videojs").Include(
                                         "~/Scripts/video.js"));
            #endregion

            #region ChartJs
            bundles.Add(new ScriptBundle("~/bundles/chartjs").Include(
                                         "~/Scripts/Chart.js"
                                         ));
            #endregion

            bundles.Add(new StyleBundle("~/Content/css").Include(
                                        "~/Content/bootstrap.css"
                                        #region 加入 jQuery DataTable
                                        , "~/Content/Datatables/jquery.dataTables.css"
                                        , "~/Content/DataTables/css/buttons.dataTables.css"
                                        #endregion
                                        #region 加入 jQuery DateTimePicker
                                        , "~/Content/jquery.datetimepicker.css"
                                        #endregion
                                        , "~/Content/site.css"
                                        ));            

            #region jQuery Mobile
            bundles.Add(new ScriptBundle("~/bundles/jquerymobile").Include(
                                         "~/Scripts/jquery.mobile-{version}.js")
                                         );

            bundles.Add(new StyleBundle("~/Content/Mobile/css").Include(
                                        "~/Content/Site.Mobile.css")
                                        );
            
            bundles.Add(new StyleBundle("~/Content/jquerymobile/css").Include(
                                        "~/Content/jquery.mobile-{version}.css")
                                        );
            #endregion

            #region AngularJS
            bundles.Add(new ScriptBundle("~/bundles/AgnularJs").Include(
                                         "~/Scripts/angular.js"
                                         , "~/Scripts/angular*"
                                         ));
            #endregion
        }
    }
}