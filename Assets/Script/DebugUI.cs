using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugUI : MonoBehaviour
{
    // ControlsScript controlsScript;
    private TMP_Text greenAppleTimerUI;
    private TMP_Text yellowAppleTimerUI;
    private TMP_Text purpleAppleTimerUI;
    // Start is called before the first frame update
    void Start()
    {
        // controlsScript = FindObjectOfType<ControlsScript>();
        GameObject greenTimerUIGO = GameObject.Find("GreenAppleTimer");
        GameObject yellowTimerUIGO = GameObject.Find("YellowAppleTimer");
        GameObject purpleTimerUIGO = GameObject.Find("PurpleAppleTimer");
        greenAppleTimerUI = greenTimerUIGO.GetComponent<TMP_Text>();
        yellowAppleTimerUI = yellowTimerUIGO.GetComponent<TMP_Text>();
        purpleAppleTimerUI = purpleTimerUIGO.GetComponent<TMP_Text>();
        greenAppleTimerUI.text = "Green 0";
        yellowAppleTimerUI.text = "Yellow 0";
        purpleAppleTimerUI.text = "Purple 0";

        InvokeRepeating(nameof(UpdateTimers), 0f, 0.05f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateTimers() {
        greenAppleTimerUI.text = "Green " + ControlsScript.greenAppleTimer;
        yellowAppleTimerUI.text = "Yellow " + ControlsScript.yellowAppleTimer;
        purpleAppleTimerUI.text = "Purple " + ControlsScript.purpleAppleTimer;
    }
}
