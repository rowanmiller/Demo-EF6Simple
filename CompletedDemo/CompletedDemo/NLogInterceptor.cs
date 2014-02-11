using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;

namespace CompletedDemo
{
    public class NLogInterceptor : IDbCommandInterceptor
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
        }

        public void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            LogCommandComplete(command, interceptionContext);
        }

        public void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
        }

        public void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            LogCommandComplete(command, interceptionContext);
        }

        public void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
        }

        public void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            LogCommandComplete(command, interceptionContext);
        }

        private void LogCommandComplete<TResult>(DbCommand command, DbCommandInterceptionContext<TResult> interceptionContext)
        {
            if (interceptionContext.Exception == null)
            {
                logger.Trace("Command completed with result {0}", interceptionContext.Result);
                logger.Trace(command.CommandText);
            }
            else
            {
                logger.WarnException("Command failed", interceptionContext.Exception);
                logger.Trace(command.CommandText);
            }
        }
    }
}
