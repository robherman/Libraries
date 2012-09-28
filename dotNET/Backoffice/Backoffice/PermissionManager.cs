using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Southapps.Framework.Backoffice.SDK.Model;

namespace Southapps.Framework.Backoffice
{
    public class PermissionManager
    {
        public static bool UserHasPermission(string module, Permissions permission)
        {
            if (HttpContext.Current.Session["isAdmin"] != null)
            {
                return true;
            }

            List<ModulePermission> permissions = GetModulePermissionsFromSession();

            return permissions.Exists(e => e.Module == module && e.Permission == permission);
        }

        private static List<ModulePermission> GetModulePermissionsFromSession()
        {
            object permissionsSessionObj = HttpContext.Current.Session["UserPermissions"];

            if (permissionsSessionObj == null)
            {
                HttpContext.Current.Response.Redirect("~/Login.aspx");
            }

            List<ModulePermission> permissions = (List<ModulePermission>)permissionsSessionObj;

            return permissions;
        }

        public static bool UserCanViewMenu(string module)
        {
            if (!UserHasPermission(module, Permissions.View) && !UserHasPermission(module, Permissions.Modify) && !UserHasPermission(module, Permissions.Add))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static void InitializePermissions(List<ModulePermission> permissions)
        {
            HttpContext.Current.Session["UserPermissions"] = permissions;
        }

        public static bool UserHasUrlAccess(string url)
        {
            if (HttpContext.Current.Session["isAdmin"] != null)
            {
                return true;
            }

            object permissionsSessionObj = HttpContext.Current.Session["UserPermissions"];

            if (permissionsSessionObj == null)
            {
                HttpContext.Current.Response.Redirect("~/Login.aspx");
            }

            List<ModulePermission> permissions = (List<ModulePermission>)permissionsSessionObj;

            return permissions.Exists(e => !string.IsNullOrEmpty(e.Url)? e.Url.ToLower().Equals(url.ToLower()) : false);
        }

        public static string GetBackendModuleFromUrl(string url)
        {
            List<ModulePermission> modulePermissions = GetModulePermissionsFromSession();

            ModulePermission modulePermission = modulePermissions.Find(e => e.Url.Equals(url.ToLower()));

            if (modulePermission == null)
            {
                //TODO: ERROR!!!
            }

            return modulePermission.Module;
        }
    }
}