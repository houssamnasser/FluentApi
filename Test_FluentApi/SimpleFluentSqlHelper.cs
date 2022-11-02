using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_FluentApi
{
    public class SimpleFluentSqlHelper
    {
        private string _server;
        private string _database;
        private string _user;
        private string _password;

        public SimpleFluentSqlHelper ForServer(string server)
        {
            _server = server;
            return this;
        }

        public SimpleFluentSqlHelper AndDatabase(string database)
        {
            _database = database;
            return this;
        }

        public SimpleFluentSqlHelper AsUser(string user)
        {
            _user = user;
            return this;
        }

        public SimpleFluentSqlHelper AndPassword(string password)
        {
            _password = password;
            return this;
        }

        public SqlConnection Connect()
        {
            var connection = new SqlConnection($"Server={_server};Database={_database};User Id={_user};Password={_password}");
            connection.Open();
            return connection;
        }

    }
}
