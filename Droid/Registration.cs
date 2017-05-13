using System;
using Android.Util;
using Gcm.Client;

[assembly: Xamarin.Forms.Dependency(typeof(FlyLUCK.Droid.Registration))]
namespace FlyLUCK.Droid
{
	public class Registration : IRegistration
	{
		public Registration()
		{
		}

		public void Register()
		{
			RegisterWithGCM();
		}

		private void RegisterWithGCM()
		{
			// Check to ensure everything's set up right
			GcmClient.CheckDevice(Android.App.Application.Context);
			GcmClient.CheckManifest(Android.App.Application.Context);

			// Register for push notifications
			Log.Info("MainActivity", "Registering...");
			GcmClient.Register(Android.App.Application.Context, Constants.SenderID);
		}

	}
}
