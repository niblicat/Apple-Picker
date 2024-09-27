using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour {
    public enum AppleType { Red, Green, Yellow, Purple, Branch };
    public static float bottomKill = -8f;
    [SerializeField] private float rotationAmount = -60f;
    [SerializeField] private bool onlyRotateByY = false;
    [SerializeField] private bool startWithRandomRotation = true;
    public AppleType appleColour = AppleType.Red;
    // Start is called before the first frame update
    void Start() {
        float randomAngle1 = Random.value * 180;
        float randomAngle2 = Random.value * 180;
        float randomAngle3 = Random.value * 180;
        Vector3 startRotation = transform.eulerAngles;
        if (startWithRandomRotation) {
            startRotation = new Vector3(randomAngle1, randomAngle2, randomAngle3);
        }
        transform.eulerAngles = startRotation;
    }

    // Update is called once per frame
    void Update() {
        float rotationWithTime = rotationAmount * Time.deltaTime;
        Vector3 addedRotation = Vector3.zero;
        if (onlyRotateByY) {
            addedRotation = new Vector3(0, rotationWithTime, 0);
        }
        else {
            addedRotation = new Vector3(rotationWithTime, rotationWithTime, rotationWithTime);
        }
        transform.Rotate(addedRotation);
        if (transform.position.y < bottomKill) {
            Destroy(this.gameObject);
            if (appleColour == AppleType.Red) {
                ApplePicker apScript = Camera.main.GetComponent<ApplePicker>();
                apScript.AppleDestroyed();
            }
        }
    }
}
