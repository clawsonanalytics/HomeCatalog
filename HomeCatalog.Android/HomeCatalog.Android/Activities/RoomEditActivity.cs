using System;
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
	[Activity (Label = "RoomEditActivity")]			
	public class RoomEditActivity : Activity
	{
		private Property Property { get; set; }
		private int roomID { get; set; }
		private Room room { get; set; }

		private EditText roomLabelField { get; set; }

		private TextView labelTest { get; set; }

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			int roomID = Intent.GetIntExtra ("roomID",0);
			Property = PropertyStore.CurrentStore.Property;

			SetContentView (Resource.Layout.RoomEditLayout);

			roomLabelField = FindViewById<EditText> (Resource.Id.roomField);
			//room = Property.RoomList.RoomWithID (roomID);
			roomLabelField.Text = Property.RoomList.RoomWithID (roomID).Label;

			labelTest = FindViewById<TextView> (Resource.Id.labelTest);

			Button saveButton = FindViewById<Button> (Resource.Id.saveRoomLabelButton);
			saveButton.Click += (sender, e) => 
			{
				//Property.RoomList.RoomWithID (roomID).Label = roomLabelField.Text;
				//Finish ();
				room = Property.RoomList.RoomWithID (roomID);
				room.Label = roomLabelField.Text;
				labelTest.Text = room.Label;
				Finish ();
			};

			Button deleteButton = FindViewById<Button> (Resource.Id.deleteRoomButton);
			deleteButton.Click += (sender, e) => 
			{
				Property.RoomList.Remove (Property.RoomList.RoomWithID (roomID));
				Finish ();
			};


			Button cancelButton = FindViewById<Button> (Resource.Id.cancelRoomEditButton);
			cancelButton.Click += (sender, e) => 
			{
				Finish ();
			};



		}


	}
}

