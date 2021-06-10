using UnityEngine;
using System.Collections;
using System.Linq;
//using UnityEngine.XR.WSA.WebCam;
using UnityEngine.Windows.WebCam;

public class PicCapture : MonoBehaviour
{
    PhotoCapture photoCaptureObject = null;
    Texture2D targetTexture = null;

    // Use this for initialization
    void Start()
    {
       
    }

    public void TakePicture()
    {
        Resolution cameraResolution = PhotoCapture.SupportedResolutions.OrderByDescending((res) => res.width * res.height).First();
        targetTexture = new Texture2D(cameraResolution.width, cameraResolution.height);

        // Create a PhotoCapture object
        PhotoCapture.CreateAsync(false, delegate (PhotoCapture captureObject) {
            photoCaptureObject = captureObject;
            CameraParameters cameraParameters = new CameraParameters();
            cameraParameters.hologramOpacity = 0.0f;
            cameraParameters.cameraResolutionWidth = cameraResolution.width;
            cameraParameters.cameraResolutionHeight = cameraResolution.height;
            cameraParameters.pixelFormat = CapturePixelFormat.BGRA32;

            // Activate the camera
            photoCaptureObject.StartPhotoModeAsync(cameraParameters, delegate (PhotoCapture.PhotoCaptureResult result) {
                // Take a picture
                photoCaptureObject.TakePhotoAsync(OnCapturedPhotoToMemory);
            });
        });
    }

    void OnCapturedPhotoToMemory(PhotoCapture.PhotoCaptureResult result, PhotoCaptureFrame photoCaptureFrame)
    {
        // Copy the raw image data into the target texture
        photoCaptureFrame.UploadImageDataToTexture(targetTexture);

        // Create a GameObject to which the texture can be applied
        GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        Renderer quadRenderer = quad.GetComponent<Renderer>() as Renderer;
        quadRenderer.material = new Material(Shader.Find("Custom/Unlit/UnlitTexture"));

        quad.transform.parent = this.transform;
        quad.transform.localPosition = new Vector3(0.0f, 0.0f, 3.0f);

        quadRenderer.material.SetTexture("_MainTex", targetTexture);

        // Deactivate the camera
        photoCaptureObject.StopPhotoModeAsync(OnStoppedPhotoMode);
    }

    void OnStoppedPhotoMode(PhotoCapture.PhotoCaptureResult result)
    {
        // Shutdown the photo capture resource
        photoCaptureObject.Dispose();
        photoCaptureObject = null;
    }



    /*
    public void SaveToTex()
    {
        Application.CaptureScreenshot("Assets/Resources/UnityScreenshot.png");
        print("space key was pressed");
        var photoFrame : GameObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
        photoFrame.renderer.material = new Material(Shader.Find(" Diffuse"));
        photoFrame.renderer.material.SetColor("_Color", Color.red);
        var photo : Texture2D = Resources.Load("UnityScreenshot.png", Texture2D);
        if (photo)
        {
            Debug.Log("Texture Loaded Sucessfully...");
            photoFrame.renderer.material.mainTexture = photo;
        }
        else
        {
            Debug.Log("Unable to Load texture...");
        }
    }
    */

        /*
    IEnumerator save_png_player()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            File.WriteAllBytes(Application.persistentDataPath + "/" + name + ".jpg", bytes);
        }
        else
        {
            File.WriteAllBytes(Application.dataPath + "/Resources/save_screen/" + name + ".jpg", bytes);
        }


        yield return null;


    }
    */
}