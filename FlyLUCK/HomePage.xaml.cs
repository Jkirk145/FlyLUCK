using System;
using System.Collections.Generic;
using Plugin.Messaging;
using Xamarin.Forms;

namespace FlyLUCK
{
	public partial class HomePage : ContentPage
	{
		public HomePage()
		{
			InitializeComponent();

			Button openCalendar = new Button { Image = "calendar.png" };
			Button openMyFlights = new Button { Image = "myflights2.png" };
			Button newFlight = new Button { Image = "newflight2.png" };
			Button aboutUs = new Button { Image = "information.png" };

			openCalendar.HeightRequest = 60;
			openCalendar.VerticalOptions = LayoutOptions.Center;
			openCalendar.HorizontalOptions = LayoutOptions.Center;

			openMyFlights.HeightRequest = 60;
			openMyFlights.VerticalOptions = LayoutOptions.Center;
			openMyFlights.HorizontalOptions = LayoutOptions.Center;

			newFlight.HeightRequest = 60;
			newFlight.VerticalOptions = LayoutOptions.Center;
			newFlight.HorizontalOptions = LayoutOptions.Center;

			aboutUs.HeightRequest = 60;
			aboutUs.VerticalOptions = LayoutOptions.Center;
			aboutUs.HorizontalOptions = LayoutOptions.Center;

			openMyFlights.Clicked += OpenMyFlights;
			openCalendar.Clicked += DoSendMessage;

			StackLayout sl1 = new StackLayout();
			StackLayout sl2 = new StackLayout();
			StackLayout sl3 = new StackLayout();
			StackLayout sl4 = new StackLayout();

			sl1.Children.Add(openCalendar);
			sl1.Children.Add(new Label { Text = "Calendar", FontSize = 12, HorizontalTextAlignment=TextAlignment.Center });

			sl2.Children.Add(openMyFlights);
			sl2.Children.Add(new Label { Text = "My Flights", FontSize = 12, HorizontalTextAlignment = TextAlignment.Center  });

			sl3.Children.Add(newFlight);
			sl3.Children.Add(new Label { Text = "Request Flight", FontSize=12, HorizontalTextAlignment = TextAlignment.Center });

			sl4.Children.Add(aboutUs);
			sl4.Children.Add(new Label { Text = "About the Flight Department", FontSize = 12, HorizontalTextAlignment = TextAlignment.Center });
			mainGrid.Children.Add(sl1, 0, 0);
			mainGrid.Children.Add(sl2, 1, 0);
			mainGrid.Children.Add(sl3, 0, 1);
			mainGrid.Children.Add(sl4, 1, 1);

		}

		async void OpenMyFlights(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new FlightList());
			return;
		}

		async void DoSendMessage(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new CalendarPage());
			return;
		}
	}
}
