﻿using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;


//Google Places API Key
//AIzaSyBu5Zz8UWR1Rjr5sqN9s3wllEoOzOSrCIQ
//Sample URL: https://maps.googleapis.com/maps/api/place/autocomplete/json?input=Paris&types=geocode&key=YOUR_API_KEY


namespace FlyLUCK
{
	public partial class FlightRequestPage : ContentPage
	{
		private ListView _autocompleteListView;
		private static string _apikey = "AIzaSyBu5Zz8UWR1Rjr5sqN9s3wllEoOzOSrCIQ";

		Entry search;
		Editor purpose;
		DatePicker start;
		DatePicker end;
		TimePicker startTime;
		TimePicker endTime;
		Entry requestor;
		Picker numPax;
		Switch rentalCar;
		Editor specials;
		SearchPlaces sp;

		private void ClosePage_Clicked(object sender, EventArgs e)
		{
			this.Navigation.PopModalAsync();
		}

		public void OnPlaceSelected(object sender, EventArgs e)
		{
			SelectedItemChangedEventArgs se = (SelectedItemChangedEventArgs)e;
			search.Text = ((Place)se.SelectedItem).description;
		}

		private async void Search_Focused(object sender, FocusEventArgs e)
		{

			sp = new SearchPlaces();
			sp.PlaceSelected += OnPlaceSelected;
			await this.Navigation.PushModalAsync(sp);

		}

		private bool ValidateForm()
		{
			bool isValid = true;
			if (requestor.Text == null)
			{
				requestor.BackgroundColor = Color.Red;
				isValid = false;
			}
			if (search.Text == null)
			{
				search.BackgroundColor = Color.Red;
				isValid = false;
			}
			if (purpose.Text == null)
			{
				purpose.BackgroundColor = Color.Red;
				isValid = false;
			}

			return isValid;
		}

		private async void ProcessRequest(object sender, EventArgs e)
		{

			if (!ValidateForm())
			{
				await DisplayAlert("ERROR!", "The highlighted fields are required!", "Close");
			}
			else
			{

				ActivityView av = new ActivityView();
				await this.Navigation.PushModalAsync(av);
				bool success = await SendRequest();
				await this.Navigation.PopModalAsync();
				if (success)
				{
					await DisplayAlert("Success!", "Your request was sent! The flight department will contact you to finalize your trip.", "OK");
					await this.Navigation.PopModalAsync();
				}
				else
				{
					await DisplayAlert("Uh oh....", "There was an error processing your request! Please try again.", "OK");
				}
			}
		}

		private async Task<bool> SendRequest()
		{

			FlyLUCK.Request request = new Request();
			request.Subject = "New Flight Request [Mobile - TEST]";
			request.Destination = search.Text;
			request.DateDepart = start.Date.ToString("MM-dd-yyyy");
			request.TimeDepart = startTime.Time.ToString();
			request.DateReturn = end.Date.ToString("MM-dd-yyyy");
			request.TimeReturn = endTime.Time.ToString();
			request.NumPax = numPax.Items[numPax.SelectedIndex];
			request.Purpose = purpose.Text;
			request.Requestor = requestor.Text;
			request.RentalCar = (rentalCar.IsToggled ? "Y" : "N");
			request.Specials = specials.Text.Replace(".", " ");

			string body = "{" + JsonConvert.SerializeObject(request) + "}";

			HttpClient client = new HttpClient();

			client.MaxResponseContentBufferSize = 512000;

			try
			{
				var uri = new Uri(String.Format(Constants.ServiceUrl + "/sendmail/" + body + "/", string.Empty));
				var response = client.GetAsync(uri).Result;
				if (response.IsSuccessStatusCode)
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

		public FlightRequestPage()
		{
			Grid grid = new Grid();

			InitializeComponent();
			search = new Entry();
			start = new DatePicker();
			end = new DatePicker();
			startTime = new TimePicker();
			endTime = new TimePicker();
			purpose = new Editor();
			purpose.HeightRequest = 100;
			requestor = new Entry();
			numPax = new Picker();
			rentalCar = new Switch();
			specials = new Editor();
			specials.HeightRequest = 100;

			//when a user selects a start date change the return date to match.
			start.DateSelected += (sender, e) => { end.Date = start.Date; };

			for (int i = 1; i <= 8; i++)
				numPax.Items.Add(i.ToString());

			RowDefinition row1 = new RowDefinition();
			RowDefinition row2 = new RowDefinition();
			RowDefinition row3 = new RowDefinition();
			RowDefinition row4 = new RowDefinition();
			RowDefinition row5 = new RowDefinition();
			RowDefinition row6 = new RowDefinition();
			RowDefinition row7 = new RowDefinition();
			RowDefinition row8 = new RowDefinition();
			RowDefinition row9 = new RowDefinition();
			RowDefinition row10 = new RowDefinition();

			row1.Height = 40;
			row2.Height = 40;
			row3.Height = 40;
			row4.Height = 40;
			row5.Height = 40;
			row6.Height = 40;
			row7.Height = 40;
			row9.Height = 40;

			grid.RowDefinitions.Add(row1);
			grid.RowDefinitions.Add(row2);
			grid.RowDefinitions.Add(row3);
			grid.RowDefinitions.Add(row4);
			grid.RowDefinitions.Add(row5);
			grid.RowDefinitions.Add(row6);
			grid.RowDefinitions.Add(row7);
			grid.RowDefinitions.Add(row8);
			grid.RowDefinitions.Add(row9);
			grid.RowDefinitions.Add(row10);

			start.Format = "ddd MMM dd";
			end.Format = "ddd MMM dd";

			search.Placeholder = "Search for address...";
			search.Focused += Search_Focused;
			search.HeightRequest = 30;
			start.HeightRequest = 20;
			end.HeightRequest = 20;
			//start.SetValue();

			grid.Children.Add(search, 0, 0);
			grid.Children.Add(new Label { Text = "Departs:", VerticalOptions = LayoutOptions.Center }, 0, 1);
			grid.Children.Add(start, 1, 1);
			grid.Children.Add(startTime, 2, 1);
			grid.Children.Add(new Label { Text = "Returns:", VerticalOptions = LayoutOptions.Center }, 0, 2);
			grid.Children.Add(end, 1, 2);
			grid.Children.Add(endTime, 2, 2);
			grid.Children.Add(new Label { Text = "Requestor:", VerticalOptions = LayoutOptions.Center }, 0, 3);
			grid.Children.Add(requestor, 1, 3);
			grid.Children.Add(new Label { Text = "# of Passengers:", VerticalOptions = LayoutOptions.Center }, 0, 4);
			grid.Children.Add(numPax, 2, 4);
			grid.Children.Add(new Label { Text = "Rental Car:", VerticalOptions = LayoutOptions.Center }, 0, 5);
			grid.Children.Add(rentalCar, 2, 5);

			//Special Requests
			grid.Children.Add(new Label { Text = "Special Requests (Catering, etc...):", VerticalOptions = LayoutOptions.Center }, 0, 6);
			StackLayout s1 = new StackLayout();
			s1.BackgroundColor = Color.Silver;
			s1.Padding = 1;
			s1.Children.Add(specials);
			grid.Children.Add(s1, 0, 7);

			//Purpose of trip
			grid.Children.Add(new Label { Text = "Purpose of Trip:", VerticalOptions = LayoutOptions.Center }, 0, 8);
			StackLayout s2 = new StackLayout();
			s2.BackgroundColor = Color.Silver;
			s2.Padding = 1;
			s2.Children.Add(purpose);
			grid.Children.Add(s2, 0, 9);

			//set column spans on fields requiring more than 1
			Grid.SetColumnSpan(search, 3);
			Grid.SetColumnSpan(requestor, 2);
			Grid.SetColumnSpan(s2, 3);
			Grid.SetColumnSpan(s1, 3);
			Grid.SetColumnSpan(grid.Children[9], 2);
			Grid.SetColumnSpan(grid.Children[13], 3);
			Grid.SetColumnSpan(grid.Children[15], 3);

			//layout.Children.Add(grid);
			ScrollView sv = new ScrollView { Orientation = ScrollOrientation.Vertical, HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand };
			sv.Content = grid;
			layout.Children.Add(sv);

			//Button bar ******************************************************

			Button submitFlightRequest = new Button();

			Button closePage = new Button { Image = "closePage.png" };
			closePage.BackgroundColor = Color.Transparent;
			submitFlightRequest.BackgroundColor = Color.Transparent;

			if (Device.OS == TargetPlatform.Android)
			{
				submitFlightRequest.Image = "newflight35.png";

				submitFlightRequest.HeightRequest = 35;
				submitFlightRequest.WidthRequest = 35;

				closePage.HeightRequest = 35;
				closePage.WidthRequest = 35;

				closePage.BorderColor = Color.Transparent;
				submitFlightRequest.BorderColor = Color.Transparent;
			}
			else if (Device.OS == TargetPlatform.iOS)
				submitFlightRequest.Image = "submit.png";

			submitFlightRequest.Clicked += ProcessRequest;
			closePage.Clicked += ClosePage_Clicked;


			buttonbar.Children.Add(closePage, 0, 0);
			buttonbar.Children.Add(submitFlightRequest, 1, 0);
		}
	}
}