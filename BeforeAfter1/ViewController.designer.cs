// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace BeforeAfter1
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton choosePhotoButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imageView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISlider sliderBrightness { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISlider sliderContrast { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISlider sliderSaturation { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (choosePhotoButton != null) {
                choosePhotoButton.Dispose ();
                choosePhotoButton = null;
            }

            if (imageView != null) {
                imageView.Dispose ();
                imageView = null;
            }

            if (sliderBrightness != null) {
                sliderBrightness.Dispose ();
                sliderBrightness = null;
            }

            if (sliderContrast != null) {
                sliderContrast.Dispose ();
                sliderContrast = null;
            }

            if (sliderSaturation != null) {
                sliderSaturation.Dispose ();
                sliderSaturation = null;
            }
        }
    }
}