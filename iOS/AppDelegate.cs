using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using XLabs.Forms;
using XLabs.Forms.Controls;
using RoundedBoxView.Forms.Plugin.iOSUnified;


namespace FlyLUCK.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : XFormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{

			global::Xamarin.Forms.Forms.Init();
			Xamarin.FormsMaps.Init();

			RoundedBoxViewRenderer.Init();

			LoadApplication(new App());


			return base.FinishedLaunching(app, options);
		}
	}
}

