using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;
public class CamUIControl : MonoBehaviour
{

    //GestureRecognizer recognizer = new GestureRecognizer();
   

    public GameObject hud;
    public GameObject controller;
    public bool isVid = false; //else is pic

    // Start is called before the first frame update
    void Start()
    {
        //recognizer.Tapped += GestureRecognizer_Tapped;
        //recognizer.SetRecognizableGestures(GestureSettings.Tap);
    }

   

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowHud(bool x)
    {
        hud.SetActive(x);
    }

    private void OnEnable()
    {
        //recognizer.StartCapturingGestures();
    }
    private void OnDisable()
    {
        //recognizer.StopCapturingGestures();
    }

    public void TakePic()
    {
        GetComponent<PicCapture>().TakePicture();
    }

    public void TakeVid()
    {
        GetComponent<VidCapture>().StartVidCap();
    }
}
