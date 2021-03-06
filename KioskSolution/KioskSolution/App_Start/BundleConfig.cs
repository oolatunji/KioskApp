﻿using System.Web;
using System.Web.Optimization;

namespace KioskSolution
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/common_libraries").Include(
                      "~/Scripts/jquery.js",
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/jquery-ui.min.js",
                      "~/Scripts/moment.min.js",
                      "~/Scripts/lodash.min.js",
                      "~/Scripts/fullcalendar.min.js",
                      "~/Scripts/jquery.rateit.min.js",
                      "~/Scripts/jquery.prettyPhoto.js",
                      "~/Scripts/jquery.slimscroll.min.js",
                      "~/Scripts/jquery.dataTables.min.js",
                      "~/Scripts/dataTables.tableTools.js",
                      "~/Scripts/excanvas.min.js",
                      "~/Scripts/jquery.flot.js",
                      "~/Scripts/jquery.flot.resize.js",
                      "~/Scripts/jquery.flot.pie.js",
                      "~/Scripts/jquery.flot.stack.js",
                      "~/Scripts/jquery.noty.js",
                      "~/Scripts/default.js",
                      "~/Scripts/layouts/bottomRight.js",
                      "~/Scripts/sparklines.js",
                      "~/Scripts/jquery.cleditor.min.js",
                      "~/Scripts/bootstrap-datetimepicker.min.js",
                      "~/Scripts/jquery.onoff.min.js",
                      "~/Scripts/filter.js",
                      "~/Scripts/custom.js",
                      "~/Scripts/Utility/configFile.js",
                      "~/Scripts/Utility/messageBox.js",
                      "~/Scripts/Utility/SystemConfiguration.js",
                      "~/Scripts/Utility/flexiAppsCustomFormValidation.js",
                      "~/Scripts/Login/Login.js"));

            bundles.Add(new ScriptBundle("~/bundles/configureSystem").Include(
                    "~/Scripts/Utility/ConfigureSystem.js"
                    ));    

            bundles.Add(new ScriptBundle("~/bundles/addfunctions").Include(
                      "~/Scripts/Function/AddFunction.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/viewfunctions").Include(
                     "~/Scripts/Function/ViewFunction.js"
                     ));

            bundles.Add(new ScriptBundle("~/bundles/addroles").Include(
                      "~/Scripts/Role/AddRole.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/viewroles").Include(
                      "~/Scripts/Role/ViewRole.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/addbranch").Include(
                      "~/Scripts/Branch/AddBranch.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/viewbranch").Include(
                      "~/Scripts/Branch/ViewBranch.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/addusers").Include(
                     "~/Scripts/User/AddUser.js"
                     ));

            bundles.Add(new ScriptBundle("~/bundles/viewusers").Include(
                      "~/Scripts/User/ViewUser.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/addcustomer").Include(
                     "~/Scripts/Customer/AddCustomer.js"
                     ));

            bundles.Add(new ScriptBundle("~/bundles/viewcustomer").Include(
                      "~/Scripts/Customer/ViewCustomer.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/customercardrequest").Include(
                     "~/Scripts/Customer/CardRequest.js"
                     ));

            bundles.Add(new ScriptBundle("~/bundles/confirmcardrequest").Include(
                    "~/Scripts/Customer/ConfirmToken.js"
                    ));

            bundles.Add(new ScriptBundle("~/bundles/viewcardrequest").Include(
                    "~/Scripts/Customer/ViewCardRequest.js"
                    ));

            bundles.Add(new ScriptBundle("~/bundles/layout").Include(
                    "~/Scripts/Login/Layout.js",
                    "~/Scripts/DynamicMenu/Layout.js"
                    ));

            bundles.Add(new ScriptBundle("~/bundles/dashboard").Include(
                    "~/Scripts/DynamicMenu/Dashboard.js"
                    ));

            bundles.Add(new ScriptBundle("~/bundles/changepassword").Include(
                    "~/Scripts/Password/ChangePassword.js"
                    ));

            bundles.Add(new ScriptBundle("~/bundles/forgotpassword").Include(
                    "~/Scripts/Password/ForgotPassword.js"
                    ));

            bundles.Add(new ScriptBundle("~/bundles/resetpassword").Include(
                    "~/Scripts/Password/PasswordReset.js"
                    ));

            bundles.Add(new ScriptBundle("~/bundles/addpan").Include(
                      "~/Scripts/Pan/AddPan.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/viewpan").Include(
                      "~/Scripts/Pan/ViewPan.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/jquery-ui.css",
                      "~/Content/fullcalendar.css",
                      "~/Content/prettyPhoto.css",
                      "~/Content/rateit.css",
                      "~/Content/bootstrap-datetimepicker.min.css",
                      "~/Content/jquery.cleditor.css",
                       "~/Content/jquery.dataTables.min.css",
                      "~/Content/dataTables.tableTools.css",
                      "~/Content/jquery.onoff.css",
                      "~/Content/style.css",
                      "~/Content/widgets.css"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
