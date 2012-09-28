using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.Common;
using System.Data;

using MySql.Data.MySqlClient;

using SouthApps.Framework.DataAccess.Exceptions;
using SouthApps.Framework.DataAccess.Engines.MySql;
using SouthApps.Framework.DataAccess;

using SouthApps.STS.SDK.DAL.Model;
using SouthApps.Framework.LoggerService;
using System.Security;

using System.Data.EntityClient;
using System.Data.Objects;

namespace Southapps.Framework.Backoffice.SDK.DataAccess
{
    public abstract class DataAccessObject : IDisposable
    {
        private bool isDisposed;
        private EntityConnection connection;

        protected BackofficeEntities EntityContext
        {
            get
            {
                if (this.isDisposed)
                {
                    throw new ObjectDisposedException("The data access object has been disposed.");
                }

                try
                {
                    EntityConnection connection = new EntityConnection(ConfigurationManager.ConnectionStrings["BackofficeEntities"].ConnectionString);

                    return new BackofficeEntities(connection);
                }
                catch (Exception ex)
                {
                    throw new DataAccessException(DataAccessErrorCode.ConnectionConfigurationError, "Error creating connection {0}.", ex.Message);
                }
            }
        }

        public void Dispose()
        {
            this.Dispose(true);

            GC.SuppressFinalize(this);
        }


        protected virtual void Dispose(bool disposing)
        {
            if (this.connection != null)
            {
                this.connection.Dispose();
                this.connection = null;
            }

            this.isDisposed = true;
        }

        protected Exception HandleException(Exception ex)
        {

            if (ex is DataException)
            {
                DataAccessException dataAccessException = new DataAccessException(ex.Message);

                if (ex.InnerException != null && ex.InnerException is MySqlException)
                {
                    MySqlException mySqlException = ex.InnerException as MySqlException;
                    dataAccessException = new DataAccessException(mySqlException.Message);

                    switch (mySqlException.Number)
                    {
                        case MySqlErrorCodes.UniqueKeyConstraint:
                            dataAccessException.DataBaseResultCode = DataAccessErrorCode.UniqueKeyConstraintViolation;
                            break;
                        case MySqlErrorCodes.ForeignKeyRowNotExists:
                        case MySqlErrorCodes.ForeignKeyRowReferenced:
                            dataAccessException.DataBaseResultCode = DataAccessErrorCode.ForeignKeyViolation;
                            break;
                        default:
                            dataAccessException.DataBaseResultCode = DataAccessErrorCode.UnknownError;
                            break;
                    }

                    return dataAccessException;
                }

                dataAccessException = new DataAccessException(ex.Message);

                return dataAccessException;
            }
            else
            {
                DataAccessException dataAccessException = new DataAccessException(ex.Message);
                if (ex.InnerException != null)
                {
                    dataAccessException = new DataAccessException(ex.InnerException.Message);
                }
                    

                return dataAccessException;
            }
        }

        protected void SaveChanges(ObjectContext entities)
        {
            int affectedRows = entities.SaveChanges();

            if (affectedRows == 0)
            {
                throw new DataAccessException(DataAccessErrorCode.RecordNotFound);
            }
        }
    }
}
