// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace RainyShinyCloudyTake2
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIStackView bigStackView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIStackView leftStackView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIStackView rightStackView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (bigStackView != null) {
                bigStackView.Dispose ();
                bigStackView = null;
            }

            if (leftStackView != null) {
                leftStackView.Dispose ();
                leftStackView = null;
            }

            if (rightStackView != null) {
                rightStackView.Dispose ();
                rightStackView = null;
            }
        }
    }
}