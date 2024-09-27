using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundHandler : MonoBehaviour {
    private AudioSource audioSource;
    [SerializeField] private AudioClip audioClipDefault;
    [SerializeField] private AudioClip audioClip0;
    [SerializeField] private AudioClip audioClip1;
    [SerializeField] private AudioClip audioClip2;
    [SerializeField] private AudioClip audioClip3;
    [SerializeField] private AudioClip audioClip4;
    [SerializeField] private AudioClip audioClip5;
    private AudioClip[] audioClips = new AudioClip[7];
    private int numAudioClips = 0;
    private int lastAudioClipIndex = -1;
    private void Start() {
        audioSource = GetComponent<AudioSource>();
        if (audioClipDefault != null) audioClips[numAudioClips++] = audioClipDefault;
        if (audioClip0 != null) audioClips[numAudioClips++] = audioClip0;
        if (audioClip1 != null) audioClips[numAudioClips++] = audioClip1;
        if (audioClip2 != null) audioClips[numAudioClips++] = audioClip2;
        if (audioClip3 != null) audioClips[numAudioClips++] = audioClip3;
        if (audioClip4 != null) audioClips[numAudioClips++] = audioClip4;
        if (audioClip5 != null) audioClips[numAudioClips++] = audioClip5;

        if (numAudioClips <= 0) {
            Debug.Log("No audio clips on " + gameObject.name);
        }
    }

    public void PlayDefaultAudio() {
        audioSource.PlayOneShot(audioClipDefault);
        lastAudioClipIndex = 0;
    }

    public void PlayRandomAudio() {
        int randomVal = (int)Mathf.Floor(Random.value * numAudioClips);
        if (randomVal > numAudioClips) randomVal--;
        audioSource.PlayOneShot(audioClips[randomVal]);
        lastAudioClipIndex = randomVal;
    }

    public void PlayPseudoRandomAudio() {
        float randomMultiplier = Random.value;
        if (numAudioClips == 1) {
            Debug.Log(gameObject.name + " only has one audio clip attached. Did you mean to play default audio (PlayDefaultAudio())?");
            audioSource.PlayOneShot(audioClips[0]);
            lastAudioClipIndex = 0;
        }
        int randomVal = (int)Mathf.Floor(randomMultiplier * numAudioClips);
        if (randomVal >= numAudioClips) randomVal--;
        if (randomVal == lastAudioClipIndex) randomVal = (randomVal + 1) % numAudioClips;
        audioSource.PlayOneShot(audioClips[randomVal]);
        lastAudioClipIndex = randomVal;
    }

    private void Empty() {
        return;
    }
}
