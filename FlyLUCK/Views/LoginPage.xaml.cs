using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Forms;
using System.Threading.Tasks;
using FlyLUCK.ViewModels;

namespace FlyLUCK
{
	public partial class LoginPage : ContentPage
	{
		private CalendarViewModel vm { get; set; }
		public event EventHandler LoggedIn;

		private void OnLoggedIn()
		{
			if (LoggedIn != null)
				LoggedIn(this, EventArgs.Empty);
		}


		async void Handle_Clicked(object sender, System.EventArgs e)
		{
			vm = new CalendarViewModel();
			BindingContext = vm;


			if (userID.Text.Length == 0 || userID.Text.Contains("@") == false)
			{
				await DisplayAlert("Oops!", "You must enter a valid email address!", "Close");
				userID.BackgroundColor = Color.Red;
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
		}

		public LoginPage()
		{
			InitializeComponent();
		}
	}
}
