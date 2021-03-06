﻿using System;
using System.Collections.Generic;
using Plugin.Messaging;
using Xamarin.Forms;
using Plugin.Connectivity;

namespace FlyLUCK
{
	public partial class HomePage : ContentPage
	{

		public void OnLoggedIn(object sender, EventArgs e)
		{
			welcomeLabel.Text = "Welcome " + Helpers.Settings.UserName;
			if (Helpers.Settings.FlightCrew || Helpers.Settings.IsAdmin)
				layoutAdmin.IsVisible = true;
			DependencyService.Get<IRegistration>().Register();
		}

		public HomePage()
		{
			InitializeComponent();

			//if this is the first login present the login page to capture the UserID
			if (DateTime.Now > Helpers.Settings.SessionExpires)
			{

				LoginPage lp = new LoginPage();
				lp.LoggedIn += OnLoggedIn;
				Navigation.PushModalAsync(lp);
			}
			else //already logged in, register for notifications
			{
				DependencyService.Get<IRegistration>().Register();
			}

			welcomeLabel.Text = "Welcome " + Helpers.Settings.UserName;

			Button openCalendar = new Button { Image = "calendar.png" };
			Button openMyFlights = new Button { Image = "myflights2.png" };
			Button newFlight = new Button { Image = "submit.png" };
			Button aboutUs = new Button { Image = "information.png" };
			Button sendMessage = new Button { Image = "message.png" };

			openCalendar.HeightRequest = 60;
			openCalendar.VerticalOptions = LayoutOptions.Center;
			openCalendar.HorizontalOptions = LayoutOptions.Center;
			openCalendar.BackgroundColor = Color.Transparent;

			openMyFlights.HeightRequest = 60;
			openMyFlights.VerticalOptions = LayoutOptions.Center;
			openMyFlights.HorizontalOptions = LayoutOptions.Center;
			openMyFlights.BackgroundColor = Color.Transparent;

			newFlight.HeightRequest = 60;
			newFlight.VerticalOptions = LayoutOptions.Center;
			newFlight.HorizontalOptions = LayoutOptions.Center;
			newFlight.BackgroundColor = Color.Transparent;

			aboutUs.HeightRequest = 60;
			aboutUs.VerticalOptions = LayoutOptions.Center;
			aboutUs.HorizontalOptions = LayoutOptions.Center;
			aboutUs.BackgroundColor = Color.Transparent;

			openMyFlights.Clicked += OpenMyFlights;
			openCalendar.Clicked += OpenCalendar;
			newFlight.Clicked += NewFlight;
			aboutUs.Clicked += OpenAbout;

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

			sendMessage.HeightRequest = 60;
			sendMessage.BackgroundColor = Color.Transparent;
			sendMessage.Clicked += SendPush;
			layoutAdmin.Children.Add(sendMessage);
			layoutAdmin.Children.Add(new Label { Text = "Send Push", FontSize = 12, HorizontalTextAlignment = TextAlignment.Center });

			if (Helpers.Settings.FlightCrew || Helpers.Settings.IsAdmin)
				layoutAdmin.IsVisible = true;

		}

		async void SendPush(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new SendMessage());
		}

		async void OpenMyFlights(object sender, EventArgs e)
		{
			if (!CrossConnectivity.Current.IsConnected)
			{
				await DisplayAlert("Oops!", "There is a problem with your network connection. Please verify your settings and try again.", "Close");
				return;
			}
			await Navigation.PushModalAsync(new FlightList());
			return;
		}

		async void OpenCalendar(object sender, EventArgs e)
		{
			if (!CrossConnectivity.Current.IsConnected)
			{
				await DisplayAlert("Oops!", "There is a problem with your network connection. Please verify your settings and try again.", "Close");
				return;
			}
			await Navigation.PushModalAsync(new Calendar());
            //await DisplayAlert("About Us", "The calendar is currently being updated. Thanks for your patience!", "Close");
			return;
		}

		async void NewFlight(object sender, EventArgs e)
		{
			if(!CrossConnectivity.Current.IsConnected)
			{
				await DisplayAlert("Oops!", "There is a problem with your network connection. Please verify your settings and try again.", "Close");
				return;
			}
			await Navigation.PushModalAsync(new FlightRequestPage());
			return;
		}

		void OpenAbout(object sender, EventArgs e)
		{
			DisplayAlert("About Us", "Coming Soon!", "Close");
		}
	}
}
