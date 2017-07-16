using System;
using System.Collections.Generic;
using Plugin.Messaging;
using Xamarin.Forms;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FlyLUCK.ViewModels;
using System.Collections.ObjectModel;

namespace FlyLUCK
{
	public partial class FlightList : ContentPage
	{
		private ObservableCollection<CardView> cvList;
		private string _authheader = "";
		private string paxID = Helpers.Settings.PaxID;

		private CalendarViewModel vm { get; set; }

		public FlightList()
		{
			InitializeComponent();

			vm = new CalendarViewModel();
			BindingContext = vm;

			LoadFlightList();


			//Button bar ******************************************************

			Button submitFlightRequest = new Button();

			Button closePage = new Button { Image = "closePage.png" };
			closePage.BackgroundColor = Color.Transparent;

			closePage.Clicked += ClosePage_Clicked;

			buttonbar.Children.Add(closePage, 0, 0);

		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
		}

		private async void LoadFlightList()
		{
			StackLayout sl = new StackLayout { HorizontalOptions = LayoutOptions.FillAndExpand };
			vm.IsLoading = true;
			var myFlightData = await GetFlights();
			if (myFlightData.Length > 3)
			{
				var myFlightObj = JsonConvert.DeserializeObject<List<Flight>>(myFlightData);
				foreach (Flight f in myFlightObj)
				{
					DateTime depart = Convert.ToDateTime(f.LOCALLEAVE);
					CardView cv = new CardView(f.FROMCITY, f.TOCITY, f.FROMSTATE, f.TOSTATE, depart.ToString("d"), depart.ToString("t"));
					var tapped = new TapGestureRecognizer();

					tapped.Tapped += (sender, e) =>
					{
						OnTapped(sender, e);
					};
					tapped.CommandParameter = f;
					cv.GestureRecognizers.Add(tapped);
					layout.Children.Add(cv);

					//layout.Children.Add(new Label { Text = f.DEST });
				}

				//flightList.Content = sl;

			}
			else
			{
				Label lblNoFlights = new Label { Text = "No flights found.", HorizontalTextAlignment=TextAlignment.Center, VerticalOptions=LayoutOptions.CenterAndExpand };
				layout.Children.Add(lblNoFlights);
			}
			vm.IsLoading = false;
			flightList.IsClippedToBounds = true;

		}

		private async Task<string> GetFlights()
		{
			HttpClient client = new HttpClient();
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _authheader);
			client.MaxResponseContentBufferSize = 512000;

			try
			{
				//call the appropriate REST api to get the list of flights based on audience: crew, admin, passenger (paxID)
				Uri myflightsUrl;
				if(Helpers.Settings.FlightCrew == true)
					myflightsUrl = new Uri(String.Format(Constants.ServiceUrl + "/getcrewflights/" + paxID, string.Empty));
				else if(Helpers.Settings.IsAdmin == true)
					myflightsUrl = new Uri(String.Format(Constants.ServiceUrl + "/getflights?start=" + DateTime.Now + "&end=" + DateTime.Now.AddDays(7), string.Empty));
				else
					myflightsUrl = new Uri(String.Format(Constants.ServiceUrl + "/getflights/" + paxID, string.Empty));

				return await client.GetStringAsync(myflightsUrl);

			}
			catch (Exception ex)
			{
				return ex.ToString();
			}

		}

		//open the flight detail view and pass in the associated flight object
		async void OnTapped(Object sender, EventArgs e)
		{
			Flight flt = (Flight)((TappedEventArgs)e).Parameter;
			await Navigation.PushModalAsync(new FlightDetail(flt));
		}

		private void ClosePage_Clicked(object sender, EventArgs e)
		{
			this.Navigation.PopModalAsync();
		}

	}
}

