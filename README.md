# Welcome to Elosnoc
Elosnoc is a CLI (command line interface) tools that basically can call public methods inside its project.

Basic elosnoc command format is : [Method Name] [Space] [Parameter1] [Parameter2]
you can have methods without parameters or have more than 1 parameters.

Example :
connect sqlserver . CustomerDB

example above shows how to call method called "connect" : public static void connect(string DbType, string ServerName, string DatabaseName){}.
