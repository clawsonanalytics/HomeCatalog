using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HomeCatalog.Core;

namespace HomeCatalog.Android
{
	[Activity (Label = "PhotoBrowserActivity")]			
	public class PhotoBrowserActivity : Activity
	{

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.PhotoBrowserView);

			GridView photoGridView = FindViewById<GridView> (Resource.Id.photoBrowserGridView);
			photoGridView.Adapter = new ImageAdapter (this);

			photoGridView.ItemClick += (sender, e) => 
			{

			};

			string[] optionList = {"Camera","Gallery"};

			AlertDialog.Builder builder = new AlertDialog.Builder (this);
			builder.SetTitle ("Add Photo By:");
			builder.SetItems (optionList, async delegate(object sender, DialogClickEventArgs e) {
				e.Which;
			});

			AlertDialog popUpMenu = builder.Create ();

			Button addPhotoButton = FindViewById<Button> (Resource.Id.addPhotoButton);
			addPhotoButton.Click += (sender, e) => 
			{
				// Alert Dialog to select adding by camera or adding existing photo from phone gallery
			};



		}
	}
}

