using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SouthApps.Framework.DataAccess.Exceptions
{
    public class DataAccessException : Exception
    {
        private DataAccessErrorCode databaseResultCode;

        public DataAccessErrorCode DataBaseResultCode
        {
            get;
            set;
        }

        public DataAccessException()
        {

        }

        public DataAccessException(DataAccessErrorCode errorCode)
        {
            this.DataBaseResultCode = errorCode;
        }

        public DataAccessException(string message) :base(message)
        {
            
        }

        public DataAccessException(DataAccessErrorCode errorCode, string message, string exceptionDetail)
            : base(string.Format(message, exceptionDetail))
        {
            this.databaseResultCode = errorCode;
        }
    }
}
