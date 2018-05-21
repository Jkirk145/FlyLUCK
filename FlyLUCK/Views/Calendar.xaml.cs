using System;
using System.Collections.Generic;
using Xamarin.Forms;
using XamForms.Controls;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FlyLUCK.ViewModels;

namespace FlyLUCK
{
	
	public partial class Calendar : ContentPage
	{
		
		private string _flightdata = null;
		private string _holddata = null;
		private string _mxdata = null;
		private string _authdata = "";
		private string _authheader = "";
		private List<string> _loadedMonths = null;
		private List<Flight> flights = null;

		Label selectedDate = new Label();

		XamForms.Controls.Calendar calendar;

		private void ClosePage_Clicked(object sender, EventArgs e)
		{
			//col.Clear();
			_loadedMonths.Clear();
			//col = null;
			this.Navigation.PopModalAsync();
		}

		private CalendarViewModel vm { get; set; }

		public Calendar()
		{
			InitializeComponent();
			vm = new CalendarViewModel();
			BindingContext = vm;

			_authdata = string.Format("{0}:{1}", Constants.Username, Constants.Password);
			_authheader = Convert.ToBase64String(Encoding.UTF8.GetBytes(_authdata));

			_loadedMonths = new List<string>();

			calendar = new XamForms.Controls.Calendar();
			calendar.DateClicked += DateSelected;
			calendar.RightArrowClicked += MonthChanged;
			calendar.LeftArrowClicked += MonthChanged;

			LoadFlights(DateTime.Now);
			_loadedMonths.Add(DateTime.Now.Month.ToString());



			//Button bar ******************************************************


			Button newFlightRequest = new Button();
			Button closePage = new Button { Image = "closePage.png" };
			closePage.BackgroundColor = Color.Transparent;
			newFlightRequest.BackgroundColor = Color.Transparent;

			switch (Device.RuntimePlatform)
			{
				case Device.Android:
					newFlightRequest.Image = "newflight35.png";

					newFlightRequest.HeightRequest = 35;
					newFlightRequest.WidthRequest = 35;

					closePage.HeightRequest = 35;
					closePage.WidthRequest = 35;

					closePage.BorderColor = Color.Transparent;
					newFlightRequest.BorderColor = Color.Transparent;

					break;
				case Device.iOS:
					newFlightRequest.Image = "submit.png";
					break;
			}


			newFlightRequest.Clicked += SendFlightRequest;
			closePage.Clicked += ClosePage_Clicked;

			layout.Children.Add(calendar);
			buttonbar.Children.Add(closePage, 0, 0);
			buttonbar.Children.Add(newFlightRequest, 1, 0);

		}



		protected override void OnAppearing()
		{
			base.OnAppearing();
			//LoadFlights(DateTime.Now);
			//_loadedMonths.Add(DateTime.Now.Month.ToString());

		}


		private void DateSelected(object sender, EventArgs e)
		{
			DisplayAlert("Aircraft Reserved!", "Flight information is currently not available. Please contact the flight department for detailed information on this flight.", "Close");
		}

		private void MonthChanged(object sender, DateTimeEventArgs e)
		{
			LoadFlights(e.DateTime);
		}

		private void LoadFlights(DateTime currDate)
		{
			vm.IsLoading = true;

			string startDate = currDate.Month.ToString() + "-1-" + currDate.Year.ToString();
			string endDate = currDate.Month.ToString() + "-" + DateTime.DaysInMonth(currDate.Year, currDate.Month).ToString() + "-" + currDate.Year.ToString();

			Task<bool> success = GetData(startDate, endDate);


			//_holddata = GetHoldData(startDate, endDate);
			//_mxdata = GetMxData(startDate, endDate);

			//calendar.DataSource = col;

		}


		public async Task<bool> GetData(string start, string end)
		{
			HttpClient client = new HttpClient();
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _authheader);
			client.MaxResponseContentBufferSize = 512000;
			Uri flightUrl = new Uri(String.Format(Constants.ServiceUrl + "/getflights?start=" + start + "&end=" + end, string.Empty));
			Uri holdUrl = new Uri(String.Format(Constants.ServiceUrl + "/getholds?start=" + start + "&end=" + end, string.Empty));
			Uri mxUrl = new Uri(String.Format(Constants.ServiceUrl + "/getmx?start=" + start + "&end=" + end, string.Empty));

			try
			{
				HttpContent responseContent;
				//get all the flights for the given dates
				var response = await client.GetAsync(flightUrl);
				if (response.IsSuccessStatusCode)
				{
					responseContent = response.Content;
					_flightdata = responseContent.ReadAsStringAsync().Result;
					flights = JsonConvert.DeserializeObject<List<Flight>>(_flightdata);

					string currDate = "";
					string prevDate = "";
					foreach (Flight f in flights)
					{
						currDate = f.LOCALLEAVE;
						if (currDate != prevDate)
						{
							calendar.SpecialDates.Add(new SpecialDate(Convert.ToDateTime(f.LOCALLEAVE)){ BackgroundColor = Color.FromHex("42a1ff"), Selectable=true });

							prevDate = currDate;
						}
					}

				}
				//next, get holds
				response = await client.GetAsync(holdUrl);
				if (response.IsSuccessStatusCode)
				{
					responseContent = response.Content;
					_holddata = responseContent.ReadAsStringAsync().Result;
					var holdobj = JsonConvert.DeserializeObject<List<Hold>>(_holddata);

					foreach (Hold h in holdobj)
					{
						calendar.SpecialDates.Add(new SpecialDate(Convert.ToDateTime(h.LEGLOCALDATE)){ BackgroundColor = Color.SpringGreen});
					}
				}
				response = await client.GetAsync(mxUrl);
				if (response.IsSuccessStatusCode)
				{
					responseContent = response.Content;
					_mxdata = responseContent.ReadAsStringAsync().Result;
					var mxobj = JsonConvert.DeserializeObject<List<Maint>>(_mxdata);
					foreach (Maint m in mxobj)
					{
						calendar.SpecialDates.Add(new SpecialDate(Convert.ToDateTime(m.LEGLOCALDATE)){ BackgroundColor = Color.OrangeRed });
					}
				}

				calendar.RaiseSpecialDatesChanged();
				vm.IsLoading = false;
				return true;
			}
			catch (Exception e)
			{
				Page p = new Page();

				await p.DisplayAlert("ERROR!", e.ToString(), "Close");
				return false;
			}
		}


		public void SendFlightRequest(object sender, EventArgs e)
		{
			FlightRequestPage frp = new FlightRequestPage();
			this.Navigation.PushModalAsync(frp);

		}
	}
}
