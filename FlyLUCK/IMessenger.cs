using System;
namespace FlyLUCK
{
	public interface IMessenger
	{
		bool SendMessage(string to, string msg);
	}
}
