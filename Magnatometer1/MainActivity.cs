using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Util;
using AndroidX.AppCompat.App;
using System;
using Xamarin.Essentials;

namespace Magnatometer1
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            new MagnetometerTest();
        }

       
        public class MagnetometerTest
        {

            SensorSpeed speed = SensorSpeed.UI;

            public MagnetometerTest()
            {

                Magnetometer.ReadingChanged += Magnetometer_ReadingChanged;
                ToggleMagnetometer();
            }

            void Magnetometer_ReadingChanged(object sender, MagnetometerChangedEventArgs e)
            {
                var data = e.Reading;

                Console.WriteLine($"Reading: X: {data.MagneticField.X}, Y: {data.MagneticField.Y}, Z: {data.MagneticField.Z}");

            }

            public void ToggleMagnetometer()
            {
                try
                {
                    if (Magnetometer.IsMonitoring)
                        Magnetometer.Stop();
                    else
                        Magnetometer.Start(speed);
                }
                catch (FeatureNotSupportedException fnsEx)
                {
                    Log.Debug("features is not support", fnsEx.Message);
                }
                catch (Exception ex)
                {
                    Log.Debug("feature is not support", ex.Message);
                }
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}