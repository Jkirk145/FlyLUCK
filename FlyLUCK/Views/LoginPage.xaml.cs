using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Forms;


namespace FlyLUCK
{
	public partial class LoginPage : ContentPage
	{
		public event EventHandler LoggedIn;

		private void OnLoggedIn()
		{
			if (LoggedIn != null)
				LoggedIn(this, EventArgs.Empty);
		}


		void Handle_Clicked(object sender, System.EventArgs e)
		{

			if (userID.Text.Length == 0 || userID.Text.Contains("@") == false)
			{
				DisplayAlert("Oops!", "You must enter a valid email address!", "Close");
				userID.BackgroundColor = Color.Red;
				return;
			}

			//call REST service to get crew info
			HttpClient client = new HttpClient();
			Uri crewUrl = new Uri(String.Format(Constants.ServiceUrl + "/getuserdata?email=" + userID.Text, string.Empty));
			string _userdata = client.GetStringAsync(crewUrl).Result;
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
			Navigation.PopModalAsync();
		}

		public LoginPage()
		{
			InitializeComponent();
		}
	}
}
