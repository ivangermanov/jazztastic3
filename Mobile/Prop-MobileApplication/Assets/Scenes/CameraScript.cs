using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour {

    private bool camAvailable;
    public static WebCamTexture backCam;
    private Texture defaultBackground;

    public RawImage background;
    public AspectRatioFitter fit;
    public RectTransform container_Forcamerainterface;

	// Use this for backCam initialization
	void Start () {

        

	}
    public void cancel_operation()
    {
        container_Forcamerainterface.gameObject.SetActive(false);
        camAvailable = false;
        backCam.Stop();
    }
    public void enableTakePicture()
    {
        background.gameObject.active = true;
        container_Forcamerainterface.gameObject.SetActive(true);
        defaultBackground = background.texture;

        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0)
        {
            Debug.Log("No camera detected");
            camAvailable = false;
            return;
        }

        for (int i = 0; i < devices.Length; i++)
        {
            if (devices[i].isFrontFacing)
            {
                backCam = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
            }
        }

        if (backCam == null)
        {
            Debug.Log("Unable to find a back camera");
            return;
        }
        else
            background.enabled = true;

        backCam.Play();
        background.texture = backCam;

        camAvailable = true;
    }
	// Update is called once per frame
	void Update () {
        if (!camAvailable)
            return;

        float ratio = (float)backCam.width / (float)backCam.height;
        fit.aspectRatio = ratio;

        float scaleY = backCam.videoVerticallyMirrored ? -1f: 1f;
        background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

        int orient = -backCam.videoRotationAngle;
        background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);


	}
}
