using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    static public int score = 0;
    // Start is called before the first frame update
    void Awake() {
        if (!PlayerPrefs.HasKey("HighScore")) {
            PlayerPrefs.SetInt("HighScore", score);
        }
        score = PlayerPrefs.GetInt("HighScore");
        TMP_Text gt = this.GetComponent<TMP_Text>();
        gt.text = "High Score: " + score;
    }
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (score > PlayerPrefs.GetInt("HighScore")) {
            TMP_Text gt = this.GetComponent<TMP_Text>();
            gt.text = "High Score: " + score;
            PlayerPrefs.SetInt("HighScore", score);
        }
    }
}
