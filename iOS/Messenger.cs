using System;
using UIKit;
using FlyLUCK.iOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(Messenger))]

namespace FlyLUCK.iOS
{
	public class Messenger : IMessenger
	{
		public Messenger()
		{
		}

		public bool SendMessage(string to, string msg)
		{
			return UIApplication.SharedApplication.OpenUrl(new Foundation.NSUrl("sms:18042480700"));
		}
	}
}
