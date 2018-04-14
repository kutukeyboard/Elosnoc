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
			} catch(Exception ex){
				Console.WriteLine(ex.Message);
				return;
			}
		}
		
	}
}
