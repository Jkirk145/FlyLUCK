using System;
using Xamarin.Forms;
using FlyLUCK.Droid;

[assembly: Dependency(typeof(Messenger))]

namespace FlyLUCK.Droid
{
	public class Messenger : IMessenger
	{
		public Messenger()
		{
		}

		public bool SendMessage(string to, string msg)
		{
			return true;
		}
	}
}
