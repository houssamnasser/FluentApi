# FluentApi
## Objectives
- Creating a library which allows the user to configure an object in a seamless way
- Isolate the specific implementation of the library so then the user can't access it directly

To demonstrate this, we'll create an SQL DB connection class, which provides the ability to configure and return an SQL connection instance.

## Why do we need it ?
- It's used in almost all libraries to guide the users to setup and configure objects in a seamless way.
- It also hides the implementation of the library by exposing some external functions.
- It uses the Builder pattern

## Example
var connection = new SimpleFluentSqlConnection()
.ForServer("localhost")
.AndDatabase("myDb")
.AsUser("myUser")
.AndPassword("Password")
.Connect();

## Drawbacks of wrong implementations
- It allows the user to call the functions randomly and regardless of the order of execution (it doesn't guide the user)
- The user can skip calling required functions
- The user has access to internal parameters and can instantiate them by creating a new object

## Best practice
- Create interfaces and so we achieve isolation
- Each interface define a function which depends on the next interface return type, example below:

In order to create a SQL object instance, we need to follow this schema in order:
1) Define the server name
2) Define the DB name
2) Define the user
3) Define the password
4) Call the Connect function

To call these functions by order, we need to create interfaces like below:

public interface IServerSelectionStage {
  public IDbSelectionStage ForServer(string server);
}

public interface IDbSelectionStage {
  public IUserSelectionStage ForDatabase(string database);
}

public interface IUserSelectionStage {
  public IPasswordSelectionStage ForUser(string user);
}

public interface IPasswordSelectionStage {
  public IConnectionInitializerStage ForPassword(string password);
}

public interface IConnectionInitializerStage {
  public IDbConnection Connect();
}

As we see above, each function return type, depends on the next function return type. For example when we call the CreateConnection, it will return IServerSelectionStage, which contains the ForServer function only !! and then when we call the ForServer it will return IDbSelectionStage, which only contains a function ForDatabase....
So then, we can't call the IServerSelectionStage without calling the IDbSelectionStage and so on...
