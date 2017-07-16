using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Forms;
using System.Threading.Tasks;
using FlyLUCK.ViewModels;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace FlyLUCK
{
	public partial class LoginPage : ContentPage
	{
		private CalendarViewModel vm { get; set; }
		public event EventHandler LoggedIn;

		//used for AD Authentication
		public static string clientId = "5891d5b7-1744-44b1-be7f-173233c6a829";
		public static string authority = "https://login.windows.net/common/";
		public static string returnUri = "http://flyluck";
		private const string apiResourceUri = "https://graph.windows.net";
		private AuthenticationResult authResult = null;

		private void OnLoggedIn()
		{
			if (LoggedIn != null)
				LoggedIn(this, EventArgs.Empty);
		}

		async void AuthenticateAD(object sender, System.EventArgs e)
		{
			vm = new CalendarViewModel();
			BindingContext = vm;

			try
			{
				//await Navigation.PopModalAsync();
				var auth = DependencyService.Get<IAuthenticator>();
				authResult = await auth.Authenticate(authority, apiResourceUri, clientId, returnUri);
				var userName = authResult.UserInfo.GivenName + " " + authResult.UserInfo.FamilyName;
				Helpers.Settings.UserName = userName;


				Helpers.Settings.UserID = authResult.UserInfo.DisplayableId;

				Helpers.Settings.AccessToken = authResult.AccessToken;


				//retrieve user data from BART
				vm.IsLoading = true;
				mainGrid.IsVisible = false;
				HttpClient client = new HttpClient();
				Uri url = new Uri(String.Format(Constants.ServiceUrl + "/getuserdata?email=" + Helpers.Settings.UserID, string.Empty));
				string _userdata = await client.GetStringAsync(url);

				if (_userdata != "[]")
				{
					var userobj = JsonConvert.DeserializeObject<List<User>>(_userdata);
					if (userobj[0].CREWID != null)
					{
						Helpers.Settings.FlightCrew = true;
						Helpers.Settings.PaxID = userobj[0].CREWID;
					}
					else if (userobj[0].CREWID == null && userobj[0].USERID != null)
					{
						Helpers.Settings.IsAdmin = true;
						Helpers.Settings.PaxID = userobj[0].USERID;
					}
					else
						Helpers.Settings.PaxID = userobj[0].PAXID;
					if (authResult.UserInfo.PasswordExpiresOn != null && (authResult.UserInfo.PasswordExpiresOn.Value.DateTime < DateTime.Now.AddDays(30)))
						Helpers.Settings.SessionExpires = authResult.UserInfo.PasswordExpiresOn.Value.DateTime;
					else
						Helpers.Settings.SessionExpires = DateTime.Now.AddDays(30);
                    LoggedIn(this, e);
					await Navigation.PopModalAsync();
				}
				else
				{
					await DisplayAlert("Ooops!", "User name not found. Please try again or contact support for further assistance.", "Close");
					vm.IsLoading = false;
					mainGrid.IsVisible = true;
				}
			}
			catch (Exception ex)
			{
				await DisplayAlert("Ooops!", "An error has occurred: " + ex.ToString(), "Close");
				mainGrid.IsVisible = true;
			}

		}

		/*async void Handle_Clicked(object sender, System.EventArgs e)
		{
			vm = new CalendarViewModel();
			BindingContext = vm;


			if (userID.Text.Length == 0 || userID.Text.Contains("@") == false)
			{
				await DisplayAlert("Oops!", "You must enter a valid email address!", "Close");
				//userID.BackgroundColor = Color.Red;
				return;
			}
			try
			{
				vm.IsLoading = true;
				mainGrid.IsVisible = false;

				//call REST service to get crew info
				HttpClient client = new HttpClient();
				Uri crewUrl = new Uri(String.Format(Constants.ServiceUrl + "/getuserdata?email=" + userID.Text, string.Empty));
				string _userdata = await client.GetStringAsync(crewUrl);
				if (_userdata == "[]")
				{
					await DisplayAlert("Oops!", "Email address not found!", "Close");
					userID.BackgroundColor = Color.Red;
					return;
				}
				var userobj = JsonConvert.DeserializeObject<List<User>>(_userdata);
				string fullName = userobj[0].NAME;
				var names = fullName.Split(',');
				Helpers.Settings.UserID = userobj[0].EMAIL;
				Helpers.Settings.UserName = names[1] + " " + names[0];

				if (userobj[0].DEPT == "525")
				{
					Helpers.Settings.FlightCrew = true;
					Helpers.Settings.PaxID = userobj[0].CREWID;
				}
				else
					Helpers.Settings.PaxID = userobj[0].PAXID;
				LoggedIn(this, e);
				await Navigation.PopModalAsync();

			}
			catch (AggregateException ae) when (ae.InnerException is HttpRequestException)
			{
				
					await DisplayAlert("Oops", "There was a problem with your network connection. Please verify your settings and try again.", "Close");
					System.Diagnostics.Debug.WriteLine(ae.ToString());
				vm.IsLoading = false;
			}
			vm.IsLoading = false;

		}*/

		public LoginPage()
		{
			InitializeComponent();
		}
	}
}
