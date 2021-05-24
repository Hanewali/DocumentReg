using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Transactions;
using Dapper;
using Dapper.Contrib.Extensions;
using DocumentRegistry.Api.Infrastructure;
using IsolationLevel = System.Transactions.IsolationLevel;

namespace DocumentRegistry.Api.Helpers
{
    public class DatabaseHelper
    {
        #region ExecProcedure

        public static IEnumerable<T> ExecProcedure<T>(string procedureName)
        {
            return ExecProcedure<T>(procedureName, null);
        }

        public static IEnumerable<T> ExecProcedure<T>(string procedureName, DynamicParameters parameters)
        {
            using var transaction = GetTransactionScope();
            using var sqlConnection = new SqlConnection(Configuration.Database.ConnectionString);

            sqlConnection.Open();
            var results = sqlConnection.Query<T>(procedureName, parameters, commandType: CommandType.StoredProcedure);
            transaction.Complete();

            return results;
        }

        public static IEnumerable<T> ExecProcedure<T>(DatabaseContext context)
        {
            using var transaction = GetTransactionScope(context.IsolationLevel, context.Timeout);
            using var sqlConnection = new SqlConnection(Configuration.Database.ConnectionString);

            sqlConnection.Open();
            var results =  sqlConnection.Query<T>(context.SqlCommand, context.Parameters, commandType: CommandType.StoredProcedure);
            transaction.Complete();

            return results;
        }

        #endregion

        #region QueryFirst

        public static T QueryFirst<T>(string sqlSyntax)
        {
            return QueryFirst<T>(sqlSyntax, null);
        }

        public static T QueryFirst<T>(string sqlSyntax, DynamicParameters parameters)
        {
            using var transaction = GetTransactionScope(); 
            using var sqlConnection = new SqlConnection(Configuration.Database.ConnectionString);

            sqlConnection.Open();
            var result = sqlConnection.QueryFirst<T>(sqlSyntax, parameters, commandType: CommandType.Text);
            transaction.Complete();

            return result;
        }

        public static T QueryFirst<T>(DatabaseContext context)
        {
            using var transaction = GetTransactionScope(context.IsolationLevel, context.Timeout);
            using var sqlConnection = new SqlConnection(Configuration.Database.ConnectionString);

            sqlConnection.Open();
            var result = sqlConnection.QueryFirst<T>(context.SqlCommand, context.Parameters, commandType: CommandType.Text);
            transaction.Complete();

            return result;
        }

        #endregion
        
        #region ExecuteNoResult

        public static Task<int> ExecuteNoResult(string sqlSyntax)
        {
            return ExecuteNoResult(sqlSyntax, null);
        }

        public static Task<int> ExecuteNoResult(string sqlSyntax, DynamicParameters parameters)
        {
            return ExecuteNoResult(sqlSyntax, parameters, CommandType.Text);
        }

        public static Task<int> ExecuteNoResult(string sqlSyntax, DynamicParameters parameters, CommandType commandType)
        {
            using var transaction = GetTransactionScope();
            using var sqlConnection = new SqlConnection(Configuration.Database.ConnectionString);

            sqlConnection.OpenAsync();
            var result = sqlConnection.ExecuteAsync(sqlSyntax, parameters, commandType: commandType);
            transaction.Complete();

            return result;
        }

        public static Task<int> ExecuteNoResult(DatabaseContext context)
        {
            using var transaction = GetTransactionScope(context.IsolationLevel, context.Timeout);
            using var sqlConnection = new SqlConnection(Configuration.Database.ConnectionString);

            sqlConnection.OpenAsync();
            var result = sqlConnection.ExecuteAsync(context.SqlCommand, context.Parameters, commandType: context.CommandType);
            transaction.Complete();

            return result;
        }

        #endregion

        #region GetValue

        public static Task<T> GetValue<T>(string sqlSyntax)
        {
            return GetValue<T>(sqlSyntax, null);
        }

        public static Task<T> GetValue<T>(string sqlSyntax, DynamicParameters parameters)
        {
            return GetValue<T>(sqlSyntax, parameters, CommandType.Text);
        }

        public static Task<T> GetValue<T>(string sqlSyntax, DynamicParameters parameters, CommandType commandType)
        {
            using var transaction = GetTransactionScope(); 
            using var sqlConnection = new SqlConnection(Configuration.Database.ConnectionString);

            sqlConnection.OpenAsync();
            var result = sqlConnection.ExecuteScalarAsync<T>(sqlSyntax, parameters, commandType: commandType);
            transaction.Complete();

            return result;
        }

        public static Task<T> GetValue<T>(DatabaseContext context)
        {
            using var transaction = GetTransactionScope(context.IsolationLevel, context.Timeout);
            using var sqlConnection = new SqlConnection(Configuration.Database.ConnectionString);

            sqlConnection.OpenAsync();
            var result = sqlConnection.ExecuteScalarAsync<T>(context.SqlCommand, context.Parameters, commandType: context.CommandType);
            transaction.Complete();

            return result;
        }

        #endregion

        #region Execute

        public static Task<int> Execute(DatabaseContext context)
        {
            using var transaction = GetTransactionScope(context.IsolationLevel, context.Timeout);
            using var sqlConnection = new SqlConnection(Configuration.Database.ConnectionString);

            sqlConnection.OpenAsync();
            var results = sqlConnection.ExecuteAsync(context.SqlCommand, context.Parameters);
            transaction.Complete();

            return results;
        }

        #endregion

        #region Insert 

        public static Task<int> Insert<T>(T objectToInsert) where T : class
        {
            using var transaction = GetTransactionScope();
            using var sqlConnection = new SqlConnection(Configuration.Database.ConnectionString);

            sqlConnection.OpenAsync();
            var result = sqlConnection.InsertAsync(objectToInsert);
            transaction.Complete();

            return result;
        }

        #endregion

        #region PrivateMethods
        private static TransactionScope GetTransactionScope(IsolationLevel isolationLevel, TimeSpan timeout)
        {
            return new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions
            {
                IsolationLevel = isolationLevel,
                Timeout = timeout
            });
        }

        private static TransactionScope GetTransactionScope(IsolationLevel isolationLevel)
        {
            return GetTransactionScope(isolationLevel, Configuration.Database.DefaultTimeout);
        }

        private static TransactionScope GetTransactionScope()
        {
            return GetTransactionScope(Configuration.Database.DefaultIsolationLevel, Configuration.Database.DefaultTimeout);
        }
        
        #endregion
    }
}