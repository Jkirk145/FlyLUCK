using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace FlyLUCK
{
	public partial class CalendarView : ContentView
	{

		private int _colCount = 0;
		private DateTime _today;
		private DateTime _calDate;

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


		void BuildGrid(DateTime today)
		{
			MonthName.Text = today.ToString("MMM"); ;
			MonthGrid.Children.Add(new Label { HorizontalOptions = LayoutOptions.End, Text = "Su", BackgroundColor = Color.Silver, TextColor = Color.Black }, 0, 0);
			MonthGrid.Children.Add(new Label { HorizontalOptions = LayoutOptions.End, Text = "Mo", BackgroundColor = Color.Silver, TextColor = Color.Black }, 1, 0);
			MonthGrid.Children.Add(new Label { HorizontalOptions = LayoutOptions.End, Text = "Tu", BackgroundColor = Color.Silver, TextColor = Color.Black }, 2, 0);
			MonthGrid.Children.Add(new Label { HorizontalOptions = LayoutOptions.End, Text = "We", BackgroundColor = Color.Silver, TextColor = Color.Black }, 3, 0);
			MonthGrid.Children.Add(new Label { HorizontalOptions = LayoutOptions.End, Text = "Th", BackgroundColor = Color.Silver, TextColor = Color.Black }, 4, 0);
			MonthGrid.Children.Add(new Label { HorizontalOptions = LayoutOptions.End, Text = "Fr", BackgroundColor = Color.Silver, TextColor = Color.Black }, 5, 0);
			MonthGrid.Children.Add(new Label { HorizontalOptions = LayoutOptions.End, Text = "Sa", BackgroundColor = Color.Silver, TextColor = Color.Black }, 6, 0);

			int rowPos = 1;
			int colPos = 0;

			DateTime firstDayOfMonth = new DateTime(today.Year, today.Month, 1);
			colPos = (int)firstDayOfMonth.DayOfWeek;

			MonthGrid.Children.Add(new Label { HorizontalTextAlignment = TextAlignment.End, Text = "1", BackgroundColor = Color.Silver, TextColor = Color.White }, colPos, rowPos);
			int numDays = DateTime.DaysInMonth(today.Year, today.Month);

			for (int i = 2; i <= numDays; i++)
			{
				colPos += 1;
				if (colPos == 7)
				{
					colPos = 0;
					rowPos += 1;
				}
				MonthGrid.Children.Add(new Label { HorizontalTextAlignment = TextAlignment.End, Text = i.ToString(), BackgroundColor = Color.Silver, TextColor = Color.White }, colPos, rowPos);

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
