using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SoundHandleButton : MonoBehaviour {
    private AudioSource audioSource;
    [SerializeField] private AudioClip audioClipDefault;
    [SerializeField] private Button[] b = null;
    private void Start() {
        audioSource = GetComponent<AudioSource>();

        foreach (Button button in b) {
            button.onClick.AddListener(PlayDefaultAudio);
        }

    }
    public void PlayDefaultAudio() {
        audioSource.PlayOneShot(audioClipDefault);
    }
}
