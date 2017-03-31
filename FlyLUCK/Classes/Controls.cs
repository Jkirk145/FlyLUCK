using System;
using Xamarin.Forms;
using RoundedBoxView.Forms.Plugin.Abstractions;


namespace FlyLUCK
{
	public class DetailViewLabel : Label
	{
		
	}

	public class CalendarBadge : Frame
	{
		Grid dayGrid;
		StackLayout layout;
		Label _date;
		RoundedBoxView.Forms.Plugin.Abstractions.RoundedBoxView _box;

		public CalendarBadge(string day, Color c)
		{
			Padding = 0;

			_date = new Label();

			layout = new StackLayout();

			_date.Text = day;
			_date.TextColor = Color.Black;
			_date.BackgroundColor = Color.FromHex("cdcdcd");
			_date.HorizontalTextAlignment = TextAlignment.Center;

			layout.Children.Add(_date);
			//layout.Children.Add(new Label { Text = "X", TextColor = c, BackgroundColor=Color.Silver, HorizontalTextAlignment=TextAlignment.Center });
			_box = new RoundedBoxView.Forms.Plugin.Abstractions.RoundedBoxView();
			_box.BackgroundColor = c;
			_box.BorderThickness = 0;
			_box.WidthRequest = 20;
			_box.HeightRequest = 20;
			_box.CornerRadius = 40;
			layout.Children.Add(_box);

			Content = layout;
			HasShadow = false;
			WidthRequest = 30;
			HeightRequest = 60;
			BackgroundColor = Color.Transparent;


			var tapped = new TapGestureRecognizer();
			tapped.Tapped += (s, e) =>
			{
				//OnTapped(s, e);
				Page p = new Page();
				p.DisplayAlert("ALERT", e.ToString(), "Close");
			};
			this.GestureRecognizers.Add(tapped);
		}



	}

	public class CardView : Frame
	{
		private string _origin = "";
		private string _destination = "";
		private string _date = "";
		private string _time = "";

		public CardView()
		{
		}

		public CardView(string origin, string destination, string deptDate, string deptTime)
		{
			Padding = 0;
			_origin = origin;
			_destination = destination;
			_date = deptDate;
			_time = deptTime;

			StackLayout layout = new StackLayout();

			if (Device.OS == TargetPlatform.iOS)
			{

				Label dateLabel = new Label { Text = "Date: " + _date };
				Label timeLabel = new Label { Text = "Departure Time: " + _time};
				Label citiesLabel = new Label { Text = "From: " + _origin + " To: " + _destination, FontSize = 24 };

				layout.Children.Add(dateLabel);
				layout.Children.Add(citiesLabel);
				layout.Children.Add(timeLabel);
				Content = layout;

				Padding = 20;
				HasShadow = false;
				OutlineColor = Color.Black;
				BackgroundColor = Color.Transparent;
				HorizontalOptions = LayoutOptions.FillAndExpand;

			}
		}


		private void BuildView()
		{
			
		}
	}
}

