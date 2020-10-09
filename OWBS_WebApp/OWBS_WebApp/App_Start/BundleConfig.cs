using System.Web;
using System.Web.Optimization;

namespace OWBS_WebApp
{
    public class BundleConfig
    {
        // 如需「搭配」的詳細資訊，請瀏覽 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                                         "~/Scripts/jquery-{version}.js"
                                         #region 加入 jQuery DataTable
                                         , "~/Scripts/jquery.dataTables.js"
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
                                         "~/Scripts/jquery.validate*")
                                         );

            // 使用開發版本的 Modernizr 進行開發並學習。然後，當您
            // 準備好實際執行時，請使用 http://modernizr.com 上的建置工具，只選擇您需要的測試。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                                         "~/Scripts/modernizr-*"
                                         ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(                                         
                                         "~/Scripts/bootstrap.js"
                                         , "~/Scripts/respond.js"
                                         ));

            bundles.Add(new ScriptBundle("~/bundles/videojs").Include(
                                         "~/Scripts/video.js"
                                         , "~/Scripts/videojs-markers.js"
                                         ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                                        "~/Content/bootstrap.css"
                                        #region 加入 jQuery DataTable
                                        , "~/Content/Datatables/jquery.dataTables.css"
                                        , "~/Content/DataTables/css/buttons.dataTables.css"
                                        #endregion
                                        #region 加入 jQuery DateTimePicker
                                        , "~/Content/jquery.datetimepicker.css"
                                        #endregion
                                        #region 加入 videojs
                                        , "~/Content/video-js.css"
                                        , "~/Content/videojs.markers.css"
                                        #endregion
                                        , "~/Content/site.css"
                                        ));

            
        }
    }
}
