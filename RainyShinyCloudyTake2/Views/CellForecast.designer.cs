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
    [Register ("CellForecast")]
    partial class CellForecast
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgWeatherType { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblDay { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblTempHigh { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblTempLow { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblWeatherType { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (imgWeatherType != null) {
                imgWeatherType.Dispose ();
                imgWeatherType = null;
            }

            if (lblDay != null) {
                lblDay.Dispose ();
                lblDay = null;
            }

            if (lblTempHigh != null) {
                lblTempHigh.Dispose ();
                lblTempHigh = null;
            }

            if (lblTempLow != null) {
                lblTempLow.Dispose ();
                lblTempLow = null;
            }

            if (lblWeatherType != null) {
                lblWeatherType.Dispose ();
                lblWeatherType = null;
            }
        }
    }
}