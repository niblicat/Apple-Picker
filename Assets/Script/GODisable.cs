using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GODisable : MonoBehaviour {
    // Start is called before the first frame update
    [SerializeField] private GameObject go;
    [SerializeField] private bool isEnabled;
    void Start() {
        isEnabled = go.activeInHierarchy;
    }
    public void Disable() {
        go.SetActive(false);
        isEnabled = false;
    }
    public void Enable() {
        go.SetActive(true);
        isEnabled = true;
    }
    public void Toggle() {
        go.SetActive(!isEnabled);
        isEnabled = !isEnabled;
    }
}
