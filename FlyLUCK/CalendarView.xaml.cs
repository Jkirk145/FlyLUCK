using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Net.Http;
using System.Threading.Tasks;
using RoundedBoxView.Forms.Plugin.Abstractions;
using Newtonsoft.Json;

namespace FlyLUCK
{
	public partial class CalendarView : ContentView
	{

		private int _colCount = 0;
		private DateTime _today;
		private DateTime _calDate;
		private DateTime _tripDate;

		private String _flights = "[{\n\t\"ORIGIN\": \"KOFP\",\n\t\"LOCALLEAVE\": \"2017-01-01 09:30:00.0\",\n\t\"DEST\": \"MYAM\"\n}, {\n\t\"ORIGIN\": \"MYAM\",\n\t\"LOCALLEAVE\": \"2017-01-01 12:00:00.0\",\n\t\"DEST\": \"KILM\"\n}, {\n\t\"ORIGIN\": \"KILM\",\n\t\"LOCALLEAVE\": \"2017-01-01 14:00:00.0\",\n\t\"DEST\": \"KOFP\"\n}, {\n\t\"ORIGIN\": \"KOFP\",\n\t\"LOCALLEAVE\": \"2017-01-01 08:00:00.0\",\n\t\"DEST\": \"MYAM\"\n}, {\n\t\"ORIGIN\": \"MYAM\",\n\t\"LOCALLEAVE\": \"2017-01-01 10:30:00.0\",\n\t\"DEST\": \"KILM\"\n}, {\n\t\"ORIGIN\": \"KILM\",\n\t\"LOCALLEAVE\": \"2017-01-01 12:45:00.0\",\n\t\"DEST\": \"KOFP\"\n}, {\n\t\"ORIGIN\": \"KOFP\",\n\t\"LOCALLEAVE\": \"2017-01-05 07:00:00.0\",\n\t\"DEST\": \"KJYO\"\n}, {\n\t\"ORIGIN\": \"KJYO\",\n\t\"LOCALLEAVE\": \"2017-01-05 07:45:00.0\",\n\t\"DEST\": \"KMKE\"\n}, {\n\t\"ORIGIN\": \"KMKE\",\n\t\"LOCALLEAVE\": \"2017-01-05 14:00:00.0\",\n\t\"DEST\": \"KJYO\"\n}, {\n\t\"ORIGIN\": \"KJYO\",\n\t\"LOCALLEAVE\": \"2017-01-05 16:45:00.0\",\n\t\"DEST\": \"KOFP\"\n}, {\n\t\"ORIGIN\": \"KOFP\",\n\t\"LOCALLEAVE\": \"2017-01-06 11:00:00.0\",\n\t\"DEST\": \"KHEF\"\n}, {\n\t\"ORIGIN\": \"KHEF\",\n\t\"LOCALLEAVE\": \"2017-01-06 14:30:00.0\",\n\t\"DEST\": \"KOFP\"\n}, {\n\t\"ORIGIN\": \"KOFP\",\n\t\"LOCALLEAVE\": \"2017-01-06 17:00:00.0\",\n\t\"DEST\": \"KGVL\"\n}, {\n\t\"ORIGIN\": \"KGVL\",\n\t\"LOCALLEAVE\": \"2017-01-06 18:30:00.0\",\n\t\"DEST\": \"KOFP\"\n}, {\n\t\"ORIGIN\": \"KOFP\",\n\t\"LOCALLEAVE\": \"2017-01-08 15:30:00.0\",\n\t\"DEST\": \"KGVL\"\n}, {\n\t\"ORIGIN\": \"KGVL\",\n\t\"LOCALLEAVE\": \"2017-01-08 17:00:00.0\",\n\t\"DEST\": \"KOFP\"\n}, {\n\t\"ORIGIN\": \"KOFP\",\n\t\"LOCALLEAVE\": \"2017-01-09 08:00:00.0\",\n\t\"DEST\": \"KOKV\"\n}, {\n\t\"ORIGIN\": \"KOKV\",\n\t\"LOCALLEAVE\": \"2017-01-09 16:00:00.0\",\n\t\"DEST\": \"KOFP\"\n}, {\n\t\"ORIGIN\": \"KOFP\",\n\t\"LOCALLEAVE\": \"2017-01-10 10:00:00.0\",\n\t\"DEST\": \"KFTW\"\n}, {\n\t\"ORIGIN\": \"KFTW\",\n\t\"LOCALLEAVE\": \"2017-01-11 11:30:00.0\",\n\t\"DEST\": \"KOFP\"\n}, {\n\t\"ORIGIN\": \"KOFP\",\n\t\"LOCALLEAVE\": \"2017-01-11 15:30:00.0\",\n\t\"DEST\": \"KTEB\"\n}, {\n\t\"ORIGIN\": \"KTEB\",\n\t\"LOCALLEAVE\": \"2017-01-11 16:40:00.0\",\n\t\"DEST\": \"KOFP\"\n}, {\n\t\"ORIGIN\": \"KOFP\",\n\t\"LOCALLEAVE\": \"2017-01-13 08:45:00.0\",\n\t\"DEST\": \"KTEB\"\n}, {\n\t\"ORIGIN\": \"KTEB\",\n\t\"LOCALLEAVE\": \"2017-01-13 10:00:00.0\",\n\t\"DEST\": \"KOFP\"\n}, {\n\t\"ORIGIN\": \"KOFP\",\n\t\"LOCALLEAVE\": \"2017-01-16 07:00:00.0\",\n\t\"DEST\": \"KMTV\"\n}, {\n\t\"ORIGIN\": \"KMTV\",\n\t\"LOCALLEAVE\": \"2017-01-16 08:00:00.0\",\n\t\"DEST\": \"KOFP\"\n}, {\n\t\"ORIGIN\": \"KMTV\",\n\t\"LOCALLEAVE\": \"2017-01-18 12:00:00.0\",\n\t\"DEST\": \"KOFP\"\n}, {\n\t\"ORIGIN\": \"KOFP\",\n\t\"LOCALLEAVE\": \"2017-01-18 11:00:00.0\",\n\t\"DEST\": \"KMTV\"\n}, {\n\t\"ORIGIN\": \"KOFP\",\n\t\"LOCALLEAVE\": \"2017-01-18 14:00:00.0\",\n\t\"DEST\": \"KTTA\"\n}, {\n\t\"ORIGIN\": \"KTTA\",\n\t\"LOCALLEAVE\": \"2017-01-18 15:15:00.0\",\n\t\"DEST\": \"KSEF\"\n}, {\n\t\"ORIGIN\": \"KSEF\",\n\t\"LOCALLEAVE\": \"2017-01-18 17:15:00.0\",\n\t\"DEST\": \"KTTA\"\n}, {\n\t\"ORIGIN\": \"KTTA\",\n\t\"LOCALLEAVE\": \"2017-01-19 14:00:00.0\",\n\t\"DEST\": \"KOFP\"\n}, {\n\t\"ORIGIN\": \"KOFP\",\n\t\"LOCALLEAVE\": \"2017-01-22 12:00:00.0\",\n\t\"DEST\": \"KSEF\"\n}, {\n\t\"ORIGIN\": \"KSEF\",\n\t\"LOCALLEAVE\": \"2017-01-22 17:00:00.0\",\n\t\"DEST\": \"KOFP\"\n}, {\n\t\"ORIGIN\": \"KOFP\",\n\t\"LOCALLEAVE\": \"2017-01-23 07:30:00.0\",\n\t\"DEST\": \"KPIA\"\n}, {\n\t\"ORIGIN\": \"KPIA\",\n\t\"LOCALLEAVE\": \"2017-01-23 13:00:00.0\",\n\t\"DEST\": \"KSPA\"\n}, {\n\t\"ORIGIN\": \"KSPA\",\n\t\"LOCALLEAVE\": \"2017-01-23 16:00:00.0\",\n\t\"DEST\": \"KOFP\"\n}, {\n\t\"ORIGIN\": \"KOFP\",\n\t\"LOCALLEAVE\": \"2017-01-24 15:00:00.0\",\n\t\"DEST\": \"KRST\"\n}, {\n\t\"ORIGIN\": \"KRST\",\n\t\"LOCALLEAVE\": \"2017-01-24 17:00:00.0\",\n\t\"DEST\": \"KOFP\"\n}, {\n\t\"ORIGIN\": \"KOFP\",\n\t\"LOCALLEAVE\": \"2017-01-24 15:00:00.0\",\n\t\"DEST\": \"KRST\"\n}, {\n\t\"ORIGIN\": \"KOFP\",\n\t\"LOCALLEAVE\": \"2017-01-27 14:30:00.0\",\n\t\"DEST\": \"KRST\"\n}, {\n\t\"ORIGIN\": \"KRST\",\n\t\"LOCALLEAVE\": \"2017-01-27 17:00:00.0\",\n\t\"DEST\": \"KOFP\"\n}]";
		//private String _flights = "";

		private List<Flight> flights;

		public CalendarView()
		{
			InitializeComponent();

			_today = DateTime.Now;
			_calDate = _today;

			cmdNext.Clicked += Next_Month;
			cmdPrev.Clicked += Prev_Month;
			cmdPrev.IsVisible = false;
			BuildGrid(_today);
		}

		public string GetFlightData(string start, string end)
		{
			HttpClient client = new HttpClient();

			string flightdata = "";

			client.MaxResponseContentBufferSize = 512000;
			var uri = new Uri(String.Format("http://localhost:8080/FlyLUCKService/getflights?start=" + start + "&end=" + end, string.Empty));
			try
			{
				var response = client.GetAsync(uri).Result;
				if (response.IsSuccessStatusCode)
				{
					var responseContent = response.Content;
					flightdata = responseContent.ReadAsStringAsync().Result;
				}
			}
			catch (Exception ex)
			{
				Page p = new Page();
				p.DisplayAlert("ERROR!", ex.ToString(), "Close");

			}

			return flightdata;
		}

		void BuildGrid(DateTime today)
		{
			MonthName.Text = today.ToString("MMM"); ;
			MonthGrid.Children.Add(new Label { HorizontalOptions = LayoutOptions.Center, Text = "Su", BackgroundColor = Color.White, TextColor = Color.Black }, 0, 0);
			MonthGrid.Children.Add(new Label { HorizontalOptions = LayoutOptions.Center, Text = "Mo", BackgroundColor = Color.White, TextColor = Color.Black }, 1, 0);
			MonthGrid.Children.Add(new Label { HorizontalOptions = LayoutOptions.Center, Text = "Tu", BackgroundColor = Color.White, TextColor = Color.Black }, 2, 0);
			MonthGrid.Children.Add(new Label { HorizontalOptions = LayoutOptions.Center, Text = "We", BackgroundColor = Color.White, TextColor = Color.Black }, 3, 0);
			MonthGrid.Children.Add(new Label { HorizontalOptions = LayoutOptions.Center, Text = "Th", BackgroundColor = Color.White, TextColor = Color.Black }, 4, 0);
			MonthGrid.Children.Add(new Label { HorizontalOptions = LayoutOptions.Center, Text = "Fr", BackgroundColor = Color.White, TextColor = Color.Black }, 5, 0);
			MonthGrid.Children.Add(new Label { HorizontalOptions = LayoutOptions.Center, Text = "Sa", BackgroundColor = Color.White, TextColor = Color.Black }, 6, 0);

			int rowPos = 1;
			int colPos = 0;

			DateTime firstDayOfMonth = new DateTime(today.Year, today.Month, 1);
			DateTime lastDayOfMonth = new DateTime(today.Year, today.Month, DateTime.DaysInMonth(today.Year, today.Month));

			//_flights = GetFlightData(firstDayOfMonth.ToString("MM-dd-yyyy"), lastDayOfMonth.ToString("MM-dd-yyyy")).ToString();
			var flightobj = JsonConvert.DeserializeObject<List<Flight>>(_flights);

			colPos = (int)firstDayOfMonth.DayOfWeek;

			//MonthGrid.Children.Add(new CalendarBadge("1", Color.Silver), colPos, rowPos);
			int numDays = DateTime.DaysInMonth(today.Year, today.Month);

			for (int i = 1; i <= numDays; i++)
			{
				_tripDate = new DateTime(today.Year, today.Month, i);
				String dateString = _tripDate.ToString("yyyy-MM-dd");

				if (_flights.Contains(dateString)){
					MonthGrid.Children.Add(new CalendarBadge(i.ToString(), Color.Red), colPos, rowPos);
					
				}
				else {
					MonthGrid.Children.Add(new CalendarBadge(i.ToString(), Color.White), colPos, rowPos);
					//MonthGrid.Children.Add(new Label { HorizontalTextAlignment = TextAlignment.End, Text = i.ToString(), BackgroundColor = Color.Silver, TextColor = Color.White }, colPos, rowPos);
				}
				colPos += 1;
				if (colPos == 7)
				{
					colPos = 0;
					rowPos += 1;
				}
			}
		}

		void Next_Month(object sender, EventArgs e)
		{
			_calDate = _calDate.AddMonths(1);
			MonthGrid.Children.Clear();
			BuildGrid(_calDate);
			cmdPrev.IsVisible = true;
		}

		void Prev_Month(object sender, EventArgs e)
		{
			
			_calDate = _calDate.AddMonths(-1);
			if (_calDate.Month == _today.Month)
			{
				cmdPrev.IsVisible = false;
			}
			MonthGrid.Children.Clear();
			BuildGrid(_calDate);
		}

	}
}
