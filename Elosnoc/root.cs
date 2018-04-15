using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Elosnoc
{
	public class root
	{
		static bool greet = false;
		
		public static Type CurrentType;

		static internal void Main()
		{
			if (greet == false)
			{
				Console.ForegroundColor= ConsoleColor.Cyan;
				Console.Write("Welcome to Elosnoc \n==================\n");
				greet = true;
				CurrentType = Type.GetType("Elosnoc.root");
				Console.Title = "Elosnoc";
			}
			
			Console.Write("elosnoc> ");

			string cmd = Console.ReadLine();
			var myRoot = new root();
			myRoot.docmd(cmd);
		}

		void getClass(string MethodName)
		{
			IEnumerable<Type> classes = Assembly.GetExecutingAssembly().GetTypes();
			foreach(Type cls in classes.Where(t => t.Namespace == "Elosnoc"))
			{
				if(cls.GetMethod(MethodName) != null)
				{
					CurrentType = cls;
					return;
				}
			}
		}
		
		void docmd(string cmd)
		{
			if (CheckGlobalFunction(cmd)) {return;}
			try
			{
				if(cmd.Contains(" ") == true)
				{
					var MyParams = cmd.Split('"')
										 .Select((element, index) => index % 2 == 0 
																					 ? element.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries) 
																					 : new string[] { element }) 
										 .SelectMany(element => element).ToList();

					getClass(MyParams[0]);
					MethodInfo mi = CurrentType.GetMethod(MyParams[0]);
					ParameterInfo[] param = mi.GetParameters();
					
					object[] pArray = new object[param.Count()];
					int paramIndex = 1;

					foreach (var methodArg in param)
					{
						Type paramType = methodArg.ParameterType;
						pArray[methodArg.Position] = Convert.ChangeType(MyParams[paramIndex], paramType);
						paramIndex++;
					}

					if(mi.ReturnType.ToString() == "System.Void")
					{
						mi.Invoke(this, pArray);
					} else {
						object retval = mi.Invoke(this, pArray);
						Console.WriteLine("{0}", retval);
					}
				}
				else
				{
					getClass(cmd);
					MethodInfo mi = CurrentType.GetMethod(cmd);
					object retval = mi.Invoke(this, new object[] { });
					Console.WriteLine("{0}", retval);
				}
				Main();

			}	catch (Exception ex){
				Debug.WriteLine(ex.Message);
				Console.WriteLine("'" + cmd + "' command is not not recognized, type 'help' to show elosnoc commands.");
				Main();
			}		
		}
		
		 public void help()
		{
			string myhelp = File.ReadAllText(Environment.CurrentDirectory + "/help.txt");
			Console.WriteLine(myhelp);
			Main();
		}

		bool CheckGlobalFunction(string cmd)
		{
			if(cmd.ToLower() == "cls" || cmd.ToLower() == "//") {
				Console.Clear();
				Main();
				return true;
			} else if (cmd.ToLower() == "help" || cmd.ToLower() == "?") {
				help();
				return true;
			}else if(cmd.ToLower() == "version" || cmd.ToLower() == "-v") {
				Console.WriteLine(Environment.Version.ToString());
				Main();
				return true;
			} else if(cmd.ToLower() == "exit") {
				Environment.Exit(0);
				return true;
			} else {
				return false;
			}
			
		}

		
	}
}
