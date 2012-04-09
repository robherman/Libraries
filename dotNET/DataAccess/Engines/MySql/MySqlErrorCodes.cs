using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SouthApps.Framework.DataAccess.Engines.MySql
{
    public static class MySqlErrorCodes
    {
        public const int UniqueKeyConstraint = 1062;
        public const int ForeignKeyRowNotExists = 1451;
        public const int ForeignKeyRowReferenced = 1452;
    }
}
