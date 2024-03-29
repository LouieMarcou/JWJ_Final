namespace VideoKit.Tests
{
    using System.Collections;
    using UnityEngine;
    using VideoKit;
    using VideoKit.Sources;

    public class VideoKitActionsManager : MonoBehaviour
    {
        SceneCameraSource source;

        private bool isSharing = false;

        public void GetImage(bool wantToShare)
        {
            if(wantToShare) { isSharing = true; }
            StartCoroutine(StartPictureCapture());
        }

        private IEnumerator StartPictureCapture()
        {
            // Wait
            yield return new WaitForSeconds(0.5f);
            // Create a camera source to generate images
            //source = new SceneCameraSource(Screen.width, Screen.height, OnImage, Camera.main);
            source = new SceneCameraSource(Screen.width, Screen.height, TakePicture, Camera.main);
        }

        private void OnImage(MediaAsset image)
        {
            // Copy the image into a texture
            var texture = new Texture2D(image.width, image.height, TextureFormat.RGBA32, false);
            //image.CopyTo(texture);
            //// Dispose the source
            //source.Dispose();

            //// Share texture
            //if (isSharing) { Share(texture); }
            //else { SavePicture(texture); }



        }

        private void TakePicture(PixelBuffer pixelBuffer)
        {
            var texture = new Texture2D(pixelBuffer.width, pixelBuffer.height, TextureFormat.RGBA32, false);
            pixelBuffer.CopyTo(texture);
            //// Dispose the source
            source.Dispose();

            //// Share texture
            if(isSharing) { Share(texture); }
            else { SavePicture(texture); }
        }

        private async void Share(Texture2D texture)
        {
            // Create media asset
            var asset = await MediaAsset.FromTexture(texture);
            // Share
            asset.Share();
            isSharing = false;
        }

        private async void SavePicture(Texture2D texture)
        {
            var asset = await MediaAsset.FromTexture(texture);
            await asset.SaveToCameraRoll();
        }
    }
}