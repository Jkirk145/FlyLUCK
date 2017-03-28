using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Net.Http;

namespace FlyLUCK
{
	public partial class SearchPlaces : ContentPage
	{

		public event EventHandler PlaceSelected;

		private void OnPlaceSelected()
		{
			if (PlaceSelected != null)
				PlaceSelected(this, EventArgs.Empty);
		}

		public void Placelist_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{

			this.PlaceSelected(this, e);
			this.Navigation.PopModalAsync();
		}

		public void HandlePlaceSelected(object sender, EventArgs e)
		{
			this.Navigation.PopModalAsync();
		}

		public SearchPlaces()
		{
			InitializeComponent();
			placelist.ItemSelected += Placelist_ItemSelected;
			search.TextChanged += Search_TextChanged;
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			search.Focus();
		}

		void Search_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (search.Text.Length > 3)
				Search();
		}

		private void Search()
		{
			GPlaceApi gp = new GPlaceApi();

			string pred = gp.GetPredictions(search.Text);

			Predictions predictions = JsonConvert.DeserializeObject<Predictions> (pred);

			placelist.ItemsSource = predictions.places;
		}

	}
}
