using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using XLabs.Forms;
using XLabs.Forms.Controls;
using RoundedBoxView.Forms.Plugin.iOSUnified;
using Syncfusion.SfCalendar.iOS;
using Syncfusion.SfCalendar.XForms.iOS;
using WindowsAzure.Messaging;
using UserNotifications;

namespace FlyLUCK.iOS
{


	[Register("AppDelegate")]
	public partial class AppDelegate : XFormsApplicationDelegate
	{

		private SBNotificationHub Hub { get; set; }

		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{

			global::Xamarin.Forms.Forms.Init();
			Xamarin.FormsMaps.Init();

			new SfCalendarRenderer();

			RoundedBoxViewRenderer.Init();


			// Request notification permissions from the user
			UNUserNotificationCenter.Current.RequestAuthorization(UNAuthorizationOptions.Alert, (approved, err) =>
			{
				// Handle approval
			});

			UIApplication.SharedApplication.RegisterForRemoteNotifications();

			LoadApplication(new App());


			return base.FinishedLaunching(app, options);
		}




		public override void RegisteredForRemoteNotifications(
			UIApplication application, NSData deviceToken)
		{
			// Get current device token
			var DeviceToken = deviceToken.Description;
			if (!string.IsNullOrWhiteSpace(DeviceToken))
			{
				DeviceToken = DeviceToken.Trim('<').Trim('>');
			}

			// Get previous device token
			var oldDeviceToken = NSUserDefaults.StandardUserDefaults.StringForKey("PushDeviceToken");

			// Has the token changed?
			if (string.IsNullOrEmpty(oldDeviceToken) || !oldDeviceToken.Equals(DeviceToken))
			{
				//TODO: Put your own logic here to notify your server that the device token has changed/been created!
			}

			// Save new device token 
			NSUserDefaults.StandardUserDefaults.SetString(DeviceToken, "PushDeviceToken");
		}

	}
}

