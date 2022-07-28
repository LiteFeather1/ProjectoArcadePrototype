using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeScreenShot : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            ScreenCapture.CaptureScreenshot("GameScreenShot.png");
    }
}
