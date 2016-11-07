using Android.Locations;
using Java.Util;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace AudioTour
{
    public class MapScreenApp : Application
    {
        Map map;
        Plugin.Geolocator.Abstractions.Position currentPosition;
        Pin currentPin;

        public MapScreenApp()
        {

            fillPosition();
          
            map = new Map
            {
                IsShowingUser = true,
                HeightRequest = 100,
                WidthRequest = 960,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            // You can use MapSpan.FromCenterAndRadius 
            //map.MoveToRegion (MapSpan.FromCenterAndRadius (new Position (37, -122), Distance.FromMiles (0.3)));
            // or create a new MapSpan object directl           

        
            // add the slider
            var slider = new Slider(1, 18, 1);
            slider.ValueChanged += (sender, e) => {
                var zoomLevel = e.NewValue; // between 1 and 18
                var latlongdegrees = 360 / (Math.Pow(2, zoomLevel));

                if (map.VisibleRegion != null)
                    map.MoveToRegion(new MapSpan(map.VisibleRegion.Center, latlongdegrees, latlongdegrees));
            };

            currentPin = new Pin();
            currentPin.Type = PinType.Generic;
            currentPin.Label = "Você";

            map.Pins.Add(currentPin);


            var pin = new Pin
            {
                Type = PinType.Place,
                Position = new Position(-23.519861, -46.404082),
                Label = "Casa",
                Address = "Rua Manuel Soares de Medeiros"
            };

            map.Pins.Add(pin);


            // put the page together
            var stack = new StackLayout { Spacing = 0 };
            stack.Children.Add(map);
            stack.Children.Add(slider);

            MainPage = new ContentPage{
                Content = stack
            };

            // for debugging output only
            map.PropertyChanged += (sender, e) => {
                if (e.PropertyName == "VisibleRegion" && map.VisibleRegion != null)
                    CalculateBoundingCoordinates(map.VisibleRegion);
            };

        }

        async void fillPosition()
        {
            var locator = CrossGeolocator.Current;

            locator.DesiredAccuracy = 100; //100 is new default

            currentPosition = await locator.GetPositionAsync(timeoutMilliseconds: 10000);

            Position p = new Position(currentPosition.Latitude, currentPosition.Longitude);

            map.MoveToRegion(MapSpan.FromCenterAndRadius(p, Distance.FromMiles(0.3)));

            map.Pins.First().Position = p;

        }

        /// <summary>
        /// In response to this forum question http://forums.xamarin.com/discussion/22493/maps-visibleregion-bounds
        /// Useful if you need to send the bounds to a web service or otherwise calculate what
        /// pins might need to be drawn inside the currently visible viewport.
        /// </summary>
        static void CalculateBoundingCoordinates(MapSpan region)
        {
            // WARNING: I haven't tested the correctness of this exhaustively!
            var center = region.Center;
            var halfheightDegrees = region.LatitudeDegrees / 2;
            var halfwidthDegrees = region.LongitudeDegrees / 2;

            var left = center.Longitude - halfwidthDegrees;
            var right = center.Longitude + halfwidthDegrees;
            var top = center.Latitude + halfheightDegrees;
            var bottom = center.Latitude - halfheightDegrees;

            // Adjust for Internation Date Line (+/- 180 degrees longitude)
            if (left < -180) left = 180 + (180 + left);
            if (right > 180) right = (right - 180) - 180;
            // I don't wrap around north or south; I don't think the map control allows this anyway
        }
    }
}
