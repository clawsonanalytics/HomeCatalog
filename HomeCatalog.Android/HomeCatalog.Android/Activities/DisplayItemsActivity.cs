using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using HomeCatalog.Core;

namespace HomeCatalog.Android
{
	[Activity (Label = "View Items")]
	public class DisplayItemsActivity : StandardActivity
	{
		private ItemListAdapter ListAdapter { get; set; }

		private Property Property { get; set; }

		private int selectedItem { get; set; }

		private Spinner sortSpinner { get; set; }

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			Property = PropertyStore.CurrentStore.Property;

			SetContentView (Resource.Layout.DisplayItemsView);

			ListView listView = FindViewById<ListView> (Resource.Id.itemsList);
			//ListAdapter = new ItemListAdapter (this,Property);
			//listView.Adapter = ListAdapter;



			Button additemButton = FindViewById<Button> (Resource.Id.addItemButton);
			additemButton.Click += (sender, e) => {
				StartActivityForResult (typeof(AddItemActivity), 0);
			};

			Spinner sortSpinner = FindViewById<Spinner> (Resource.Id.sortSpinner);
			//sortSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs> (sortSpinner.ItemSelected);
			var adapter = ArrayAdapter.CreateFromResource (
				              this, Resource.Array.SortOptions, Android.Resource.Layout.GenericSpinnerItem);

			adapter.SetDropDownViewResource (Android.Resource.Layout.GenericSpinnerItem);

			sortSpinner.Adapter = adapter;
			sortSpinner.ItemSelected += (sender, e) => {
				ListAdapter = new ItemListAdapter (this, Property, e.Position);
				listView.Adapter = ListAdapter;
			};

			listView.ItemClick += (sender, e) => {
				var ItemRequest = new Intent (this, typeof(AddItemActivity));
				ItemRequest.PutExtra (Item.ItemIDKey, ListAdapter [e.Position].ID);
				StartActivityForResult (ItemRequest, 0);
			};
		}

		public void showItem (AdapterView.ItemClickEventArgs e)
		{
			var ItemRequest = new Intent (this, typeof(ItemsDetailActivity));
			ItemRequest.PutExtra (Item.ItemIDKey, ListAdapter [e.Position].ID);
			StartActivity (ItemRequest);
		}

		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult (requestCode, resultCode, data);
			ListAdapter.NotifyDataSetChanged ();

		}
	}
}


