using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (Application.platform == RuntimePlatform.Android) {
            SetFrameRate(60);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void SetFrameRate(int newFR) {
        Application.targetFrameRate = newFR;
    }
    public static void SetVSync() {
        QualitySettings.vSyncCount = 1;
    }
}
