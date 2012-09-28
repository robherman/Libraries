using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SouthApps.STS.SDK.DAL.Model;
using System.Data.Objects;

namespace Southapps.Framework.Backoffice.SDK.DataAccess
{
    public class RolePermissionDao : DataAccessObject
    {
        #region Get

        public List<RolePermission> GetRolePermissions(int roleId)
        {
            try
            {
                using (var entities = this.EntityContext)
                {
                    var query = from e in entities.RolePermissions.Include("bo_module_permission.bo_module").Include("bo_module_permission.bo_permission")
                                where e.roleId == roleId
                                select e;

                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                throw this.HandleException(ex);
            }
        }

        #endregion
    }
}
