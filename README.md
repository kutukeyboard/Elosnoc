# Welcome to Elosnoc
Elosnoc is a CLI (command line interface) tools that basically can call public methods inside its project.

Basic elosnoc command format is : [Method Name] [Space] [Parameter1] [Parameter2]
You can have methods without parameters or have more than 1 parameters.

Example :
connect sqlserver . CustomerDB

Example above shows how to call method called "connect" : public static void connect(string DbType, string ServerName, string DatabaseName){}.

To parse argument with space, you can wrap it with double quote, example : file_compare d:/text.txt "c:/program files/text2.txt"
