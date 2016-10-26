using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using AudioTour;

namespace AudioTour
{
    [Activity(Label = "AudioTour", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button buttonAT = FindViewById<Button>(Resource.Id.ButtonMain);

            buttonAT.Click += delegate {
                StartActivity(typeof(AudioTourScreenActivity));
            };

            Button buttonNU = FindViewById<Button>(Resource.Id.ButtonAddNewUser);

            buttonNU.Click += delegate {
                StartActivity(typeof(NewUserScreenActivity));
            };

            Button buttonVM = FindViewById<Button>(Resource.Id.btnMap);

            buttonVM.Click += delegate {
                StartActivity(typeof(MapScreenActivity));
            };
        }
    }
}

