using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using FlyLUCK.ViewModels;
using Xamarin.Forms;
using System.Linq;

namespace FlyLUCK
{
	public partial class SendMessage : ContentPage
	{
		private List<Passenger> _passengers;
		private CalendarViewModel vm { get; set; }
		private string _tags = "";

		public SendMessage()
		{
            InitializeComponent();
			vm = new CalendarViewModel();
			BindingContext = vm;
		}

		public SendMessage(List<Passenger> passengers) : this()
		{
			_passengers = passengers;
			//vm.IsLoading = false;
			string[] tagList = _passengers.Select(x => x.EMAIL).ToArray();
			_tags = String.Join(",", tagList);
		}

		void ClosePage(object sender, EventArgs e)
		{
			this.Navigation.PopModalAsync();
		}

		async void Send(object sender, EventArgs e)
		{
			ActivityView av = new ActivityView();
			await this.Navigation.PushModalAsync(av);
			bool success = await SendRequest();
			await this.Navigation.PopModalAsync();
			if (success)
			{
				await DisplayAlert("Success!", "Your push notification was sent.", "OK");
				await this.Navigation.PopModalAsync();
			}
			else
			{
				await DisplayAlert("Uh oh....", "There was an error processing your request! Please try again.", "OK");
			}
		}

		private async Task<bool> SendRequest()
		{
			try
			{
				Uri url;
				//call REST service to get crew info
				HttpClient client = new HttpClient();
				url = new Uri(Constants.ServiceUrl + "/sendmessage?message=" + messageBody.Text + "&tags=" + _tags);

				var result = client.GetAsync(url).Result;
				if (result.IsSuccessStatusCode)
				{
					return true;
				}
			}
			catch (Exception ex)
			{
				await DisplayAlert("ERROR!", ex.ToString(), "Close");
				return false;
			}
			return false;
		}


	}
}
