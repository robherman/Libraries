using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SouthApps.STS.SDK.DAL.Model;
using System.Data.Objects;

namespace Southapps.Framework.Backoffice.SDK.DataAccess
{
    public class RoleDao : DataAccessObject
    {
        #region Add

        public List<Role> GetRoles()
        {
            try
            {
                using (var entities = this.EntityContext)
                {
                    var query = from e in entities.Roles
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
