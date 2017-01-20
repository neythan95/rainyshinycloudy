// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace RainyShinyCloudyTake2
{
    [Register ("WeatherVC")]
    partial class WeatherVC
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgWeatherType { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblCity { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblCurrentTemp { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblToday { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblWeatherType { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIStackView leftStackView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView tblForecast { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (imgWeatherType != null) {
                imgWeatherType.Dispose ();
                imgWeatherType = null;
            }

            if (lblCity != null) {
                lblCity.Dispose ();
                lblCity = null;
            }

            if (lblCurrentTemp != null) {
                lblCurrentTemp.Dispose ();
                lblCurrentTemp = null;
            }

            if (lblToday != null) {
                lblToday.Dispose ();
                lblToday = null;
            }

            if (lblWeatherType != null) {
                lblWeatherType.Dispose ();
                lblWeatherType = null;
            }

            if (leftStackView != null) {
                leftStackView.Dispose ();
                leftStackView = null;
            }

            if (tblForecast != null) {
                tblForecast.Dispose ();
                tblForecast = null;
            }
        }
    }
}