using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
#if UNITY_STANDALONE_WIN
    private bool isFullScreen = true;

    private void Start()
    {
        Application.targetFrameRate = 60;
        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isFullScreen)
            {
                Screen.SetResolution(1280, 720, false);
                isFullScreen = false;
                return;
            }
            else
            {
                Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
                isFullScreen = true;
                return;
            }
        }
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(Screen.width - 160, 10, 150, 90), "Resolution");
        GUI.TextArea(new Rect(Screen.width - 150, 70, 130, 20), "Full screen: " + isFullScreen.ToString());
        GUI.TextArea(new Rect(Screen.width - 150, 40, 130, 20), Screen.width + "x" + Screen.height);
    }
#endif
}
