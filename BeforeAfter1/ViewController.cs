using CoreGraphics;
using CoreImage;
using Foundation;
using System;

using UIKit;

namespace BeforeAfter1
{
    public partial class ViewController : UIViewController
    {
        UIImagePickerController imagePicker;
        UIImage originalImage;
        CIContext context;
       
        
        public ViewController(IntPtr handle) : base(handle)
        {
            
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
           
            choosePhotoButton.TouchUpInside += (s, e) =>
            {
                // create a new picker controller
                imagePicker = new UIImagePickerController();

                // set our source to the photo library
                imagePicker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;

                // set what media types
                imagePicker.MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.PhotoLibrary);

                imagePicker.FinishedPickingMedia += Handle_FinishedPickingMedia;
                //imagePicker.Canceled += Handle_Canceled;

                // show the picker
                NavigationController.PresentModalViewController(imagePicker, true);
                //UIPopoverController picc = new UIPopoverController(imagePicker);

            };
        }
        

        protected void Handle_FinishedPickingMedia(object sender, UIImagePickerMediaPickedEventArgs e)
        {
            // determine what was selected, video or image
            bool isImage = false;
            switch (e.Info[UIImagePickerController.MediaType].ToString())
            {
                case "public.image":
                    Console.WriteLine("Image selected");
                    isImage = true;
                    break;

                case "public.video":
                    Console.WriteLine("Video selected");
                    break;
            }

            Console.Write("Reference URL: [" + UIImagePickerController.ReferenceUrl + "]");

            // get common info (shared between images and video)
            NSUrl referenceURL = e.Info[new NSString("UIImagePickerControllerReferenceUrl")] as NSUrl;
            if (referenceURL != null)
                Console.WriteLine(referenceURL.ToString());

            // if it was an image, get the other image info
            if (isImage)
            {

                // get the original image
                originalImage = e.Info[UIImagePickerController.OriginalImage] as UIImage;
                if (originalImage != null)
                {
                    // do something with the image
                    Console.WriteLine("got the original image");
                    imageView.Image = originalImage;
                    imageView.Alpha = .3f;
                }

                // get the edited image
                UIImage editedImage = e.Info[UIImagePickerController.EditedImage] as UIImage;
                if (editedImage != null)
                {
                    // do something with the image
                    Console.WriteLine("got the edited image");
                    imageView.Image = editedImage;
                }

                //- get the image metadata
                NSDictionary imageMetadata = e.Info[UIImagePickerController.MediaMetadata] as NSDictionary;
                if (imageMetadata != null)
                {
                    // do something with the metadata
                    Console.WriteLine("got image metadata");
                }

            }
            // if it's a video
            else
            {
                // get video url
                NSUrl mediaURL = e.Info[UIImagePickerController.MediaURL] as NSUrl;
                if (mediaURL != null)
                {
                    //
                    Console.WriteLine(mediaURL.ToString());
                }
            }

            // dismiss the picker
            imagePicker.DismissModalViewController(true);
        }
        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            var destination = (Show)segue.DestinationViewController;
            destination.originalImage = imageView.Image;
            //destination.DoSomething();
        }

       

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}