using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.NetworkInformation;

namespace Elosnoc
{
	class net
	{
		static char[] dict = {'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p',	'q','r','s','t','u','v','w','x','y','z','A','B','C','D','E','F','G','H','I','J','C','L','M','N','O','P','Q','R','S','T','U','V','X','Y','Z','0', '1', '2', '3', '4', '5', '6', '7', '8','9','!','"','#','$','%','&','(',')','*','+',',','-','.','/',':',';','<','=','>','?','@','[', ']','^','_','`','{','|','}','~'};

		public static void getkey(int length, string mkey)
		{
			string pwd1 = "", pwd2 = "", pwd3 = "";

			Random rnd = new Random();
			
			while (pwd1 != mkey && pwd2 != mkey && pwd3 != mkey)
			{
				pwd1=""; pwd2=""; pwd3 ="";
				Parallel.For(0, length, i =>
				{
					pwd1 += dict[rnd.Next(0, dict.Length)];
					pwd2 += dict[rnd.Next(0, dict.Length)];
					pwd3 += dict[rnd.Next(0, dict.Length)];
				});
				
				Console.Write("\r{0} {1} {2}", pwd1, pwd2, pwd3);
				if (pwd1 == mkey)
				{
					Console.WriteLine("\npassword is : " + pwd1);
					return;
				}
				else if (pwd2 == mkey)
				{
					Console.WriteLine("\npassword is : " + pwd2);
					return;
				}
				else if (pwd3 == mkey)
				{
					Console.WriteLine("\npassword is : " + pwd3);
					return;
				}
			}
		}

		public static void ping(string destinantion)
		{
			Ping pinger = new Ping();
			try
			{
				Console.Write("pinging [" + destinantion + "] with 32 bytes of data:\n");
				for(int i =0;i<9;i++)
				{
					PingReply reply = pinger.Send(destinantion,32);
					Console.Write("Reply from " + reply.Address + ": " + reply.Status + ", time=" + reply.RoundtripTime + "ms\n");
				}
			} catch{}
		}

		public static void config()
		{
			NetworkInterface[] adapters  = NetworkInterface.GetAllNetworkInterfaces();
			
			Console.WriteLine("Adapter \tSpeed \tDescription");

			foreach(NetworkInterface adapter in adapters)
			{
				Console.WriteLine(adapter.Name + "\t" + adapter.Speed + "\t" + adapter.Description);
			}
		}

	}
}
