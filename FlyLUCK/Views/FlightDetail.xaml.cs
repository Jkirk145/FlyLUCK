using System;
using System.Collections.Generic;
using Plugin.Messaging;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using FlyLUCK.ViewModels;

namespace FlyLUCK
{
	public partial class FlightDetail : ContentPage
	{
		Map map;
		Pin pin;
		string address;
		private string _crewcontact = "";
		private Flight _flight;
		private List<Passenger> _passengers;
		private CalendarViewModel vm { get; set; }

		void Handle_Clicked(object sender, System.EventArgs e)
		{
			this.Navigation.PopModalAsync();
		}

		void OpenMaps(object sender, System.EventArgs e)
		{
			string url = "";
			if (Device.RuntimePlatform == Device.iOS)
			{
				url = "http://maps.apple.com/?daddr=" + address;
			}
			else
			{
				url = "geo: 0,0 ? q = " + address;
			}
			
			Device.OpenUri(new Uri(url));

		}

		void SendPush(object sender, EventArgs e)
		{
			SendMessage sm = new SendMessage(_passengers);
			Navigation.PushModalAsync(sm);
			//call REST service to get crew info
			//HttpClient client = new HttpClient();
			//Uri url = new Uri(String.Format(Constants.ServiceUrl + "/sendmessage?message={\n    \"aps\" : { \"alert\" : \"Message received from Bob\" }}&tags=john.kirksey@luckcompanies.com", string.Empty));
			//var result = client.GetStringAsync(url);
		}

		void SendMessage(object sender, EventArgs e)
		{

			try
			{
				Device.OpenUri(new Uri(String.Format("sms:{0}", _crewcontact)));
			}
			catch (Exception ex)
			{
				DisplayAlert("Oops!", ex.ToString(), "Close");
			}
		}

		/*void SendMessage(object sender, EventArgs e)
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
			send.Clicked += (sndr, eva) => {
				var messenger = DependencyService.Get<IMessenger>();
				messenger.SendMessage(_crewcontact, "On our way back to the airport!");
				//var msg = new Messaging();
				//msg.SendMessage("18042480700");
				/*var smsMessage = MessagingPlugin.SmsMessenger;
				if (smsMessage.CanSendSms)
				{
					try
					{
						smsMessage.SendSms("+18042480700", messageText.Text);
						DisplayAlert("Success", "Your message has been sent!: " + messageText.Text, "OK");
					}
					catch (Exception ex)
					{
						DisplayAlert("ERROR!", ex.ToString(), "Close");
					}
				}
			};
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
		}*/

		public FlightDetail(Flight flt)
		{
			InitializeComponent();

			vm = new CalendarViewModel();
			BindingContext = vm;

			_flight = flt;

			LoadDetailData();


		}

		private async void LoadDetailData()
		{
			//Call to retrieve flight details
			//TODO: Insert call to database here...

			vm.IsLoading = true;

			//From-To section**************************************************
			Label origin = new Label { Text = _flight.ORIGIN };
			Image plane = new Image { Source = "airplane.png" };
			Label dest = new Label { Text = _flight.DEST };
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
			Label originCity = new Label { Text = _flight.FROMAIRPORTNAME };
			Label destCity = new Label { Text = _flight.TOAIRPORTNAME };
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
			Label deptTime = new Label { Text = _flight.LOCALLEAVE };
			Label arrTime = new Label { Text = _flight.LOCALARRIVE };

			deptTime.FontSize = 14;
			arrTime.FontSize = 14;
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

			Label lineLabel = new Label();
			lineLabel.BackgroundColor = Color.Silver;
			DetailGrid.Children.Add(lineLabel, 0, 3);
			Grid.SetColumnSpan(lineLabel, 5);

			//Crew Section ******************************************************


			//call REST service to get crew info
			HttpClient client = new HttpClient();
			Uri crewUrl = new Uri(String.Format(Constants.ServiceUrl + "/getcrew/" + _flight.LEGID, string.Empty));
			string _crewdata = await client.GetStringAsync(crewUrl);
			var crewobj = JsonConvert.DeserializeObject<List<Crew>>(_crewdata);


			Label pilot1 = new Label { Text = crewobj[0].NAME };
			Label pilot1Phone = new Label { Text = crewobj[0].CELLULAR };
			Label pilot2 = new Label { Text = crewobj[1].NAME };
			Label pilot2Phone = new Label { Text = crewobj[1].CELLULAR };

			_crewcontact = "sms:" + crewobj[0].CELLULAR;
			//_crewcontact = "sms:18042480700";

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

			Grid.SetColumnSpan(pilot1, 3);
			Grid.SetColumnSpan(pilot1Phone, 2);
			Grid.SetColumnSpan(pilot2, 3);
			Grid.SetColumnSpan(pilot2Phone, 2);

			//Passengers Label ************************************************


			//call REST service to get crew info
			Uri paxUrl = new Uri(String.Format(Constants.ServiceUrl + "/getpax/" + _flight.LEGID, string.Empty));
			string _paxdata = await client.GetStringAsync(paxUrl);
			_passengers = JsonConvert.DeserializeObject<List<Passenger>>(_paxdata);

			ListView paxListView = new ListView
			{
				RowHeight = 20,
				ItemsSource = _passengers,
				ItemTemplate = new DataTemplate(() =>
			   		{
						   Label lbl = new Label();
						   lbl.SetBinding(Label.TextProperty, "PAXNAME");
						   lbl.FontSize = 14;
						   return new ViewCell
						   {
							   View = new StackLayout
							   {
								   VerticalOptions = LayoutOptions.Center,
								   Spacing = 0,
								   Orientation = StackOrientation.Horizontal,
								   Children = { lbl }
							   }
						   };
					   })
			};


			Label paxLabel = new Label { Text = "Passenger List" };
			paxLabel.HorizontalTextAlignment = TextAlignment.Start;
			paxLabel.VerticalTextAlignment = TextAlignment.Center;
			paxLabel.FontSize = 16;
			paxLabel.FontAttributes = FontAttributes.Bold;
			paxLabel.TextColor = Color.FromRgb(92, 134, 79); ;

			DetailGrid.Children.Add(paxLabel, 0, 8);
			Grid.SetColumnSpan(paxLabel, 5);
			DetailGrid.Children.Add(paxListView, 0, 9);
			Grid.SetColumnSpan(paxListView, 5);
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
			Label destAddr = new Label { Text = _flight.FBONAME };
			Label destAddr1 = new Label { Text = _flight.FBOADDRESS1 };
			Label destAddr2 = new Label { Text = _flight.FBOCITY + ", " + _flight.FBOSTATE + " " + _flight.FBOZIP };

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
			address = _flight.FBOADDRESS1 + ", " + _flight.FBOCITY + ", " + _flight.FBOSTATE + " " + _flight.FBOZIP;
			double x = 0.0, y = 0.0;

			GetPosition(_flight.DEST);

			map = new Map(
			MapSpan.FromCenterAndRadius(
					new Position(x, y), Distance.FromMiles(2.0)))
			{
				IsShowingUser = true,
				HeightRequest = 160,
				WidthRequest = 160,
				VerticalOptions = LayoutOptions.FillAndExpand
			};
			map.MapType = MapType.Street;

			DetailGrid.Children.Add(map, 3, 12);
			Grid.SetColumnSpan(map, 2);

			//Button bar ******************************************************
			//Button cancelFlight = new Button { Image = "cancelFlight.png", Text = "Delete" };
			Button closePage = new Button { Image = "closePage.png" };
			Button sendCrewMessage = new Button { Image = "sendMessage2.png" };

			closePage.BackgroundColor = Color.Transparent;
			sendCrewMessage.BackgroundColor = Color.Transparent;


			closePage.Clicked += Handle_Clicked;
			sendCrewMessage.Clicked += SendMessage;

			buttonbar.Children.Add(closePage, 0, 0);
			buttonbar.Children.Add(sendCrewMessage, 1, 0);

			if (Helpers.Settings.FlightCrew)
			{
				Button sendPushNotification = new Button { Image = "message.png" };
				sendPushNotification.Clicked += SendPush;
				sendPushNotification.BackgroundColor = Color.Transparent;
				buttonbar.Children.Add(sendPushNotification, 2, 0);
			}

			vm.IsLoading = false;
		}

		public async void GetPosition(string pinLabel)
		{



			Geocoder gcoder = new Geocoder();
			double x = 0.0, y = 0.0;
			try
			{
				var posList = await gcoder.GetPositionsForAddressAsync(address);
				foreach (var p in posList)
				{
					x = p.Latitude;
					y = p.Longitude;
				}
				map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(x, y), Distance.FromMiles(2.0)));

				pin = new Pin
				{
					Type = PinType.Place,
					Position = new Position(x, y),
					Label = pinLabel,
					Address = address
				};

				pin.Clicked += OpenMaps;
				map.Pins.Add(pin);
			}
			catch (Exception ex)
			{
				await DisplayAlert("Oops", ex.ToString(), "Close");
			}

		}


	}
}
