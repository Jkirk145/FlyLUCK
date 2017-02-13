using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Syncfusion.SfCalendar.XForms;
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


		//private string _flightdata = "[{\n\t\"ORIGIN\": \"KOFP\",\n\t\"LOCALLEAVE\": \"2017-01-01 09:30:00.0\",\n\t\"DEST\": \"MYAM\"\n}, {\n\t\"ORIGIN\": \"MYAM\",\n\t\"LOCALLEAVE\": \"2017-01-01 12:00:00.0\",\n\t\"DEST\": \"KILM\"\n}, {\n\t\"ORIGIN\": \"KILM\",\n\t\"LOCALLEAVE\": \"2017-01-01 14:00:00.0\",\n\t\"DEST\": \"KOFP\"\n}, {\n\t\"ORIGIN\": \"KOFP\",\n\t\"LOCALLEAVE\": \"2017-01-01 08:00:00.0\",\n\t\"DEST\": \"MYAM\"\n}, {\n\t\"ORIGIN\": \"MYAM\",\n\t\"LOCALLEAVE\": \"2017-01-01 10:30:00.0\",\n\t\"DEST\": \"KILM\"\n}, {\n\t\"ORIGIN\": \"KILM\",\n\t\"LOCALLEAVE\": \"2017-01-01 12:45:00.0\",\n\t\"DEST\": \"KOFP\"\n}, {\n\t\"ORIGIN\": \"KOFP\",\n\t\"LOCALLEAVE\": \"2017-01-05 07:00:00.0\",\n\t\"DEST\": \"KJYO\"\n}, {\n\t\"ORIGIN\": \"KJYO\",\n\t\"LOCALLEAVE\": \"2017-01-05 07:45:00.0\",\n\t\"DEST\": \"KMKE\"\n}, {\n\t\"ORIGIN\": \"KMKE\",\n\t\"LOCALLEAVE\": \"2017-01-05 14:00:00.0\",\n\t\"DEST\": \"KJYO\"\n}, {\n\t\"ORIGIN\": \"KJYO\",\n\t\"LOCALLEAVE\": \"2017-01-05 16:45:00.0\",\n\t\"DEST\": \"KOFP\"\n}, {\n\t\"ORIGIN\": \"KOFP\",\n\t\"LOCALLEAVE\": \"2017-01-06 11:00:00.0\",\n\t\"DEST\": \"KHEF\"\n}, {\n\t\"ORIGIN\": \"KHEF\",\n\t\"LOCALLEAVE\": \"2017-01-06 14:30:00.0\",\n\t\"DEST\": \"KOFP\"\n}, {\n\t\"ORIGIN\": \"KOFP\",\n\t\"LOCALLEAVE\": \"2017-01-06 17:00:00.0\",\n\t\"DEST\": \"KGVL\"\n}, {\n\t\"ORIGIN\": \"KGVL\",\n\t\"LOCALLEAVE\": \"2017-01-06 18:30:00.0\",\n\t\"DEST\": \"KOFP\"\n}, {\n\t\"ORIGIN\": \"KOFP\",\n\t\"LOCALLEAVE\": \"2017-01-08 15:30:00.0\",\n\t\"DEST\": \"KGVL\"\n}, {\n\t\"ORIGIN\": \"KGVL\",\n\t\"LOCALLEAVE\": \"2017-01-08 17:00:00.0\",\n\t\"DEST\": \"KOFP\"\n}, {\n\t\"ORIGIN\": \"KOFP\",\n\t\"LOCALLEAVE\": \"2017-01-09 08:00:00.0\",\n\t\"DEST\": \"KOKV\"\n}, {\n\t\"ORIGIN\": \"KOKV\",\n\t\"LOCALLEAVE\": \"2017-01-09 16:00:00.0\",\n\t\"DEST\": \"KOFP\"\n}, {\n\t\"ORIGIN\": \"KOFP\",\n\t\"LOCALLEAVE\": \"2017-01-10 10:00:00.0\",\n\t\"DEST\": \"KFTW\"\n}, {\n\t\"ORIGIN\": \"KFTW\",\n\t\"LOCALLEAVE\": \"2017-01-11 11:30:00.0\",\n\t\"DEST\": \"KOFP\"\n}, {\n\t\"ORIGIN\": \"KOFP\",\n\t\"LOCALLEAVE\": \"2017-01-11 15:30:00.0\",\n\t\"DEST\": \"KTEB\"\n}, {\n\t\"ORIGIN\": \"KTEB\",\n\t\"LOCALLEAVE\": \"2017-01-11 16:40:00.0\",\n\t\"DEST\": \"KOFP\"\n}, {\n\t\"ORIGIN\": \"KOFP\",\n\t\"LOCALLEAVE\": \"2017-01-13 08:45:00.0\",\n\t\"DEST\": \"KTEB\"\n}, {\n\t\"ORIGIN\": \"KTEB\",\n\t\"LOCALLEAVE\": \"2017-01-13 10:00:00.0\",\n\t\"DEST\": \"KOFP\"\n}, {\n\t\"ORIGIN\": \"KOFP\",\n\t\"LOCALLEAVE\": \"2017-01-16 07:00:00.0\",\n\t\"DEST\": \"KMTV\"\n}, {\n\t\"ORIGIN\": \"KMTV\",\n\t\"LOCALLEAVE\": \"2017-01-16 08:00:00.0\",\n\t\"DEST\": \"KOFP\"\n}, {\n\t\"ORIGIN\": \"KMTV\",\n\t\"LOCALLEAVE\": \"2017-01-18 12:00:00.0\",\n\t\"DEST\": \"KOFP\"\n}, {\n\t\"ORIGIN\": \"KOFP\",\n\t\"LOCALLEAVE\": \"2017-01-18 11:00:00.0\",\n\t\"DEST\": \"KMTV\"\n}, {\n\t\"ORIGIN\": \"KOFP\",\n\t\"LOCALLEAVE\": \"2017-01-18 14:00:00.0\",\n\t\"DEST\": \"KTTA\"\n}, {\n\t\"ORIGIN\": \"KTTA\",\n\t\"LOCALLEAVE\": \"2017-01-18 15:15:00.0\",\n\t\"DEST\": \"KSEF\"\n}, {\n\t\"ORIGIN\": \"KSEF\",\n\t\"LOCALLEAVE\": \"2017-01-18 17:15:00.0\",\n\t\"DEST\": \"KTTA\"\n}, {\n\t\"ORIGIN\": \"KTTA\",\n\t\"LOCALLEAVE\": \"2017-01-19 14:00:00.0\",\n\t\"DEST\": \"KOFP\"\n}, {\n\t\"ORIGIN\": \"KOFP\",\n\t\"LOCALLEAVE\": \"2017-01-22 12:00:00.0\",\n\t\"DEST\": \"KSEF\"\n}, {\n\t\"ORIGIN\": \"KSEF\",\n\t\"LOCALLEAVE\": \"2017-01-22 17:00:00.0\",\n\t\"DEST\": \"KOFP\"\n}, {\n\t\"ORIGIN\": \"KOFP\",\n\t\"LOCALLEAVE\": \"2017-01-23 07:30:00.0\",\n\t\"DEST\": \"KPIA\"\n}, {\n\t\"ORIGIN\": \"KPIA\",\n\t\"LOCALLEAVE\": \"2017-01-23 13:00:00.0\",\n\t\"DEST\": \"KSPA\"\n}, {\n\t\"ORIGIN\": \"KSPA\",\n\t\"LOCALLEAVE\": \"2017-01-23 16:00:00.0\",\n\t\"DEST\": \"KOFP\"\n}, {\n\t\"ORIGIN\": \"KOFP\",\n\t\"LOCALLEAVE\": \"2017-01-24 15:00:00.0\",\n\t\"DEST\": \"KRST\"\n}, {\n\t\"ORIGIN\": \"KRST\",\n\t\"LOCALLEAVE\": \"2017-01-24 17:00:00.0\",\n\t\"DEST\": \"KOFP\"\n}, {\n\t\"ORIGIN\": \"KOFP\",\n\t\"LOCALLEAVE\": \"2017-01-24 15:00:00.0\",\n\t\"DEST\": \"KRST\"\n}, {\n\t\"ORIGIN\": \"KOFP\",\n\t\"LOCALLEAVE\": \"2017-01-27 14:30:00.0\",\n\t\"DEST\": \"KRST\"\n}, {\n\t\"ORIGIN\": \"KRST\",\n\t\"LOCALLEAVE\": \"2017-01-27 17:00:00.0\",\n\t\"DEST\": \"KOFP\"\n}]";
		private string _flightdata = null;
		private string _holddata = null;
		private string _mxdata = null;
		private string _authdata = "";
		private string _authheader = "";
		private List<string> _loadedMonths = null;

		SfCalendar calendar;
		CalendarEventCollection col = new CalendarEventCollection();


		private void ClosePage_Clicked(object sender, EventArgs e)
		{
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



			calendar = new SfCalendar();
			calendar.ShowInlineEvents = true;
			calendar.ShowNavigationButtons = true;
			calendar.MonthChanged += LoadNewMonth;

			LoadFlights(DateTime.Now);
			_loadedMonths.Add(DateTime.Now.Month.ToString());

			layout.Children.Add(calendar);

			//Button bar ******************************************************

			Button closePage = new Button { Text = "Close", Image = "closePage.png" };

			Button newFlightRequest = new Button { Text = "Request Flight", Image = "submit.png" };


			newFlightRequest.Clicked += SendFlightRequest;
			closePage.Clicked += ClosePage_Clicked;


			buttonbar.Children.Add(closePage, 0, 0);
			buttonbar.Children.Add(newFlightRequest, 1, 0);


		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			//LoadFlights(DateTime.Now);
			//_loadedMonths.Add(DateTime.Now.Month.ToString());

		}


		private void LoadFlights(DateTime currDate)
		{
			string startDate = currDate.Month.ToString() + "/1/" + currDate.Year.ToString();
			string endDate = currDate.Month.ToString() + "/" + DateTime.DaysInMonth(currDate.Year, currDate.Month).ToString() + "/" + currDate.Year.ToString();
			indicator.IsRunning = true;

			_flightdata = GetFlightData(startDate, endDate);
			_holddata = GetHoldData(startDate, endDate);
			_mxdata = GetMxData(startDate, endDate);



			/*if (_flightdata != null)
			{
				var flightobj = JsonConvert.DeserializeObject<List<Flight>>(_flightdata);
				foreach (Flight f in flightobj)
				{
					CalendarInlineEvent ev = new CalendarInlineEvent();
					ev.Subject = f.ORIGIN + "-" + f.DEST;
					ev.StartTime = Convert.ToDateTime(f.LOCALLEAVE);
					ev.EndTime = Convert.ToDateTime(f.LOCALLEAVE).AddHours(1);
					ev.Color = Color.FromHex("68A0ED");
					col.Add(ev);
				}
				
			}

			if (_holddata != null)
			{
				var holdobj = JsonConvert.DeserializeObject<List<Hold>>(_holddata);
				foreach (Hold h in holdobj)
				{
					CalendarInlineEvent ev = new CalendarInlineEvent();
					ev.Subject = "HOLD";
					ev.StartTime = Convert.ToDateTime(h.LEGLOCALDATE);
					ev.EndTime = Convert.ToDateTime(h.LEGLOCALDATE).AddHours(12);
					ev.Color = Color.FromHex("6AED68");
					col.Add(ev);
				}
			}

			if (_mxdata != null)
			{
				var mxobj = JsonConvert.DeserializeObject<List<Maint>>(_mxdata);
				foreach (Maint m in mxobj)
				{
					CalendarInlineEvent ev = new CalendarInlineEvent();
					ev.Subject = "MAINTENANCE";
					ev.StartTime = Convert.ToDateTime(m.LEGLOCALDATE);
					ev.EndTime = Convert.ToDateTime(m.LEGLOCALDATE).AddHours(12);
					ev.Color = Color.FromHex("FF8033");
					col.Add(ev);
				}
			}*/

			//calendar.DataSource = col;

		}

		private void LoadNewMonth(object sender, MonthChangedEventArgs e)
		{
			DateTime currDate = e.args.CurrentValue;
			DateTime prevDate = e.args.PreviousValue;

			if (!_loadedMonths.Contains(currDate.Month.ToString()))
			{
				_loadedMonths.Add(currDate.Month.ToString());
				LoadFlights(currDate);

			}

			calendar.MoveToDate = e.args.CurrentValue;


		}
		public string GetHoldData(string start, string end)
		{
			Task<string> holdData = DoHttpRequest(new Uri(String.Format(Constants.ServiceUrl + "/getholds?start=" + start + "&end=" + end, string.Empty)), 2);
			return holdData.ToString();
		}

		public string GetMxData(string start, string end)
		{
			Task<string> mxData = DoHttpRequest(new Uri(String.Format(Constants.ServiceUrl + "/getmx?start=" + start + "&end=" + end, string.Empty)), 3);
			return mxData.ToString();
		}

		public string GetFlightData(string start, string end)
		{
			Task<string> flightdata = DoHttpRequest(new Uri(String.Format(Constants.ServiceUrl + "/getflights?start=" + start + "&end=" + end, string.Empty)), 1);
			return flightdata.ToString();
		}

		public async Task<string> DoHttpRequest(Uri uri, int flag)
		{
			HttpClient client = new HttpClient();
			vm.IsLoading = true;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _authheader);
			client.MaxResponseContentBufferSize = 512000;

			try
			{
				var response = await client.GetAsync(uri);
				if (response.IsSuccessStatusCode)
				{
					var responseContent = response.Content;

					switch(flag)
					{
						case 1:
							_flightdata = responseContent.ReadAsStringAsync().Result;
							var flightobj = JsonConvert.DeserializeObject<List<Flight>>(_flightdata);
							foreach (Flight f in flightobj)
							{
								CalendarInlineEvent ev = new CalendarInlineEvent();
								ev.Subject = f.ORIGIN + "-" + f.DEST;
								ev.StartTime = Convert.ToDateTime(f.LOCALLEAVE);
								ev.EndTime = Convert.ToDateTime(f.LOCALLEAVE).AddHours(1);
								ev.Color = Color.FromHex("68A0ED");
								col.Add(ev);
							}
							break;
						case 2:
							_holddata = responseContent.ReadAsStringAsync().Result;
							var holdobj = JsonConvert.DeserializeObject<List<Hold>>(_holddata);
							foreach (Hold h in holdobj)
							{
								CalendarInlineEvent ev = new CalendarInlineEvent();
								ev.Subject = "HOLD";
								ev.StartTime = Convert.ToDateTime(h.LEGLOCALDATE);
								ev.EndTime = Convert.ToDateTime(h.LEGLOCALDATE).AddHours(12);
								ev.Color = Color.FromHex("6AED68");
								col.Add(ev);
							}
							break;
						case 3:
							_mxdata = responseContent.ReadAsStringAsync().Result;
							var mxobj = JsonConvert.DeserializeObject<List<Maint>>(_mxdata);
							foreach (Maint m in mxobj)
							{
								CalendarInlineEvent ev = new CalendarInlineEvent();
								ev.Subject = "MAINTENANCE";
								ev.StartTime = Convert.ToDateTime(m.LEGLOCALDATE);
								ev.EndTime = Convert.ToDateTime(m.LEGLOCALDATE).AddHours(12);
								ev.Color = Color.FromHex("FF8033");
								col.Add(ev);
							}
							break;
						default:
							break;
					}

					calendar.DataSource = col;
					//indicator.IsRunning = false;
					vm.IsLoading = false;
					return "baller";
				}
			}
			catch (Exception ex)
			{
				Page p = new Page();

				await p.DisplayAlert("ERROR!", ex.ToString(), "Close");
			}

			return null;
		}

		public void SendFlightRequest(object sender, EventArgs e)
		{
			FlightRequestPage frp = new FlightRequestPage();
			this.Navigation.PushModalAsync(frp);

		}
	}
}
