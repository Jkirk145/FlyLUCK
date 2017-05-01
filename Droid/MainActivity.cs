using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace FlyLUCK.Droid
{
	[Activity(Label = "FlyLUCK.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);
			AuthenticationAgentContinuationHelper.SetAuthenticationAgentContinuationEventArgs(requestCode, resultCode, data);   
		}

		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			Xamarin.Forms.Forms.Init(this, bundle);
			Xamarin.FormsMaps.Init(this, bundle);

			LoadApplication(new App());
		}
	}
}

