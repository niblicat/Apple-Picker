using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : SceneLoader {
    private float waitTime = 5f;
    [SerializeField] private string nextScene = "AppleGame";
    // Start is called before the first frame update
    public SoundHandler mySoundHandler;
    // Update is called once per frame
    void Start() {
        mySoundHandler = GetComponent<SoundHandler>();
    }
    void Update() {
        if (Input.anyKeyDown) {
            waitTime = 20f;
            mySoundHandler.PlayDefaultAudio();
            Invoke(nameof(LoadGameScene), 0.2f);
        }
        if (waitTime > 0) {
            waitTime -= Time.deltaTime;
            if (waitTime <= 0) {
                LoadGameScene();
            }
        }
    }
    void LoadGameScene() {
        LoadScene(nextScene);
        waitTime = 5f;
        // Invoke(nameof(DestroyMe), 0.)
    }
}
