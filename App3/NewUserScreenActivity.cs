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

namespace AudioTour
{
    [Activity(Label = "Novo Usuário")]
    public class NewUserScreenActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the layout resource
            SetContentView(Resource.Layout.NewUserScreen);

            
        }
    }
}