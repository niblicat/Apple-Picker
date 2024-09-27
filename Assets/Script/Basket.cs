using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Basket : MonoBehaviour
{
    ControlsScript controlsScript;
    [SerializeField] private float slowBasketSpeed = 3f;
    [SerializeField] SoundHandler appleCatchSoundHandler;
    [SerializeField] SoundHandler powerDownSoundHandler;
    [SerializeField] SoundHandler powerUpSoundHandler;
    void Start() {
        controlsScript = FindObjectOfType<ControlsScript>();
        // controlsScript = GameObject.Find("Controls").GetComponent<ControlsScript>();
    }
    
    void OnCollisionEnter(Collision collision) {
        appleCatchSoundHandler.PlayPseudoRandomAudio();

        // make sure to tag item so it can be detected
        if (collision.transform.CompareTag("Apple")) {
            Apple.AppleType appleType = collision.gameObject.GetComponent<Apple>().appleColour;
            Destroy(collision.gameObject);
            switch (appleType) {
                case Apple.AppleType.Green:
                    controlsScript.IncreaseScore(400);
                    controlsScript.GreenAppleFrenzy();
                    powerUpSoundHandler.PlayDefaultAudio();
                    break;
                case Apple.AppleType.Yellow:
                    controlsScript.IncreaseScore(1000);
                    controlsScript.YellowAppleProtection();
                    powerUpSoundHandler.PlayDefaultAudio();
                    break;
                case Apple.AppleType.Purple:
                    controlsScript.IncreaseScore(-200);
                    if (!ControlsScript.isProtected) {
                        controlsScript.PurpleAppleBurden();
                        powerDownSoundHandler.PlayDefaultAudio();
                    }
                    break;
                case Apple.AppleType.Branch:
                    ApplePicker apScript = Camera.main.GetComponent<ApplePicker>();
                    apScript.AppleDestroyed();
                    break;
                default: // assume red
                    controlsScript.IncreaseScore(200);
                    break;
            }
        }
    }

    void Update() {
        bool isPaused = ControlsScript.isPaused;
        if (!isPaused) {
            BasketsToMouse();
        }
    }
    private void BasketsToMouse() {
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = - Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
        Vector3 pos = this.transform.position;
        pos.x = mousePos3D.x; // pos x is where the mouse actually is
        if (ControlsScript.basketsAreSlow) {
            Vector3 newSlowedPosition;
            float delta = MathF.Abs(this.transform.position.x - pos.x);
            float distanceTraveled = slowBasketSpeed * Time.deltaTime * delta;
            if (pos.x > this.transform.position.x) { // new position is right of old position
                newSlowedPosition = new Vector3(this.transform.position.x + distanceTraveled, this.transform.position.y, this.transform.position.z);
            }
            else if (pos.x < this.transform.position.x) {
                newSlowedPosition = new Vector3(this.transform.position.x - distanceTraveled, this.transform.position.y, this.transform.position.z);
            }
            else {
                newSlowedPosition = this.transform.position;
            }
            this.transform.position = newSlowedPosition;
        }
        else {
            this.transform.position = pos;
        }
    }


}
