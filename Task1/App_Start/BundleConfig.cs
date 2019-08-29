using System.Web.Optimization;

namespace Yevhenii_KoliesnikTask1
{
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
            bundles.Add(new ScriptBundle("~/OrderHistoryBoundle").Include("~/Scripts/StoreScript/OrderHistory.js"));

            bundles.Add(new ScriptBundle("~/StoreJS").Include("~/Scripts/StoreScript/PaginationScript.js",
                                                              "~/Scripts/StoreScript/GameCommentScript.js"));

            bundles.Add(new ScriptBundle("~/GenreJS").Include("~/Scripts/StoreScript/GenreScript.js"));

            bundles.Add(new ScriptBundle("~/CommentAjax").Include("~/Scripts/StoreScript/CommentsAjax.js"));
            bundles.Add(new ScriptBundle("~/IBOX").Include("~/Scripts/StoreScript/IBOX.js"));
            bundles.Add(new ScriptBundle("~/WYSIWYGeditor").Include("~/Scripts/StoreScript/EditorWYSIWYG.js"));
            bundles.Add(new ScriptBundle("~/StoreJavaScript").Include("~/Scripts/jquery-1.8.2.min.js",
                                                                      "~/Scripts/jquery.validate.min.js",
                                                                      "~/Scripts/jquery.validate.unobtrusive.min.js",
                                                                      "~/Scripts/jquery-ui.min.js",
                                                                      "~/Scripts/jquery.uploadify.min.js"
                                                                      ));
            bundles.Add(new ScriptBundle("~/GameFilters").Include("~/Scripts/StoreScript/GameFilter.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/content/css").Include(
             "~/Scripts/jquery-ui.min.css",
             "~/Content/bootstrap.css",
             "~/Content/style.css",
             "~/Content/bootswatch.min.css",
             "~/Content/body.css",
             "~/Content/bootstrap-responsive.min.css",
             "~/Content/bootstrap-mvc-validation.css",
              "~/CSS/GenreCSS.css",
              "~/CSS/uploadify.css"
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



            bundles.Add(new ScriptBundle("~/js").Include(
              "~/Scripts/jquery-*",
               "~/Scripts/bootswatch.js",
              "~/Scripts/bootstrap.js"
              ));

        }
    }
}