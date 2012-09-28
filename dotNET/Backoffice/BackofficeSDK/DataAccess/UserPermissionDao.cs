using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SouthApps.STS.SDK.DAL.Model;

namespace Southapps.Framework.Backoffice.SDK.DataAccess
{
    public class UserPermissionDao : DataAccessObject
    {
        #region Get

        public List<UserPermission> GetUserPermissions(int userId)
        {
            try
            {
                using (var entities = this.EntityContext)
                {
                    var query = from e in entities.UserPermissions.Include("bo_module_permission.bo_module").Include("bo_module_permission.bo_permission")
                                where e.userId == userId
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
