using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace Elosnoc
{
	class db
	{
		static string myConnectionString="";
		static string myDb = "";
		public static string getconnectionstring()
		{
			return myConnectionString;
		}

		public static string connect(string myDbType, string myServer, string myDB)
		{
			Console.Write("Please login to database \nUser ID : ");
			string UID = null;
			while (true)
			{
				var key = System.Console.ReadKey(true);
				if (key.Key == ConsoleKey.Enter)
					break;

				if (key.Key == ConsoleKey.Backspace)
				{
					if (UID.Length > 0)
					{
						UID = UID.Remove(UID.Length - 1);
					}
				}
				else
				{
					UID += key.KeyChar;
				}
			}
			Console.Write("\nPassword : ");
			string PWD = null;
			while (true)
			{
				var key = System.Console.ReadKey(true);
				if (key.Key == ConsoleKey.Enter)
					break;

				if (key.Key == ConsoleKey.Backspace)
				{
					if (PWD.Length > 0)
					{
						PWD = PWD.Remove(PWD.Length - 1);
					}
				}
				else
				{
					PWD += key.KeyChar;
				}
			}
			DbConnection connection;

			if (myDbType.ToLower() == "sqlserver")
			{
				myConnectionString = "Server=" + myServer + ";Database=" + myDB + ";User Id=" + UID + ";Password=" + PWD + ";";
				connection = new SqlConnection(myConnectionString);
				 
			} else {
				myConnectionString = "Server=" + myServer + ";Database=" + myDB + ";Uid=" + UID + ";Pwd=" + PWD + ";";
				connection = new MySqlConnection(myConnectionString);
			}

			try
			{
				connection.Open();
				connection.Close();
				myDb = myDbType;
				return "Connected to " + UID + "@" + myServer + "." + myDB;
			} catch (Exception ex){
				connection.Close();
				return(ex.Message);
			}
			
		}
		
		static void ReadData(string myDbType,string myQuery)
		{
			DbConnection connection;
			DbCommand command;
			if (myDbType.ToLower() == "sqlserver")
			{
				connection = new SqlConnection(myConnectionString);
				command = new SqlCommand();
			}
			else
			{
				connection = new MySqlConnection(myConnectionString);
				command = new MySqlCommand();
			}
			command.Connection = connection;
			command.CommandText = myQuery;

			try
			{
				connection.Open();
				using (command)
				{
					using (DbDataReader reader = command.ExecuteReader())
					{
						string myfield = "";
						for (int i = 0; i < reader.FieldCount; i++)
						{
							myfield += reader.GetName(i).ToString();
							if (i < reader.FieldCount - 1)
								myfield += "\t";
						}

						Console.WriteLine("\n" + myfield);
						while (reader.Read())
						{
							string myResult = "";
							for (int i = 0; i < reader.FieldCount; i++)
							{
								myResult += reader.GetValue(i);
								if (i < reader.FieldCount - 1)
									myResult += "\t";
							}
							Console.WriteLine(myResult);
						}
					}
				}
				connection.Close();
			} catch(Exception ex){
				connection.Close();
				Console.Write(ex.Message);
				return;
			}
			
		}

		static void ExecQuery(string myDbType, string myQuery)
		{
			DbConnection connection;
			DbCommand command;
			if (myDbType.ToLower() == "sqlserver")
			{
				connection = new SqlConnection(myConnectionString);
				command = new SqlCommand();
			}
			else
			{
				connection = new MySqlConnection(myConnectionString);
				command = new MySqlCommand();
			}
			command.Connection = connection;
			command.CommandText = myQuery;

			try
			{
				connection.Open();
				using (command)
				{
					command.ExecuteNonQuery();
				}
				connection.Close();
				Console.Write("Query execution success !");
			}
			catch (Exception ex)
			{
				connection.Close();
				Console.Write(ex.Message);
				return;
			}
		}

		public static void read()
		{
			if(myConnectionString == "")
			{
				Console.Write("No db connection has been made, please run 'connect [server] [database]' command first!");
				return;
			}
			Console.Write("\n" + myDb +"> ");
			string myQuery = Console.ReadLine();
			if(myQuery == "//"){return;}
			ReadData(myDb,myQuery);
			read();
		}

		public static void exec()
		{
			if (myConnectionString == "")
			{
				Console.Write("No db connection has been made, please run 'connect [server] [database]' command first!");
				return;
			}
			Console.Write("\n" + myDb + "> ");
			string myQuery = Console.ReadLine();
			if (myQuery == "//") { return; }
			ExecQuery(myDb, myQuery);
			exec();
		}
		
	}
}
