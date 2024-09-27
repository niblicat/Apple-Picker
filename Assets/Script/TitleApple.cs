using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleApple : MonoBehaviour {
    public static float bottomKill = -20f;
    [SerializeField] private float rotationAmount = -60f;
    [SerializeField] private bool isTitleApple = false;
    // Start is called before the first frame update
    void Start() {
        float randomAngle1 = Random.value * 180;
        float randomAngle2 = Random.value * 180;
        float randomAngle3 = Random.value * 180;
        Vector3 startRotation = new Vector3(randomAngle1, randomAngle2, randomAngle3);
        transform.eulerAngles = startRotation;
    }

    // Update is called once per frame
    void Update() {
        float rotationWithTime = rotationAmount * Time.deltaTime;
        Vector3 addedRotation = new Vector3(rotationWithTime, rotationWithTime, rotationWithTime);
        transform.Rotate(addedRotation);
        if (transform.position.y < bottomKill) {
            Destroy(this.gameObject);
            if (!isTitleApple) {
                ApplePicker apScript = Camera.main.GetComponent<ApplePicker>();
                apScript.AppleDestroyed();
            }
        }
    }
}
