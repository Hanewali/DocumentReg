using System;
using System.Data;
using Dapper;
using IsolationLevel = System.Transactions.IsolationLevel;


namespace DocumentRegistry.Api.Infrastructure
{
    public class DatabaseContext
    {
        public string SqlCommand { get; private set; }
        public DynamicParameters Parameters { get; private set; }
        public CommandType CommandType { get; private set; }
        public IsolationLevel IsolationLevel { get; private set; }
        public TimeSpan Timeout { get; set; }

        public DatabaseContext(string sqlCommand, TimeSpan timeout, IsolationLevel isolationLevel, CommandType commandType = CommandType.Text, DynamicParameters parameters = null)
        {
            SqlCommand = sqlCommand;
            CommandType = commandType;
            Parameters = parameters;
            IsolationLevel = isolationLevel;
            Timeout = timeout;
        }
        public DatabaseContext(string sqlCommand, IsolationLevel isolationLevel, CommandType commandType = CommandType.Text, DynamicParameters parameters = null)
            : this(sqlCommand, Configuration.Database.DefaultTimeout, isolationLevel, commandType, parameters)
        {
        }

        public DatabaseContext(string sqlCommand, CommandType commandType = CommandType.Text, DynamicParameters parameters = null)
            : this(sqlCommand, Configuration.Database.DefaultIsolationLevel, commandType, parameters)
        {
        }
    }
}