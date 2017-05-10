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

		void Send(object sender, EventArgs e)
		{
			
			//call REST service to get crew info
			HttpClient client = new HttpClient();
			Uri url = new Uri(Constants.ServiceUrl + "/sendmessage?message={\"aps\":{\"alert\":\"" + messageBody.Text + "\"}}&tags=" + _tags);
			var result = client.GetStringAsync(url);
		}


	}
}
