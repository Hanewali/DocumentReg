﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using Dapper;
using Dapper.Contrib.Extensions;
using DocumentRegistry.Infrastructure;
using IsolationLevel = System.Transactions.IsolationLevel;

namespace DocumentRegistry.Helpers
{
    public class DatabaseHelper : IDatabaseHelper
    {
        #region ExecProcedure

        public IEnumerable<T> ExecProcedure<T>(string procedureName)
        {
            return ExecProcedure<T>(procedureName, null);
        }

        public IEnumerable<T> ExecProcedure<T>(string procedureName, DynamicParameters parameters)
        {
            using var transaction = GetTransactionScope();
            using var sqlConnection = new SqlConnection(Configuration.Database.ConnectionString);

            sqlConnection.Open();
            var results = sqlConnection.Query<T>(procedureName, parameters, commandType: CommandType.StoredProcedure);
            transaction.Complete();

            return results.ToList();
        }

        public IEnumerable<T> ExecProcedure<T>(DatabaseContext context)
        {
            using var transaction = GetTransactionScope(context.IsolationLevel, context.Timeout);
            using var sqlConnection = new SqlConnection(Configuration.Database.ConnectionString);

            sqlConnection.Open();
            var results = sqlConnection.Query<T>(context.SqlCommand, context.Parameters, commandType: CommandType.StoredProcedure);
            transaction.Complete();

            return results.ToList();
        }

        #endregion

        #region Query

        public IEnumerable<T> Query<T>(string sqlSyntax)
        {
            return Query<T>(sqlSyntax, null);
        }

        public IEnumerable<T> Query<T>(string sqlSyntax, DynamicParameters parameters)
        {
            using var transaction = GetTransactionScope();
            using var sqlConnection = new SqlConnection(Configuration.Database.ConnectionString);

            sqlConnection.Open();
            var results = sqlConnection.Query<T>(sqlSyntax, parameters, commandType: CommandType.Text);
            transaction.Complete();

            return results.ToList();
        }

        public IEnumerable<T> Query<T>(DatabaseContext context)
        {
            using var transaction = GetTransactionScope(context.IsolationLevel, context.Timeout);
            using var sqlConnection = new SqlConnection(Configuration.Database.ConnectionString);

            sqlConnection.Open();
            var results = sqlConnection.Query<T>(context.SqlCommand, context.Parameters, commandType: CommandType.Text);
            transaction.Complete();

            return results.ToList();
        }

        #endregion

        #region QueryFirst

        public T QueryFirst<T>(string sqlSyntax)
        {
            return QueryFirst<T>(sqlSyntax, null);
        }

        public T QueryFirst<T>(string sqlSyntax, DynamicParameters parameters)
        {
            using var transaction = GetTransactionScope();
            using var sqlConnection = new SqlConnection(Configuration.Database.ConnectionString);

            sqlConnection.Open();
            var result = sqlConnection.QueryFirst<T>(sqlSyntax, parameters, commandType: CommandType.Text);
            transaction.Complete();

            return result;
        }

        public T QueryFirst<T>(DatabaseContext context)
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

        public int ExecuteNoResult(string sqlSyntax)
        {
            return ExecuteNoResult(sqlSyntax, null);
        }

        public int ExecuteNoResult(string sqlSyntax, DynamicParameters parameters)
        {
            return ExecuteNoResult(sqlSyntax, parameters, CommandType.Text);
        }

        public int ExecuteNoResult(string sqlSyntax, DynamicParameters parameters, CommandType commandType)
        {
            using var transaction = GetTransactionScope();
            using var sqlConnection = new SqlConnection(Configuration.Database.ConnectionString);

            sqlConnection.Open();
            var result = sqlConnection.Execute(sqlSyntax, parameters, commandType: commandType);
            transaction.Complete();

            return result;
        }

        public int ExecuteNoResult(DatabaseContext context)
        {
            using var transaction = GetTransactionScope(context.IsolationLevel, context.Timeout);
            using var sqlConnection = new SqlConnection(Configuration.Database.ConnectionString);

            sqlConnection.Open();
            var result = sqlConnection.Execute(context.SqlCommand, context.Parameters, commandType: context.CommandType);
            transaction.Complete();

            return result;
        }

        #endregion

        #region GetValue

        public T GetValue<T>(string sqlSyntax)
        {
            return GetValue<T>(sqlSyntax, null);
        }

        public T GetValue<T>(string sqlSyntax, DynamicParameters parameters)
        {
            return GetValue<T>(sqlSyntax, parameters, CommandType.Text);
        }

        public T GetValue<T>(string sqlSyntax, DynamicParameters parameters, CommandType commandType)
        {
            using var transaction = GetTransactionScope();
            using var sqlConnection = new SqlConnection(Configuration.Database.ConnectionString);

            sqlConnection.Open();
            var result = sqlConnection.ExecuteScalar<T>(sqlSyntax, parameters, commandType: commandType);
            transaction.Complete();

            return result;
        }

        public T GetValue<T>(DatabaseContext context)
        {
            using var transaction = GetTransactionScope(context.IsolationLevel, context.Timeout);
            using var sqlConnection = new SqlConnection(Configuration.Database.ConnectionString);

            sqlConnection.Open();
            var result = sqlConnection.ExecuteScalar<T>(context.SqlCommand, context.Parameters, commandType: context.CommandType);
            transaction.Complete();

            return result;
        }

        #endregion

        #region Execute

        public int Execute(DatabaseContext context)
        {
            using var transaction = GetTransactionScope(context.IsolationLevel, context.Timeout);
            using var sqlConnection = new SqlConnection(Configuration.Database.ConnectionString);

            sqlConnection.Open();
            var results = sqlConnection.Execute(context.SqlCommand, context.Parameters);
            transaction.Complete();

            return results;
        }

        #endregion

        #region Insert 

        public long Insert<T>(T objectToInsert) where T : class
        {
            using var transaction = GetTransactionScope();
            using var sqlConnection = new SqlConnection(Configuration.Database.ConnectionString);

            sqlConnection.Open();
            var result = sqlConnection.Insert(objectToInsert);
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