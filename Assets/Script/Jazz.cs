using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jazz : MonoBehaviour {
    public bool jazzEnabled = true;
    void Awake() {
        if (PlayerPrefs.HasKey("JazzEnabled"))
            jazzEnabled = PlayerPrefs.GetInt("JazzEnabled") != 0;
        else
            PlayerPrefs.SetInt("JazzEnabled", jazzEnabled ? 1 : 0);
    }
    void Start() {
        if (!jazzEnabled) {
            gameObject.SetActive(false);
        }
    }

    public void ToggleMusic() {
        jazzEnabled = !jazzEnabled;
        PlayerPrefs.SetInt("JazzEnabled", jazzEnabled ? 1 : 0);
        gameObject.SetActive(jazzEnabled);
    }
    public void ToggleMusic(bool newValue) {
        jazzEnabled = newValue;
        PlayerPrefs.SetInt("JazzEnabled", jazzEnabled ? 1 : 0);
        gameObject.SetActive(jazzEnabled);
    }

}
