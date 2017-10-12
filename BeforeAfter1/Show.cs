using Foundation;
using System;
using UIKit;
using AVFoundation;
using System.Threading.Tasks;
using CoreGraphics;

namespace BeforeAfter1
{
    public partial class Show : UIViewController
    {
        AVCaptureSession captureSession;
        AVCaptureDeviceInput captureDeviceInput;
        AVCaptureStillImageOutput stillImageOutput;
        AVCaptureVideoPreviewLayer videoPreviewLayer;
        public UIImage originalImage;
        public Show (IntPtr handle) : base (handle)
        {
        }

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();

            await AuthorizeCameraUse();
            SetupLiveCameraStream();
        }
        async Task AuthorizeCameraUse()
        {
            var authorizationStatus = AVCaptureDevice.GetAuthorizationStatus(AVMediaType.Video);

            if (authorizationStatus != AVAuthorizationStatus.Authorized)
            {
                await AVCaptureDevice.RequestAccessForMediaTypeAsync(AVMediaType.Video);
            }
        }
        public void SetupLiveCameraStream()
        {
            
            
            captureSession = new AVCaptureSession();

            var viewLayer = liveCameraStream.Layer;
            videoPreviewLayer = new AVCaptureVideoPreviewLayer(captureSession)
            {
                Frame = this.View.Frame
            };
            var imageView = new UIImageView(originalImage);
            imageView.Alpha = .3f;
            CGRect rect = liveCameraStream.Frame;
            imageView.Frame = new CGRect(rect.X, rect.Y, rect.Width, rect.Height);
            View.AddSubview(imageView);
            liveCameraStream.Layer.AddSublayer(videoPreviewLayer);

            var captureDevice = AVCaptureDevice.DefaultDeviceWithMediaType(AVMediaType.Video);
            ConfigureCameraForDevice(captureDevice);
            captureDeviceInput = AVCaptureDeviceInput.FromDevice(captureDevice);
            captureSession.AddInput(captureDeviceInput);

            var dictionary = new NSMutableDictionary();
            dictionary[AVVideo.CodecKey] = new NSNumber((int)AVVideoCodec.JPEG);
            stillImageOutput = new AVCaptureStillImageOutput()
            {
                OutputSettings = new NSDictionary()
            };

            captureSession.AddOutput(stillImageOutput);
            captureSession.StartRunning();
        }
        void ConfigureCameraForDevice(AVCaptureDevice device)
        {
            var error = new NSError();
            if (device.IsFocusModeSupported(AVCaptureFocusMode.ContinuousAutoFocus))
            {
                device.LockForConfiguration(out error);
                device.FocusMode = AVCaptureFocusMode.ContinuousAutoFocus;
                device.UnlockForConfiguration();
            }
            else if (device.IsExposureModeSupported(AVCaptureExposureMode.ContinuousAutoExposure))
            {
                device.LockForConfiguration(out error);
                device.ExposureMode = AVCaptureExposureMode.ContinuousAutoExposure;
                device.UnlockForConfiguration();
            }
            else if (device.IsWhiteBalanceModeSupported(AVCaptureWhiteBalanceMode.ContinuousAutoWhiteBalance))
            {
                device.LockForConfiguration(out error);
                device.WhiteBalanceMode = AVCaptureWhiteBalanceMode.ContinuousAutoWhiteBalance;
                device.UnlockForConfiguration();
            }
        }

    }


}