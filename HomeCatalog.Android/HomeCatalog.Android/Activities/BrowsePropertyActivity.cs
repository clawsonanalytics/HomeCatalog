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
	[Activity (Label = "HomeCatalog.Android", MainLauncher = true)]
	public class BrowsePropertyActivity : Activity
	{
		private PropertyListAdapter ListAdapter { get; set; }
		private enum PropertyRequest
		{
			ADD_PROPERTY
		}

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			for (int i=0; i < 5; i++) {
				Property property = new Property();
				property.PropertyName = "Property" + i;
				PropertyCollection.SharedCollection.AddProperty (property);
			}

			SetContentView (Resource.Layout.MainView);

			ListView listView = FindViewById<ListView> (Resource.Id.propertyList);
			ListAdapter = new PropertyListAdapter (this);
			listView.Adapter = ListAdapter;

			Button AddPropertyButton = FindViewById<Button> (Resource.Id.AddPropertyButton);


			AddPropertyButton.Click += (sender,e) => {

				Property property = new Property();
				PropertyCollection.SharedCollection.AddProperty(property);
				Intent PassPropertyID = new Intent(this,typeof(AddEditPropertyActivity));
				PassPropertyID.PutExtra (Property.PropertyIDKey,property.PropertyID);
				StartActivityForResult (PassPropertyID, (int)PropertyRequest.ADD_PROPERTY);
			};
		}

		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult (requestCode, resultCode, data);

			if (requestCode == (int)PropertyRequest.ADD_PROPERTY) {
				ListAdapter.NotifyDataSetChanged ();
			}
		}

//		protected override void OnListItemClick (ListView l, View v, int position, long id)
//		{
//			Property Property = PropertyCollection.SharedCollection [position];
//			//Toast.MakeText (this, t, ToastLength.Short).Show ();
//			Intent PassPropertyID = new Intent (this, typeof(PropertyDetailActivity));
//			PassPropertyID.PutExtra (Property.PropertyIDKey, Property.PropertyID);
//			StartActivity (PassPropertyID);
//
//		}
	}
}


