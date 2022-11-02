using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_FluentApi
{
    public class BetterFluentSqlHelper :
        IServerSelectionStage,
        IDbSelectionStage,
        IUserSelectionStage,
        IPasswordSelectionStage,
        IConnectionInitializerStage
    {
        private string _server;
        private string _database;
        private string _user;
        private string _password;

        private BetterFluentSqlHelper() { }

        public static IServerSelectionStage CreateConnection()
        {
            return new BetterFluentSqlHelper();
        }

        //optional, can be deleted
        public static IServerSelectionStage CreateConnectionWithConfigs(Action<ConnectionConfiguration> configs)
        {
            var configuration = new ConnectionConfiguration();
            
            //invoke will replace map the values of configs to configuration object
            //(must have identical params name in order to map them)
            configs?.Invoke(configuration);

            return new BetterFluentSqlHelper();
        }

        public IDbSelectionStage ForServer(string server)
        {
            _server = server;
            return this;
        }

        public IUserSelectionStage ForDatabase(string database)
        {
            _database = database;
            return this;
        }

        public IPasswordSelectionStage ForUser(string user)
        {
            _user = user;
            return this;
        }

        public IConnectionInitializerStage ForPassword(string password)
        {
            _password = password;
            return this;
        }

        public IDbConnection Connect()
        {
            var connection = new SqlConnection($"Server={_server};Database={_database};User Id={_user};Password={_password}");
            connection.Open();
            return connection;
        }
    }

    public interface IServerSelectionStage
    {
        public IDbSelectionStage ForServer(string server);
    }

    public interface IDbSelectionStage
    {
        public IUserSelectionStage ForDatabase(string database);
    }

    public interface IUserSelectionStage
    {
        public IPasswordSelectionStage ForUser(string user);
    }

    public interface IPasswordSelectionStage
    {
        public IConnectionInitializerStage ForPassword(string password);
    }

    public interface IConnectionInitializerStage
    {
        public IDbConnection Connect();
    }

    //optional, can be deleted
    public class ConnectionConfiguration
    {
        public string Configuration { get; set; }
    }
}
