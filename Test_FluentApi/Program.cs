using Test_FluentApi;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        //the drawback here is we can skip calling functions
        //and we can call them without respecting the order of execution
        var connection = new SimpleFluentSqlHelper()
            .ForServer("localhost")
            .AndDatabase("myDb")
            .AsUser("myUser")
            .AndPassword("Password")
            .Connect();

        //bad call, this will error
        var badConnection = new SimpleFluentSqlHelper()
            .AndPassword("Password")
            .AsUser("myUser")
            //.ForServer("localhost")
            .AndDatabase("myDb")
            .Connect();


        //better implementation
        var betterConnection = BetterFluentSqlHelper
            .CreateConnection()
            .ForServer("localhost")
            .ForDatabase("myDb")
            .ForUser("myUser")
            .ForPassword("Password")
            .Connect();

        //BONUS
        //same better implementation with optional configs
        var betterConnectionWithConfigs = BetterFluentSqlHelper
            .CreateConnectionWithConfigs(config =>
            {
                config = new ConnectionConfiguration() { Configuration = "abcd" };
            })
            .ForServer("localhost")
            .ForDatabase("myDb")
            .ForUser("myUser")
            .ForPassword("Password")
            .Connect();
    }
}