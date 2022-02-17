using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotSaver : MonoBehaviour
{
    private int width = 240, height = 170;
    private CustomColor[] thumbnail;
    private Color[] pixels;

    public Coroutine screenCaptureWait { get; private set; }
  
	public void GetSnapShot()
    {
        screenCaptureWait = StartCoroutine(ProcessSnapShot());
	}
	
	IEnumerator ProcessSnapShot()
    {
        RenderTexture rt = new RenderTexture(width, height, 24);
        CameraManager.Instance.currentCamera.targetTexture = rt;

        Texture2D ss = new Texture2D(width, height, TextureFormat.ARGB32, false);

        yield return new WaitForEndOfFrame();

        RenderTexture.active = rt;
        ss.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        ss.Apply();

        pixels = ss.GetPixels();
        //convert the color array to custom color array
        TextureToArray(pixels);

        CameraManager.Instance.currentCamera.targetTexture = null;
        Destroy(rt);

        StopAllCoroutines();
        screenCaptureWait = null;
    }

    public CustomColor[] GetThumbnail()
    {
        return thumbnail;
    }

    void TextureToArray(Color[] colorPixels)
    {
        thumbnail = new CustomColor[colorPixels.Length];

        for (int i = 0; i < colorPixels.Length; i++)
        {
            thumbnail[i].r = colorPixels[i].r;
            thumbnail[i].g = colorPixels[i].g;
            thumbnail[i].b = colorPixels[i].b;
        }
    }

    public Texture2D ArrayToTexture(CustomColor[] savedThumbnail)
    {
        if (savedThumbnail == null)
            return null;

        int length = savedThumbnail.Length;
        pixels = new Color[length];

        Texture2D ss = new Texture2D(width, height, TextureFormat.ARGB32, false);

        for (int i = 0; i < length; i++)
        {
            pixels[i] = savedThumbnail[i].GetColor();
        }

        ss.SetPixels(pixels);
        ss.Apply();

        return ss;
    }
}
