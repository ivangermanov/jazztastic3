using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.InteropServices;
using System;
using System.Drawing;

public class TakePhotoScript : MonoBehaviour {

    public Transform image_placeholder;
    public Transform ui_placheolder;
    public void OnClickTakePhotos()
    {
        StartCoroutine(UploadPNG());
    }
        // Take a shot immediately
        /*IEnumerator Start() {
            UploadPNG();
            yield return null;
        }*/

    IEnumerator UploadPNG()
    {
        /*
        // We should only read the screen buffer after rendering is complete
        yield return new WaitForEndOfFrame();

       
        // Create a texture the size of the screen, RGB24 format
        int width = Screen.width;
        int height = Screen.height;
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);

        // Read screen contents into the texture
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex.Apply();

        //debug tex
        Debug.Log("text created");

        // Encode texture into PNG
        byte[] bytes = tex.EncodeToPNG();



        
        Destroy(tex);

        // For testing purposes, also write to a file in the project folder
        File.WriteAllBytes(Application.dataPath + "/../SavedScreen.png", bytes);
        //File.WriteAllBytes("Internal shared storage" + "/SavedScreen.png", bytes);

        image_placeholder.gameObject.SetActive(false);
        */
        yield return new WaitForEndOfFrame();
        int width, height;
        width = CameraScript.backCam.width;
        height = CameraScript.backCam.height;
        Color32[] tex = new Color32[width*height];
        CameraScript.backCam.GetPixels32(tex);
        byte[] res = Color32ArrayToByteArray(tex);
        FeedBack_manager.result_image_to_insert = res;
        Debug.Log(res.Length);
        CameraScript.backCam.Stop();
        ui_placheolder.gameObject.SetActive(false);
        /*
        Texture2D asd = new Texture2D(width,height, TextureFormat.RGBA32, false);
        asd.LoadRawTextureData(res);
        asd.Apply();
        File.WriteAllBytes(Application.dataPath + "/../SavedScreen.png", asd.EncodeToPNG());
        */

        yield return null;
    }

    private static byte[] Color32ArrayToByteArray(Color32[] colors)
    {
        if (colors == null || colors.Length == 0)
            return null;

        int lengthOfColor32 = Marshal.SizeOf(typeof(Color32));
        int length = lengthOfColor32 * colors.Length;
        byte[] bytes = new byte[length];

        GCHandle handle = default(GCHandle);
        try
        {
            handle = GCHandle.Alloc(colors, GCHandleType.Pinned);
            IntPtr ptr = handle.AddrOfPinnedObject();
            Marshal.Copy(ptr, bytes, 0, length);
        }
        finally
        {
            if (handle != default(GCHandle))
                handle.Free();
        }

        return bytes;
    }
}
