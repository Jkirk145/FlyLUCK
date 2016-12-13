using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace FlyLUCK
{
	public partial class CalendarPage : ContentPage
	{
		CalendarView _calendarView;
		StackLayout _stacker;
		Label _flightinfo;
		Button _closepage;
		Button _SelectDate;

		void ClosePage(Object sender, EventArgs e)
		{
			this.Navigation.PopModalAsync();
		}

		void SelectDate(Object sender, EventArgs e)
		{
			_calendarView.SelectedDate = new DateTime(2016, 11, 8);

			_calendarView.SelectedDate = new DateTime(2016, 11, 26);
		}


		public CalendarPage()
		{
			InitializeComponent();

			Title = "Aircraft Calendar";
			_stacker = new StackLayout();
			_flightinfo = new Label
			{
				VerticalOptions = LayoutOptions.Start,
				HorizontalOptions = LayoutOptions.CenterAndExpand
			};
			_closepage = new Button
			{
				Image = "close.png"
			};
			_closepage.Clicked += ClosePage;

			_SelectDate = new Button { Text = "Do It" };
			_SelectDate.Clicked += SelectDate;

			_stacker.Padding = 30;
			Content = _stacker;

			_calendarView = new CalendarView
			{
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Center,
				MinDate = CalendarView.FirstDayOfMonth(DateTime.Now),
				MaxDate = CalendarView.LastDayOfMonth(DateTime.Now.AddYears(1))
			};
			_stacker.Children.Add(_closepage);
			_stacker.Children.Add(_calendarView);
			_stacker.Children.Add(_flightinfo);
			_stacker.Children.Add(_SelectDate);

			_calendarView.DateSelected += (object sender, DateTime e) =>
			{
				_flightinfo.Text = "Date Was Selected" + e.ToString("d");
			};

		}
	}
}
