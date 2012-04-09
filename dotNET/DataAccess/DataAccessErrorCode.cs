using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SouthApps.Framework.DataAccess
{
    public enum DataAccessErrorCode
    {
        UnknownError = 0,

        ConnectionConfigurationError = 1,

        RecordNotFound = 2,

        UniqueKeyConstraintViolation = 3,

        ForeignKeyViolation = 4
    }
}
