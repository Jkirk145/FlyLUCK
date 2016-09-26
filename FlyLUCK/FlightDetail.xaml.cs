using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace FlyLUCK
{
	public partial class FlightDetail : ContentPage
	{
		void Handle_Clicked(object sender, System.EventArgs e)
		{
			this.Navigation.PopModalAsync();
		}

		void OpenMaps(object sender, System.EventArgs e)
		{
			string url = "http://maps.apple.com/?daddr=1001%20Sycolin%20Rd,%20Leesburg,%20VA";
			url = url.Replace(" ", "%20");
			Device.OpenUri(new Uri(url));

		}

		void SendMessage(object sender, EventArgs e)
		{
			SendMessage sm = new FlyLUCK.SendMessage();
			AbsoluteLayout layout = new AbsoluteLayout();
			Image blurImage = new Image { Source = "blur.png" };

			Frame frame = new Frame();
			Grid grd = new Grid();
			Editor messageText = new Editor();
			messageText.HorizontalOptions = LayoutOptions.FillAndExpand;
			messageText.VerticalOptions = LayoutOptions.FillAndExpand;
			StackLayout lb = new StackLayout();
			lb.BackgroundColor = Color.Silver;
			lb.Opacity = 0.7;
			lb.Padding = 1;
			lb.Children.Add(messageText);
			Button send = new Button { Text = "Send" };
			Button cancel = new Button { Text = "Cancel" };
			cancel.Clicked += (s, a) => { this.Navigation.PopModalAsync();};
			send.Clicked += (s, a) => { DisplayAlert("Success", "Your message has been sent!", "OK"); };
			RowDefinition row = new RowDefinition { Height = 200 };
			RowDefinition row2 = new RowDefinition();
			ColumnDefinition col1 = new ColumnDefinition();
			ColumnDefinition col2 = new ColumnDefinition();
			grd.RowDefinitions.Add(row);
			grd.RowDefinitions.Add(row2);
			grd.ColumnDefinitions.Add(col1);
			grd.ColumnDefinitions.Add(col2);
			grd.Children.Add(lb, 0, 0);
			Grid.SetColumnSpan(lb, 2);
			grd.Children.Add(send, 0, 1);
			grd.Children.Add(cancel, 1, 1);
			frame.Content = grd;


			layout.Children.Add(blurImage);
			layout.Children.Add(frame, new Rectangle(40, 150, 300, 300));
			sm.Content = layout;
			this.Navigation.PushModalAsync(sm,false);
		}

		public FlightDetail()
		{
			InitializeComponent();

			//Call to retrieve flight details
			//TODO: Insert call to database here...

			//From-To section**************************************************
			Label origin = new Label { Text = "OFP" };
			Image plane = new Image { Source = "airplane.png"};
			Label dest = new Label { Text = "JYO" };
			origin.HorizontalTextAlignment = TextAlignment.Start;
			origin.VerticalTextAlignment = TextAlignment.Center;
			origin.FontSize = 28;

			plane.HorizontalOptions = LayoutOptions.CenterAndExpand;

			dest.HorizontalTextAlignment = TextAlignment.End;
			dest.VerticalTextAlignment = TextAlignment.Center;
			dest.FontSize = 28;

			DetailGrid.Children.Add(origin, 0, 0);
			DetailGrid.Children.Add(plane, 2, 0);
			DetailGrid.Children.Add(dest, 3, 0);

			Grid.SetColumnSpan(origin, 2);
			Grid.SetColumnSpan(dest, 2);

			//City pairs ******************************************************
			Label originCity = new Label { Text = "Hanover County" };
			Label destCity = new Label { Text = "Leesburg Executive" };
			originCity.HorizontalTextAlignment = TextAlignment.Start;
			originCity.VerticalTextAlignment = TextAlignment.Center;
			originCity.FontSize = 12;

			destCity.HorizontalTextAlignment = TextAlignment.End;
			destCity.VerticalTextAlignment = TextAlignment.Center;
			destCity.FontSize = 12;

			DetailGrid.Children.Add(originCity, 0, 1);
			DetailGrid.Children.Add(destCity, 3, 1);

			Grid.SetColumnSpan(originCity, 2);
			Grid.SetColumnSpan(destCity, 2);

			//Time Section ****************************************************
			Label deptLabel = new Label { Text = "Departs" };
			Label arrLabel = new Label { Text = "Arrives" };
			Label deptTime = new Label { Text = "08:00 AM" };
			Label arrTime = new Label { Text = "08:30 AM" };


			arrLabel.HorizontalTextAlignment = TextAlignment.End;
			arrTime.HorizontalTextAlignment = TextAlignment.End;

			StackLayout deptLayout = new StackLayout();
			deptLayout.Orientation = StackOrientation.Vertical;
			deptLayout.Children.Add(deptLabel);
			deptLayout.Children.Add(deptTime);

			DetailGrid.Children.Add(deptLayout, 0, 2);
			Grid.SetColumnSpan(deptLayout, 2);

			StackLayout arrLayout = new StackLayout();
			arrLayout.Orientation = StackOrientation.Vertical;
			arrLayout.Children.Add(arrLabel);
			arrLayout.Children.Add(arrTime);

			DetailGrid.Children.Add(arrLayout, 3, 2);
			Grid.SetColumnSpan(arrLayout, 2);

			//Crew Line separator ************************************************

			Label crewLabel = new Label { Text = "Flight Crew" };
			crewLabel.HorizontalTextAlignment = TextAlignment.Start;
			crewLabel.VerticalTextAlignment = TextAlignment.Center;
			crewLabel.FontSize = 16;
			crewLabel.TextColor = Color.FromRgb(92, 134, 79);
			crewLabel.FontAttributes = FontAttributes.Bold;

			DetailGrid.Children.Add(crewLabel, 0, 4);
			Grid.SetColumnSpan(crewLabel, 5);

			Label lineLabel = new Label { Text = " " };
			lineLabel.BackgroundColor = Color.Silver;
			DetailGrid.Children.Add(lineLabel, 0, 3);
			Grid.SetColumnSpan(lineLabel, 5);

			//Crew Section ******************************************************

			Label pilot1 = new Label { Text = "Ryan Blanchard" };
			Label pilot1Phone = new Label { Text = "(804) 380-0451" };
			Label pilot2 = new Label { Text = "John Kirksey" };
			Label pilot2Phone = new Label { Text = "(804) 248-0700" };

			pilot1.HorizontalTextAlignment = TextAlignment.Start;
			pilot2.HorizontalTextAlignment = TextAlignment.Start;
			pilot2.VerticalTextAlignment = TextAlignment.Start;
			pilot1Phone.HorizontalTextAlignment = TextAlignment.End;
			pilot2Phone.HorizontalTextAlignment = TextAlignment.End;
			pilot2Phone.VerticalTextAlignment = TextAlignment.Start;

			pilot1.FontSize = 14;
			pilot1Phone.FontSize = 14;
			pilot2.FontSize = 14;
			pilot2Phone.FontSize = 14;

			DetailGrid.Children.Add(pilot1, 0, 5);
			DetailGrid.Children.Add(pilot1Phone, 3, 5);
			DetailGrid.Children.Add(pilot2, 0, 6);
			DetailGrid.Children.Add(pilot2Phone, 3, 6);

			Grid.SetColumnSpan(pilot1, 2);
			Grid.SetColumnSpan(pilot1Phone, 2);
			Grid.SetColumnSpan(pilot2, 2);
			Grid.SetColumnSpan(pilot2Phone, 2);

			//Passengers Label ************************************************
			Label paxLabel = new Label { Text = "Passenger List" };
			paxLabel.HorizontalTextAlignment = TextAlignment.Start;
			paxLabel.VerticalTextAlignment = TextAlignment.Center;
			paxLabel.FontSize = 16;
			paxLabel.FontAttributes = FontAttributes.Bold;
			paxLabel.TextColor = Color.FromRgb(92, 134, 79);;

			DetailGrid.Children.Add(paxLabel, 0, 8);
			Grid.SetColumnSpan(paxLabel, 5);

			Label lineLabel2 = new Label { Text = " " };
			lineLabel2.BackgroundColor = Color.Silver;
			DetailGrid.Children.Add(lineLabel2, 0, 7);
			Grid.SetColumnSpan(lineLabel2, 5);

			//Destination Info Section ****************************************
			Label destLabel = new Label { Text = "Destination Information" };
			destLabel.HorizontalTextAlignment = TextAlignment.Start;
			destLabel.FontSize = 16;
			destLabel.FontAttributes = FontAttributes.Bold;
			destLabel.TextColor = Color.FromRgb(92, 134, 79);

			DetailGrid.Children.Add(destLabel, 0, 11);
			Grid.SetColumnSpan(destLabel, 5);

			Label lineLabel3 = new Label { Text = " " };
			lineLabel3.BackgroundColor = Color.Silver;
			DetailGrid.Children.Add(lineLabel3, 0, 10);
			Grid.SetColumnSpan(lineLabel3, 5);

			StackLayout addressLayout = new StackLayout();
			Label destAddr = new Label { Text = "ProJet Aviation" };
			Label destAddr1 = new Label { Text = "1001 Sycolin Road" };
			Label destAddr2 = new Label { Text = "Leesburg, VA 20175" };

			destAddr.FontSize = 14;
			destAddr1.FontSize = 14;
			destAddr2.FontSize = 14;

			addressLayout.Orientation = StackOrientation.Vertical;
			addressLayout.Children.Add(destAddr);
			addressLayout.Children.Add(destAddr1);
			addressLayout.Children.Add(destAddr2);

			DetailGrid.Children.Add(addressLayout, 0, 12);
			Grid.SetColumnSpan(addressLayout, 3);


			/*Button openMaps = new Button();
			openMaps.Image = "map_marker.png";
			openMaps.Clicked += OpenMaps;
			DetailGrid.Children.Add(openMaps, 3, 12);
			Grid.SetColumnSpan(openMaps, 2);*/
			//Map *************************************************************
			var map = new Map(
			MapSpan.FromCenterAndRadius(
					new Position(39.0682111, -77.5547217), Distance.FromMiles(1.0)))
			{
				IsShowingUser = true,
				HeightRequest = 100,
				WidthRequest = 100,
				VerticalOptions = LayoutOptions.FillAndExpand
			};
			map.MapType = MapType.Street;

			var position = new Position(39.0682111, -77.5547217); // Latitude, Longitude
			var pin = new Pin
			{
				Type = PinType.Place,
				Position = position,
				Label = "JYO",
				Address = "1001 Sycolin Rd, Leesburg, VA"
			};
			pin.Clicked += OpenMaps;
			map.Pins.Add(pin);

			DetailGrid.Children.Add(map, 3, 12);
			Grid.SetColumnSpan(map, 2);

			//Button bar ******************************************************
			Button cancelFlight = new Button { Image = "cancelFlight.png", Text = "Delete" };
			Button closePage = new Button { Text = "Close", Image = "closePage.png" };
			Button sendCrewMessage = new Button { Text = "Message", Image = "sendMessage2.png" };

			closePage.Clicked += Handle_Clicked;
			sendCrewMessage.Clicked += SendMessage;

			DetailGrid.Children.Add(closePage, 0, 14);
			DetailGrid.Children.Add(sendCrewMessage, 2, 14);
			DetailGrid.Children.Add(cancelFlight, 4, 14);


		}
	}
}
