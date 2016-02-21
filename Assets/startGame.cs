using UnityEngine;
using XboxCtrlrInput;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class startGame : MonoBehaviour
{ 
    void Update()
    {
        for (int i = 1; i <= XCI.GetNumPluggedCtrlrs(); i++)
        {
            if (XCI.GetButtonDown(XboxButton.Start, i))
            {
                SceneManager.LoadScene(1);
            }
        }
    }

}

