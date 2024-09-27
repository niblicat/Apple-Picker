using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplePicker : MonoBehaviour
{
    [SerializeField] private GameObject basket;
    public int numBaskets = 3;
    public float basketBottomY = -3f;
    public float basketSpacingY = 2f;
    public List<GameObject> basketList;
    private ControlsScript controlsScript;
    void Start() {
        controlsScript = FindObjectOfType<ControlsScript>();
        basketList = new List<GameObject>();
        for (int i = 0; i < numBaskets; i++) {
            GameObject tBasketGO = Instantiate<GameObject>(basket);
            Vector3 pos = Vector3.zero;
            pos.y = basketBottomY + (basketSpacingY * i);
            tBasketGO.transform.position = pos;
            basketList.Add(tBasketGO);
        }
    }

    public void AppleDestroyed() {
        if (!ControlsScript.isProtected) {
            GameObject[] tAppleArray = GameObject.FindGameObjectsWithTag("Apple");
            foreach (GameObject tGO in tAppleArray) {
                Destroy(tGO);
            }
            SoundHandler mySoundHandler = gameObject.GetComponent<SoundHandler>();
            mySoundHandler.PlayPseudoRandomAudio();

            int basketIndex = basketList.Count - 1;
            GameObject tBasketGO = basketList[basketIndex];
            basketList.RemoveAt(basketIndex);
            Destroy(tBasketGO);
            
            controlsScript.ResetEffects();
            
            if (basketList.Count == 0) {
                controlsScript.StartGameOver();
            }
        }
    }
}
