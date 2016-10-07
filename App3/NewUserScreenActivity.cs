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

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core;
using System.Threading.Tasks;

namespace AudioTour
{
    [Activity(Label = "Novo Usuário")]
    public class NewUserScreenActivity : Activity
    {
        EditText txtName, txtEmail, txtLocation;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the layout resource
            SetContentView(Resource.Layout.NewUserScreen);

            //Get elements from the layout
            txtName = FindViewById<EditText>(Resource.Id.txtName);
            txtEmail = FindViewById<EditText>(Resource.Id.txtEmail);
            txtLocation = FindViewById<EditText>(Resource.Id.txtLocation);

        }

        private async void EventClickAddUser(object sender, EventArgs ea)
        {
            DatabaseConnection db = new DatabaseConnection();
            await db.addNewUser(txtName.Text, txtEmail.Text);
        }
    }
}